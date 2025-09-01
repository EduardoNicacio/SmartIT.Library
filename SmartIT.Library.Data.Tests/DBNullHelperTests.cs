namespace SmartIT.Library.Data.Tests
{
	[TestFixture]
	internal class DBNullHelperTests
	{
		[SetUp]
		public void SetUp()
		{
			// Method intentionally left empty.
		}

		[Test]
		public void Validate_GetValue_Decimal()
		{
			// Arrange
			decimal value = 0m;

			// Act
			var result = DbNullHelper.GetValue(value);

			// Assert
			Assert.That(result, Is.EqualTo(DBNull.Value));
		}

		[Test]
		public void Validate_GetValue_NullableDecimal()
		{
			// Arrange
			decimal? value = null;

			// Act
			var result = DbNullHelper.GetValue(value);

			// Assert
			Assert.That(result, Is.EqualTo(DBNull.Value));
		}

		[Test]
		public void Validate_GetValue_Double()
		{
			// Arrange
			double value = 0D;

			// Act
			var result = DbNullHelper.GetValue(value);

			// Assert
			Assert.That(result, Is.EqualTo(DBNull.Value));
		}

		[Test]
		public void Validate_GetValue_Long()
		{
			// Arrange
			long value = 0L;

			// Act
			var result = DbNullHelper.GetValue(value);

			// Assert
			Assert.That(result, Is.EqualTo(DBNull.Value));
		}
	}
}
