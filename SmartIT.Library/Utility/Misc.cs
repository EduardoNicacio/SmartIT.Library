// <copyright file="Misc.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary> Provides many methods for unsorted features.</summary>

namespace SmartIT.Library.Utility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Provides many methods for unsorted features.
    /// </summary>
    public static class Misc
    {
        /// <summary>
        /// Limit a string to a certain number of chars.
        /// </summary>
        /// <param name="value"> Input string.</param>
        /// <param name="limit"> Limit of chars (greates than 0).</param>
        /// <returns> Result string.</returns>
        public static string LimitString(object value, int limit)
        {
            string str = value.ToString();

            if (str.Length >= limit)
            {
                str = str.Substring(0, limit) + "...";
            }

            return str;
        }

        /// <summary>
        /// Retrieves the Description property given an Enum object.
        /// </summary>
        /// <param name="value"> Enum object.</param>
        /// <returns> The value of the Description property.</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// Retrieves the StringValue property given an Enum object.
        /// </summary>
        /// <param name="value"> Enum object.</param>
        /// <returns> The value of the StringValue property.</returns>
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
        /// Retrieves the Enum value given its StringValue property.
        /// </summary>
        /// <param name="value"> StringValue value.</param>
        /// <param name="enumType"> Enum type.</param>
        /// <returns> Enum as object.</returns>
        public static object GetEnumValue(string value, Type enumType)
        {
            foreach (FieldInfo fi in enumType.GetFields())
            {
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

                if (attrs.Length > 0 && attrs[0].Value == value)
                {
                    return Enum.Parse(enumType, fi.Name);
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a boolean value into "Active" or "Inactive" strings.
        /// </summary>
        /// <param name="value"> Boolean value.</param>
        /// <returns> Active or Inactive.</returns>
        public static string GetBooleanToStatus(object value)
        {
            bool v = Convert.ToBoolean(value);
            return v ? "Active" : "Inactive";
        }

        /// <summary>
        /// Clear all special chars given a string.
        /// </summary>
        /// <param name="value"> Input string.</param>
        /// <returns> Output string, without special chars.</returns>
        public static string RemoveAccents(string value)
        {
            string normalizedString = value.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a DateTime object into an UNIX timestamp value.
        /// </summary>
        /// <param name="value"> Input DateTime.</param>
        /// <returns> Output DateTime (Unix format).</returns>
        public static double DateTimeToUnixTimestamp(DateTime value)
        {
            return (value - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
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
            if (birthdate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        /// <summary>
        /// Class that do a generic comparison between two objects.
        /// </summary>
        /// <typeparam name="T"> Generic Type.</typeparam>
        public class GenericComparer<T> : IComparer<T>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Misc.GenericComparer{T}" /> class.
            /// </summary>
            public GenericComparer()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Misc.GenericComparer{T}" /> class.
            /// </summary>
            /// <param name="sortExpression"> Sorting expression.</param>
            /// <param name="sortDirection"> Sorting direction (ASC ou DESC).</param>
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
            /// Compare two objects based on the given sorting expression and direction.
            /// </summary>
            /// <param name="x">Generic Type.</param>
            /// <param name="y">Generic Type.</param> 
            /// <returns> Returns -1 if x lesser than y, 0 if the values are equal, 1 if y greater than x.</returns>
            public int Compare(T x, T y)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(GenericSortExpression);
                IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);
                IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);

                return GenericSortDirection == SortDirection.Ascending ? obj1.CompareTo(obj2) : obj2.CompareTo(obj1);
            }
        }
    }
}