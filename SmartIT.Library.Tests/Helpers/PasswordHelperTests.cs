using SmartIT.Library.Helpers;

namespace SmartIT.Library.Tests.Helpers
{
	[TestFixture]
	public class PasswordHelperTests
	{
		private PasswordHelper _pwdHelper1;
		private PasswordHelper _pwdHelper2;

		private string _pwd1;

		private string _salt1;
		private string _salt2;

		[SetUp]
		public void Setup()
		{
			_pwd1 = PasswordHelper.GeneratePassword(16);

			_salt1 = PasswordHelper.GenerateSalt();
			_salt2 = PasswordHelper.GenerateSalt();

			_pwdHelper1 = new PasswordHelper(_pwd1, _salt1);
			_pwdHelper2 = new PasswordHelper(_pwd1, _salt2);
		}

		[Test]
		public void Validate_GenerateSalt()
		{
			// Arrange

			// Act
			var result = PasswordHelper.GenerateSalt();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public async Task Validate_GenerateSaltAsync()
		{
			// Arrange

			// Act
			var result = await PasswordHelper.GenerateSaltAsync();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public void Validate_GeneratePassword_ValidLength()
		{
			// Arrange

			// Act
			var result = PasswordHelper.GeneratePassword(16);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public async Task Validate_GeneratePasswordAsync_ValidLength()
		{
			// Arrange

			// Act
			var result = await PasswordHelper.GeneratePasswordAsync(16);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public void Validate_GeneratePassword_LengthUnderflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => PasswordHelper.GeneratePassword(0));
		}

		[Test]
		public Task Validate_GeneratePasswordAsync_LengthUnderflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await PasswordHelper.GeneratePasswordAsync(0));
			return Task.CompletedTask;
		}

		[Test]
		public void Validate_GeneratePassword_LengthOverflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => PasswordHelper.GeneratePassword(129));
		}

		[Test]
		public Task Validate_GeneratePasswordAsync_LengthOverflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await PasswordHelper.GeneratePasswordAsync(129));
			return Task.CompletedTask;
		}

		[Test]
		public void Validate_GeneratePassword_InvalidNumberOfNonAlphanumericCharacters_TestCase1()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => PasswordHelper.GeneratePassword(16, 32));
		}

		[Test]
		public void Validate_GeneratePassword_InvalidNumberOfNonAlphanumericCharacters_TestCase2()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => PasswordHelper.GeneratePassword(16, -1));
		}

		[Test]
		public Task Validate_GeneratePasswordAsync_InvalidNumberOfNonAlphanumericCharacters_TestCase1()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await PasswordHelper.GeneratePasswordAsync(16, 32));
			return Task.CompletedTask;
		}

		[Test]
		public Task Validate_GeneratePasswordAsync_InvalidNumberOfNonAlphanumericCharacters_TestCase2()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await PasswordHelper.GeneratePasswordAsync(16, -1));
			return Task.CompletedTask;
		}

		[Test]
		public void Validate_ComputeDigest()
		{
			// Arrange

			// Act
			var result = _pwdHelper1.ComputeDigest();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
		}

		[Test]
		public async Task Validate_ComputeDigestAsync()
		{
			// Arrange

			// Act
			var result = await _pwdHelper1.ComputeDigestAsync();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
		}

		[Test]
		public void Validate_CompareTwoDigests()
		{
			// Arrange

			// Act
			var result1 = _pwdHelper1.ComputeDigest();
			var result2 = _pwdHelper2.ComputeDigest();

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result1, Has.Length.GreaterThan(0));
				Assert.That(result2, Has.Length.GreaterThan(0));
				Assert.That(result1, Has.Length.EqualTo(result2.Length));
				Assert.That(result1, Is.Not.EqualTo(result2));
			});
		}

		[Test]
		public async Task Validate_CompareTwoDigestsAsync()
		{
			// Arrange

			// Act
			var result1 = await _pwdHelper1.ComputeDigestAsync();
			var result2 = await _pwdHelper2.ComputeDigestAsync();

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result1, Has.Length.GreaterThan(0));
				Assert.That(result2, Has.Length.GreaterThan(0));
				Assert.That(result1, Has.Length.EqualTo(result2.Length));
				Assert.That(result1, Is.Not.EqualTo(result2));
			});
		}

		[Test]
		public void Validate_GeneratePassword_IsDangerousString() 
		{
			// Arrange
			int matchIndex = 0;

			// Act
			var result1 = PasswordHelper.IsDangerousString("<!", out matchIndex);
			var result2 = PasswordHelper.IsDangerousString("</", out matchIndex);
			var result3 = PasswordHelper.IsDangerousString("<?", out matchIndex);
			var result4 = PasswordHelper.IsDangerousString("&#", out matchIndex);
			var result5 = PasswordHelper.IsDangerousString("00", out matchIndex);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.True);
				Assert.That(result2, Is.True);
				Assert.That(result3, Is.True);
				Assert.That(result4, Is.True);
				Assert.That(result5, Is.False);
			});
		}

		[Test]
		public void Validate_GeneratePassword_IsAtoZ()
		{
			// Arrange

			// Act
			var result1 = PasswordHelper.IsAtoZ('^');
			var result2 = PasswordHelper.IsAtoZ('|');
			var result3 = PasswordHelper.IsAtoZ('a');
			var result4 = PasswordHelper.IsAtoZ('z');
			var result5 = PasswordHelper.IsAtoZ('A');
			var result6 = PasswordHelper.IsAtoZ('Z');
			var result7 = PasswordHelper.IsAtoZ('$');

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.False);
				Assert.That(result2, Is.False);
				Assert.That(result3, Is.True);
				Assert.That(result4, Is.True);
				Assert.That(result5, Is.True);
				Assert.That(result6, Is.True);
				Assert.That(result7, Is.False);
			});
		}
	}
}
