// <copyright file="DaoBase.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Classe basica para manipulacao de bancos de dados relacionais (SQL, Oracle ou OleDB).</summary>

namespace SmartIT.Library.Data.DAL
{
	using System;
	using System.Collections.Generic;
	using System.Data.OleDb;
	using System.Data.OracleClient;
	using System.Data.SqlClient;
	using System.Text;

	/// <summary>
	/// Base class for the manipulation of relational database entities (SQL Server, Oracle, or OleDB).
	/// </summary>
	/// <typeparam name="TEntity">Tipo Entity.</typeparam>
	[Serializable]
	public class DaoBase<TEntity>
	{
		/// <summary>
		/// Default command timeout = 30 seconds.
		/// </summary>
		private readonly int defaultCommandTimeout = 30;

		/// <summary>
		/// Command timeout.
		/// </summary>
		private readonly int commandTimeout;

		/// <summary>
		/// Gets or sets the DBHelper.
		/// </summary>
		/// <value> The DbHelper value.</value>
		public DbHelper DbHelper { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DaoBase{TEntity}" /> class.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Necessary when the class throwns an ArgumentNullException.")]
		public DaoBase(string activeConnection = "ActiveConnection")
		{
			// Get the active connection
			ActiveConnectionAttribute activeCnn = DbHelper.GetActiveConnection(typeof(TEntity)) ?? throw new ArgumentNullException(nameof(activeConnection), string.Format("The class '{0}' does not contain the [ActiveConnection] attribute.", typeof(TEntity).ToString()));

			// Get the default instance of DbHelper
			DbHelper = DbHelper.GetInstance(activeCnn.Value, activeCnn.CommandTimeout);

			// Custom timeout for commands in seconds
			commandTimeout = activeCnn.CommandTimeout > 0 ? activeCnn.CommandTimeout : defaultCommandTimeout;
		}

		#region SQL Server DB

		/// <summary>
		/// Realiza uma busca e carrega uma lista de objetos chamando o metodo GetList, o qual deve ser sobrescrito(override).
		/// </summary>
		/// <param name="criteria"> Dictionary de alias/valor para geração de clausula Where.</param>
		/// <param name="alias">Dictionary de alias/nome de coluna para mapeamento.</param>
		/// <param name="sql">Consulta SQL que será executada, a qual deve possuir o parametro {0} onde será colocado a clausula Where.</param>
		/// <returns> Object representando uma lista de entididades definida na sobrescrita do metodo GetList.</returns>
		protected object SqlSearch(Dictionary<string, object> criteria, Dictionary<string, string> alias, string sql)
		{
			// Cria um SQL Command
			SqlCommand cmd = SqlCreateCriteriaBasedCommand(criteria, alias, sql);
			return SqlSearch(cmd);
		}

		/// <summary>
		/// Executa um comando no SQL Server e retorna um objeto.
		/// </summary>
		/// <param name="cmd">A Sql Command.</param>
		/// <returns> Lista de objetos.</returns>
		protected object SqlSearch(SqlCommand cmd)
		{
			// Cria um novo SafeDataReader
			SafeDataReader reader = new SafeDataReader(DbHelper.ExecuteReader(cmd));
			object list = GetList(ref reader);

			reader.Close();
			cmd.Dispose();

			// Retorna a lista
			return list;
		}

		/// <summary>
		/// Cria um comando do SQLServer com base nos critérios informados.
		/// </summary>
		/// <param name="criteria"> Par chave/valor com os critérios de busca.</param>
		/// <param name="alias">Par chave/valor com os alias a serem substutuídos.</param>
		/// <param name="sql">Comando SQL a ser montado.</param>
		/// <returns> Returns an SQL Server Command object.</returns>
		protected SqlCommand SqlCreateCriteriaBasedCommand(Dictionary<string, object> criteria, Dictionary<string, string> alias, string sql)
		{
			// Cria um SQL Command
			SqlCommand cmd = (SqlCommand)DbHelper.CreateCommand(sql, typeof(SqlCommand));
			cmd.CommandTimeout = commandTimeout;
			string cmdSymbol = DataBaseProviderFactory.GetParamSymbol("system.data.sqlclient");

			StringBuilder customWhere = new StringBuilder(string.Empty);

			// Para cada chave nos critérios de busca, formata a cláusula WHERE
			foreach (string s in criteria.Keys)
			{
				string[] paramInfo = s.Split('|');

				bool hasCompareOperator = paramInfo.Length > 1;
				bool hasLogicalOperator = paramInfo.Length > 2;

				if (hasCompareOperator)
				{
					string key = (!hasLogicalOperator) ? paramInfo[0] : paramInfo[1];
					string oper = (!hasLogicalOperator) ? paramInfo[1] : paramInfo[2];

					if (alias.ContainsKey(key))
					{
						if (hasLogicalOperator)
						{
							string logicalOperator = paramInfo[0];

							/*
                             * Operador logico definido pelo usuario
                             * 0 = operador logico, ex: 'AND', 'OR'
                             * 1 = nome do campo da tabela
                             * 2 = operador de comparação, ex : '=', 'IN', '<>', LIKE
                             * 3 = nome do parametro
                             */
							if (criteria[s] is DBNull)
							{
								customWhere.Append(string.Format(" {0} {1} {2} NULL", logicalOperator, alias[key], oper));
							}
							else
							{
								if (oper == "IN")
								{
									customWhere.Append(string.Format(" {0} {1} IN {2}", logicalOperator, alias[key], criteria[s]));
								}
								else
								{
									customWhere.Append(string.Format(" {0} {1} {2} {3}{4}", logicalOperator, alias[key], oper, cmdSymbol, key));
									cmd.Parameters.AddWithValue(cmdSymbol + key, criteria[s]);
								}
							}
						}
						else
						{
							// Operador logico não definido pelo usuario - default é 'AND'
							if (criteria[s] is DBNull)
							{
								customWhere.Append(string.Format(" AND {0} {1} NULL", alias[key], oper));
							}
							else
							{
								if (oper == "IN")
								{
									customWhere.Append(string.Format(" AND {0} IN {1}", alias[key], criteria[s]));
								}
								else
								{
									if (oper.ToUpperInvariant().Equals("LIKE"))
									{
										customWhere.Append(string.Format(" AND {0} {1} '%{2}%'", alias[key], oper, criteria[s]));
									}
									else
									{
										customWhere.Append(string.Format(" AND {0} {1} {2}{3}", alias[key], oper, cmdSymbol, key));
										cmd.Parameters.AddWithValue(cmdSymbol + key, criteria[s]);
									}
								}
							}
						}
					}
					else
					{
						criteria.Clear();
						throw new ArgumentNullException(string.Format("The key '{0}' does not exist in the search aliases collection.", s));
					}
				}
				else
				{
					if (alias.ContainsKey(s))
					{
						if (criteria[s] is DBNull)
						{
							customWhere.Append(string.Format(" AND {0} IS NULL", alias[s]));
						}
						else
						{
							customWhere.Append(string.Format(" AND {0} = {1}{2}", alias[s], cmdSymbol, s));
							cmd.Parameters.AddWithValue(cmdSymbol + s, criteria[s]);
						}
					}
					else
					{
						criteria.Clear();
						throw new ArgumentNullException(string.Format("The key '{0}' does not exist in the search aliases collection.", s));
					}
				}
			}

			criteria.Clear();

			string tmpWhere = (customWhere.Length > 0) ? " WHERE " + customWhere.ToString().Substring(4) : string.Empty;

			customWhere.Clear();
			customWhere.Append(tmpWhere);

			cmd.CommandText = string.Format(sql, customWhere.ToString());

			return cmd;
		}

		#endregion

		#region Oracle DB

		/// <summary>
		/// Realiza uma busca e carrega uma lista de objetos chamando o metodo GetList, o qual deve ser sobrescrito(override).
		/// </summary>
		/// <param name="criteria"> Dictionary de alias/valor para geração de clausula Where.</param>
		/// <param name="alias">Dictionary de alias/nome de coluna para mapeamento.</param>
		/// <param name="sql">Consulta SQL que será executada, a qual deve possuir o parametro {0} onde será colocado a clausula Where.</param>
		/// <returns> Object representando uma lista de entididades definida na sobrescrita do metodo GetList.</returns>
		protected object OracleSearch(Dictionary<string, object> criteria, Dictionary<string, string> alias, string sql)
		{
			// Cria um Oracle Command
			OracleCommand cmd = OracleCreateCriteriaBasedCommand(criteria, alias, sql);
			SafeDataReader reader = new SafeDataReader(DbHelper.ExecuteReader(cmd));
			object list = GetList(ref reader);

			reader.Close();
			cmd.Dispose();

			// Retorna a lista
			return list;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		protected object OracleSearch(OracleCommand cmd)
		{
			// Cria um novo SafeDataReader
			SafeDataReader reader = new SafeDataReader(DbHelper.ExecuteReader(cmd));
			object list = GetList(ref reader);

			reader.Close();
			cmd.Dispose();

			// Retorna a lista
			return list;
		}

		/// <summary>
		/// Cria um comando do Oracle com base nos critérios informados.
		/// </summary>
		/// <param name="criteria"> Par chave/valor com os critérios de busca.</param>
		/// <param name="alias">Par chave/valor com os alias a serem substutuídos.</param>
		/// <param name="sql">Comando SQL a ser montado.</param>
		/// <returns> Returns an Oracle Command object.</returns>
		protected OracleCommand OracleCreateCriteriaBasedCommand(Dictionary<string, object> criteria, Dictionary<string, string> alias, string sql)
		{
			// Cria um Oracle Command
			OracleCommand cmd = (OracleCommand)DbHelper.CreateCommand(sql, typeof(OracleCommand));
			cmd.CommandTimeout = commandTimeout;
			string cmdSymbol = DataBaseProviderFactory.GetParamSymbol("oracle.dataaccess");

			StringBuilder customWhere = new StringBuilder(string.Empty);

			// Para cada chave nos critérios de busca, monta cláusula WHERE
			foreach (string o in criteria.Keys)
			{
				bool hasCompareOperator = o.Split('|').Length > 1;
				bool hasLogicalOperator = o.Split('|').Length > 2;

				if (hasCompareOperator)
				{
					string key = (!hasLogicalOperator) ? o.Split('|')[0] : o.Split('|')[1];
					string oper = (!hasLogicalOperator) ? o.Split('|')[1] : o.Split('|')[2];

					if (alias.ContainsKey(key))
					{
						if (hasLogicalOperator)
						{
							string logicalOperator = o.Split('|')[0];

							/*
                             * Operador logico definido pelo usuario
                             * 0 = operador logico, ex: 'AND'
                             * 1 = nome do campo da tabela
                             * 2 = operador de comparação, ex : '='
                             * 3 = nome do parametro                              
                             */
							if (criteria[o] is DBNull)
							{
								customWhere.Append(string.Format(" {0} {1} {2} NULL", logicalOperator, alias[key], oper));
							}
							else
							{
								customWhere.Append(string.Format(" {0} {1} {2} {3}{4}", logicalOperator, alias[key], oper, cmdSymbol, key));
								cmd.Parameters.Add(cmdSymbol + key, criteria[o]);
							}
						}
						else
						{
							// Operador logico não definido pelo usuario - default é 'AND'
							if (criteria[o] is DBNull)
							{
								customWhere.Append(string.Format(" AND {0} {1} NULL", alias[key], oper));
							}
							else
							{
								customWhere.Append(string.Format(" AND {0} {1} {2}{3}", alias[key], oper, cmdSymbol, key));
								cmd.Parameters.Add(cmdSymbol + key, criteria[o]);
							}
						}
					}
					else
					{
						criteria.Clear();
						throw new ArgumentNullException(string.Format("The key '{0}' does not exist in the search aliases collection.", o));
					}
				}
				else
				{
					if (alias.ContainsKey(o))
					{
						if (criteria[o] is System.DBNull)
						{
							customWhere.Append(string.Format(" AND {0} IS NULL", alias[o]));
						}
						else
						{
							customWhere.Append(string.Format(" AND {0} = {1}{2}", alias[o], cmdSymbol, o));
							cmd.Parameters.Add(cmdSymbol + o, criteria[o]);
						}
					}
					else
					{
						criteria.Clear();
						throw new ArgumentNullException(string.Format("The key '{0}' does not exist in the search aliases collection.", o));
					}
				}
			}

			criteria.Clear();

			string tmpWhere = (customWhere.Length > 0) ? " WHERE " + customWhere.ToString().Substring(4) : string.Empty;

			customWhere.Clear();
			customWhere.Append(tmpWhere);

			cmd.CommandText = string.Format(sql, customWhere.ToString());

			return cmd;
		}

		#endregion

		#region OleDb DB

		/// <summary>
		/// Realiza uma busca e carrega uma lista de objetos chamando o metodo GetList, o qual deve ser sobrescrito(override).
		/// </summary>
		/// <param name="criteria"> Dictionary de alias/valor para geração de clausula Where.</param>
		/// <param name="alias">Dictionary de alias/nome de coluna para mapeamento.</param>
		/// <param name="sql">Consulta SQL que será executada, a qual deve possuir o parametro {0} onde será colocado a clausula Where.</param>
		/// <returns> Object representando uma lista de entididades definida na sobrescrita do metodo GetList.</returns>
		protected object OleDbSearch(Dictionary<string, object> criteria, Dictionary<string, string> alias, string sql)
		{
			// Cria um OleDb Command
			OleDbCommand cmd = OleDbCreateCriteriaBasedCommand(criteria, alias, sql);
			return OleDbSearch(cmd);
		}

		/// <summary>
		/// Executa um comando no OleDB e retorna uma lista de objetos.
		/// </summary>
		/// <param name="cmd">An OleDb Command.</param>
		/// <returns> Lista de objetos.</returns>
		protected object OleDbSearch(OleDbCommand cmd)
		{
			// Cria uma nova instância de SafeDataReader
			SafeDataReader reader = new SafeDataReader(DbHelper.ExecuteReader(cmd));
			object list = this.GetList(ref reader);

			reader.Close();
			cmd.Dispose();

			// Retorna a lista
			return list;
		}

		/// <summary>
		/// Cria um comando do SQLServer com base nos critérios informados.
		/// </summary>
		/// <param name="criteria"> Par chave/valor com os critérios de busca.</param>
		/// <param name="alias">Par chave/valor com os alias a serem substutuídos.</param>
		/// <param name="sql">Comando SQL a ser montado.</param>
		/// <returns> Returns an OleDb Command object.</returns>
		protected OleDbCommand OleDbCreateCriteriaBasedCommand(Dictionary<string, object> criteria, Dictionary<string, string> alias, string sql)
		{
			// Cria um OleDbCommand
			OleDbCommand cmd = (OleDbCommand)DbHelper.CreateCommand(sql, typeof(OleDbCommand));
			cmd.CommandTimeout = commandTimeout;
			string cmdSymbol = DataBaseProviderFactory.GetParamSymbol("system.data.oledb");

			StringBuilder customWhere = new StringBuilder(string.Empty);

			// Para cada chave nos critérios de busca, formata a cláusula WHERE
			foreach (string c in criteria.Keys)
			{
				string[] paramInfo = c.Split('|');

				bool hasCompareOperator = paramInfo.Length > 1;
				bool hasLogicalOperator = paramInfo.Length > 2;

				if (hasCompareOperator)
				{
					string key = (!hasLogicalOperator) ? paramInfo[0] : paramInfo[1];
					string oper = (!hasLogicalOperator) ? paramInfo[1] : paramInfo[2];

					if (alias.ContainsKey(key))
					{
						if (hasLogicalOperator)
						{
							string logicalOperator = paramInfo[0];

							/*
                             * Operador logico definido pelo usuario
                             * 0 = operador logico, ex: 'AND', 'OR'
                             * 1 = nome do campo da tabela
                             * 2 = operador de comparação, ex : '=', 'IN', '<>'
                             * 3 = nome do parametro                              
                             */
							if (criteria[c] is DBNull)
							{
								customWhere.Append(string.Format(" {0} {1} {2} NULL", logicalOperator, alias[key], oper));
							}
							else
							{
								if (oper == "IN")
								{
									customWhere.Append(string.Format(" {0} {1} IN {2}", logicalOperator, alias[key], criteria[c]));
								}
								else
								{
									customWhere.Append(string.Format(" {0} {1} {2} {3}{4}", logicalOperator, alias[key], oper, cmdSymbol, key));
									cmd.Parameters.AddWithValue(cmdSymbol + key, criteria[c]);
								}
							}
						}
						else
						{
							// Operador logico não definido pelo usuario - default é 'AND'
							if (criteria[c] is DBNull)
							{
								customWhere.Append(string.Format(" AND {0} {1} NULL", alias[key], oper));
							}
							else
							{
								if (oper == "IN")
								{
									customWhere.Append(string.Format(" AND {0} IN {1}", alias[key], criteria[c]));
								}
								else
								{
									customWhere.Append(string.Format(" AND {0} {1} {2}{3}", alias[key], oper, cmdSymbol, key));
									cmd.Parameters.AddWithValue(cmdSymbol + key, criteria[c]);
								}
							}
						}
					}
					else
					{
						criteria.Clear();
						throw new ArgumentNullException(string.Format("The key '{0}' does not exist in the search aliases collection.", c));
					}
				}
				else
				{
					if (alias.ContainsKey(c))
					{
						if (criteria[c] is DBNull)
						{
							customWhere.Append(string.Format(" AND {0} IS NULL", alias[c]));
						}
						else
						{
							customWhere.Append(string.Format(" AND {0} = {1}{2}", alias[c], cmdSymbol, c));
							cmd.Parameters.AddWithValue(cmdSymbol + c, criteria[c]);
						}
					}
					else
					{
						criteria.Clear();
						throw new ArgumentNullException(string.Format("The key '{0}' does not exist in the search aliases collection.", c));
					}
				}
			}

			criteria.Clear();

			string tmpWhere = (customWhere.Length > 0) ? " WHERE " + customWhere.ToString().Substring(4) : string.Empty;

			customWhere.Clear();
			customWhere.Append(tmpWhere);

			cmd.CommandText = string.Format(sql, customWhere.ToString());

			return cmd;
		}

		#endregion

		/// <summary>
		/// Returns a list of <see cref="TEntity"/> for the given SafeDataReader.
		/// </summary>
		/// <remarks>This method must be implemented in the concrete class <see cref="TEntity"/>.</remarks>
		/// <param name="reader"> A <see cref="SafeDataReader"/> object.</param>
		/// <returns> A list of <see cref="TEntity"/> objects.</returns>
		protected virtual object GetList(ref SafeDataReader reader)
		{
			reader.Close();
			throw new NotImplementedException("This method must be implemented in the concrete class (TEntity).");
		}
	}
}