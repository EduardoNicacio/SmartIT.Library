// <copyright file="Hash.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>17/04/2017</date>
// <summary>Utility class to easily generate CRC16, CRC32, CRC64, MD5, SHA1, SHA256, SHA384 and SHA512 hashes.</summary>

namespace SmartIT.Library.Utilities
{
	using SmartIT.Library.Helpers;
	using SmartIT.Library.Utilities.Hashes;
	using System;
	using System.IO;
	using System.Net;
	using System.Net.NetworkInformation;
	using System.Security.Cryptography;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Utility class to synchronously or asynchronously generate CRC16, CRC32, CRC64, MD5, SHA1, SHA256, SHA384 and SHA512 hashes.
	/// </summary>
	public static class Hash
	{
		/// <summary>
		/// Returns the CRC16-ARC digest for the informed string.
		/// </summary>
		/// <param name="input">String de entrada.</param>
		/// <returns>CRC16 digest in hexadecimal format.</returns>
		public static string GetCrc16Hash(string input)
		{
			return Crc16.ComputeHash(Encoding.UTF8.GetBytes(input)).ToString("x2").ToUpperInvariant();
		}

		/// <summary>
		/// Returns the CRC16-ARC digest for the informed array of bytes.
		/// </summary>
		/// <param name="input">Input byte array.</param>
		/// <returns>CRC16 digest in hexadecimal format.</returns>
		public static string GetCrc16Hash(byte[] input)
		{
			return Crc16.ComputeHash(input).ToString("x2").ToUpperInvariant();
		}

		/// <summary>
		/// Returns the CRC16-ARC digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns>CRC16 digest in hexadecimal format.</returns>
		public static string GetCrc16Hash(Stream stream)
		{
			byte[] fileData;
			using (var binaryReader = new BinaryReader(stream))
			{
				fileData = binaryReader.ReadBytes((int)stream.Length);
			}
			return Crc16.ComputeHash(fileData).ToString("x2").ToUpperInvariant();
		}

		/// <summary>
		/// Asynchronously returns the CRC16-ARC digest for the informed string.
		/// </summary>
		/// <param name="input">String de entrada.</param>
		/// <returns>CRC16 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc16HashAsync(string input)
		{
			return await Task.Run(() => GetCrc16Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the CRC16-ARC digest for the informed array of bytes.
		/// </summary>
		/// <param name="input">Input byte array.</param>
		/// <returns>CRC16 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc16HashAsync(byte[] input) 
		{
			return await Task.Run(() => GetCrc16Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the CRC16-ARC digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns>CRC16 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc16HashAsync(Stream stream)
		{
			return await Task.Run(() => GetCrc16Hash(stream));
		}

		/// <summary>
		/// Returns the CRC32 digest for the informed string.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>CRC32 digest in hexadecimal format.</returns>
		public static string GetCrc32Hash(string input)
		{
			using (Crc32 crc32Hash = new Crc32())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = crc32Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the CRC32 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns>CRC32 digest in hexadecimal format.</returns>
		public static string GetCrc32Hash(Stream stream)
		{
			using (Crc32 crc32Hash = new Crc32())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = crc32Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the CRC32 digest for the informed string.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>CRC32 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc32HashAsync(string input)
		{
			return await Task.Run(() => GetCrc32Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the CRC32 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns>CRC32 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc32HashAsync(Stream stream)
		{
			return await Task.Run(() => GetCrc32Hash(stream));
		}

		/// <summary>
		/// Returns the ISO-3309 compliant CRC64 digest for the informed string.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>ISO-3309 compliant CRC64 digest in hexadecimal format.</returns>
		public static string GetCrc64IsoHash(string input)
		{
			using (Crc64Iso crc64Hash = new Crc64Iso())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = crc64Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the ISO-3309 compliant CRC64 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns>ISO-3309 compliant CRC64 digest in hexadecimal format.</returns>
		public static string GetCrc64IsoHash(Stream stream)
		{
			using (Crc64Iso crc64Hash = new Crc64Iso())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = crc64Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the ISO-3309 compliant CRC64 digest for the informed string.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>ISO-3309 compliant CRC64 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc64IsoHashAsync(string input)
		{
			return await Task.Run(() => GetCrc64IsoHash(input));
		}

		/// <summary>
		/// Asynchronously returns the ISO-3309 compliant CRC64 digest for the informed string.
		/// </summary>
		/// <param name="stream">Input string.</param>
		/// <returns>ISO-3309 compliant CRC64 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc64IsoHashAsync(Stream stream)
		{
			return await Task.Run(() => GetCrc64IsoHash(stream));
		}

		/// <summary>
		/// Returns the ECMA-182 compliant CRC64 digest for the informed string.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>ECMA-182 compliant CRC64 digest in hexadecimal format.</returns>
		public static string GetCrc64EcmaHash(string input)
		{
			// Convert the input string to a byte array and compute the hash.
			byte[] buffer = Encoding.UTF8.GetBytes(input);
			byte[] data = System.IO.Hashing.Crc64.Hash(buffer);

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString().ToUpperInvariant();
		}

		/// <summary>
		/// Returns the ECMA-182 compliant CRC64 digest for the informed stream.
		/// </summary>
		/// <param name="stream">The input <see cref="Stream"/> object.</param>
		/// <returns>ECMA-182 compliant CRC64 digest in hexadecimal format.</returns>
		public static string GetCrc64EcmaHash(Stream stream)
		{
			byte[] buffer = StreamHelper.StreamToByteArray(stream);
			byte[] data = System.IO.Hashing.Crc64.Hash(buffer);

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString().ToUpperInvariant();
		}

		/// <summary>
		/// Asynchronously returns the ECMA-182 compliant CRC64 digest for the informed string.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>ECMA-182 compliant CRC64 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc64EcmaHashAsync(string input)
		{
			return await Task.Run(() => GetCrc64EcmaHash(input));
		}

		/// <summary>
		/// Asynchronously returns the ECMA-182 compliant CRC64 digest for the informed stream.
		/// </summary>
		/// <param name="stream">The input <see cref="Stream"/> object.</param>
		/// <returns>ECMA-182 compliant CRC64 digest in hexadecimal format.</returns>
		public static async Task<string> GetCrc64EcmaHashAsync(Stream stream)
		{
			return await Task.Run(() => GetCrc64EcmaHash(stream));
		}

		/// <summary>
		/// Returns the MD5 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> MD5 digest in hexadecimal format.</returns>
		public static string GetMd5Hash(string input)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the MD5 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> MD5 digest in hexadecimal format.</returns>
		public static string GetMd5Hash(Stream stream)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = md5Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the MD5 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> MD5 digest in hexadecimal format.</returns>
		public static async Task<string> GetMd5HashAsync(string input)
		{
			return await Task.Run(() => GetMd5Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the MD5 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> MD5 digest in hexadecimal format.</returns>
		public static async Task<string> GetMd5HashAsync(Stream stream)
		{
			return await Task.Run(() => GetMd5Hash(stream));
		}

		/// <summary>
		/// Returns the SHA-1 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-1 digest in hexadecimal format.</returns>
		public static string GetSha1Hash(string input)
		{
			using (SHA1 sha1Hash = SHA1.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the SHA-1 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-1 digest in hexadecimal format.</returns>
		public static string GetSha1Hash(Stream stream)
		{
			using (SHA1 sha1Hash = SHA1.Create())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = sha1Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the SHA-1 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-1 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha1HashAsync(string input)
		{
			return await Task.Run(() => GetSha1Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the SHA-1 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-1 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha1HashAsync(Stream stream)
		{
			return await Task.Run(() => GetSha1Hash(stream));
		}

		/// <summary>
		/// Returns the SHA-256 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-256 digest in hexadecimal format.</returns>
		public static string GetSha256Hash(string input)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the SHA-256 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-256 digest in hexadecimal format.</returns>
		public static string GetSha256Hash(Stream stream)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = sha256Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the SHA-256 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-256 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha256HashAsync(string input)
		{
			return await Task.Run(() => GetSha256Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the SHA-256 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-256 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha256HashAsync(Stream stream)
		{
			return await Task.Run(() => GetSha256Hash(stream));
		}

		/// <summary>
		/// Returns the SHA-384 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-384 digest in hexadecimal format.</returns>
		public static string GetSha384Hash(string input)
		{
			using (SHA384 sha384Hash = SHA384.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the SHA-384 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-384 digest in hexadecimal format.</returns>
		public static string GetSha384Hash(Stream stream)
		{
			using (SHA384 sha384Hash = SHA384.Create())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = sha384Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the SHA-384 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-384 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha384HashAsync(string input)
		{
			return await Task.Run(() => GetSha384Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the SHA-384 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-384 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha384HashAsync(Stream stream)
		{
			return await Task.Run(() => GetSha384Hash(stream));
		}

		/// <summary>
		/// Returns the SHA-512 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-512 digest in hexadecimal format.</returns>
		public static string GetSha512Hash(string input)
		{
			using (SHA512 sha512Hash = SHA512.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Returns the SHA-512 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-512 digest in hexadecimal format.</returns>
		public static string GetSha512Hash(Stream stream)
		{
			using (SHA512 sha512Hash = SHA512.Create())
			{
				// Convert the input stream to a byte array and compute the hash.
				byte[] data = sha512Hash.ComputeHash(stream);

				// Create a new Stringbuilder to collect the bytes
				// and create a string. 
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString().ToUpperInvariant();
			}
		}

		/// <summary>
		/// Asynchronously returns the SHA-512 digest for the informed string.
		/// </summary>
		/// <param name="input"> Input string.</param>
		/// <returns> SHA-512 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha512HashAsync(string input)
		{
			return await Task.Run(() => GetSha512Hash(input));
		}

		/// <summary>
		/// Asynchronously returns the SHA-512 digest for the informed stream.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <returns> SHA-512 digest in hexadecimal format.</returns>
		public static async Task<string> GetSha512HashAsync(Stream stream)
		{
			return await Task.Run(() => GetSha512Hash(stream));
		}
	}
}