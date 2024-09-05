// <copyright file="StringExtensions.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/03/2015</date>
// <summary>String Extensions class.</summary>

namespace SmartIT.Library.Extensions
{
	using System;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	/// <summary>
	/// String extensions class.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Adjusts the source string to LowerCase.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string AdjustL(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				string.Empty :
				source.Trim().ToLowerInvariant();
		}

		/// <summary>
		/// Asynchronously ddjusts the source string to LowerCase.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> AdjustLAsync(this string source)
		{
			return await Task.Run(() => AdjustL(source));
		}

		/// <summary>
		/// Adjusts the source string to UpperCase.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string AdjustU(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				string.Empty :
				source.Trim().ToUpperInvariant();
		}

		/// <summary>
		/// Asynchronously adjusts the source string to UpperCase.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> AdjustUAsync(this string source)
		{
			return await Task.Run(() => AdjustU(source));
		}

		/// <summary>
		/// Returns a numbers-only string.
		/// </summary>
		/// <param name="source">Original string.</param>
		/// <returns>Return string.</returns>
		public static string OnlyNumbers(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				string.Empty :
				new Regex(@"[^\d]", RegexOptions.None, TimeSpan.FromMilliseconds(100)).Replace(source.Trim(), string.Empty);
		}

		/// <summary>
		/// Asynchronously returns a numbers-only string.
		/// </summary>
		/// <param name="source">Original string.</param>
		/// <returns>Return string.</returns>
		public static async Task<string> OnlyNumbersAsync(this string source)
		{
			return await Task.Run(() => OnlyNumbers(source));
		}

		/// <summary>
		/// Returns a formated string.
		/// </summary>
		/// <param name="source">Original string.</param>
		/// <returns>Return string.</returns>
		public static string RemoveSymbols(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				string.Empty :
				Regex.Replace(source, @"[^0-9a-zA-Z]+", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(100));
		}

		/// <summary>
		/// Asynchronously returns a formated string.
		/// </summary>
		/// <param name="source">Original string.</param>
		/// <returns>Return string.</returns>
		public static async Task<string> RemoveSymbolsAsync(this string source)
		{
			return await Task.Run(() => RemoveSymbols(source));
		}

		/// <summary>
		/// Converts the source string to the Brazilian CPF format with special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string ToCpf(this string source)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				return new string('0', 11);
			}
			if (source.Length > 11)
			{
				return source;
			}
			return source.PadLeft(11, '0').Substring(0, 3) + "." + source.Substring(3, 3) + "." + source.Substring(6, 3) + "-" + source.Substring(9, 2);
		}

		/// <summary>
		/// Asynchronously converts the source string to the Brazilian CPF format with special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> ToCpfAsync(this string source)
		{
			return await Task.Run(() => ToCpf(source));
		}

		/// <summary>
		/// Converts the source string to the Brazilian CNPJ format with special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string ToCnpj(this string source)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				return new string('0', 14);
			}
			if (source.Length > 14)
			{
				return source;
			}
			return source.PadLeft(14, '0').Substring(0, 2) + "." + source.Substring(2, 3) + "." + source.Substring(5, 3) + "/" + source.Substring(8, 4) + "-" + source.Substring(12, 2);
		}

		/// <summary>
		/// Asynchronously converts the source string to the Brazilian CNPJ format with special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> ToCnpjAsync(this string source)
		{
			return await Task.Run(() => ToCnpj(source));
		}

		/// <summary>
		/// Formats the source string to the Brazilian CPF format without special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string ToCleanCpf(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				string.Empty :
				source.Replace(".", "").Replace("-", "").Replace("/", "").PadLeft(11, '0');
		}

		/// <summary>
		/// Asynchronously formats the source string to the Brazilian CPF format without special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> ToCleanCpfAsync(this string source)
		{
			return await Task.Run(() => ToCleanCpf(source));
		}

		/// <summary>
		/// Formats the source string to the Brazilian CNPJ format without special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string ToCleanCnpj(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				string.Empty :
				source.Replace(".", "").Replace("-", "").Replace("/", "").PadLeft(14, '0');
		}

		/// <summary>
		/// Asynchronously formats the source string to the Brazilian CNPJ format without special chars.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> ToCleanCnpjAsync(this string source)
		{
			return await Task.Run(() => ToCleanCnpj(source));
		}

		/// <summary>
		/// Formats the source string to the Brazilian ZIP code pattern with special char.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string ToZipCode(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				"00000-000" :
				string.Format("{0}-{1}", source.Substring(0, 5), source.Substring(5, 3));
		}

		/// <summary>
		/// Asynchronously formats the source string to the Brazilian ZIP code pattern with special char.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> ToZipCodeAsync(this string source)
		{
			return await Task.Run(() => ToZipCode(source));
		}

		/// <summary>
		/// Formats the source string to the Brazilian ZIP code pattern without special char.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static string ToCleanZipCode(this string source)
		{
			return string.IsNullOrWhiteSpace(source) ?
				"00000000" :
				source.Replace(".", string.Empty).Replace("-", string.Empty);
		}

		/// <summary>
		/// Asynchronously formats the source string to the Brazilian ZIP code pattern without special char.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> ToCleanZipCodeAsync(this string source)
		{
			return await Task.Run(() => ToCleanZipCode(source));
		}

		/// <summary>
		/// Truncates the source string to the indicated length.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <param name="length">Number of chars to extract.</param>
		/// <returns>Result string.</returns>
		public static string Truncate(this string source, int length)
		{
			if (!string.IsNullOrWhiteSpace(source))
			{
				return source.Length > length ? source.Substring(0, length) : source;
			}
			return string.Empty;
		}

		/// <summary>
		/// Asynchronously truncates the source string to the indicated length.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <param name="length">Number of chars to extract.</param>
		/// <returns>Result string.</returns>
		public static async Task<string> TruncateAsync(this string source, int length)
		{
			return await Task.Run(() => Truncate(source, length));
		}
	}
}