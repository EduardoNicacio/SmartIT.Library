// <copyright file="Validation.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary> Class that provides methods for diverse validation routines.</summary>

namespace SmartIT.Library.Utility
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class that provides methods for diverse validation routines.
    /// </summary> 
    public static class Validation
    {
        /// <summary>
        /// Validate numbers.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsNumber(string value)
        {
            bool isValid = true;
            try
            {
                isValid = int.Parse(value) >= 0 || int.Parse(value) <= 0;
            }
            catch (ArgumentNullException)
            {
                isValid = false;
            }
            catch (FormatException)
            {
                isValid = false;
            }
            catch (OverflowException)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Validate DateTime objects.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsDate(string value)
        {
            bool isValid = true;
            try
            {
                isValid = DateTime.Parse(value) >= DateTime.MinValue || DateTime.Parse(value) <= DateTime.MaxValue;
            }
            catch (ArgumentNullException)
            {
                isValid = false;
            }
            catch (FormatException)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Validates decimal numbers.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsDecimal(string value)
        {
            bool isValid = true;
            try
            {
                isValid = decimal.Parse(value) >= 0 || decimal.Parse(value) <= 0;
            }
            catch (ArgumentNullException)
            {
                isValid = false;
            }
            catch (FormatException)
            {
                isValid = false;
            }
            catch (OverflowException)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Validates email addresses.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsEmail(string value)
        {
            string patternStrictEmail = @"^(([^<>()[\]\\.,;:\s@\""]+"
                + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                + @"[a-zA-Z]{2,}))$";
            Regex re = new Regex(patternStrictEmail);
            return re.IsMatch(value);
        }

        /// <summary>
        /// Validates the Brazilian CPF format.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsCpf(string value)
        {
            int[] mul1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mul2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string temp;
            string digit;
            string cpf;
            int sum;
            int rest;

            cpf = value.Trim().Replace(".", string.Empty).Replace("-", string.Empty);

            // MUST HAVE 11 chars! ALWAYS!
            if (cpf.Length != 11)
            {
                return false;
            }

            temp = cpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(temp[i].ToString()) * mul1[i];
            }

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = rest.ToString();

            temp = temp + digit;

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(temp[i].ToString()) * mul2[i];
            }

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();

            return cpf.EndsWith(digit);
        }

        /// <summary>
        /// Validates the Brazilian CNPJ format.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsCnpj(string value)
        {
            int[] mul1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mul2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string digit;
            string temp;
            string cnpj;
            int sum;
            int rest;

            cnpj = value.Trim().Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);

            // MUST HAVE 14 chars! ALWAYS!
            if (cnpj.Length != 14)
            {
                return false;
            }

            temp = cnpj.Substring(0, 12);

            sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(temp[i].ToString()) * mul1[i];
            }

            rest = (sum % 11);

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = rest.ToString();

            temp = temp + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(temp[i].ToString()) * mul2[i];
            }

            rest = (sum % 11);

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit);
        }

        /// <summary>
        /// Validates a Brazilian ZIP code.
        /// </summary>
        /// <param name="value"> Value to validate.</param>
        /// <returns> True if valid; false instead.</returns>
        public static bool IsCep(string value)
        {
            Regex regEx = new Regex("^[0-9]{5}-[0-9]{3}$");
            return regEx.IsMatch(value);
        }
    }
}