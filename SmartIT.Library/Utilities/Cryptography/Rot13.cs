// <copyright file="Rot13.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Rot13 helper.</summary>

namespace SmartIT.Library.Utilities.Cryptography
{
	using System.IO;

	/// <summary>
	/// Rot13 helper.
	/// </summary>
	public static class Rot13
	{
		/// <summary>
		/// Encodes a plain text using the ROT13 algorithm.
		/// </summary>
		/// <param name="input">Input plain text.</param>
		/// <returns>ROT13 encoded string.</returns>
		public static string Rot13Encode(string input)
		{
			return CaesarCipher.Encipher(input, 13);
		}

		/// <summary>
		/// Encodes a stream using the ROT13 algorithm.
		/// </summary>
		/// <param name="input">Input stream.</param>
		/// <returns>ROT13 encoded string.</returns>
		public static string Rot13Encode(Stream input)
		{
			return CaesarCipher.Encipher(input, 13);
		}

		/// <summary>
		/// Encodes a byte array using the ROT13 algorithm.
		/// </summary>
		/// <param name="input">Input byte array.</param>
		/// <returns>ROT13 encoded string.</returns>
		public static string Rot13Encode(byte[] input)
		{
			return CaesarCipher.Encipher(input, 13);
		}

		/// <summary>
		/// Decodes a plain text using the ROT13 algorithm.
		/// </summary>
		/// <param name="input">Input plain text.</param>
		/// <returns>Decoded string.</returns>
		public static string Rot13Decode(string input)
		{
			return Rot13Encode(input);
		}

		/// <summary>
		/// Decodes a Stream using the ROT13 algorithm.
		/// </summary>
		/// <param name="input">Input stream.</param>
		/// <returns>Decoded string.</returns>
		public static string Rot13Decode(Stream input)
		{
			return Rot13Encode(input);
		}

		/// <summary>
		/// Decodes a byte array using the ROT13 algorithm.
		/// </summary>
		/// <param name="input">Input byte array.</param>
		/// <returns>Decoded string.</returns>
		public static string Rot13Decode(byte[] input)
		{
			return Rot13Encode(input);
		}
	}
}