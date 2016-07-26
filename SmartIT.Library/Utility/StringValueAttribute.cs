// <copyright file="StringValueAttribute.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Classe que adiciona o atributo StringValue aos tipos enumerados.</summary>

namespace SmartIT.Library.Utility
{
    /// <summary>
    /// Classe que adiciona o atributo StringValue aos tipos enumerados.
    /// </summary>
    public class StringValueAttribute : System.Attribute
    {
        /// <summary>
        /// Atributo value.
        /// </summary>
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueAttribute" /> class.
        /// </summary>
        /// <param name="value">String com o valor.</param>
        public StringValueAttribute(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Propriedade Valor.
        /// </summary>
        public string Value
        {
            get { return value; }
        }
    }
}