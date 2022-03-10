// <copyright file="CaesarCipher.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>CaesarCipher implementation with variable shift (1-26).</summary>

namespace SmartIT.Library.Utility.Criptography
{
    using SmartIT.Library.Helper;
    using System.IO;

    /// <summary>
    /// CaesarCipher implementation with variable shift (1-26).
    /// </summary>
    public static class CaesarCipher
    {
        /// <summary>
        /// Encodes/Decodes a Stream using the Caesar algorithm.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <param name="shift">Shift value.</param>
        /// <returns>The encoded string.</returns>
        public static string Caesar(Stream input, int shift)
        {
            return Caesar(StreamHelper.StreamToByteArray(input), shift);
        }

        /// <summary>
        /// Encodes/Decodes a byte array using the Caesar algorithm.
        /// </summary>
        /// <param name="input">Input byte array.</param>
        /// <param name="shift">Shift value.</param>
        /// <returns>The encoded string.</returns>
        public static string Caesar(byte[] input, int shift)
        {
            return Caesar(System.Text.Encoding.UTF8.GetString(input, 0, input.Length), shift);
        }

        /// <summary>
        /// Encodes/Decodes a plain text using the Caesar algorithm.
        /// </summary>
        /// <param name="input">Plain text.</param>
        /// <param name="shift">Shift value.</param>
        /// <returns>The encoded string.</returns>
        public static string Caesar(string input, int shift)
        {
            char[] buffer = input.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                letter = (char)(letter + shift);
                if (letter > 'z')
                {
                    letter = (char)(letter - 26);
                }
                else if (letter < 'a')
                {
                    letter = (char)(letter + 26);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
    }
}