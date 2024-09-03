namespace SmartIT.Library.Tests.Utilities
{
	using SmartIT.Library.Utilities;

	[TestFixture]
	public class ValidationTests
	{
		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test]
		public void Validate_IsInteger_NullValue()
		{
			// Arrange

			// Act
			var result = Validation.IsInteger(null);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsInteger_EmptyValue()
		{
			// Arrange

			// Act
			var result = Validation.IsInteger(string.Empty);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsInteger_ValidValue()
		{
			// Arrange

			// Act
			var result = Validation.IsInteger("123");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Validate_IsLong_NullValue()
		{
			// Arrange

			// Act
			var result = Validation.IsLong(null);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsLong_EmptyValue()
		{
			// Arrange

			// Act
			var result = Validation.IsLong(string.Empty);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsLong_ValidValue()
		{
			// Arrange

			// Act
			var result = Validation.IsLong("123");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Validate_IsDecimal_NullValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDecimal(null);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsDecimal_EmptyValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDecimal(string.Empty);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsDecimal_ValidValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDecimal("123.456");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Validate_IsDouble_NullValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDouble(null);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsDouble_EmptyValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDouble(string.Empty);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsDouble_ValidValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDouble("123.456");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Validate_IsDate_NullValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDate(null);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsDate_EmptyValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDate(string.Empty);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsDate_ValidValue()
		{
			// Arrange

			// Act
			var result = Validation.IsDate("01/01/2000 12:00 PM");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Validate_IsEmail_NullOrEmptyEmail()
		{
			// Arrange

			// Act
			var result1 = Validation.IsEmail(null);
			var result2 = Validation.IsEmail(string.Empty);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.False);
				Assert.That(result2, Is.False);
			});
		}

		[Test]
		public void Validate_IsEmail_InvalidEmail()
		{
			// Arrange

			// Act
			var result = Validation.IsEmail("http://www.somewhere.com");

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Validate_IsEmail_ValidEmail()
		{
			// Arrange

			// Act
			var result = Validation.IsEmail("someone@mail.com");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Validate_IsCpf_NullOrEmptyValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCpf(null);
			var result2 = Validation.IsCpf(string.Empty);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.False);
				Assert.That(result2, Is.False);
			});
		}

		[Test]
		public void Validate_IsCpf_InvalidValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCpf("12345678910");

			// Assert
			Assert.That(result1, Is.False);
		}

		[Test]
		public void Validate_IsCpf_ValidValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCpf("11111111111");

			// Assert
			Assert.That(result1, Is.True);
		}

		[Test]
		public void Validate_IsCnpj_NullOrEmptyValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCnpj(null);
			var result2 = Validation.IsCnpj(string.Empty);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.False);
				Assert.That(result2, Is.False);
			});
		}

		[Test]
		public void Validate_IsCnpj_InvalidValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCnpj("12.345.678/9999-99");

			// Assert
			Assert.That(result1, Is.False);
		}

		[Test]
		public void Validate_IsCnpj_ValidValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCnpj("00.000.000/0001-91"); //Banco do Brasil

			// Assert
			Assert.That(result1, Is.True);
		}

		[Test]
		public void Validate_IsCep_NullOrEmptyValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCep(null);
			var result2 = Validation.IsCep(string.Empty);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.False);
				Assert.That(result2, Is.False);
			});
		}

		[Test]
		public void Validate_IsCep_InvalidValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCep("abcde-zzz");

			// Assert
			Assert.That(result1, Is.False);
		}

		[Test]
		public void Validate_IsCep_ValidValue()
		{
			// Arrange

			// Act
			var result1 = Validation.IsCep("00000-999");

			// Assert
			Assert.That(result1, Is.True);
		}
	}
}
