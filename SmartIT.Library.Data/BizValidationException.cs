// <copyright file="BizValidationException.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Classe de validacao de objetos que extende a classe Exception.</summary>

namespace SmartIT.Library.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

    /// <summary>
    /// Classe de validacao de objetos que extende a classe Exception.
    /// </summary>
    [Serializable]
    public sealed class BizValidationException : Exception
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
        /// Initializes a new instance of the <see cref="BizValidationException" /> class.
        /// </summary>
        /// <param name="info"> The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context"> The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        private BizValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            errors = (List<string>)info.GetValue("errors", errors.GetType());
        }

        /// <summary>
        /// Overrides the GetObjectData method on the Exception class.
        /// </summary>
        /// <param name="info"> SerializationInfo.</param>
        /// <param name="context"> StreamingContext.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("errors", errors, errors.GetType());
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets or sets the Erros list.
        /// </summary>
        /// <value> Padrao List de string vazia.</value>
        public List<string> Errors
        {
            get
            {
                return errors ?? new List<string>();
            }
            set
            {
                errors = value;
            }
        }

        /// <summary>
        /// Retorna a lista de erros com formatacao em HTML.
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
        /// Retorna a lista de erros sem formatacao.
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