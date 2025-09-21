// <copyright file="DataBaseProviderFactory.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Implementation of a Database provider factory. Currently it supports SQL Server, Oracle and OleDb.</summary>

namespace SmartIT.Library.Data
{
	using Microsoft.Data.SqlClient;
	using Oracle.ManagedDataAccess.Client;
	using System;
	using System.Configuration;
	using System.Data;
	using System.Data.OleDb;

	/// <summary>
	/// Implementation of a Database provider factory. Currently it supports SQL Server, Oracle and OleDb.
	/// </summary>
	public static class DataBaseProviderFactory
	{
		private const string SYSTEM_DATA_SQLCLIENT = "system.data.sqlclient";
		private const string SYSTEM_DATA_OLEDB = "system.data.oledb";
		private const string SYSTEM_DATA_ORACLECLIENT = "system.data.oracleclient";
		private const string ORACLE_DATAACCESS = "oracle.dataaccess";
		private const string ORACLE_DATAACCESS_CLIENT = "oracle.dataaccess.client";
		private const string ORACLE_MANAGEDDATAACCESS = "oracle.manageddataaccess";
		private const string ORACLE_MANAGEDDATAACCESS_CLIENT = "oracle.manageddataaccess.client";

		/// <summary>
		/// Returns a database connection based on the connection string defined in the configuration file.
		/// </summary>
		/// <param name="connectionStringName">The connection string name as it appears in the configuration file.</param>
		/// <returns> An <see cref="IDbConnection"/> instance.</returns>
		public static IDbConnection CreateConnection(string connectionStringName)
		{
			if (string.IsNullOrWhiteSpace(connectionStringName))
			{
				throw new ArgumentNullException(nameof(connectionStringName));
			}
			if (ConfigurationManager.ConnectionStrings[connectionStringName] == null ||
				string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString))
			{
				throw new ArgumentNullException(nameof(connectionStringName));
			}
			if (ConfigurationManager.ConnectionStrings[connectionStringName] == null ||
				string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName))
			{
				throw new ArgumentNullException($"Attribute 'ProviderName' not defined for the connection string '{connectionStringName}' in the configuration file.");
			}

			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

			return CreateConnection(connectionStringName, providerName);
		}

		/// <summary>
		/// Returns a database connection based on the connection string defined in the configuration file.
		/// </summary>
		/// <param name="connectionStringName"> The connection string name as it appears in the configuration file.</param>
		/// <param name="providerName"> The provider name as it appears in the configuration file.</param>
		/// <returns> An <see cref="IDbConnection"/> instance.</returns>
		public static IDbConnection CreateConnection(string connectionStringName, string providerName)
		{
			if (string.IsNullOrWhiteSpace(connectionStringName))
			{
				throw new ArgumentNullException(nameof(connectionStringName));
			}
			if (string.IsNullOrWhiteSpace(providerName))
			{
				throw new ArgumentNullException($"Attribute 'ProviderName' not defined for the connection string '{connectionStringName}' in the configuration file.");
			}
			if (ConfigurationManager.ConnectionStrings[connectionStringName] == null ||
				string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString))
			{
				throw new ArgumentNullException(nameof(connectionStringName));
			}

			// Retrieves the connection string from the configuration file
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

			IDbConnection connection = null;

			// Creates a Connection according to the data provider (SQL Server, Oracle, or OleDb).
			switch (providerName.ToLowerInvariant())
			{
				// SQL Server - default
				case SYSTEM_DATA_SQLCLIENT:
					connection = new SqlConnection(connectionString);
					break;

				// OleDb
				case SYSTEM_DATA_OLEDB:
					connection = new OleDbConnection(connectionString);
					break;

				// Oracle
				case SYSTEM_DATA_ORACLECLIENT:
				case ORACLE_DATAACCESS:
				case ORACLE_DATAACCESS_CLIENT:
				case ORACLE_MANAGEDDATAACCESS:
				case ORACLE_MANAGEDDATAACCESS_CLIENT:
					connection = new OracleConnection(connectionString);
					break;

				default: break;
			}

			return connection;
		}

		/// <summary>
		/// Returns an instance of a DataAdapter.
		/// </summary>
		/// <param name="connectionStringName"> The connection string name as it appears in the configuration file.</param>
		/// <returns> An <see cref="IDbDataAdapter"/> instance.</returns>
		public static IDbDataAdapter CreateDataAdapter(string connectionStringName)
		{
			if (string.IsNullOrWhiteSpace(connectionStringName))
			{
				throw new ArgumentNullException(nameof(connectionStringName));
			}

			// Retrieves the ProviderName from the configuration file
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

			if (string.IsNullOrWhiteSpace(providerName))
			{
				throw new ArgumentNullException($"Attribute 'ProviderName' not defined for the connection string '{connectionStringName}' in the configuration file.");
			}

			IDbDataAdapter dataAdapter = null;

			// Creates a new DataAdapter according to the provider defined in the configuration file
			switch (providerName.ToLowerInvariant())
			{
				// SQL Server - default
				case SYSTEM_DATA_SQLCLIENT:
					dataAdapter = new SqlDataAdapter();
					break;

				// OleDb
				case SYSTEM_DATA_OLEDB:
					dataAdapter = new OleDbDataAdapter();
					break;

				// Oracle
				case SYSTEM_DATA_ORACLECLIENT:
				case ORACLE_DATAACCESS:
				case ORACLE_DATAACCESS_CLIENT:
				case ORACLE_MANAGEDDATAACCESS:
				case ORACLE_MANAGEDDATAACCESS_CLIENT:
					dataAdapter = new OracleDataAdapter();
					break;

				default: break;
			}

			return dataAdapter;
		}

		/// <summary>
		/// Returns an instance of a DbCommand.
		/// </summary>
		/// <param name="connectionStringName"> The connection string name as it appears in the configuration file.</param>
		/// <param name="commandText"> The SQL command that will be assigned to the DbCommand object.</param>
		/// <returns> An <see cref="IDbCommand"/> instance.</returns>
		public static IDbCommand CreateCommand(string connectionStringName, string commandText)
		{
			if (string.IsNullOrWhiteSpace(connectionStringName))
			{
				throw new ArgumentNullException(nameof(connectionStringName));
			}

			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

			if (string.IsNullOrWhiteSpace(providerName))
			{
				throw new ArgumentNullException($"Attribute 'ProviderName' not defined for the connection string '{connectionStringName}' in the configuration file.");
			}
			if (string.IsNullOrWhiteSpace(commandText))
			{
				throw new ArgumentNullException(nameof(commandText));
			}

			IDbCommand command = null;

			// Creates a Command according to the data provider (SQL Server, Oracle, or OleDb).
			switch (providerName.ToLowerInvariant())
			{
				// SQL Server - default
				case SYSTEM_DATA_SQLCLIENT:
					command = new SqlCommand(commandText);
					break;

				// OleDb
				case SYSTEM_DATA_OLEDB:
					command = new OleDbCommand(commandText);
					break;

				// Oracle
				case SYSTEM_DATA_ORACLECLIENT:
				case ORACLE_DATAACCESS:
				case ORACLE_DATAACCESS_CLIENT:
				case ORACLE_MANAGEDDATAACCESS:
				case ORACLE_MANAGEDDATAACCESS_CLIENT:
					command = new OracleCommand(commandText);
					break;

				default: break;
			}

			return command;
		}

		/// <summary>
		/// Returns the symbol used to create parameters in the SQL query.
		/// </summary>
		/// <param name="providerName"> The provider name as it appears in the configuration file.</param>
		/// <returns> The symbol associated with the given provider [@, ?, or :].</returns>
		public static string GetParamSymbol(string providerName)
		{
			string paramSymbol = null;

			// Retorna o símbolo do SQL conforme o nome do provider
			switch (providerName.ToLowerInvariant())
			{
				// SQL Server - default
				case SYSTEM_DATA_SQLCLIENT:
					paramSymbol = "@";
					break;

				// OleDB
				case SYSTEM_DATA_OLEDB:
					paramSymbol = "?";
					break;

				// Oracle
				case SYSTEM_DATA_ORACLECLIENT:
				case ORACLE_DATAACCESS:
				case ORACLE_DATAACCESS_CLIENT:
				case ORACLE_MANAGEDDATAACCESS:
				case ORACLE_MANAGEDDATAACCESS_CLIENT:
					paramSymbol = ":";
					break;

				default: break;
			}

			return paramSymbol;
		}
	}
}
