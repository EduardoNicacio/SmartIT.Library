// <copyright file="StringExtensions.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/03/2015</date>
// <summary>Authentication helper.</summary>

namespace SmartIT.Library.Extensions
{
    using System.Text.RegularExpressions;

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
            return source == null ? string.Empty : source.Trim().ToLowerInvariant();
        }

        /// <summary>
        /// Adjusts the source string to UpperCase.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string AdjustU(this string source)
        {
            return source == null ? string.Empty : source.Trim().ToUpperInvariant();
        }

        /// <summary>
        /// Returns a number-only string.
        /// </summary>
        /// <param name="str">Original string.</param>
        /// <returns>Return string.</returns>
        public static string OnlyNumbers(this string str)
        {
            str = str.Trim();

            Regex regex = new Regex(@"[^\d]");

            str = regex.Replace(str, string.Empty);

            return str;
        }

        /// <summary>
        /// Returns a formated string.
        /// </summary>
        /// <param name="str">Original string.</param>
        /// <returns>Return string.</returns>
        public static string RemoveSymbols(this string str)
        {
            str = str.Trim();

            Regex regex = new Regex(@"^[a-zA-Z0-9]+$");

            str = regex.Replace(str, string.Empty);

            return str;
        }

        /// <summary>
        /// Converts the source string to the Brazilian CPF format with special chars.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string ToCpf(this string source)
        {
            if (source == null)
            {
                return new string('0', 11);
            }
            if (source.Length >= 11)
            {
                return source;
            }
            source = source.PadLeft(11, '0');
            return source.Substring(0, 3) + "." + source.Substring(3, 3) + "." + source.Substring(6, 3) + "-" + source.Substring(9, 2);
        }

        /// <summary>
        /// Converts the source string to the Brazilian CNPJ format with special chars.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string ToCnpj(this string source)
        {
            if (source == null)
            {
                return new string('0', 14);
            }
            if (source.Length >= 14)
            {
                return source;
            }
            source = source.PadLeft(14, '0');
            return source.Substring(0, 2) + "." + source.Substring(2, 3) + "." + source.Substring(5, 3) + "/" + source.Substring(8, 4) + "-" + source.Substring(12, 2);
        }

        /// <summary>
        /// Formats the source string to the Brazilian CPF format without special chars.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string ToCleanCpf(this string source)
        {
            return source.Replace(".", "").Replace("-", "").Replace("/", "").PadLeft(11, '0');
        }

        /// <summary>
        /// Formats the source string to the Brazilian CNPJ format without special chars.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string ToCleanCnpj(this string source)
        {
            return source.Replace(".", "").Replace("-", "").Replace("/", "").PadLeft(14, '0');
        }

        /// <summary>
        /// Formats the source string to the Brazilian ZIP code pattern with special char.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string ToZipCode(this string source)
        {
            if (source == null)
            {
                return "00000-000";
            }
            return string.Format("{0}-{1}", source.Substring(0, 5), source.Substring(6, 3));
        }

        /// <summary>
        /// Formats the source string to the Brazilian ZIP code pattern without special char.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Result string.</returns>
        public static string ToCleanZipCode(this string source)
        {
            if (source == null)
            {
                return "00000000";
            }
            return source.Replace(".", "").Replace("-", "");
        }

        /// <summary>
        /// Truncates the source string to the indicated length.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="length">Number of chars to extract.</param>
        /// <returns>Result string.</returns>
        public static string Truncate(this string source, int length)
        {
            return source == null ? string.Empty : source.Length > length ? source.Substring(0, length) : source;
        }
    }
}