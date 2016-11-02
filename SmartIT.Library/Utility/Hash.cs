// <copyright file="Hash.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>16/07/2014</date>
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
        /// Retorna o Hash CRC16 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns>Hash CRC16 em formato Hexadecimal.</returns>
        public static string GetCrc16Hash(string input)
        {
            return Crc16.ComputeHash(Encoding.UTF8.GetBytes(input)).ToString("x2");
        }

        /// <summary>
        /// Retorna o Hash CRC16 do stream informado.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <returns>Hash CRC16 em formato Hexadecimal.</returns>
        public static string GetCrc16Hash(Stream input)
        {
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(input))
            {
                fileData = binaryReader.ReadBytes((int)input.Length);
            }
            return Crc16.ComputeHash(fileData).ToString("x2");
        }

        /// <summary>
        /// Retorna o Hash CRC32 da string informada.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Hash CRC32 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash MD5 da string informada.
        /// </summary>
        /// <param name="input"> String de entrada.</param>
        /// <returns> Hash MD5 em formato Hexadecimal.</returns>
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
        /// Retorna o hash MD5 do FileStream informado.
        /// </summary>
        /// <param name="input">Input File Stream.</param>
        /// <returns> Hash MD5 em formato Hexadecimal.</returns>
        public static string GetMD5Hash(Stream stream)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(input);

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
        /// Retorna o Hash RIPEMD160 da string informada.
        /// </summary>
        /// <param name="input"> String de entrada.</param>
        /// <returns> Hash RIPEMD160 em formato Hexadecimal.</returns>
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
        /// Retorna o hash RIPEMD160 do FileStream informado.
        /// </summary>
        /// <param name="input">Input File Stream.</param>
        /// <returns> Hash RIPEMD160 em formato Hexadecimal.</returns>
        public static string GetRipeMd160Hash(Stream input)
        {
            using (RIPEMD160 ripeMd160Hash = RIPEMD160.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = ripeMd160Hash.ComputeHash(input);

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
        /// Retorna o Hash SHA-1 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-1 em formato Hexadecimal.</returns>
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
        /// Retorna o hash SHA-1 do FileStream informado.
        /// </summary>
        /// <param name="input">Input File Stream.</param>
        /// <returns> Hash SHA-1 em formato Hexadecimal.</returns>
        public static string GetSha1Hash(Stream input)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = sha1Hash.ComputeHash(input);

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
        /// Retorna o Hash RIPEMD160 da string informada.
        /// </summary>
        /// <param name="input"> String de entrada.</param>
        /// <returns> Hash RIPEMD160 em formato Hexadecimal.</returns>
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
        /// Retorna o hash RIPEMD160 do FileStream informado.
        /// </summary>
        /// <param name="stream">Input File Stream.</param>
        /// <returns> Hash RIPEMD160 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-1 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-1 em formato Hexadecimal.</returns>
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
        /// Retorna o hash SHA-1 do FileStream informado.
        /// </summary>
        /// <param name="stream">Input File Stream.</param>
        /// <returns> Hash SHA-1 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-256 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-256 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-256 do FileStream informado.
        /// </summary>
        /// <param name="input">Input File Stream.</param>
        /// <returns> Hash SHA-256 em formato Hexadecimal.</returns>
        public static string GetSHA256Hash(Stream stream)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = sha256Hash.ComputeHash(input);

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
        /// Retorna o Hash SHA-384 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-384 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-384 do FileStream informado.
        /// </summary>
        /// <param name="input">Input File Stream.</param>
        /// <returns> Hash SHA-384 em formato Hexadecimal.</returns>
        public static string GetSha384Hash(Stream input)
        {
            using (SHA384 sha384Hash = SHA384.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = sha384Hash.ComputeHash(input);

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
        /// Retorna o Hash SHA-384 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-384 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-384 do FileStream informado.
        /// </summary>
        /// <param name="stream">Input File Stream.</param>
        /// <returns> Hash SHA-384 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-512 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-512 em formato Hexadecimal.</returns>
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
        /// Retorna o Hash SHA-512 do FileStream informado.
        /// </summary>
        /// <param name="input">Input File Stream.</param>
        /// <returns> Hash SHA-512 em formato Hexadecimal.</returns>
        public static string GetSHA512Hash(Stream stream)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // Convert the input stream to a byte array and compute the hash.
                byte[] data = sha512Hash.ComputeHash(input);

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