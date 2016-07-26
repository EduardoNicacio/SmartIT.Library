// <copyright file="BizValidationException.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Classe de validação de objetos que extende a classe Exception.</summary>

namespace SmartIT.Library.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Classe de validação de objetos que extende a classe Exception.
    /// </summary>
    public class BizValidationException : Exception
    {
        /// <summary>
        /// Mostra a lista de erros.
        /// </summary>
        private List<string> errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BizValidationException" /> class.
        /// </summary>
        /// <param name="msg"> Mensagem de erro.</param>
        public BizValidationException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BizValidationException" /> class.
        /// </summary>
        /// <param name="listMsgs"> Lista de mensagens.</param>
        public BizValidationException(List<string> listMsgs)
            : base("Business Validation Error")
        {
            errors = listMsgs;
        }

        /// <summary>
        /// Gets or sets the Erros list.
        /// </summary>
        /// <value> Padrão List de string vazia.</value>
        public List<string> Errors
        {
            get
            {
                if (errors == null)
                {
                    errors = new List<string>();
                }

                return errors;
            }

            set
            {
                errors = value;
            }
        }

        /// <summary>
        /// Retorna a lista de erros com formatação em HTML.
        /// </summary>
        /// <returns> Erros no formato Html.</returns>
        public string GetHtmlErrorMessage()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string e in errors)
            {
                sb.AppendLine(e + "<br />");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Retorna a lista de erros sem formatação.
        /// </summary>
        /// <returns> Erros no formato texto.</returns>
        public string GetTextErrorMessage()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string e in errors)
            {
                sb.AppendLine(e);
            }

            return sb.ToString();
        }
    }
}