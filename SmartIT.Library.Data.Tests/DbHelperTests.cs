using Microsoft.Data.SqlClient;
using SmartIT.Library.Data.Tests.DAL;

namespace SmartIT.Library.Data.Tests
{
	[TestFixture]
	internal class DbHelperTests
	{
		const string CONNECTIONSTRING_NAME = "NUnitTests";
		const int CONNECTION_TIMEOUT = 30;

		readonly UserHelper.User user1 = new() { Id = 1, Name = "Eduardo", Email = "eduardo@email.com", CreationDate = DateTime.Now };
		readonly UserHelper.User user2 = new() { Id = 2, Name = "Claudio", Email = "claudio@email.com", CreationDate = DateTime.Now.AddDays(-1) };
		readonly UserHelper.User user3 = new() { Id = 3, Name = "Nicacio", Email = "nicacio@email.com", CreationDate = DateTime.Now.AddDays(-2) };

		[OneTimeSetUp]
		public void SetUp()
		{
			// Delete existent records
			UserHelper.UserBll.SearchCriteria.Clear();
			var users = UserHelper.UserBll.Search();
			foreach (var user in users)
			{
				UserHelper.UserBll.Delete(user);
			}

			// Insert new records
			UserHelper.UserBll.Insert(user1);
			UserHelper.UserBll.Insert(user2);
			UserHelper.UserBll.Insert(user3);
		}

		[Test, Order(1)]
		public void Validate_GetInstance_EmptyCnnString_ThrowsException()
		{
			// Arrange
			string cnnStringName = string.Empty;

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DbHelper.GetInstance(cnnStringName, 30));
		}

		[Test, Order(2)]
		public void Validate_GetInstance_InexistentCnnString_ThrowsException()
		{
			// Arrange
			string cnnStringName = "SteveJobsBillGates";

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => DbHelper.GetInstance(cnnStringName, 30));
		}

		[Test, Order(3)]
		public void Validate_GetInstance_ValidCnnString_ReturnsTransaction()
		{
			// Arrange

			// Act
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var transaction = dbHelper.CreateTransaction();

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(transaction, Is.Not.Null);
				Assert.That(transaction.Connection, Is.Not.Null);
			}
		}

		[Test, Order(4)]
		public void Validate_GetInstance_GetNextValue_SimpleQuery_ReturnsValue()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			var nextUserId = dbHelper.GetNextValue("Id", "dbo.[User]");

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(nextUserId, Is.GreaterThan(0));
			}
		}

		[Test, Order(5)]
		public void Validate_GetInstance_GetNextValue_ValidCommand_ReturnsValue()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var command = dbHelper.CreateCommand("SELECT MAX(Id) VAL FROM dbo.[User];", typeof(SqlCommand));

			// Act
			var nextUserId = dbHelper.GetNextValue(command);

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(command, Is.Not.Null);
				Assert.That(nextUserId, Is.GreaterThan(0));
			}
		}

		[Test, Order(6)]
		public void Validate_GetInstance_GetNextValue_ExistingTransaction_ReturnsValue()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			int nextUserId = -1;
			nextUserId = dbHelper.GetNextValue("Id", "dbo.[User]");

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(nextUserId, Is.GreaterThanOrEqualTo(0));
			}
		}

		[Test, Order(7)]
		public void Validate_GetInstance_CreateCommand_ExecuteReader_Read_ReturnUserId()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var transaction = dbHelper.CreateTransaction();

			// Act
			var cmd = dbHelper.CreateCommand("SELECT TOP 1 Id FROM dbo.[User]", typeof(SqlCommand));
			var reader = cmd.ExecuteReader();
			var userId = -1;
			while (reader.Read())
			{
				userId = reader.GetInt32(0);
			}

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(transaction, Is.Not.Null);
				Assert.That(cmd, Is.Not.Null);
				Assert.That(reader, Is.Not.Null);
				Assert.That(userId, Is.GreaterThanOrEqualTo(0));
			}
		}

		[Test, Order(8)]
		public void Validate_GetInstance_SetActiveTransaction_GetActiveTransaction()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			var transaction = dbHelper.CreateTransaction();
			var activeTransaction = dbHelper.ActiveTransaction;

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(transaction, Is.Not.Null);
				Assert.That(activeTransaction, Is.EqualTo(dbHelper.ActiveTransaction));
			}
		}

		[Test, Order(9)]
		public void Validate_GetInstance_ExecuteReader_SqlString()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			int count = -1;
			var reader = dbHelper.ExecuteReader("SELECT COUNT(*) FROM dbo.[User]");

			while (reader.Read())
			{
				count = reader.GetInt32(0);
			}

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(reader, Is.Not.Null);
				Assert.That(count, Is.GreaterThanOrEqualTo(0));
			}
		}

		[Test, Order(10)]
		public void Validate_GetInstance_ExecuteReader_WithTransaction_SqlString()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var transaction = dbHelper.CreateTransaction();

			// Act
			int count = -1;
			var reader = dbHelper.ExecuteReader("SELECT COUNT(*) FROM dbo.[User]");

			while (reader.Read())
			{
				count = reader.GetInt32(0);
			}

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(transaction, Is.Not.Null);
				Assert.That(reader, Is.Not.Null);
				Assert.That(count, Is.GreaterThanOrEqualTo(0));
			}
		}

		[Test, Order(11)]
		public void Validate_GetInstance_ExecuteReader_NullCommand_ThrowsArgumentNullException()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => dbHelper.ExecuteReader(cmd: null, cnn: null));
		}

		[Test, Order(12)]
		public void Validate_GetInstance_ExecuteReader_NullConnection_ThrowsArgumentNullException()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var cmd = dbHelper.CreateCommand("SELECT COUNT(*) FROM dbo.[User]", typeof(SqlCommand));

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => dbHelper.ExecuteReader(cmd: cmd, cnn: null));
		}

		[Test, Order(13)]
		public void Validate_GetInstance_ExecuteNonQuery_ValidSqlStatement()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			var count = dbHelper.ExecuteNonQuery("UPDATE dbo.[User] SET CreationDate = '2025-02-17 00:00:00.000' WHERE Id > 0;");

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(count, Is.GreaterThanOrEqualTo(1));
			}
		}

		[Test, Order(14)]
		public void Validate_GetInstance_ExecuteNonQuery_ValidCommand()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var cmd = dbHelper.CreateCommand("UPDATE dbo.[User] SET CreationDate = '2025-02-17 00:00:00.000' WHERE Id > 0;", typeof(SqlCommand));

			// Act
			var count = dbHelper.ExecuteNonQuery(cmd);

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(cmd, Is.Not.Null);
				Assert.That(count, Is.GreaterThanOrEqualTo(1));
			}
		}

		[Test, Order(15)]
		public void Validate_GetInstance_ExecuteNonQuery_NullCommand_ThrowsArgumentNullException()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => dbHelper.ExecuteNonQuery(cmd: null, cnn: null));
		}

		[Test, Order(16)]
		public void Validate_GetInstance_ExecuteNonQuery_NullConnection_ThrowsArgumentNullException()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var cmd = dbHelper.CreateCommand("UPDATE dbo.[User] SET CreationDate = '2025-02-17 00:00:00.000' WHERE Id > 0;", typeof(SqlCommand));

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => dbHelper.ExecuteNonQuery(cmd: cmd, cnn: null));
		}

		[Test, Order(17)]
		public void Validate_GetInstance_ExecuteNonQuery_ActiveTransaction()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var transaction = dbHelper.CreateTransaction();

			// Act
			int count = dbHelper.ExecuteNonQuery("UPDATE dbo.[User] SET CreationDate = '2025-02-17 00:00:00.000' WHERE Id > 0;");

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(transaction, Is.Not.Null);
				Assert.That(count, Is.GreaterThanOrEqualTo(0));
			}
		}

		[Test, Order(18)]
		public void Validate_GetInstance_ExecuteScalarWithScopeIdentity_ValidCommand()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var command = dbHelper.CreateCommand("INSERT INTO dbo.[User] (Name, Email, CreationDate) values ('oNicacioo', 'onicacioo@mail.com', '2025-02-17 00:00:00.000'); SELECT @@Identity;", typeof(SqlCommand));

			// Act
			var result = dbHelper.ExecuteScalarWithScopeIdentity((SqlCommand)command);

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(command, Is.Not.Null);
				Assert.That(result, Is.GreaterThan(0));
			}
		}

		[Test, Order(19)]
		public void Validate_GetInstance_ExecuteScalar_ValidSqlStatement()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			var result = Convert.ToInt32(dbHelper.ExecuteScalar("INSERT INTO dbo.[User] (Name, Email, CreationDate) values ('oNicacioo', 'onicacioo@mail.com', '2025-02-17 00:00:00.000'); SELECT @@Identity;"));

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(result, Is.GreaterThan(0));
			}
		}

		[Test, Order(20)]
		public void Validate_GetInstance_ExecuteScalar_InvalidSqlStatement()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act
			var result = Convert.ToInt32(dbHelper.ExecuteScalar("INSERT INTO dbo.[User] (Name, Email, CreationDate) values ('oNicacioo', 'onicacioo@mail.com', '2025-02-17 00:00:00.000');"));

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(result, Is.Zero);
			}
		}

		[Test, Order(21)]
		public void Validate_GetInstance_ExecuteScalar_ValidCommand()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var command = dbHelper.CreateCommand("INSERT INTO dbo.[User] (Name, Email, CreationDate) values ('oNicacioo', 'onicacioo@mail.com', '2025-02-17 00:00:00.000'); SELECT @@Identity;", typeof(SqlCommand));

			// Act
			var result = Convert.ToInt32(dbHelper.ExecuteScalar(command));

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(dbHelper, Is.Not.Null);
				Assert.That(command, Is.Not.Null);
				Assert.That(result, Is.GreaterThan(0));
			}
		}

		[Test, Order(22)]
		public void Validate_GetInstance_ExecuteScalar_NullCommand_ThrowsArgumentNullException()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => dbHelper.ExecuteScalar(cmd: null, cnn: null));
		}

		[Test, Order(23)]
		public void Validate_GetInstance_ExecuteScalar_NullConnection_ThrowsArgumentNullException()
		{
			// Arrange
			var dbHelper = DbHelper.GetInstance(CONNECTIONSTRING_NAME, CONNECTION_TIMEOUT);
			var cmd = dbHelper.CreateCommand("INSERT INTO dbo.[User] (Name, Email, CreationDate) values ('oNicacioo', 'onicacioo@mail.com', '2025-02-17 00:00:00.000'); SELECT @@Identity;", typeof(SqlCommand));

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => dbHelper.ExecuteScalar(cmd: cmd, cnn: null));
		}
	}
}
