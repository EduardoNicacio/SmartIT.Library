using SmartIT.Library.Utilities;
using SmartIT.Library.Utilities.Hashes;
using System.Text;

namespace SmartIT.Library.Tests.Utilities.Hashes
{
	[TestFixture]
	public class HashTests
	{
		private const string emptyString = "";
		private const string notEmptyString = "Eduardo Claudio Nicacio";

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		#region CRC16-ARC tests

		[Test]
		public void Verify_GetCrc16Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc16Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(2));
			Assert.That(result, Is.EqualTo("00"));
		}

		[Test]
		public void Verify_GetCrc16Hash_EmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act
			var result = Hash.GetCrc16Hash(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(2));
			Assert.That(result, Is.EqualTo("00"));
		}

		[Test]
		public void Verify_GetCrc16Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc16Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(2));
			Assert.That(result, Is.EqualTo("00"));
		}

		[Test(ExpectedResult = "00")]
		public async Task<string> Verify_GetCrc16HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc16HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(2));
			Assert.That(result, Is.EqualTo("00"));

			return result;
		}

		[Test(ExpectedResult = "00")]
		public async Task<string> Verify_GetCrc16HashAsync_EmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act
			var result = await Hash.GetCrc16HashAsync(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(2));
			Assert.That(result, Is.EqualTo("00"));

			return result;
		}

		[Test(ExpectedResult = "00")]
		public async Task<string> Verify_GetCrc16HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc16HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(2));
			Assert.That(result, Is.EqualTo("00"));

			return result;
		}

		[Test]
		public void Verify_Crc16ComputeHash_ByteArray()
		{
			// Arrange
			var byteArray = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			var result = Crc16.ComputeHash(byteArray);

			// Assert
			Assert.That(result, Is.GreaterThan(0));
		}

		[Test]
		public void Verify_Crc16ComputeHash_String()
		{
			// Arrange

			// Act
			var result = Crc16.ComputeHash("1d9a");

			// Assert
			Assert.That(result, Is.GreaterThan(0));
		}

		[Test]
		public void Verify_GetCrc16Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc16Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(2));
			Assert.That(result, Is.EqualTo("1d9a"));
		}

		[Test]
		public void Verify_GetCrc16Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc16Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(2));
			Assert.That(result, Is.EqualTo("1d9a"));
		}

		[Test(ExpectedResult = "1d9a")]
		public async Task<string> Verify_GetCrc16HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc16HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(2));
			Assert.That(result, Is.EqualTo("1d9a"));

			return result;
		}

		[Test(ExpectedResult = "1d9a")]
		public async Task<string> Verify_GetCrc16HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc16HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(2));
			Assert.That(result, Is.EqualTo("1d9a"));

			return result;
		}

		#endregion

		#region CRC32 tests

		[Test]
		public void Verify_Crc32_InternalMethods()
		{
			// Arrange
			const uint defaultSeed = 0xffffffffu;
			const uint defaultPolynomial = 0xedb88320u;

			var byteArray = Encoding.UTF8.GetBytes(notEmptyString);
			var crc32 = new Crc32();

			// Act
			var hashSize = crc32.HashSize;
			uint compute1 = Crc32.Compute(byteArray);
			uint compute2 = Crc32.Compute(defaultSeed, byteArray);
			uint compute3 = Crc32.Compute(defaultPolynomial, defaultSeed, byteArray);

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(hashSize, Is.EqualTo(32));
				Assert.That(compute1, Is.GreaterThan(0));
				Assert.That(compute2, Is.GreaterThan(0));
				Assert.That(compute3, Is.GreaterThan(0));
			}
		}

		[Test]
		public void Verify_GetCrc32Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc32Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("00000000"));
		}

		[Test]
		public void Verify_GetCrc32Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc32Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("00000000"));
		}

		[Test(ExpectedResult = "00000000")]
		public async Task<string> Verify_GetCrc32HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc32HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("00000000"));

			return result;
		}

		[Test(ExpectedResult = "00000000")]
		public async Task<string> Verify_GetCrc32HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc32HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("00000000"));

			return result;
		}

		[Test]
		public void Verify_GetCrc32Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc32Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("bfeccdb4"));
		}

		[Test]
		public void Verify_GetCrc32Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc32Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("bfeccdb4"));
		}

		[Test(ExpectedResult = "bfeccdb4")]
		public async Task<string> Verify_GetCrc32HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc32HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("bfeccdb4"));

			return result;
		}

		[Test(ExpectedResult = "bfeccdb4")]
		public async Task<string> Verify_GetCrc32HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc32HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(8));
			Assert.That(result, Is.EqualTo("bfeccdb4"));

			return result;
		}

		#endregion

		#region CRC64-ISO tests

		[Test]
		public void Verify_Crc64_InternalMethods()
		{
			// Arrange
			const ulong defaultSeed = 0x0;
			var crc64 = new Crc64Iso(defaultSeed);
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			var hashSize = crc64.HashSize;
			var result1 = Crc64Iso.Compute(buffer);
			var result2 = Crc64Iso.Compute(defaultSeed, buffer);

			// Assert
			using (Assert.EnterMultipleScope())
			{
				Assert.That(hashSize, Is.EqualTo(64));
				Assert.That(result1, Is.GreaterThan(0));
				Assert.That(result2, Is.GreaterThan(0));
			}
		}

		[Test]
		public void Verify_Crc64_InternalMethods_NullTable()
		{
			// Arrange
			const ulong defaultSeed = 0x0;
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			Crc64Iso.Table = null;
			var result = Crc64Iso.Compute(defaultSeed, buffer);

			Assert.That(result, Is.GreaterThan(0));
		}

		[Test]
		public void Verify_GetCrc64IsoHash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc64IsoHash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));
		}

		[Test]
		public void Verify_GetCrc64IsoHash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc64IsoHash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));
		}

		[Test(ExpectedResult = "0000000000000000")]
		public async Task<string> Verify_GetCrc64IsoHashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc64IsoHashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));

			return result;
		}

		[Test(ExpectedResult = "0000000000000000")]
		public async Task<string> Verify_GetCrc64IsoHashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc64IsoHashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));

			return result;
		}

		[Test]
		public void Verify_GetCrc64IsoHash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc64IsoHash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("7b84c529aedcc51b"));
		}

		[Test]
		public void Verify_GetCrc64IsoHash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc64IsoHash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("7b84c529aedcc51b"));
		}

		[Test(ExpectedResult = "7b84c529aedcc51b")]
		public async Task<string> Verify_GetCrc64IsoHashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc64IsoHashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("7b84c529aedcc51b"));

			return result;
		}

		[Test(ExpectedResult = "7b84c529aedcc51b")]
		public async Task<string> Verify_GetCrc64IsoHashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc64IsoHashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("7b84c529aedcc51b"));

			return result;
		}

		#endregion

		#region CRC64-ECMA tests

		[Test]
		public void Verify_GetCrc64EcmaHash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc64EcmaHash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));
		}

		[Test]
		public void Verify_GetCrc64EcmaHash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc64EcmaHash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));
		}

		[Test(ExpectedResult = "0000000000000000")]
		public async Task<string> Verify_GetCrc64EcmaHashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc64EcmaHashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));

			return result;
		}

		[Test(ExpectedResult = "0000000000000000")]
		public async Task<string> Verify_GetCrc64EcmaHashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc64EcmaHashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("0000000000000000"));

			return result;
		}

		[Test]
		public void Verify_GetCrc64EcmaHash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetCrc64EcmaHash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("ddae32ebf5164344"));
		}

		[Test]
		public void Verify_GetCrc64EcmaHash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetCrc64EcmaHash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("ddae32ebf5164344"));
		}

		[Test(ExpectedResult = "ddae32ebf5164344")]
		public async Task<string> Verify_GetCrc64EcmaHashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetCrc64EcmaHashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("ddae32ebf5164344"));

			return result;
		}

		[Test(ExpectedResult = "ddae32ebf5164344")]
		public async Task<string> Verify_GetCrc64EcmaHashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetCrc64EcmaHashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(16));
			Assert.That(result, Is.EqualTo("ddae32ebf5164344"));

			return result;
		}

		#endregion

		#region MD5 tests

		[Test]
		public void Verify_GetMd5Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetMd5Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("d41d8cd98f00b204e9800998ecf8427e"));
		}

		[Test]
		public void Verify_GetMd5Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetMd5Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("d41d8cd98f00b204e9800998ecf8427e"));
		}

		[Test(ExpectedResult = "d41d8cd98f00b204e9800998ecf8427e")]
		public async Task<string> Verify_GetMd5HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetMd5HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("d41d8cd98f00b204e9800998ecf8427e"));

			return result;
		}

		[Test(ExpectedResult = "d41d8cd98f00b204e9800998ecf8427e")]
		public async Task<string> Verify_GetMd5HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetMd5HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("d41d8cd98f00b204e9800998ecf8427e"));

			return result;
		}

		[Test]
		public void Verify_GetMd5Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetMd5Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("f8b9d28a62a3b6ba7235fc3e3d9c343d"));
		}

		[Test]
		public void Verify_GetMd5Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetMd5Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("f8b9d28a62a3b6ba7235fc3e3d9c343d"));
		}

		[Test(ExpectedResult = "f8b9d28a62a3b6ba7235fc3e3d9c343d")]
		public async Task<string> Verify_GetMd5HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetMd5HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("f8b9d28a62a3b6ba7235fc3e3d9c343d"));

			return result;
		}

		[Test(ExpectedResult = "f8b9d28a62a3b6ba7235fc3e3d9c343d")]
		public async Task<string> Verify_GetMd5HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetMd5HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(32));
			Assert.That(result, Is.EqualTo("f8b9d28a62a3b6ba7235fc3e3d9c343d"));

			return result;
		}

		#endregion

		#region SHA-1 tests

		[Test]
		public void Verify_GetSha1Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha1Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("da39a3ee5e6b4b0d3255bfef95601890afd80709"));
		}

		[Test]
		public void Verify_GetSha1Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha1Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("da39a3ee5e6b4b0d3255bfef95601890afd80709"));
		}

		[Test(ExpectedResult = "da39a3ee5e6b4b0d3255bfef95601890afd80709")]
		public async Task<string> Verify_GetSha1HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha1HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("da39a3ee5e6b4b0d3255bfef95601890afd80709"));

			return result;
		}

		[Test(ExpectedResult = "da39a3ee5e6b4b0d3255bfef95601890afd80709")]
		public async Task<string> Verify_GetSha1HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha1HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("da39a3ee5e6b4b0d3255bfef95601890afd80709"));

			return result;
		}

		[Test]
		public void Verify_GetSha1Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha1Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("ffe6b516f1892d9afa27e312ca32be58eb45a667"));
		}

		[Test]
		public void Verify_GetSha1Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha1Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("ffe6b516f1892d9afa27e312ca32be58eb45a667"));
		}

		[Test(ExpectedResult = "ffe6b516f1892d9afa27e312ca32be58eb45a667")]
		public async Task<string> Verify_GetSha1HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha1HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("ffe6b516f1892d9afa27e312ca32be58eb45a667"));

			return result;
		}

		[Test(ExpectedResult = "ffe6b516f1892d9afa27e312ca32be58eb45a667")]
		public async Task<string> Verify_GetSha1HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha1HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(40));
			Assert.That(result, Is.EqualTo("ffe6b516f1892d9afa27e312ca32be58eb45a667"));

			return result;
		}

		#endregion

		#region SHA-256 tests

		[Test]
		public void Verify_GetSha256Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha256Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"));
		}

		[Test]
		public void Verify_GetSha256Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha256Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"));
		}

		[Test(ExpectedResult = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
		public async Task<string> Verify_GetSha256HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha256HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"));

			return result;
		}

		[Test(ExpectedResult = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
		public async Task<string> Verify_GetSha256HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha256HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"));

			return result;
		}

		[Test]
		public void Verify_GetSha256Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha256Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("c5a81646915861c287fddfe16001395a3a79caadf30d04f36417c21944b01d91"));
		}

		[Test]
		public void Verify_GetSha256Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha256Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("c5a81646915861c287fddfe16001395a3a79caadf30d04f36417c21944b01d91"));
		}

		[Test(ExpectedResult = "c5a81646915861c287fddfe16001395a3a79caadf30d04f36417c21944b01d91")]
		public async Task<string> Verify_GetSha256HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha256HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("c5a81646915861c287fddfe16001395a3a79caadf30d04f36417c21944b01d91"));

			return result;
		}

		[Test(ExpectedResult = "c5a81646915861c287fddfe16001395a3a79caadf30d04f36417c21944b01d91")]
		public async Task<string> Verify_GetSha256HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha256HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(64));
			Assert.That(result, Is.EqualTo("c5a81646915861c287fddfe16001395a3a79caadf30d04f36417c21944b01d91"));

			return result;
		}

		#endregion

		#region SHA-384 tests

		[Test]
		public void Verify_GetSha384Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha384Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b"));
		}

		[Test]
		public void Verify_GetSha384Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha384Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b"));
		}

		[Test(ExpectedResult = "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b")]
		public async Task<string> Verify_GetSha384HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha384HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b"));

			return result;
		}

		[Test(ExpectedResult = "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b")]
		public async Task<string> Verify_GetSha384HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha384HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b"));

			return result;
		}

		[Test]
		public void Verify_GetSha384Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha384Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("6dab350464c4b5cb17c281ab0c2a6d929806a6f04fd5b3897228e67d3bb4b8ee30cc04b6c86081bced4461abcc52b9ef"));
		}

		[Test]
		public void Verify_GetSha384Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha384Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("6dab350464c4b5cb17c281ab0c2a6d929806a6f04fd5b3897228e67d3bb4b8ee30cc04b6c86081bced4461abcc52b9ef"));
		}

		[Test(ExpectedResult = "6dab350464c4b5cb17c281ab0c2a6d929806a6f04fd5b3897228e67d3bb4b8ee30cc04b6c86081bced4461abcc52b9ef")]
		public async Task<string> Verify_GetSha384HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha384HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("6dab350464c4b5cb17c281ab0c2a6d929806a6f04fd5b3897228e67d3bb4b8ee30cc04b6c86081bced4461abcc52b9ef"));

			return result;
		}

		[Test(ExpectedResult = "6dab350464c4b5cb17c281ab0c2a6d929806a6f04fd5b3897228e67d3bb4b8ee30cc04b6c86081bced4461abcc52b9ef")]
		public async Task<string> Verify_GetSha384HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha384HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(96));
			Assert.That(result, Is.EqualTo("6dab350464c4b5cb17c281ab0c2a6d929806a6f04fd5b3897228e67d3bb4b8ee30cc04b6c86081bced4461abcc52b9ef"));

			return result;
		}

		#endregion

		#region SHA-512 tests

		[Test]
		public void Verify_GetSha512Hash_EmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha512Hash(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e"));
		}

		[Test]
		public void Verify_GetSha512Hash_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha512Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e"));
		}

		[Test(ExpectedResult = "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e")]
		public async Task<string> Verify_GetSha512HashAsync_EmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha512HashAsync(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e"));

			return result;
		}

		[Test(ExpectedResult = "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e")]
		public async Task<string> Verify_GetSha512HashAsync_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha512HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e"));

			return result;
		}

		[Test]
		public void Verify_GetSha512Hash_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Hash.GetSha512Hash(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("72096406e2e0eb91c661df08e8b52e7f8bc1e1523d5909db1ff270bc213e2c318720f4af72d5f9189a7f446e353ffeac523d52e0e8e3244417789d08e4eac258"));
		}

		[Test]
		public void Verify_GetSha512Hash_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Hash.GetSha512Hash(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("72096406e2e0eb91c661df08e8b52e7f8bc1e1523d5909db1ff270bc213e2c318720f4af72d5f9189a7f446e353ffeac523d52e0e8e3244417789d08e4eac258"));
		}

		[Test(ExpectedResult = "72096406e2e0eb91c661df08e8b52e7f8bc1e1523d5909db1ff270bc213e2c318720f4af72d5f9189a7f446e353ffeac523d52e0e8e3244417789d08e4eac258")]
		public async Task<string> Verify_GetSha512HashAsync_NotEmptyString()
		{
			// Arrange

			// Act
			var result = await Hash.GetSha512HashAsync(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("72096406e2e0eb91c661df08e8b52e7f8bc1e1523d5909db1ff270bc213e2c318720f4af72d5f9189a7f446e353ffeac523d52e0e8e3244417789d08e4eac258"));

			return result;
		}

		[Test(ExpectedResult = "72096406e2e0eb91c661df08e8b52e7f8bc1e1523d5909db1ff270bc213e2c318720f4af72d5f9189a7f446e353ffeac523d52e0e8e3244417789d08e4eac258")]
		public async Task<string> Verify_GetSha512HashAsync_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await Hash.GetSha512HashAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(128));
			Assert.That(result, Is.EqualTo("72096406e2e0eb91c661df08e8b52e7f8bc1e1523d5909db1ff270bc213e2c318720f4af72d5f9189a7f446e353ffeac523d52e0e8e3244417789d08e4eac258"));

			return result;
		}

		#endregion
	}
}
