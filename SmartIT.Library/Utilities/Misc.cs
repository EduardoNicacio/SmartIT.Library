// <copyright file="Misc.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary> Provides many methods for unsorted features.</summary>

namespace SmartIT.Library.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Provides many methods for unsorted features.
	/// </summary>
	public static class Misc
	{
		/// <summary>
		/// Limits a string to a certain number of chars.
		/// </summary>
		/// <param name="value"> Input string.</param>
		/// <param name="limit"> Limit of chars (greater than 0).</param>
		/// <returns> Resulting string.</returns>
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
		/// Asynchronously limits a string to a certain number of chars.
		/// </summary>
		/// <param name="value"> Input string.</param>
		/// <param name="limit"> Limit of chars (greater than 0).</param>
		/// <returns> Resulting string.</returns>
		public static async Task<string> LimitStringAsync(object value, int limit)
		{
			return await Task.Run(() => LimitString(value, limit));
		}

		/// <summary>
		/// Retrieves the Description property for a given Enum object.
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
		/// Asynchronously retrieves the Description property for a given Enum object.
		/// </summary>
		/// <param name="value"> Enum object.</param>
		/// <returns> The value of the Description property.</returns>
		public static async Task<string> GetEnumDescriptionAsync(Enum value)
		{
			return await Task.Run(() => GetEnumDescription(value));
		}

		/// <summary>
		/// Retrieves the StringValue property for a given Enum object.
		/// </summary>
		/// <param name="value"> Enum object.</param>
		/// <returns> The value of the StringValue property.</returns>
		public static string GetEnumStringValue(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			StringValueAttribute[] attributes = (StringValueAttribute[])fi.GetCustomAttributes(typeof(StringValueAttribute), false);

			return (attributes.Length > 0) ? attributes[0].Value : value.ToString();
		}

		/// <summary>
		/// Asynchronously retrieves the StringValue property for a given Enum object.
		/// </summary>
		/// <param name="value"> Enum object.</param>
		/// <returns> The value of the StringValue property.</returns>
		public static async Task<string> GetEnumStringValueAsync(Enum value)
		{
			return await Task.Run(() => GetEnumStringValue(value));
		}

		/// <summary>
		/// Returns an Enum object given its Description or StringValue attributes.
		/// </summary>
		/// <param name="value"> Description or StringValue value.</param>
		/// <param name="enumType"> Enum type.</param>
		/// <returns> Enum object.</returns>
		public static object GetEnumValue(string value, Type enumType)
		{
			var fields = enumType.GetFields();
			foreach (FieldInfo fi in fields)
			{
				StringValueAttribute[] stringValueAttributes = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
				DescriptionAttribute[] descriptionAttributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

				if (stringValueAttributes.Length > 0 && stringValueAttributes[0].Value.Equals(value))
				{
					return Enum.Parse(enumType, fi.Name);
				}
				else if (descriptionAttributes.Length > 0 && descriptionAttributes[0].Description.Equals(value))
				{
					return Enum.Parse(enumType, fi.Name);
				}
			}

			return null;
		}

		/// <summary>
		/// Asynchronously returns an Enum object given its Description or StringValue attributes
		/// </summary>
		/// <param name="value"> Description or StringValue value.</param>
		/// <param name="enumType"> Enum type.</param>
		/// <returns> Enum object.</returns>
		public static async Task<object> GetEnumValueAsync(string value, Type enumType)
		{
			return await Task.Run(() => GetEnumValue(value, enumType));
		}

		/// <summary>
		/// Converts a boolean value into "Active" or "Inactive" strings.
		/// </summary>
		/// <param name="value"> The input boolean.</param>
		/// <returns> "Active", if the input boolean is true; "Inactive" instead.</returns>
		public static string GetBooleanToStatus(object value)
		{
			const string A = "Active";
			const string I = "Inactive";

			if (value is null)
			{
				return null;
			}

			bool v = Convert.ToBoolean(value);
			return v ? A : I;
		}

		/// <summary>
		/// Asynchronously converts a boolean value into "Active" or "Inactive" strings.
		/// </summary>
		/// <param name="value"> The input boolean.</param>
		/// <returns> "Active", if the input boolean is true; "Inactive" instead.</returns>
		public static async Task<string> GetBooleanToStatusAsync(object value)
		{
			return await Task.Run(() => GetBooleanToStatus(value));
		}

		/// <summary>
		/// Converts the string values "Active" or "Inactive" into their correspondent boolean values.
		/// </summary>
		/// <param name="value">The input string.</param>
		/// <returns>True, if the input string is "Active"; false instead.</returns>
		public static bool GetStatusToBoolean(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			return "Active".Equals(value);
		}

		/// <summary>
		/// Asynchronously converts the string values "Active" or "Inactive" into their correspondent boolean values.
		/// </summary>
		/// <param name="value">The input string.</param>
		/// <returns>True, if the input string is "Active"; false instead.</returns>
		public static async Task<bool> GetStatusToBooleanAsync(string value)
		{
			return await Task.Run(() => GetStatusToBoolean(value));
		}

		/// <summary>
		/// Clears all special chars for a given a string.
		/// </summary>
		/// <param name="value"> Input string.</param>
		/// <returns> Output string, without special chars.</returns>
		public static string RemoveAccents(string value)
		{
			string normalizedString = value.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < normalizedString.Length; i++)
			{
				char c = normalizedString[i];
				if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Asynchronously clears all special chars for a given a string.
		/// </summary>
		/// <param name="value"> Input string.</param>
		/// <returns> Output string, without special chars.</returns>
		public static async Task<string> RemoveAccentsAsync(string value)
		{
			return await Task.Run(() => RemoveAccents(value));
		}

		/// <summary>
		/// Converts a DateTime object into an UTC UNIX timestamp value.
		/// </summary>
		/// <param name="value"> Input DateTime.</param>
		/// <returns> Output UTC DateTime (Unix format).</returns>
		public static double DateTimeToUnixTimestamp(DateTime value)
		{
			return (value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime()).TotalSeconds;
		}

		/// <summary>
		/// Asynchronously converts a DateTime object into an UTC UNIX timestamp value.
		/// </summary>
		/// <param name="value"> Input DateTime.</param>
		/// <returns> Output UTC DateTime (Unix format).</returns>
		public static async Task<double> DateTimeToUnixTimestampAsync(DateTime value)
		{
			return await Task.Run(() => DateTimeToUnixTimestamp(value));
		}

		/// <summary>
		/// Converts an UNIX timestamp into a valid DateTime object.
		/// </summary>
		/// <param name="timestamp">The UNIX timestamp.</param>
		/// <returns>A valid DateTime object.</returns>
		public static DateTime UnixTimestampToDateTime(double timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
		}

		/// <summary>
		/// Asynchronously converts an UNIX timestamp into a valid DateTime object.
		/// </summary>
		/// <param name="timestamp">The UNIX timestamp.</param>
		/// <returns>A valid DateTime object.</returns>
		public static async Task<DateTime> UnixTimestampToDateTimeAsync(double timestamp)
		{
			return await Task.Run(() => UnixTimestampToDateTime(timestamp));
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
		/// Asynchronously returns the class name and its properties.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <returns>The properties of an object.</returns>
		public static async Task<string> GetClassPropertiesAsync(object obj)
		{
			return await Task.Run(() => GetClassProperties(obj));
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
		/// Asynchronously returns the age of a person by comparing its birth date with the current datetime.
		/// </summary>
		/// <param name="birthdate">Birth date.</param>
		/// <returns>The age, in years.</returns>
		public static async Task<int> ToAgeAsync(this DateTime birthdate)
		{
			return await Task.Run(() => ToAge(birthdate));
		}

		/// <summary>
		/// Asynchronously returns the age of a person by comparing its birth date with somewhere in time.
		/// </summary>
		/// <param name="birthdate">Birth date.</param>
		/// <param name="referenceDate">Reference date.</param>
		/// <returns>The age, in years.</returns>
		public static async Task<int> ToAgeAsync(this DateTime birthdate, DateTime referenceDate)
		{
			return await Task.Run(() => ToAge(birthdate, referenceDate));
		}

		/// <summary>
		/// Class that performs a generic comparison between two objects.
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
			/// Compares two objects based on the given sorting expression and direction.
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

			/// <summary>
			/// Asynchronously compares two objects based on the given sorting expression and direction.
			/// </summary>
			/// <param name="x">Generic Type.</param>
			/// <param name="y">Generic Type.</param> 
			/// <returns> Returns -1 if x lesser than y, 0 if the values are equal, 1 if y greater than x.</returns>
			public async Task<int> CompareAsync(T x, T y)
			{
				return await Task.Run(() => Compare(x, y));
			}
		}

		/// <summary>
		/// Specifies the direction in which to sort a list of items.
		/// </summary>
		public enum SortDirection
		{
			/// <summary>
			/// Sort from smallest to largest. For example, from A to Z.
			/// </summary>
			Ascending,
			/// <summary>
			/// Sort from largest to smallest. For example, from Z to A.
			/// </summary>
			Descending
		}
	}
}