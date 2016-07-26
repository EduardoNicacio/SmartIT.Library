// <copyright file="Hash.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>16/07/2014</date>
// <summary>Classe utilitária para geração de Hashes MD5, SHA256 e SHA512.</summary>

namespace SmartIT.Library.Utility
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Classe utilitária para geração de Hashes MD5, SHA256 e SHA512.
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Retorna o Hash MD5 da string informada.
        /// </summary>
        /// <param name="input"> String de entrada.</param>
        /// <returns> Hash MD5 em formato Hexadecimal.</returns>
        public static string GetMD5Hash(string input)
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
        /// <param name="stream">Input File Stream.</param>
        /// <returns> Hash MD5 em formato Hexadecimal.</returns>
        public static string GetMD5Hash(Stream stream)
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
        /// Retorna o Hash SHA-256 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-256 em formato Hexadecimal.</returns>
        public static string GetSHA256Hash(string input)
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
        /// <param name="stream">Input File Stream.</param>
        /// <returns> Hash SHA-256 em formato Hexadecimal.</returns>
        public static string GetSHA256Hash(Stream stream)
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
        /// Retorna o Hash SHA-512 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-512 em formato Hexadecimal.</returns>
        public static string GetSHA512Hash(string input)
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
        /// <param name="stream">Input File Stream.</param>
        /// <returns> Hash SHA-512 em formato Hexadecimal.</returns>
        public static string GetSHA512Hash(Stream stream)
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