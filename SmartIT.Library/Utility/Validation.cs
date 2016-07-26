// <copyright file="Validation.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Prov� m�todos para valida��o de valores.</summary>

namespace SmartIT.Library.Utility
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Prov� m�todos para valida��o de valores.
    /// </summary> 
    public static class Validation
    {
        /// <summary>
        /// Valida n�mero.
        /// </summary>
        /// <param name="value">Valor em string para validar.</param>
        /// <returns> True se for um n�mero v�lido, False se inv�lido.</returns>
        public static bool IsNumber(string value)
        {
            bool isValid = true;

            // Tenta chamar o parse do tipo
            // Retorna true se o n�mero � um inteiro v�lido
            // Ou false, caso contr�rio
            try
            {
                int.Parse(value);
            }
            catch (ArgumentNullException)
            {
                // If throw an ArgumentNullException, isValid is false
                isValid = false;
            }
            catch (FormatException)
            {
                // If throw an FormatException, isValid is false
                isValid = false;
            }
            catch (OverflowException)
            {
                // If throw an OverflowException, isValid is false
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Valida data.
        /// </summary>
        /// <param name="value">Valor em string para validar.</param>
        /// <returns> True se for uma data v�lida, False se inv�lida.</returns>
        public static bool IsDate(string value)
        {
            bool isValid = true;

            // Tenta chamar o parse do tipo
            // Retorna true se a Data � um DateTime v�lido
            // Ou false, caso contr�rio
            try
            {
                DateTime.Parse(value);
            }
            catch (ArgumentNullException)
            {
                // If throw an ArgumentNullException, isValid is false
                isValid = false;
            }
            catch (FormatException)
            {
                // If throw an FormatException, isValid is false
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Valida um n�mero decimal.
        /// </summary>
        /// <param name="value">Valor em string para validar.</param>
        /// <returns> True se for um n�mero decimal v�lido, False se inv�lido.</returns>
        public static bool IsDecimal(string value)
        {
            bool isValid = true;

            // Tenta chamar o parse do tipo
            // Retorna true se o n�mero � um decimal v�lido
            // Ou false, caso contr�rio
            try
            {
                decimal.Parse(value);
            }
            catch (ArgumentNullException)
            {
                // If throw an ArgumentNullException, isValid is false
                isValid = false;
            }
            catch (FormatException)
            {
                // If throw an FormatException, isValid is false
                isValid = false;
            }
            catch (OverflowException)
            {
                // If throw an OverflowException, isValid is false
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Valida e-mail.
        /// </summary>
        /// <param name="value">E-mail para validar.</param>
        /// <returns> True se for um e-mail v�lido, False se inv�lido.</returns>
        public static bool IsEmail(string value)
        {
            // Cria a express�o regular para valida��o do e-mail
            string patternStrictEmail = @"^(([^<>()[\]\\.,;:\s@\""]+"
                + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                + @"[a-zA-Z]{2,}))$";
            Regex re = new Regex(patternStrictEmail);

            // Retorna true se for um e-mail v�lido
            // Ou false caso contr�rio
            return re.IsMatch(value);
        }

        /// <summary>
        /// Valida CPF.
        /// </summary>
        /// <param name="value">CPF para validar.</param>
        /// <returns> True se for um CPF v�lido, False se inv�lido.</returns>
        public static bool IsCPF(string value)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            string cpf;
            int soma;
            int resto;

            cpf = value;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", string.Empty).Replace("-", string.Empty);

            // Obrigatoriamente deve ter 11 caracteres
            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Valida CNPJ.
        /// </summary>
        /// <param name="value">CNPJ para validar.</param>
        /// <returns> True se for um CNPJ v�lido, False se inv�lido.</returns>
        public static bool IsCNPJ(string value)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            string cnpj = value;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);

            // Obrigatoriamente deve ter 14 caracteres
            if (cnpj.Length != 14)
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Valida a string como sendo um CEP v�lido.
        /// </summary>
        /// <param name="cep">String.</param>
        /// <returns> True ou False.</returns>
        public static bool IsCEP(string cep)
        {
            // Valida se o CEP passado � v�lido
            // No formato 99999-999
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9]{5}-[0-9]{3}$");
            return r.IsMatch(cep);
        }
    }
}