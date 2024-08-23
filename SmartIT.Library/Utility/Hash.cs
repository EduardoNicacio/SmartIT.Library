// <copyright file="Hash.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>17/04/2017</date>
// <summary>Utility class to easily generate CRC16, CRC32, CRC64, MD5, SHA1, SHA256, SHA384 and SHA512 hashes.</summary>

namespace SmartIT.Library.Utility
{
    using SmartIT.Library.Utility.Criptography;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Utility class to easily generate CRC16, CRC32, CRC64, MD5, SHA1, SHA256, SHA384 and SHA512 hashes.
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Returns the CRC16 digest for the informed string.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns>CRC16 digest in hexadecimal format.</returns>
        public static string GetCrc16Hash(string input)
        {
            return Crc16.ComputeHash(Encoding.UTF8.GetBytes(input)).ToString("x2");
        }

        /// <summary>
        /// Returns the CRC16 digest for the informed stream.
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
            return Crc16.ComputeHash(fileData).ToString("x2");
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
        /// <param name="input">Input stream.</param>
        /// <returns>CRC32 digest in hexadecimal format.</returns>
        public static string GetCrc32Hash(Stream input)
        {
            using (Crc32 crc32Hash = new Crc32())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = crc32Hash.ComputeHash(input);

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
        /// Returns the ISO-3309 compliant CRC64 digest for the informed string.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>ISO-3309 compliant CRC64 digest in hexadecimal format.</returns>
        public static string GetCrc64Hash(string input)
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
        /// <param name="input">Input stream.</param>
        /// <returns>ISO-3309 compliant CRC64 digest in hexadecimal format.</returns>
        public static string GetCrc64Hash(Stream input)
        {
            using (Crc64Iso crc64Hash = new Crc64Iso())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = crc64Hash.ComputeHash(input);

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
        /// Returns the RIPEMD160 digest for the informed string.
        /// </summary>
        /// <param name="input"> Input string.</param>
        /// <returns> RIPEMD160 digest in hexadecimal format.</returns>
        public static string GetRipeMd160Hash(string input)
        {
            using (RIPEMD160 ripeMd160Hash = RIPEMD160.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = ripeMd160Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

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
        /// Returns the RIPEMD160 digest for the informed stream.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns> RIPEMD160 digest in hexadecimal format.</returns>
        public static string GetRipeMd160Hash(Stream stream)
        {
            using (RIPEMD160 ripeMd160Hash = RIPEMD160.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = ripeMd160Hash.ComputeHash(stream);

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
    }
}