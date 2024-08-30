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
		public void Verify_GenerateSalt()
		{
			// Arrange

			// Act
			var result = PasswordHelper.GenerateSalt();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public async Task Verify_GenerateSaltAsync()
		{
			// Arrange

			// Act
			var result = await PasswordHelper.GenerateSaltAsync();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public void Verify_GeneratePassword_ValidLength()
		{
			// Arrange

			// Act
			var result = PasswordHelper.GeneratePassword(16);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public async Task Verify_GeneratePasswordAsync_ValidLength()
		{
			// Arrange

			// Act
			var result = await PasswordHelper.GeneratePasswordAsync(16);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
		}

		[Test]
		public void Verify_GeneratePassword_LengthUnderflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentException>(() => PasswordHelper.GeneratePassword(0));
		}

		[Test]
		public async Task Verify_GeneratePasswordAsync_LengthUnderflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentException>(async () => await PasswordHelper.GeneratePasswordAsync(0));
		}

		[Test]
		public void Verify_GeneratePassword_LengthOverflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentException>(() => PasswordHelper.GeneratePassword(129));
		}

		[Test]
		public async Task Verify_GeneratePasswordAsync_LengthOverflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentException>(async () => await PasswordHelper.GeneratePasswordAsync(129));
		}

		[Test]
		public void Verify_ComputeDigest_1()
		{
			// Arrange

			// Act
			var result = _pwdHelper1.ComputeDigest();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
		}

		[Test]
		public async Task Verify_ComputeDigestAsync_1()
		{
			// Arrange

			// Act
			var result = await _pwdHelper1.ComputeDigestAsync();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
		}

		[Test]
		public void Verify_CompareTwoDigests()
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
				Assert.That(result1.Length, Is.EqualTo(result2.Length));
				Assert.That(result1, Is.Not.EqualTo(result2));
			});
		}

		[Test]
		public async Task Verify_CompareTwoDigestsAsync()
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
				Assert.That(result1.Length, Is.EqualTo(result2.Length));
				Assert.That(result1, Is.Not.EqualTo(result2));
			});
		}
	}
}
