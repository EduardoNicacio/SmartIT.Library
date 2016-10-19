// <copyright file="Base64.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Base64 helper.</summary>

namespace SmartIT.Library.Utility.Criptography
{
    using System.IO;
    using SmartIT.Library.Helper;

    /// <summary>
    /// Base64 helper.
    /// </summary>
    public static class Base64
    {
        /// <summary>
        /// Encodes a Plain Text using the Base64 algorithm.
        /// </summary>
        /// <param name="plainText">Plain text.</param>
        /// <returns>Base64 encoded string.</returns>
        public static string Base64Encode(string plainText)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encodes a Byte array using the Base64 algorithm.
        /// </summary>
        /// <param name="byteArray">Byte array.</param>
        /// <returns>Base64 encoded string.</returns>
        public static string Base64Encode(byte[] byteArray)
        {
            return System.Convert.ToBase64String(byteArray);
        }

        /// <summary>
        /// Encodes a Stream using the Base64 algorithm.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base64 encoded string.</returns>
        public static string Base64Encode(Stream stream)
        {
            return System.Convert.ToBase64String(StreamHelper.StreamToByteArray(stream));
        }

        /// <summary>
        /// Decodes a plain text using the Base64 algorithm.
        /// </summary>
        /// <param name="base64EncodedData">Base64 encoded string.</param>
        /// <returns>Plain string.</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}