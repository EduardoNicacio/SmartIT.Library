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
			Assert.Throws<ArgumentException>(() => DataBaseProviderFactory.CreateConnection(cnnStringName));
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
		public void Validate_DataBaseProviderFactory_CreateConnection_ValidParameters()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateConnection(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(5)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ConnectionStringNameMissing()
		{
			// Arrange
			string cnnStringName = "";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateDataAdapter(cnnStringName));
		}

		[Test, Order(6)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ProviderNameMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnMissingProviderName";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateDataAdapter(cnnStringName));
		}

		[Test, Order(7)]
		public void Validate_DataBaseProviderFactory_CreateDataAdapter_ValidParameters()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";

			// Act
			var result = DataBaseProviderFactory.CreateDataAdapter(cnnStringName);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(8)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ConnectionStringNameMissing()
		{
			// Arrange
			string cnnStringName = "";
			string commandText = "select * from dbo.Users;";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateCommand(cnnStringName, commandText));
		}

		[Test, Order(9)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ProviderNameMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnMissingProviderName";
			string commandText = "select * from dbo.Users;";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateCommand(cnnStringName, commandText));
		}

		[Test, Order(10)]
		public void Validate_DataBaseProviderFactory_CreateCommand_CommandTextMissing()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";
			string commandText = "";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DataBaseProviderFactory.CreateCommand(cnnStringName, commandText));
		}

		[Test, Order(11)]
		public void Validate_DataBaseProviderFactory_CreateCommand_ValidParameters()
		{
			// Arrange
			string cnnStringName = "SQLCnnFull";
			string commandText = "select * from dbo.Users;";

			// Act
			var result = DataBaseProviderFactory.CreateCommand(cnnStringName, commandText);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test, Order(12)]
		public void Validate_DataBaseProviderFactory_GetParamSymbol_AllBranches()
		{
			// Arrange

			// Act
			var resultNull = DataBaseProviderFactory.GetParamSymbol(string.Empty);
			var resultSql = DataBaseProviderFactory.GetParamSymbol("system.data.sqlclient");
			var resultOracle1 = DataBaseProviderFactory.GetParamSymbol("oracle.dataaccess");
			var resultOracle2 = DataBaseProviderFactory.GetParamSymbol("oracle.dataaccess.client");
			var resultOledb = DataBaseProviderFactory.GetParamSymbol("system.data.oledb");

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(resultNull, Is.Null);
				Assert.That(resultSql, Is.EqualTo("@"));
				Assert.That(resultOracle1, Is.EqualTo(":"));
				Assert.That(resultOracle2, Is.EqualTo(resultOracle1));
				Assert.That(resultOledb, Is.EqualTo("?"));
			});
		}
	}
}
