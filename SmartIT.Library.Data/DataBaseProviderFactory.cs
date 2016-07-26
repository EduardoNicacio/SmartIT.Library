// <copyright file="DataBaseProviderFactory.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Factory de conexões com bases de dados.</summary>

namespace SmartIT.Library.Data
{
    using Oracle.DataAccess.Client;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;

    /// <summary>
    /// Factory de conexões com bases de dados.
    /// </summary>
    public static class DataBaseProviderFactory
    {
        /// <summary>
        /// Retorna uma conexão baseado na chave definida no arquivo de configuração.
        /// </summary>
        /// <param name="connectionStringName">Nome da string de conexão.</param>
        /// <param name="providerName">Nome do provider para acesso.</param>
        /// <returns> Conexão referente a chave.</returns>
        public static IDbConnection CreateConnection(string connectionStringName, string providerName)
        {
            IDbConnection connection = null;

            // Recupera a string de conexão
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            
            // Cria a Conexão de acordo com o provider (SQL, Oracle, OleDb)
            switch (providerName.ToLowerInvariant())
            {
                case "system.data.sqlclient":
                    connection = new SqlConnection(connectionString);
                    break;

                case "system.data.oledb":
                    connection = new OleDbConnection(connectionString);
                    break;

                case "oracle.dataaccess":
                case "oracle.dataaccess.client":
                    connection = new OracleConnection(connectionString);
                    break;
                default: break;
            }

            return connection;
        }

        /// <summary>
        /// Retorna uma conexão baseado na chave definida no arquivo de configuração.
        /// </summary>
        /// <param name="connectionStringName">Nome da string de conexão.</param>
        /// <returns> Conexão referente a chave.</returns>
        public static IDbConnection CreateConnection(string connectionStringName)
        {
            string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            
            if (string.IsNullOrEmpty(providerName))
            {
                throw new Exception("Atributo 'ProviderName' não definido para connection string '" + connectionStringName + "' no arquivo de configuração.");
            }

            return CreateConnection(connectionStringName, providerName);
        }

        /// <summary>
        /// Retorna uma instancia concreta de um DataAdapter.
        /// </summary>
        /// <param name="connectionStringName">Nome da string de conexão.</param>
        /// <returns> Instância concreta de um DataAdapter.</returns>
        public static IDbDataAdapter CreateDataAdapter(string connectionStringName)
        {
            IDbDataAdapter dataAdapter = null;

            // Recupera o nome do Provider
            string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            
            // Cria um DataAdapter de acordo com o provider utilizado
            switch (providerName.ToLowerInvariant())
            {
                case "system.data.sqlclient":
                    dataAdapter = new SqlDataAdapter();
                    break;

                case "system.data.oledb":
                    dataAdapter = new OleDbDataAdapter();
                    break;

                case "oracle.dataaccess":
                case "oracle.dataaccess.client":
                    dataAdapter = new OracleDataAdapter();
                    break;
                default: break;
            }

            return dataAdapter;
        }

        /// <summary>
        /// Retorna uma instancia concreta de um DbCommand.
        /// </summary>
        /// <param name="connectionStringName">Nome da string de conexão.</param>
        /// <param name="commandText">Comando SQL que será atribuido ao objeto DbCommand.</param>
        /// <returns> Objeto IDbCommand.</returns>
        public static IDbCommand CreateCommand(string connectionStringName, string commandText)
        {
            IDbCommand command = null;
            string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            
            // Cria o command correspondente ao nome do Provider (SQL, Oracle ou OleDb)
            switch (providerName.ToLowerInvariant())
            {
                case "system.data.sqlclient":
                    command = new SqlCommand(commandText);
                    break;

                case "system.data.oledb":
                    command = new OleDbCommand(commandText);
                    break;

                case "oracle.dataaccess":
                case "oracle.dataaccess.client":
                    command = new OracleCommand(commandText);
                    break;
                default: break;
            }

            return command;
        }

        /// <summary>
        /// Retorna o simbolo uilizado para criação de parametros na consulta SQL.
        /// </summary>
        /// <param name="providerName">Nome do provider.</param>
        /// <returns> String com o simbolo para o provider.</returns>
        public static string GetParamSymbol(string providerName)
        {
            string paramSymbol = null;
            
            // Retorna o símbolo do SQL conforme o nome do provider
            switch (providerName.ToLowerInvariant())
            {
                // SQL Server
                case "system.data.sqlclient":
                    paramSymbol = "@";
                    break;

                // OleDB
                case "system.data.oledb":
                    paramSymbol = "?";
                    break;

                // Oracle Data Access
                case "oracle.dataaccess":
                case "oracle.dataaccess.client":
                    paramSymbol = ":";
                    break;
                default: break;
            }

            return paramSymbol;
        }
    }
}