// <copyright file="ActiveConnectionAttribute.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Active Connection Attribute.</summary>

namespace SmartIT.Library.Data
{
    using SmartIT.Library.Utility;

    /// <summary>
    /// Active Connection Attribute.
    /// </summary>
    public class ActiveConnectionAttribute : StringValueAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveConnectionAttribute" /> class.
        /// </summary>
        /// <param name="connectionStringName"> Nome da String de conexão.</param>
        public ActiveConnectionAttribute(string connectionStringName)
            : base(connectionStringName)
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveConnectionAttribute" /> class.
        /// </summary>
        /// <param name="connectionStringName">Nome da string de conexão.</param>
        /// <param name="commandTimeout">Timeout do objeto Command.</param>
        public ActiveConnectionAttribute(string connectionStringName, int commandTimeout)
            : base(connectionStringName)
        {
            this.CommandTimeout = commandTimeout;
        }

        /// <summary>
        /// Gets or sets the command timeout.
        /// </summary>
        /// <value> Default: 30 seconds.</value>
        public int CommandTimeout { get; set; }
    }
}