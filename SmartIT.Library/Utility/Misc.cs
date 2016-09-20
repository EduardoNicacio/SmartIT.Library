// <copyright file="Misc.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Provê métodos para diversas funcionalidades.</summary>

namespace SmartIT.Library.Utility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Provê métodos para diversas funcionalidades.
    /// </summary>
    public static class Misc
    {
        /// <summary>
        /// Limita texto para uma quantidade determinada de caracteres.
        /// </summary>
        /// <param name="texto">Texto a ser limitado.</param>
        /// <param name="limit">Limite de caracteres pra ser retornado.</param>
        /// <returns> Texto limitado a quantidade de caracteres definida.</returns>
        public static string LimitString(object texto, int limit)
        {
            string str = texto.ToString();

            if (str.Length >= limit)
            {
                str = str.Substring(0, limit) + "...";
            }

            return str;
        }

        /// <summary>
        /// Recupera a informação contida no atributo Description de um elemento do enum.
        /// </summary>
        /// <param name="value">Enum.</param>
        /// <returns> Texto contido no Description do Enum.</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// Recupera o valor do atributo StringValue associado a um valor Enum.
        /// </summary>
        /// <param name="value">Valor de um Enum.</param>
        /// <returns> string associado ao valor do Enum.</returns>
        public static string GetEnumStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

        /// <summary>
        /// Recupera o valor de um Enum através do valor do atributo StringValue.
        /// </summary>
        /// <param name="strValue">Valor atribuido ao StringValue da opção do Enum.</param>
        /// <param name="enumType">Tipo do Enum.</param>
        /// <returns> Objeto que mapeia um Enum.</returns>
        public static object GetEnumValue(string strValue, Type enumType)
        {
            foreach (FieldInfo fi in enumType.GetFields())
            {
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

                if (attrs.Length > 0 && attrs[0].Value == strValue)
                {
                    return Enum.Parse(enumType, fi.Name);
                }
            }

            return null;
        }

        /// <summary>
        /// Converte o um valor boleano para flag "Ativo" ou "Inativo" para utilização em grids.
        /// </summary>
        /// <param name="value">Objeto representando um valor boleano.</param>
        /// <returns> "Ativo" se o valor for True e "Inativo" se o valor for False.</returns>
        public static string GetBooleanToStatus(object value)
        {
            bool v = Convert.ToBoolean(value);
            return v ? "Active" : "Inactive";
        }

        /// <summary>
        /// Remove acentos de uma String.
        /// </summary>
        /// <param name="value"> Texto a ter os acentos removidos.</param>
        /// <returns> Texto formatado, sem os acentos.</returns>
        public static string RemoveAccents(string value)
        {
            // Retorna a mesma string identificando cada caracter (til, cedilha, acentos, etc) da string setada.            
            // FormD -> Indica que uma seqüência de caracteres Unicode é normalizada usando decomposição completo canônica.
            string normalizedString = value.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];

                // Compara cada caracter Unicode 
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Retorna a string formatada
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Retorna a data no formato utilizado pelo Moodle.
        /// </summary>
        /// <param name="dateTime">Data e hora informado.</param>
        /// <returns>Data e hora no formato Unix.</returns>
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        /// <summary>
        /// Returns the class name and its properties.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>The properties of an object.</returns>
        public static string GetClassProperties(object obj)
        {
            StringBuilder sResult = new StringBuilder(string.Empty);

            PropertyInfo[] propertyInfo = obj.GetType().GetProperties();

            sResult.Append(string.Format("Class={0}|", obj.GetType().Name));

            foreach (PropertyInfo pInfo in propertyInfo)
            {
                sResult.Append(string.Format("{0}={1}|", pInfo.Name, pInfo.GetValue(obj, null)));
            }

            return sResult.ToString();
        }

        /// <summary>
        /// Returns the age of a person by comparing its birth date with the current datetime.
        /// </summary>
        /// <param name="birthdate">Birth date.</param>
        /// <returns>The age, in years.</returns>
        public static int ToAge(this DateTime birthdate)
        {
            return ToAge(birthdate, DateTime.Today);
        }

        /// <summary>
        /// Returns the age of a person by comparing its birth date with somewhere in time.
        /// </summary>
        /// <param name="birthdate">Birth date.</param>
        /// <param name="referenceDate">Reference date.</param>
        /// <returns>The age, in years.</returns>
        public static int ToAge(this DateTime birthdate, DateTime referenceDate)
        {
            var today = referenceDate;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Classe para comparação genérica de dois objetos.
        /// </summary>
        /// <typeparam name="T"> Generic Type.</typeparam>
        public class GenericComparer<T> : IComparer<T>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GenericComparer" /> class.
            /// </summary>
            public GenericComparer()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="GenericComparer" /> class.
            /// </summary>
            /// <param name="sortExpression">Expressão de ordenação.</param>
            /// <param name="sortDirection">Direção de ordenação (ASC ou DESC).</param>
            public GenericComparer(string sortExpression, SortDirection sortDirection)
            {
                this.GenericSortExpression = sortExpression;
                this.GenericSortDirection = sortDirection;
            }

            /// <summary>
            /// Expression to compare.
            /// </summary>
            public string GenericSortExpression { get; set; }

            /// <summary>
            /// Direction in which to sort.
            /// </summary>
            public SortDirection GenericSortDirection { get; set; }

            /// <summary>
            /// Compara dois objetos com base no atributo e direção especificados.
            /// </summary>
            /// <param name="x">Generic Type.</param>
            /// <param name="y">Generic Type.</param> 
            /// <returns> Retorna -1 se x menor que y, 0 se forem iguais, 1 se y maior que x.</returns>
            public int Compare(T x, T y)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(GenericSortExpression);
                IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);
                IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);

                if (GenericSortDirection == SortDirection.Ascending)
                {
                    return obj1.CompareTo(obj2);
                }
                else
                {
                    return obj2.CompareTo(obj1);
                }
            }
        }
    }
}