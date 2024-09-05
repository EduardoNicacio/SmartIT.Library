using SmartIT.Library.Utilities.Cryptography;
using System.Text;

namespace SmartIT.Library.Tests.Utilities.Cryptography
{
	[TestFixture]
	public class CryptographyTests
	{
		private const string emptyString = "";
		private const string notEmptyString = "Eduardo Claudio Nicacio";

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		#region Base64 encoding/decoding tests

		[Test]
		public void Validate_Base64Encode_EmptyString()
		{
			// Arrange

			// Act
			var result = Base64.Base64Encode(emptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(0));
		}

		[Test]
		public void Validate_Base64Encode_EmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act
			var result = Base64.Base64Encode(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(0));
		}

		[Test]
		public void Validate_Base64Encode_EmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Base64.Base64Encode(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(0));
		}

		[Test]
		public void Validate_Base64Decode_EmptyString()
		{
			// Arrange

			// Act
			var result = Base64.Base64Decode("");

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(0));
		}

		[Test]
		public void Validate_Base64Encode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Base64.Base64Encode(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("RWR1YXJkbyBDbGF1ZGlvIE5pY2FjaW8="));
		}

		[Test]
		public void Validate_Base64Encode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			var result = Base64.Base64Encode(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("RWR1YXJkbyBDbGF1ZGlvIE5pY2FjaW8="));
		}

		[Test]
		public void Validate_Base64Encode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Base64.Base64Encode(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("RWR1YXJkbyBDbGF1ZGlvIE5pY2FjaW8="));
		}

		[Test]
		public void Validate_Base64Decode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Base64.Base64Decode("RWR1YXJkbyBDbGF1ZGlvIE5pY2FjaW8=");

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		#endregion

		#region Caesar Cipher encoding/decoding tests

		[Test]
		public void Validate_CaesarCipherEncode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = CaesarCipher.Encipher(notEmptyString, 13);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("Rqhneqb Pynhqvb Avpnpvb"));
		}

		[Test]
		public void Validate_CaesarCipherDecode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = CaesarCipher.Decipher("Rqhneqb Pynhqvb Avpnpvb", 13);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_CaesarCipherEncode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			var result = CaesarCipher.Encipher(buffer, 13);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("Rqhneqb Pynhqvb Avpnpvb"));
		}

		[Test]
		public void Validate_CaesarCipherDecode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes("Rqhneqb Pynhqvb Avpnpvb");

			// Act
			var result = CaesarCipher.Decipher(buffer, 13);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_CaesarCipherEncode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = CaesarCipher.Encipher(memoryStream, 13);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("Rqhneqb Pynhqvb Avpnpvb"));
		}

		[Test]
		public void Validate_CaesarCipherDecode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes("Rqhneqb Pynhqvb Avpnpvb");
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = CaesarCipher.Decipher(memoryStream, 13);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_CaesarCipherEncode_NullOrEmptyString()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => CaesarCipher.Encipher(string.Empty, 13));
		}

		[Test]
		public void Validate_CaesarCipherEncode_ShiftUnderflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => CaesarCipher.Encipher(notEmptyString, 0));
		}

		[Test]
		public void Validate_CaesarCipherEncode_ShiftOverflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => CaesarCipher.Encipher(notEmptyString, 27));
		}

		[Test]
		public void Validate_CaesarCipherDecode_NullOrEmptyString()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => CaesarCipher.Decipher(string.Empty, 13));
		}

		[Test]
		public void Validate_CaesarCipherDecode_ShiftUnderflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => CaesarCipher.Decipher(notEmptyString, 0));
		}

		[Test]
		public void Validate_CaesarCipherDecode_ShiftOverflow()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => CaesarCipher.Decipher(notEmptyString, 27));
		}

		#endregion

		#region Rot13 encoding/decoding tests

		[Test]
		public void Validate_Rot13Encode_NullOrEmptyString()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot13.Rot13Encode(string.Empty));
		}

		[Test]
		public void Validate_Rot13Encode_NullOrEmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot13.Rot13Encode(buffer));
		}

		[Test]
		public void Validate_Rot13Encode_NullOrEmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot13.Rot13Encode(memoryStream));
		}

		[Test]
		public void Validate_Rot13Encode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Rot13.Rot13Encode(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("Rqhneqb Pynhqvb Avpnpvb"));
		}

		[Test]
		public void Validate_Rot13Encode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			var result = Rot13.Rot13Encode(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("Rqhneqb Pynhqvb Avpnpvb"));
		}

		[Test]
		public void Validate_Rot13Encode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Rot13.Rot13Encode(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("Rqhneqb Pynhqvb Avpnpvb"));
		}

		[Test]
		public void Validate_Rot13Decode_NullOrEmptyString()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot13.Rot13Decode(string.Empty));
		}

		[Test]
		public void Validate_Rot13Decode_NullOrEmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot13.Rot13Decode(buffer));
		}

		[Test]
		public void Validate_Rot13Decode_NullOrEmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot13.Rot13Decode(memoryStream));
		}

		[Test]
		public void Validate_Rot13Decode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Rot13.Rot13Decode("Rqhneqb Pynhqvb Avpnpvb");

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_Rot13Decode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes("Rqhneqb Pynhqvb Avpnpvb");

			// Act
			var result = Rot13.Rot13Decode(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_Rot13Decode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes("Rqhneqb Pynhqvb Avpnpvb");
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Rot13.Rot13Decode(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		#endregion

		#region Rot47 encoding/decoding tests

		[Test]
		public void Validate_Rot47Encode_NullOrEmptyString()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot47.Rot47Encode(string.Empty));
		}

		[Test]
		public void Validate_Rot47Encode_NullOrEmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot47.Rot47Encode(buffer));
		}

		[Test]
		public void Validate_Rot47Encode_NullOrEmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot47.Rot47Encode(memoryStream));
		}

		[Test]
		public void Validate_Rot47Encode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Rot47.Rot47Encode(notEmptyString);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("t5F2C5@ r=2F5:@ }:424:@"));
		}

		[Test]
		public void Validate_Rot47Encode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);

			// Act
			var result = Rot47.Rot47Encode(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("t5F2C5@ r=2F5:@ }:424:@"));
		}

		[Test]
		public void Validate_Rot47Encode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes(notEmptyString);
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Rot47.Rot47Encode(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo("t5F2C5@ r=2F5:@ }:424:@"));
		}

		[Test]
		public void Validate_Rot47Decode_NullOrEmptyString()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot47.Rot47Decode(string.Empty));
		}

		[Test]
		public void Validate_Rot47Decode_NullOrEmptyByteArray()
		{
			// Arrange
			var buffer = Array.Empty<byte>();

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot47.Rot47Decode(buffer));
		}

		[Test]
		public void Validate_Rot47Decode_NullOrEmptyStream()
		{
			// Arrange
			var buffer = Array.Empty<byte>();
			var memoryStream = new MemoryStream(buffer);

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => Rot47.Rot47Decode(memoryStream));
		}

		[Test]
		public void Validate_Rot47Decode_NotEmptyString()
		{
			// Arrange

			// Act
			var result = Rot47.Rot47Decode("t5F2C5@ r=2F5:@ }:424:@");

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_Rot47Decode_NotEmptyByteArray()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes("t5F2C5@ r=2F5:@ }:424:@");

			// Act
			var result = Rot47.Rot47Decode(buffer);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		[Test]
		public void Validate_Rot47Decode_NotEmptyStream()
		{
			// Arrange
			var buffer = Encoding.UTF8.GetBytes("t5F2C5@ r=2F5:@ }:424:@");
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = Rot47.Rot47Decode(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.GreaterThan(0));
			Assert.That(result, Is.EqualTo(notEmptyString));
		}

		#endregion
	}
}
