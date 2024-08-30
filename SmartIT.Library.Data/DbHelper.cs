// <copyright file="DbHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Prove acesso ao banco de dados, e possui metodos que facilitam manipulacao dos dados.</summary>

namespace SmartIT.Library.Data
{
	using System;
	using System.Configuration;
	using System.Data;
	using System.Data.Common;

	/// <summary>
	/// Prove acesso ao banco de dados, e possui metodos que facilitam manipulacao dos dados.
	/// </summary>
	public class DbHelper
	{
		/// <summary>
		/// Command timeout.
		/// </summary>
		private readonly int commandTimeout;

		/// <summary>
		/// Connection string name.
		/// </summary>
		private readonly string connectionStringName = null;

		/// <summary>
		/// SQL Transaction.
		/// </summary>
		private IDbTransaction transaction = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="DbHelper" /> class.
		/// </summary>
		/// <param name="connectionStringName"> String de conexão.</param>
		/// <param name="commandTimeout"> Timeout da conexão.</param>
		private DbHelper(string connectionStringName, int commandTimeout)
		{
			if (ConfigurationManager.ConnectionStrings[connectionStringName] == null
			 || string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[connectionStringName].ToString()))
			{
				throw new ArgumentException("Conexão não encontrada.");
			}

			this.connectionStringName = connectionStringName;

			// Custom timeout for commands in seconds
			this.commandTimeout = commandTimeout > 0 ? commandTimeout : 30;
		}

		/// <summary>
		/// Gets or sets the active Transaction.
		/// </summary>
		/// <returns> IDbTransaction value.</returns>
		/// <value> Obrigatório pelo SONAR.</value>
		public IDbTransaction ActiveTransaction
		{
			get
			{
				if (transaction != null && transaction.Connection == null)
				{
					transaction = null;
				}

				return transaction;
			}
			set
			{
				transaction = value;
			}
		}

		/// <summary>
		/// Retorna uma instância da classe DbHelper.
		/// </summary>
		/// <param name="connectionStringName"> String de conexão.</param>
		/// <param name="commandTimeout"> Timeout do comando.</param>
		/// <returns> Instância de DbHelper.</returns>
		public static DbHelper GetInstance(string connectionStringName, int commandTimeout)
		{
			return new DbHelper(connectionStringName, commandTimeout);
		}

		/// <summary>
		/// Recupera o o valor do atributo ActiveConnectionAttribute de uma classe.
		/// </summary>
		/// <param name="type"> Tipo da classe que contem o atributo.</param>
		/// <returns> Valor do atributo.</returns>
		public static ActiveConnectionAttribute GetActiveConnection(Type type)
		{
			ActiveConnectionAttribute[] attrs = type.GetCustomAttributes(typeof(ActiveConnectionAttribute), false) as ActiveConnectionAttribute[];
			return attrs.Length > 0 ? attrs[0] : null;
		}

		/// <summary>
		/// Cria um Command baseado no tipo informado e o associa a uma transação quando houver.
		/// </summary>
		/// <param name="sql">String SQL.</param>
		/// <param name="cmdType">Tipo do Command (baseado no provider desejado).</param>
		/// <returns> Nova instância de um Command.</returns>
		public IDbCommand CreateCommand(string sql, Type cmdType)
		{
			IDbCommand cmd;

			if (transaction != null && transaction.Connection != null)
			{
				cmd = transaction.Connection.CreateCommand();
				cmd.Transaction = transaction;
			}
			else
			{
				cmd = (IDbCommand)Activator.CreateInstance(cmdType);
			}

			cmd.CommandText = sql;
			cmd.CommandTimeout = commandTimeout;

			return cmd;
		}

		/// <summary>
		/// Inicia uma transação.
		/// </summary>
		/// <returns> Instância da transação.</returns>
		public IDbTransaction CreateTransaction()
		{
			IDbConnection cnn = DataBaseProviderFactory.CreateConnection(connectionStringName);
			cnn.Open();

			transaction = cnn.BeginTransaction();

			return transaction;
		}

		/// <summary>
		/// Executa uma consulta no banco de dados e retorna um DataReader.
		/// O comportamento da conexao é de que será fechada quando o reader for fechado.
		/// </summary>
		/// <param name="sql"> SQL a ser executado.</param>
		/// <returns> Objeto IDataReader.</returns>
		public IDataReader ExecuteReader(string sql)
		{
			DbCommand cmd = (DbCommand)DataBaseProviderFactory.CreateCommand(connectionStringName, sql);
			cmd.CommandText = sql;
			cmd.CommandTimeout = commandTimeout;

			return ExecuteReader(cmd);
		}

		/// <summary>
		/// Executa um comando no banco de dados e retorna um DataReader.
		/// O comportamento da conexão é de que será fechada quando o reader for fechado.
		/// </summary>
		/// <param name="cmd"> IDbCommand a ser executado.</param>
		/// <returns> Objeto IDataReader.</returns>
		public IDataReader ExecuteReader(IDbCommand cmd)
		{
			IDataReader reader;
			cmd.CommandTimeout = commandTimeout;

			if (transaction != null && transaction.Connection != null)
			{
				cmd.Connection = transaction.Connection;
				cmd.Transaction = transaction;
				reader = cmd.ExecuteReader();
			}
			else
			{
				IDbConnection cnn = DataBaseProviderFactory.CreateConnection(connectionStringName);
				reader = ExecuteReader(cmd, cnn);
			}

			return reader;
		}

		/// <summary>
		/// Executa um comando no banco de dados e retorna um DataReader. 
		/// </summary>
		/// <param name="cmd"> IDbCommand a ser executado.</param>
		/// <param name="cnn"> IDbConnection a ser executada.</param>
		/// <returns> Objeto IDataReader.</returns>
		public IDataReader ExecuteReader(IDbCommand cmd, IDbConnection cnn)
		{
			IDataReader reader;
			cmd.CommandTimeout = commandTimeout;

			try
			{
				if (cnn.State != ConnectionState.Open)
				{
					cnn.Open();
				}

				cmd.Connection = cnn;
				reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}

			return reader;
		}

		/// <summary>
		/// Executa um comando Db e retorna a quantidade de linhas afetadas.
		/// </summary>
		/// <param name="sql"> Script SQL a ser executado.</param>
		/// <returns> Qtd. de linhas afetadas.</returns>
		public int ExecuteNonQuery(string sql)
		{
			DbCommand cmd = (DbCommand)DataBaseProviderFactory.CreateCommand(connectionStringName, sql);
			cmd.CommandTimeout = commandTimeout;

			return ExecuteNonQuery(cmd);
		}

		/// <summary>
		/// Executa um comando e retorna a quantidade de linhas afetadas.
		/// </summary>
		/// <param name="cmd"> IDbCommand a ser executado.</param>
		/// <returns> Quantidade de linhas afetadas.</returns>
		public int ExecuteNonQuery(IDbCommand cmd)
		{
			cmd.CommandTimeout = commandTimeout;
			int rows;

			if (transaction != null && transaction.Connection != null)
			{
				cmd.Connection = transaction.Connection;
				cmd.Transaction = transaction;
				rows = cmd.ExecuteNonQuery();
			}
			else
			{
				IDbConnection cnn = DataBaseProviderFactory.CreateConnection(connectionStringName);
				rows = ExecuteNonQuery(cmd, cnn);
			}

			return rows >= 0 ? rows : -1;
		}

		/// <summary>
		/// Executa um comando e retorna a quantidade de linhas afetadas.
		/// </summary>
		/// <param name="cmd"> IDbCommand a ser executado.</param>
		/// <param name="cnn"> IDbConnection - conexão.</param>
		/// <returns> Qtd. de linhas afetadas.</returns>
		public int ExecuteNonQuery(IDbCommand cmd, IDbConnection cnn)
		{
			cmd.CommandTimeout = commandTimeout;
			int rows;

			try
			{
				if (cnn.State != ConnectionState.Open)
				{
					cnn.Open();
				}

				cmd.Connection = cnn;
				rows = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			finally
			{
				if (cnn.State != ConnectionState.Closed && cmd.Transaction == null)
				{
					cnn.Close();
				}
			}

			return rows >= 0 ? rows : -1;
		}

		/// <summary>
		/// Executa um comando e retorna apenas a primeira coluna da primeira linha do
		/// resultado. Caso não haja resultado, o método retorna null.
		/// </summary>
		/// <param name="cmd"> DbCommand a ser executado.</param>
		/// <returns> O valor encontrado, ou -1.</returns>
		public int ExecuteScalarWithScopeIdentity(DbCommand cmd)
		{
			cmd.CommandTimeout = commandTimeout;
			object scopeID = ExecuteScalar(cmd);

			return scopeID != null && scopeID != DBNull.Value ? Convert.ToInt32(scopeID) : -1;
		}

		/// <summary>
		/// Executa um comando e retorna apenas a primeira coluna da primeira linha do
		/// resultado. Caso não haja resultado, o método retorna null.
		/// </summary>
		/// <param name="sql"> Script SQL a ser executado.</param>
		/// <returns> Objeto de banco.</returns>
		public object ExecuteScalar(string sql)
		{
			IDbCommand cmd;

			if (transaction != null)
			{
				cmd = transaction.Connection.CreateCommand();
				cmd.CommandText = sql;
			}
			else
			{
				cmd = DataBaseProviderFactory.CreateCommand(connectionStringName, sql);
			}

			cmd.CommandTimeout = commandTimeout;
			return ExecuteScalar(cmd);
		}

		/// <summary>
		/// Executa uma consulta e retorna apenas a primeira coluna da primeira linha do
		/// resultado. Caso não haja resultado, o método retorna null.
		/// </summary>
		/// <param name="cmd"> IDbCommand a ser executado.</param>
		/// <returns> Objeto de banco.</returns>
		public object ExecuteScalar(IDbCommand cmd)
		{
			cmd.CommandTimeout = commandTimeout;
			object obj;

			if (transaction != null && transaction.Connection != null)
			{
				obj = cmd.ExecuteScalar();
			}
			else
			{
				IDbConnection cnn = DataBaseProviderFactory.CreateConnection(connectionStringName);
				obj = ExecuteScalar(cmd, cnn);
			}

			return obj;
		}

		/// <summary>
		/// Executa um comando e retorna algum valor.
		/// </summary>
		/// <param name="cmd"> IDbCommand a ser executado.</param>
		/// <param name="cnn"> IDbConnection - conexão.</param>
		/// <returns> Objeto de banco.</returns>
		public object ExecuteScalar(IDbCommand cmd, IDbConnection cnn)
		{
			cmd.CommandTimeout = commandTimeout;
			object obj = null;

			try
			{
				if (cnn.State != ConnectionState.Open)
				{
					cnn.Open();
				}

				cmd.Connection = cnn;
				obj = cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			finally
			{
				if (cnn.State != ConnectionState.Closed && cmd.Transaction == null)
				{
					cnn.Close();
				}
			}

			return obj;
		}

		/// <summary>
		/// Executa uma consulta e retorna um DataView.
		/// </summary>
		/// <param name="sql"> Script SQL a ser executado.</param>
		/// <returns> DataView com o resultado da consulta.</returns>
		public DataView ExecuteDataView(string sql)
		{
			DbCommand cmd = (DbCommand)DataBaseProviderFactory.CreateCommand(connectionStringName, sql);
			cmd.CommandTimeout = commandTimeout;

			return ExecuteDataView(cmd);
		}

		/// <summary>
		/// Executa uma consulta e retorna um DataView.
		/// </summary>
		/// <param name="cmd"> Objeto IDbCommand a ser executado.</param>
		/// <returns> DataView com o resultado da consulta.</returns>
		public DataView ExecuteDataView(IDbCommand cmd)
		{
			DataTable dt = ExecuteDataTable(cmd);
			cmd.CommandTimeout = commandTimeout;

			return dt.DefaultView;
		}

		/// <summary>
		/// Executa um comando Db e retorna um DataTable.
		/// </summary>
		/// <param name="sql"> Script SQL a ser executado.</param>
		/// <returns> DataView com o resultado da consulta.</returns>
		public DataTable ExecuteDataTable(string sql)
		{
			DbCommand cmd = (DbCommand)DataBaseProviderFactory.CreateCommand(connectionStringName, sql);
			cmd.CommandTimeout = commandTimeout;

			return ExecuteDataTable(cmd);
		}

		/// <summary>
		/// Executa um DbCommand e retorna um data source.
		/// </summary>
		/// <param name="cmd"> Objeto IDbCommand a ser executado.</param>
		/// <returns> DataView com o resultado da consulta.</returns>
		public DataTable ExecuteDataTable(IDbCommand cmd)
		{
			IDbConnection cnn = null;
			DataSet ds = new DataSet();

			try
			{
				cmd.CommandTimeout = commandTimeout;

				IDbDataAdapter adapter = DataBaseProviderFactory.CreateDataAdapter(connectionStringName);
				adapter.SelectCommand = cmd;

				if (transaction == null || transaction.Connection == null)
				{
					cnn = DataBaseProviderFactory.CreateConnection(connectionStringName);
					cnn.Open();

					adapter.SelectCommand.Connection = cnn;
				}

				adapter.Fill(ds);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			finally
			{
				if (cnn != null && cnn.State != ConnectionState.Closed)
				{
					cnn.Close();
				}
			}

			return ds.Tables.Count > 0 ? ds.Tables[0].Copy() : null;
		}

		/// <summary>
		/// Executa uma consulta e retorna o proximo valor inteiro (para campos não Identity).
		/// </summary>
		/// <param name="columnName"> Nome da coluna.</param>
		/// <param name="tableName"> Nome da tabela.</param>
		/// <returns> O próximo valor para a coluna procurada.</returns>
		public int GetNextValue(string columnName, string tableName)
		{
			string sql = string.Format("select max({0}) val from {1}", columnName, tableName);

			IDbCommand cmd;
			if (transaction != null && transaction.Connection != null)
			{
				cmd = transaction.Connection.CreateCommand();
				cmd.CommandText = sql;
			}
			else
			{
				cmd = DataBaseProviderFactory.CreateCommand(connectionStringName, sql);
			}

			cmd.CommandTimeout = commandTimeout;
			return GetNextValue(cmd);
		}

		/// <summary>
		/// Executa uma consulta e retorna o proximo valor inteiro (para campos não Identity).
		/// </summary>
		/// <param name="cmd"> Objeto IDbCommand a ser executado.</param>
		/// <returns> O próximo valor para a coluna procurada.</returns>
		public int GetNextValue(IDbCommand cmd)
		{
			object o = ExecuteScalar(cmd);
			return (o != null && o != DBNull.Value) ? Convert.ToInt32(o) + 1 : 1;
		}
	}
}