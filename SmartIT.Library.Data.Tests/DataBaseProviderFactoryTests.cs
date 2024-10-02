namespace SmartIT.Library.Data.Tests
{
	[TestFixture]
	internal class DataBaseProviderFactoryTests
	{
		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test, Order(1)]
		public void Validate_DataBaseProviderFactory_CreateConnection_ConnectionStringNameMissing()
		{
			// Arrange
			string cnnStringName = string.Empty;

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateConnection(cnnStringName));
		}

		[Test, Order(2)]
		public void Validate_DataBaseProviderFactory_CreateConnection_ConnectionStringMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnMissingConnectionString";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateConnection(cnnStringName));
		}

		[Test, Order(3)]
		public void Validate_DataBaseProviderFactory_CreateConnection_ProviderNameMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnMissingProviderName";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateConnection(cnnStringName));
		}

		[Test, Order(4)]
		public void Validate_DataBaseProviderFactory_CreateConnection_BothParametersMissing()
		{
			// Arrange
			string cnnStringName = string.Empty;
			string providerName = string.Empty;

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateConnection(cnnStringName, providerName));
		}

		[Test, Order(5)]
		public void Validate_DataBaseProviderFactory_CreateConnection_ValidParameters_SqlServer()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateConnection(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(6)]
		public void Validate_DataBaseProviderFactory_CreateConnection_ValidParameters_OleDb()
		{
			// Arrange
			string cnnStringName = "OleDbCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateConnection(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(7), Ignore("Not yet implemented")]
		public void Validate_DataBaseProviderFactory_CreateConnection_ValidParameters_Oracle()
		{
			// Arrange
			string cnnStringName = "OracleCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateConnection(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(6)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ConnectionStringNameMissing()
		{
			// Arrange
			string cnnStringName = "";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateDataAdapter(cnnStringName));
		}

		[Test, Order(7)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ProviderNameMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnMissingProviderName";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateDataAdapter(cnnStringName));
		}

		[Test, Order(8)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ValidParameters_SqlServer()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateDataAdapter(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(9)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ValidParameters_OleDb()
		{
			// Arrange
			string cnnStringName = "OleDbCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateDataAdapter(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(10), Ignore("Not yet implemented")]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ValidParameters_Oracle()
		{
			// Arrange
			string cnnStringName = "OracleCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateDataAdapter(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(11)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ConnectionStringNameMissing()
		{
			// Arrange
			string cnnStringName = "";
			string commandText = "select * from dbo.Users;";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateCommand(cnnStringName, commandText));
		}

		[Test, Order(12)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ProviderNameMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnMissingProviderName";
			string commandText = "select * from dbo.Users;";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateCommand(cnnStringName, commandText));
		}

		[Test, Order(13)]
		public void Validate_DataBaseProviderFactory_CreateCommand_CommandTextMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";
			string commandText = "";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateCommand(cnnStringName, commandText));
		}

		[Test, Order(14)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ValidParameters_SqlServer()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";
			string commandText = "select * from dbo.Users;";

			// Act
			var result = DataBaseProviderFactory.CreateCommand(cnnStringName, commandText);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(15)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ValidParameters_OleDb()
		{
			// Arrange
			string cnnStringName = "OleDbCnnFull";
			string commandText = "select * from dbo.Users;";

			// Act
			var result = DataBaseProviderFactory.CreateCommand(cnnStringName, commandText);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(16), Ignore("Not yet implemented")]
		public void Validate_DataBaseProviderFactory_CreateCommand_ValidParameters_Oracle()
		{
			// Arrange
			string cnnStringName = "OracleCnnFull";
			string commandText = "select * from Users;";

			// Act
			var result = DataBaseProviderFactory.CreateCommand(cnnStringName, commandText);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(17)]
		public void Validate_DataBaseProviderFactory_GetParamSymbol_AllBranches()
		{
			// Arrange

			// Act
			var resultNull = DataBaseProviderFactory.GetParamSymbol(string.Empty);
			var resultSql = DataBaseProviderFactory.GetParamSymbol("system.data.sqlclient");
			var resultOracle = DataBaseProviderFactory.GetParamSymbol("system.data.oracleclient");
			var resultOledb = DataBaseProviderFactory.GetParamSymbol("system.data.oledb");

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(resultNull, Is.Null);
				Assert.That(resultSql, Is.EqualTo("@"));
				Assert.That(resultOracle, Is.EqualTo(":"));
				Assert.That(resultOledb, Is.EqualTo("?"));
			});
		}
	}
}
