﻿// <copyright file="Base64.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Base64 helper.</summary>

namespace SmartIT.Library.Utilities.Cryptography
{
	using Helpers;
	using System.IO;

	/// <summary>
	/// Base64 helper.
	/// </summary>
	public static class Base64
	{
		/// <summary>
		/// Encodes a Plain Text using the Base64 algorithm.
		/// </summary>
		/// <param name="input">Plain text.</param>
		/// <returns>Base64 encoded string.</returns>
		public static string Base64Encode(string input)
		{
			return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));
		}

		/// <summary>
		/// Encodes a Byte array using the Base64 algorithm.
		/// </summary>
		/// <param name="input">Byte array.</param>
		/// <returns>Base64 encoded string.</returns>
		public static string Base64Encode(byte[] input)
		{
			return System.Convert.ToBase64String(input);
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
		/// <param name="input">Base64 encoded string.</param>
		/// <returns>Plain string.</returns>
		public static string Base64Decode(string input)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(input);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}
}