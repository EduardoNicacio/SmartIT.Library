// <copyright file="ManagerBase.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Manager Base das classes DAO.</summary>

namespace SmartIT.Library.Data.ModelManager
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Manager Base das classes DAO.
    /// </summary>
    /// <typeparam name="TEntityDb"> Classes da camada DAO.</typeparam>
    [Serializable]
    public class ManagerBase<TEntityDb> where TEntityDb : new()
    {
        /// <summary>
        /// Initializes static members of the <see cref="ManagerBase" /> class.
        /// </summary>
        protected ManagerBase()
        {
            EntityDb = new TEntityDb();
            SearchCriteria = SearchCriteria ?? new Dictionary<string, object>();
            Values = Values ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes static members of the <see cref="ManagerBase" /> class.
        /// </summary>
        static ManagerBase()
        {
            EntityDb = new TEntityDb();
            SearchCriteria = SearchCriteria ?? new Dictionary<string, object>();
            Values = Values ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or sets the transporte de valores para outras camadas.
        /// </summary>
        /// <value> Valores disponíveis para busca.</value>
        public static Dictionary<string, object> Values { get; set; }

        /// <summary>
        /// Gets or sets the critérios de busca para o método Search e SearchItem.
        /// </summary>
        /// <value> Critérios disponíveis para busca.</value>
        public static Dictionary<string, object> SearchCriteria { get; set; }

        /// <summary>
        /// Gets the instance of DAO object.
        /// </summary>
        /// <value> Valor da entidade.</value>
        public static TEntityDb EntityDb { get; set; }
    }
}