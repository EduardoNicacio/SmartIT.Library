using SmartIT.Library.Extensions;

namespace SmartIT.Library.Tests.Extensions
{
	[TestFixture]
	public class StringExtensionsTests
	{
		private const string emptyString = "";
		private const string adjustL = " EduardoClaudioNicacio ";
		private const string adjustU = " EduardoClaudioNicacio ";
		private const string onlyNumbers = "a1b2c3d4e5f6g7h8i9j10";
		private const string removeSymbols = "a!b@c#d$e%f^g&h*i(j)";
		private const string toCpf = "19236186874";
		private const string toCleanCpf = "192.361.868-74";
		private const string toCnpj = "12345678000100";
		private const string toCleanCnpj = "12.345.678/0001-00";
		private const string toZipCode = "09880000";
		private const string toCleanZipCode = "09880-000";
		private const string truncateString = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test]
		public void Verify_AdjustL_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.AdjustL(emptyString);
			// Assert

			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_AdjustLAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.AdjustLAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));

			return result;
		}

		[Test]
		public void Verify_AdjustL_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.AdjustL(adjustL);

			// Assert
			Assert.That(result, Is.EqualTo(adjustL.Trim().ToLowerInvariant()));
		}

		[Test(ExpectedResult = "eduardoclaudionicacio")]
		public async Task<string> Verify_AdjustLAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.AdjustLAsync(adjustL);

			// Assert
			Assert.That(result, Is.EqualTo(adjustL.Trim().ToLowerInvariant()));

			return result;
		}

		[Test]
		public void Verify_AdjustU_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.AdjustU(emptyString);
			// Assert

			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_AdjustUAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.AdjustUAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));

			return result;
		}

		[Test]
		public void Verify_AdjustU_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.AdjustU(adjustU);

			// Assert
			Assert.That(result, Is.EqualTo(adjustL.Trim().ToUpperInvariant()));
		}

		[Test(ExpectedResult = "EDUARDOCLAUDIONICACIO")]
		public async Task<string> Verify_AdjustUAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.AdjustUAsync(adjustU);

			// Assert
			Assert.That(result, Is.EqualTo(adjustL.Trim().ToUpperInvariant()));

			return result;
		}

		[Test]
		public void Verify_OnlyNumbers_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.OnlyNumbers(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_OnlyNumbersAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.OnlyNumbersAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));

			return result;
		}

		[Test]
		public void Verify_OnlyNumbers_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.OnlyNumbers(onlyNumbers);

			// Assert
			Assert.That(result, Is.EqualTo("12345678910"));
		}

		[Test(ExpectedResult = "12345678910")]
		public async Task<string> Verify_OnlyNumbersAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.OnlyNumbersAsync(onlyNumbers);

			// Assert
			Assert.That(result, Is.EqualTo("12345678910"));

			return result;
		}

		[Test]
		public void Verify_RemoveSymbols_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.RemoveSymbols(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_RemoveSymbolsAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.RemoveSymbolsAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));

			return result;
		}

		[Test]
		public void Verify_RemoveSymbols_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.RemoveSymbols(removeSymbols);

			// Assert
			Assert.That(result, Is.EqualTo("abcdefghij"));
		}

		[Test(ExpectedResult = "abcdefghij")]
		public async Task<string> Verify_RemoveSymbolsAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.RemoveSymbolsAsync(removeSymbols);

			// Assert
			Assert.That(result, Is.EqualTo("abcdefghij"));

			return result;
		}

		[Test]
		public void Verify_ToCpf_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCpf(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000000000"));
		}

		[Test(ExpectedResult = "00000000000")]
		public async Task<string> Verify_ToCpfAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCpfAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000000000"));

			return result;
		}

		[Test]
		public void Verify_ToCpf_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCpf(toCpf);

			// Assert
			Assert.That(result, Is.EqualTo(toCleanCpf));
		}

		[Test(ExpectedResult = toCleanCpf)]
		public async Task<string> Verify_ToCpfAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCpfAsync(toCpf);

			// Assert
			Assert.That(result, Is.EqualTo(toCleanCpf));

			return result;
		}

		[Test]
		public void Verify_ToCleanCpf_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCleanCpf(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_ToCleanCpfAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCleanCpfAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));

			return result;
		}

		[Test]
		public void Verify_ToCleanCpf_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCleanCpf(toCleanCpf);

			// Assert
			Assert.That(result, Is.EqualTo(toCpf));
		}

		[Test(ExpectedResult = toCpf)]
		public async Task<string> Verify_ToCleanCpfAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCleanCpfAsync(toCleanCpf);

			// Assert
			Assert.That(result, Is.EqualTo(toCpf));

			return result;
		}

		[Test]
		public void Verify_ToCnpj_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCnpj(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000000000000"));
		}

		[Test(ExpectedResult = "00000000000000")]
		public async Task<string> Verify_ToCnpjAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCnpjAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000000000000"));

			return result;
		}

		[Test]
		public void Verify_ToCnpj_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCnpj(toCnpj);

			// Assert
			Assert.That(result, Is.EqualTo(toCleanCnpj));
		}

		[Test(ExpectedResult = toCleanCnpj)]
		public async Task<string> Verify_ToCnpjAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCnpjAsync(toCnpj);

			// Assert
			Assert.That(result, Is.EqualTo(toCleanCnpj));

			return result;
		}

		[Test]
		public void Verify_ToCleanCnpj_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCleanCnpj(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_ToCleanCnpjAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCleanCnpjAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo(string.Empty));

			return result;
		}

		[Test]
		public void Verify_ToCleanCnpj_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCleanCnpj(toCleanCnpj);

			// Assert
			Assert.That(result, Is.EqualTo(toCnpj));
		}

		[Test(ExpectedResult = toCnpj)]
		public async Task<string> Verify_ToCleanCnpjAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCleanCnpjAsync(toCleanCnpj);

			// Assert
			Assert.That(result, Is.EqualTo(toCnpj));

			return result;
		}

		[Test]
		public void Verify_ToZipCode_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToZipCode(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000-000"));
		}

		[Test(ExpectedResult = "00000-000")]
		public async Task<string> Verify_ToZipCodeAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToZipCodeAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000-000"));

			return result;
		}

		[Test]
		public void Verify_ToZipCode_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToZipCode(toZipCode);

			// Assert
			Assert.That(result, Is.EqualTo(toCleanZipCode));
		}

		[Test(ExpectedResult = toCleanZipCode)]
		public async Task<string> Verify_ToZipCodeAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToZipCodeAsync(toZipCode);

			// Assert
			Assert.That(result, Is.EqualTo(toCleanZipCode));

			return result;
		}

		[Test]
		public void Verify_ToCleanZipCode_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCleanZipCode(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000000"));
		}

		[Test(ExpectedResult = "00000000")]
		public async Task<string> Verify_ToCleanZipCodeAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCleanZipCodeAsync(emptyString);

			// Assert
			Assert.That(result, Is.EqualTo("00000000"));

			return result;
		}

		[Test]
		public void Verify_ToCleanZipCode_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.ToCleanZipCode(toCleanZipCode);

			// Assert
			Assert.That(result, Is.EqualTo(toZipCode));
		}

		[Test(ExpectedResult = toZipCode)]
		public async Task<string> Verify_ToCleanZipCodeAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.ToCleanZipCodeAsync(toCleanZipCode);

			// Assert
			Assert.That(result, Is.EqualTo(toZipCode));

			return result;
		}

		[Test]
		public void Verify_Truncate_EmptyString()
		{
			// Arrange

			// Act
			var result = StringExtensions.Truncate(emptyString, 10);

			// Assert
			Assert.That(result, Is.EqualTo(emptyString));
		}

		[Test(ExpectedResult = emptyString)]
		public async Task<string> Verify_TruncateAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.TruncateAsync(emptyString, 10);

			// Assert
			Assert.That(result, Is.EqualTo(emptyString));

			return result;
		}

		[Test]
		public void Verify_Truncate_RegularString()
		{
			// Arrange

			// Act
			var result = StringExtensions.Truncate(truncateString, 10);

			// Assert
			Assert.That(result, Is.EqualTo("0123456789"));
		}

		[Test(ExpectedResult = "0123456789")]
		public async Task<string> Verify_TruncateAsync_RegularString()
		{
			// Arrange

			// Act
			var result = await StringExtensions.TruncateAsync(truncateString, 10);

			// Assert
			Assert.That(result, Is.EqualTo("0123456789"));

			return result;
		}

		[Test]
		public void Verify_Truncate_LengthOverflow()
		{
			// Arrange

			// Act
			var result = StringExtensions.Truncate(truncateString, 100);

			// Assert
			Assert.That(result, Is.EqualTo(truncateString));
		}

		[Test(ExpectedResult = truncateString)]
		public async Task<string> Verify_TruncateAsync_LengthOverflow()
		{
			// Arrange

			// Act
			var result = await StringExtensions.TruncateAsync(truncateString, 100);

			// Assert
			Assert.That(result, Is.EqualTo(truncateString));

			return result;
		}
	}
}