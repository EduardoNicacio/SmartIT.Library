// <copyright file="EnumToList.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>02/09/2016</date>
// <summary>EnumToList utility class.</summary>

namespace SmartIT.Library.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	/// <summary>
	/// EnumToList utility class.
	/// </summary>
	public static class EnumToList
	{
		/// <summary>
		/// Returns the type T as a KeyValuePar list with the Enum value as integer and string.
		/// </summary>
		/// <typeparam name="T">Enum type.</typeparam>
		/// <returns>List of <seealso cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> objects.</returns>
		public static List<KeyValuePair<int, string>> GetList<T>()
		{
			T[] array = (T[])Enum.GetValues(typeof(T)).Cast<T>();
			return array.Select(x => new KeyValuePair<int, string>(Convert.ToInt32(x), x.ToString())).ToList();
		}

		/// <summary>
		/// Returns the type T as a KeyValuePar list with the Enum value as integer and description as string.
		/// </summary>
		/// <typeparam name="T">Enum type.</typeparam>
		/// <returns>List of <seealso cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> objects.</returns>
		public static List<KeyValuePair<int, string>> GetDescriptionList<T>()
		{
			T[] array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());
			return array.Select(x => new KeyValuePair<int, string>(Convert.ToInt32(x), GetEnumDescription<T>(Convert.ToInt32(x)))).ToList();
		}

		/// <summary>
		/// Returns the type T as a KeyValuePar list with the Enum value as boolean and description as string.
		/// </summary>
		/// <typeparam name="T">Enum type.</typeparam>
		/// <returns>An object that implements the <seealso cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> interface.</returns>
		public static IDictionary<bool, string> GetDescriptionBooleanList<T>()
		{
			T[] array = (T[])Enum.GetValues(typeof(T)).Cast<T>();
			return array.ToDictionary(item => Convert.ToBoolean(item), item => GetEnumDescription<T>(Convert.ToInt32(item)));
		}

		/// <summary>
		/// Gets the Enum description value.
		/// </summary>
		/// <typeparam name="TEnum">Enum type.</typeparam>
		/// <param name="value">Enum value (starting at zero).</param>
		/// <returns>The enum element description.</returns>
		public static string GetEnumDescription<TEnum>(int value)
		{
			return GetEnumDescription((Enum)(object)(TEnum)(object)value);
		}

		/// <summary>
		/// Gets the Enum description value.
		/// </summary>
		/// <param name="value">Enum.</param>
		/// <returns>The enum element description.</returns>
		public static string GetEnumDescription(Enum value)
		{
			var fi = value.GetType().GetField(value.ToString());
			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		/// <summary>
		/// Gets the Enum string value.
		/// </summary>
		/// <typeparam name="TEnum">Enum type.</typeparam>
		/// <param name="value">Enum value (starting at zero)</param>
		/// <returns>The enum element string value.</returns>
		public static string GetEnumStringValue<TEnum>(int value)
		{
			return GetEnumStringValue((Enum)(object)(TEnum)(object)value);
		}

		/// <summary>
		/// Gets the Enum string value.
		/// </summary>
		/// <param name="value">Enum element.</param>
		/// <returns>The enum element string value.</returns>
		public static string GetEnumStringValue(Enum value)
		{
			var fi = value.GetType().GetField(value.ToString());
			var attributes = (StringValueAttribute[])fi.GetCustomAttributes(typeof(StringValueAttribute), false);

			return attributes.Length > 0 ? attributes[0].Value : value.ToString();
		}
	}
}