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
    }
}