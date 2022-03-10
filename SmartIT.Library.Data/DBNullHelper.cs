﻿// <copyright file="DbNullHelper.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
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
            return v == DateTime.MinValue ? DBNull.Value : (object)v;
        }

        /// <summary>
        /// Retorna o valor de um objeto passado como parâmetro.
        /// </summary>
        /// <param name="v"> Valor int?.</param>
        /// <returns> Valor do objeto.</returns>
        public static object GetValue(int? v)
        {
            return GetValue((object)v);
        }

        /// <summary>
        /// Retorna o valor de um objeto passado como parâmetro.
        /// </summary>
        /// <param name="v"> Valor int.</param>
        /// <returns> Valor do objeto.</returns>
        public static object GetValue(int v)
        {
            return v == 0 ? DBNull.Value : (object)v;
        }

        /// <summary>
        /// Retorna o valor de um objeto passado como parâmetro.
        /// </summary>
        /// <param name="v"> Valor Bool.</param>
        /// <returns> Valor do objeto.</returns>
        public static object GetValue(bool? v)
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