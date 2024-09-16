// <copyright file="Rot47.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Rot47 helper.</summary>

namespace SmartIT.Library.Utilities.Cryptography
{
	using Helpers;
	using System;
	using System.IO;
	using System.Text;

	/// <summary>
	/// Rot47 helper.
	/// </summary>
	public static class Rot47
	{
		/// <summary>
		/// Encodes a Stream using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Stream input.</param>
		/// <returns>Encoded string.</returns>
		public static string Rot47Encode(Stream input)
		{
			return Rot47Encode(StreamHelper.StreamToByteArray(input));
		}

		/// <summary>
		/// Encodes a Stream using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Input Byte array.</param>
		/// <returns>Encoded string.</returns>
		public static string Rot47Encode(byte[] input)
		{
			return Rot47Encode(Encoding.UTF8.GetString(input, 0, input.Length));
		}

		/// <summary>
		/// Encodes a plain text using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>Encoded string.</returns>
		public static string Rot47Encode(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentNullException(nameof(input));
			}

			StringBuilder RetStr = new StringBuilder(string.Empty);

			foreach (char c in input)
			{
				RetStr.Append(Rot47Shifter(c).ToString());
			}

			return RetStr.ToString();
		}

		/// <summary>
		/// Decodes a Stream using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>The plain string.</returns>
		public static string Rot47Decode(Stream input)
		{
			return Rot47Encode(input);
		}

		/// <summary>
		/// Decodes a byte array using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Input byte array.</param>
		/// <returns>The plain string.</returns>
		public static string Rot47Decode(byte[] input)
		{
			return Rot47Encode(input);
		}

		/// <summary>
		/// Decodes a plain text using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Input plain text.</param>
		/// <returns>The plain string.</returns>
		public static string Rot47Decode(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentNullException(nameof(input));
			}

			return Rot47Encode(input);
		}

		/// <summary>
		/// Shifts the specified char using the ROT47 algorithm.
		/// </summary>
		/// <param name="input">Input char.</param>
		/// <returns>Shifted char.</returns>
		private static char Rot47Shifter(char input)
		{
			if (input.Equals(' ')) return ' ';
			int ascii = input;
			ascii += 47;
			if (ascii > 126) ascii -= 94;
			if (ascii < 33) ascii += 94;
			return (char)ascii;
		}
	}
}