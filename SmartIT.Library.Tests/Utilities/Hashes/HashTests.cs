using SmartIT.Library.Utilities;
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
        public void Verify_GetCrc16Hash_NotEmptyString()
        {
            // Arrange

            // Act
            var result = Hash.GetCrc16Hash(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.GreaterThan(2));
            Assert.That(result, Is.EqualTo("1D9A"));
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
            Assert.That(result, Is.EqualTo("1D9A"));
        }

        [Test(ExpectedResult = "1D9A")]
        public async Task<string> Verify_GetCrc16HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetCrc16HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.GreaterThan(2));
            Assert.That(result, Is.EqualTo("1D9A"));

            return result;
        }

        [Test(ExpectedResult = "1D9A")]
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
            Assert.That(result, Is.EqualTo("1D9A"));

            return result;
        }

        #endregion

        #region CRC32 tests

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
            Assert.That(result, Is.EqualTo("BFECCDB4"));
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
            Assert.That(result, Is.EqualTo("BFECCDB4"));
        }

        [Test(ExpectedResult = "BFECCDB4")]
        public async Task<string> Verify_GetCrc32HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetCrc32HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(8));
            Assert.That(result, Is.EqualTo("BFECCDB4"));

            return result;
        }

        [Test(ExpectedResult = "BFECCDB4")]
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
            Assert.That(result, Is.EqualTo("BFECCDB4"));

            return result;
        }

        #endregion

        #region CRC64-ISO tests

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
            Assert.That(result, Is.EqualTo("7B84C529AEDCC51B"));
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
            Assert.That(result, Is.EqualTo("7B84C529AEDCC51B"));
        }

        [Test(ExpectedResult = "7B84C529AEDCC51B")]
        public async Task<string> Verify_GetCrc64IsoHashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetCrc64IsoHashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(16));
            Assert.That(result, Is.EqualTo("7B84C529AEDCC51B"));

            return result;
        }

        [Test(ExpectedResult = "7B84C529AEDCC51B")]
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
            Assert.That(result, Is.EqualTo("7B84C529AEDCC51B"));

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
            Assert.That(result, Is.EqualTo("DDAE32EBF5164344"));
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
            Assert.That(result, Is.EqualTo("DDAE32EBF5164344"));
        }

        [Test(ExpectedResult = "DDAE32EBF5164344")]
        public async Task<string> Verify_GetCrc64EcmaHashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetCrc64EcmaHashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(16));
            Assert.That(result, Is.EqualTo("DDAE32EBF5164344"));

            return result;
        }

        [Test(ExpectedResult = "DDAE32EBF5164344")]
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
            Assert.That(result, Is.EqualTo("DDAE32EBF5164344"));

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
            Assert.That(result, Is.EqualTo("D41D8CD98F00B204E9800998ECF8427E"));
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
            Assert.That(result, Is.EqualTo("D41D8CD98F00B204E9800998ECF8427E"));
        }

        [Test(ExpectedResult = "D41D8CD98F00B204E9800998ECF8427E")]
        public async Task<string> Verify_GetMd5HashAsync_EmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetMd5HashAsync(emptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(32));
            Assert.That(result, Is.EqualTo("D41D8CD98F00B204E9800998ECF8427E"));

            return result;
        }

        [Test(ExpectedResult = "D41D8CD98F00B204E9800998ECF8427E")]
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
            Assert.That(result, Is.EqualTo("D41D8CD98F00B204E9800998ECF8427E"));

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
            Assert.That(result, Is.EqualTo("F8B9D28A62A3B6BA7235FC3E3D9C343D"));
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
            Assert.That(result, Is.EqualTo("F8B9D28A62A3B6BA7235FC3E3D9C343D"));
        }

        [Test(ExpectedResult = "F8B9D28A62A3B6BA7235FC3E3D9C343D")]
        public async Task<string> Verify_GetMd5HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetMd5HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(32));
            Assert.That(result, Is.EqualTo("F8B9D28A62A3B6BA7235FC3E3D9C343D"));

            return result;
        }

        [Test(ExpectedResult = "F8B9D28A62A3B6BA7235FC3E3D9C343D")]
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
            Assert.That(result, Is.EqualTo("F8B9D28A62A3B6BA7235FC3E3D9C343D"));

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
            Assert.That(result, Is.EqualTo("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709"));
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
            Assert.That(result, Is.EqualTo("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709"));
        }

        [Test(ExpectedResult = "DA39A3EE5E6B4B0D3255BFEF95601890AFD80709")]
        public async Task<string> Verify_GetSha1HashAsync_EmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha1HashAsync(emptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(40));
            Assert.That(result, Is.EqualTo("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709"));

            return result;
        }

        [Test(ExpectedResult = "DA39A3EE5E6B4B0D3255BFEF95601890AFD80709")]
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
            Assert.That(result, Is.EqualTo("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709"));

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
            Assert.That(result, Is.EqualTo("FFE6B516F1892D9AFA27E312CA32BE58EB45A667"));
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
            Assert.That(result, Is.EqualTo("FFE6B516F1892D9AFA27E312CA32BE58EB45A667"));
        }

        [Test(ExpectedResult = "FFE6B516F1892D9AFA27E312CA32BE58EB45A667")]
        public async Task<string> Verify_GetSha1HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha1HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(40));
            Assert.That(result, Is.EqualTo("FFE6B516F1892D9AFA27E312CA32BE58EB45A667"));

            return result;
        }

        [Test(ExpectedResult = "FFE6B516F1892D9AFA27E312CA32BE58EB45A667")]
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
            Assert.That(result, Is.EqualTo("FFE6B516F1892D9AFA27E312CA32BE58EB45A667"));

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
            Assert.That(result, Is.EqualTo("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855"));
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
            Assert.That(result, Is.EqualTo("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855"));
        }

        [Test(ExpectedResult = "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855")]
        public async Task<string> Verify_GetSha256HashAsync_EmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha256HashAsync(emptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(64));
            Assert.That(result, Is.EqualTo("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855"));

            return result;
        }

        [Test(ExpectedResult = "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855")]
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
            Assert.That(result, Is.EqualTo("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855"));

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
            Assert.That(result, Is.EqualTo("C5A81646915861C287FDDFE16001395A3A79CAADF30D04F36417C21944B01D91"));
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
            Assert.That(result, Is.EqualTo("C5A81646915861C287FDDFE16001395A3A79CAADF30D04F36417C21944B01D91"));
        }

        [Test(ExpectedResult = "C5A81646915861C287FDDFE16001395A3A79CAADF30D04F36417C21944B01D91")]
        public async Task<string> Verify_GetSha256HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha256HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(64));
            Assert.That(result, Is.EqualTo("C5A81646915861C287FDDFE16001395A3A79CAADF30D04F36417C21944B01D91"));

            return result;
        }

        [Test(ExpectedResult = "C5A81646915861C287FDDFE16001395A3A79CAADF30D04F36417C21944B01D91")]
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
            Assert.That(result, Is.EqualTo("C5A81646915861C287FDDFE16001395A3A79CAADF30D04F36417C21944B01D91"));

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
            Assert.That(result, Is.EqualTo("38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B"));
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
            Assert.That(result, Is.EqualTo("38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B"));
        }

        [Test(ExpectedResult = "38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B")]
        public async Task<string> Verify_GetSha384HashAsync_EmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha384HashAsync(emptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(96));
            Assert.That(result, Is.EqualTo("38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B"));

            return result;
        }

        [Test(ExpectedResult = "38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B")]
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
            Assert.That(result, Is.EqualTo("38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B"));

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
            Assert.That(result, Is.EqualTo("6DAB350464C4B5CB17C281AB0C2A6D929806A6F04FD5B3897228E67D3BB4B8EE30CC04B6C86081BCED4461ABCC52B9EF"));
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
            Assert.That(result, Is.EqualTo("6DAB350464C4B5CB17C281AB0C2A6D929806A6F04FD5B3897228E67D3BB4B8EE30CC04B6C86081BCED4461ABCC52B9EF"));
        }

        [Test(ExpectedResult = "6DAB350464C4B5CB17C281AB0C2A6D929806A6F04FD5B3897228E67D3BB4B8EE30CC04B6C86081BCED4461ABCC52B9EF")]
        public async Task<string> Verify_GetSha384HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha384HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(96));
            Assert.That(result, Is.EqualTo("6DAB350464C4B5CB17C281AB0C2A6D929806A6F04FD5B3897228E67D3BB4B8EE30CC04B6C86081BCED4461ABCC52B9EF"));

            return result;
        }

        [Test(ExpectedResult = "6DAB350464C4B5CB17C281AB0C2A6D929806A6F04FD5B3897228E67D3BB4B8EE30CC04B6C86081BCED4461ABCC52B9EF")]
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
            Assert.That(result, Is.EqualTo("6DAB350464C4B5CB17C281AB0C2A6D929806A6F04FD5B3897228E67D3BB4B8EE30CC04B6C86081BCED4461ABCC52B9EF"));

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
            Assert.That(result, Is.EqualTo("CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E"));
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
            Assert.That(result, Is.EqualTo("CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E"));
        }

        [Test(ExpectedResult = "CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E")]
        public async Task<string> Verify_GetSha512HashAsync_EmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha512HashAsync(emptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(128));
            Assert.That(result, Is.EqualTo("CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E"));

            return result;
        }

        [Test(ExpectedResult = "CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E")]
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
            Assert.That(result, Is.EqualTo("CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E"));

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
            Assert.That(result, Is.EqualTo("72096406E2E0EB91C661DF08E8B52E7F8BC1E1523D5909DB1FF270BC213E2C318720F4AF72D5F9189A7F446E353FFEAC523D52E0E8E3244417789D08E4EAC258"));
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
            Assert.That(result, Is.EqualTo("72096406E2E0EB91C661DF08E8B52E7F8BC1E1523D5909DB1FF270BC213E2C318720F4AF72D5F9189A7F446E353FFEAC523D52E0E8E3244417789D08E4EAC258"));
        }

        [Test(ExpectedResult = "72096406E2E0EB91C661DF08E8B52E7F8BC1E1523D5909DB1FF270BC213E2C318720F4AF72D5F9189A7F446E353FFEAC523D52E0E8E3244417789D08E4EAC258")]
        public async Task<string> Verify_GetSha512HashAsync_NotEmptyString()
        {
            // Arrange

            // Act
            var result = await Hash.GetSha512HashAsync(notEmptyString);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(128));
            Assert.That(result, Is.EqualTo("72096406E2E0EB91C661DF08E8B52E7F8BC1E1523D5909DB1FF270BC213E2C318720F4AF72D5F9189A7F446E353FFEAC523D52E0E8E3244417789D08E4EAC258"));

            return result;
        }

        [Test(ExpectedResult = "72096406E2E0EB91C661DF08E8B52E7F8BC1E1523D5909DB1FF270BC213E2C318720F4AF72D5F9189A7F446E353FFEAC523D52E0E8E3244417789D08E4EAC258")]
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
            Assert.That(result, Is.EqualTo("72096406E2E0EB91C661DF08E8B52E7F8BC1E1523D5909DB1FF270BC213E2C318720F4AF72D5F9189A7F446E353FFEAC523D52E0E8E3244417789D08E4EAC258"));

            return result;
        }

        #endregion
    }
}
