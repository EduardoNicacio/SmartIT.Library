// <copyright file="SystemWebMail.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/03/2015</date>
// <summary>Class that encapsulates an object System.Web.Mail.</summary>

namespace SmartIT.Library.Utility.Mail
{
    using System;
    using System.Web.Mail;

    /// <summary>
    /// Classe que encapsula um objeto System.Web.Mail, utilizado, por exemplo, para envio de informações de recuperação de senha ou Comunicados aos usuários do Portal.
    /// </summary>
    public class SystemWebMail : MarshalByRefObject, IDisposable
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
#pragma warning disable CS0618 // Type or member is obsolete
            MailPriority MailPriority,
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            System.Web.Mail.MailFormat MailFormat,
#pragma warning restore CS0618 // Type or member is obsolete
            string SmtpServer)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            MailMessage objemail = new MailMessage
#pragma warning restore CS0618 // Type or member is obsolete
            {
                // Define os parametros do e-mail
                Priority = MailPriority,
                BodyFormat = MailFormat,
                To = To,
                Cc = Cc,
                Bcc = Bcc,
                From = From,
                Subject = Subject,
                Body = Body
            };

#pragma warning disable CS0618 // Type or member is obsolete
            SmtpMail.SmtpServer = SmtpServer;
#pragma warning restore CS0618 // Type or member is obsolete

            // Envio do e-mail
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                SmtpMail.Send(objemail);
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch(Exception ex)
            {
                throw ex;
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
#pragma warning disable CS0618 // Type or member is obsolete
            MailPriority MailPriority,
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            System.Web.Mail.MailFormat MailFormat,
#pragma warning restore CS0618 // Type or member is obsolete
            string SmtpServer,
            string[] Attachments)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            MailMessage objemail = new MailMessage
#pragma warning restore CS0618 // Type or member is obsolete
            {
                // Define os parametros do e-mail
                Priority = MailPriority,
                BodyFormat = MailFormat,
                To = To,
                Cc = Cc,
                Bcc = Bcc,
                From = From,
                Subject = Subject,
                Body = Body
            };

#pragma warning disable CS0618 // Type or member is obsolete
            SmtpMail.SmtpServer = SmtpServer;
#pragma warning restore CS0618 // Type or member is obsolete

            // Envio do e-mail
            foreach (string strpath_anexo in Attachments)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                objemail.Attachments.Add(new MailAttachment(strpath_anexo));
#pragma warning restore CS0618 // Type or member is obsolete
            }

            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                SmtpMail.Send(objemail);
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SystemWebMail" /> class.
        /// </summary>
        ~SystemWebMail()
        {
            Dispose(false);
        }

        /// <summary>
        /// Implementation of Dispose pattern.
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