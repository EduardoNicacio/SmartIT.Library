// <copyright file="DbNullHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Classe que contém métodos para conversão de tipos entre o C# e os Bancos de Dados.</summary>

namespace SmartIT.Library.Data
{
	using System;

	/// <summary>
	/// Classe que contém métodos para conversão de tipos entre o C# e os Bancos de Dados.
	/// </summary>
	public static class DbNullHelper
	{
		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor  Valor Decimal.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(decimal v)
		{
			return v == 0 ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor  Valor Decimal.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(decimal? v)
		{
			return GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor String.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(string v)
		{
			return GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor DateTime.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(DateTime? v)
		{
			return GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor DateTime.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(DateTime v)
		{
			return v == DateTime.MinValue ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor int.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(int v)
		{
			return v == 0 ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor int?.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(int? v)
		{
			return v == null ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Bool.</param>
		/// <returns> Valor do objeto.</returns>
		public static object GetValue(bool? v)
		{
			return (v == null) ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(Guid? v)
		{
			return (v == null) ? Guid.Empty : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(byte[] v)
		{
			return (v.Length == 0) ? Array.Empty<byte>() : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(double v)
		{
			return Math.Truncate((decimal)v) == 0 ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(double? v)
		{
			return GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(long v)
		{
			return v == 0 ? DBNull.Value : GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		public static object GetValue(long? v)
		{
			return GetValue((object)v);
		}

		/// <summary>
		/// Retorna o valor de um objeto passado como parâmetro.
		/// </summary>
		/// <param name="v"> Valor Object.</param>
		/// <returns> Valor do objeto, ou DBNull.</returns>
		private static object GetValue(object v)
		{
			return (v == null || string.IsNullOrWhiteSpace(v.ToString())) ? DBNull.Value : v;
		}
	}
}