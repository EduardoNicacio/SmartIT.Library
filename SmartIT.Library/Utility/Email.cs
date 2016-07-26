// <copyright file="Email.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/03/2015</date>
// <summary>Classe que encapsula um objeto System.Web.Mail.</summary>

namespace SmartIT.Library.Utility
{
    using System;
    using System.Web.Mail;

    /// <summary>
    /// Classe que encapsula um objeto System.Web.Mail, utilizado, por exemplo, para envio de informações de recuperação de senha ou Comunicados aos usuários do Portal.
    /// </summary>
    public class Email : MarshalByRefObject, IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;

        /// <summary>
        /// Envio de email genérico.
        /// </summary>
        /// <param name="From"> Quem está enviando a mensagem.</param>
        /// <param name="To"> Para quem a mensagem será envianda.</param>
        /// <param name="Cc"> Para quem a mensagem irá em cópia.</param>
        /// <param name="Bcc"> Para quem a mensagem irá em cópia oculta.</param>
        /// <param name="Subject"> Assunto do email.</param>
        /// <param name="Body"> Corpo do Email.</param>
        /// <param name="MailPriority"> Prioridade do Email.</param>
        /// <param name="MailFormat"> Tipo do formato do email.</param>
        /// <param name="SmtpServer"> IP do servidor SMTP.</param>
        public static void SendMail(
            string From,
            string To,
            string Cc,
            string Bcc,
            string Subject,
            string Body,
            MailPriority MailPriority,
            MailFormat MailFormat,
            string SmtpServer)
        {
            MailMessage objemail = new MailMessage();

            // Define os parametros do e-mail
            objemail.Priority = MailPriority;
            objemail.BodyFormat = MailFormat;
            objemail.To = To;
            objemail.Cc = Cc;
            objemail.Bcc = Bcc;
            objemail.From = From;
            objemail.Subject = Subject;
            objemail.Body = Body;

            SmtpMail.SmtpServer = SmtpServer;

            // Envio do e-mail
            try
            {
                SmtpMail.Send(objemail);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Envio de email com arquivos anexados.
        /// </summary>
        /// <param name="From"> Quem está enviando a mensagem.</param>
        /// <param name="To"> Para quem a mensagem será enviada.</param>
        /// <param name="Cc"> Para quem a mensagem irá em cópia.</param>
        /// <param name="Bcc"> Para quem a mensagem irá em cópia oculta.</param>
        /// <param name="Subject"> Assunto do email.</param>
        /// <param name="Body"> Corpo do Email.</param>
        /// <param name="MailPriority"> Prioridade do Email.</param>
        /// <param name="MailFormat"> Tipo do formato do email.</param>
        /// <param name="SmtpServer"> Numero IP do servidor SMTP.</param>
        /// <param name="Attachments"> Lista de arquivos que serão enviados como anexo.</param>
        public static void SendMail(
            string From,
            string To,
            string Cc,
            string Bcc,
            string Subject,
            string Body,
            MailPriority MailPriority,
            MailFormat MailFormat,
            string SmtpServer,
            string[] Attachments)
        {
            MailMessage objemail = new MailMessage();

            // Define os parametros do e-mail
            objemail.Priority = MailPriority;
            objemail.BodyFormat = MailFormat;
            objemail.To = To;
            objemail.Cc = Cc;
            objemail.Bcc = Bcc;
            objemail.From = From;
            objemail.Subject = Subject;
            objemail.Body = Body;

            SmtpMail.SmtpServer = SmtpServer;

            // Envio do e-mail
            foreach (string strpath_anexo in Attachments)
            {
                objemail.Attachments.Add(new MailAttachment(strpath_anexo));
            }

            try
            {
                SmtpMail.Send(objemail);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementação do método Dispose da classe, obrigatório pela interface IDisposable.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                this.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }
    }
}