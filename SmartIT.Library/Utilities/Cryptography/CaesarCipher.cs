// <copyright file="CaesarCipher.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>CaesarCipher implementation with variable shift (1-26).</summary>

namespace SmartIT.Library.Utilities.Cryptography
{
	using SmartIT.Library.Helpers;
	using System;
	using System.IO;
	using System.Text;

	/// <summary>
	/// CaesarCipher implementation with variable shift (1-26).
	/// </summary>
	public static class CaesarCipher
	{
		/// <summary>
		/// Encodes a plain text using the Caesar algorithm.
		/// </summary>
		/// <param name="input">Plain text.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The encoded string.</returns>
		public static string Encipher(string input, int shift)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentNullException(nameof(input));
			}

			if (shift < 1 || shift > 26)
			{
				throw new ArgumentOutOfRangeException(nameof(shift));
			}

			StringBuilder output = new StringBuilder();

			foreach (char ch in input)
			{
				output.Append(CaesarShifter(ch, shift));
			}

			return output.ToString();
		}

		/// <summary>
		/// Encodes a byte array using the Caesar algorithm.
		/// </summary>
		/// <param name="input">Input byte array.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The encoded string.</returns>
		public static string Encipher(byte[] input, int shift)
		{
			return Encipher(Encoding.UTF8.GetString(input, 0, input.Length), shift);
		}

		/// <summary>
		/// Encodes a Stream using the Caesar algorithm.
		/// </summary>
		/// <param name="input">Input stream.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The encoded string.</returns>
		public static string Encipher(Stream input, int shift)
		{
			return Encipher(StreamHelper.StreamToByteArray(input), shift);
		}

		/// <summary>
		/// Decodes an encoded string using the Caesar algorithm.
		/// </summary>
		/// <param name="input">Plain text.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The decoded string.</returns>
		public static string Decipher(string input, int shift)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentNullException(nameof(input));
			}

			if (shift < 1 || shift > 26)
			{
				throw new ArgumentOutOfRangeException(nameof(shift));
			}

			return Encipher(input, 26 - shift);
		}

		/// <summary>
		/// Decodes an encoded array of bytes using the Caesar algorithm.
		/// </summary>
		/// <param name="input">A byte array.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The decoded string.</returns>
		public static string Decipher(byte[] input, int shift)
		{
			return Decipher(Encoding.UTF8.GetString(input, 0, input.Length), shift);
		}

		/// <summary>
		/// Decodes an encoded Stream using the Caesar algorithm.
		/// </summary>
		/// <param name="input">A <see cref="Stream"/> object.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The decoded string.</returns>
		public static string Decipher(Stream input, int shift)
		{
			return Decipher(StreamHelper.StreamToByteArray(input), shift);
		}

		/// <summary>
		/// Shifts a single char for a given shift value using the Caesar algorithm.
		/// </summary>
		/// <param name="ch">The input char.</param>
		/// <param name="shift">The shift value.</param>
		/// <returns>The shifted char.</returns>
		public static char CaesarShifter(char ch, int shift)
		{
			if (!char.IsLetter(ch))
			{
				return ch;
			}

			char d = char.IsUpper(ch) ? 'A' : 'a';
			return (char)((((ch + shift) - d) % 26) + d);
		}
	}
}