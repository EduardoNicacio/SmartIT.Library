// <copyright file="SystemWebMail.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>02/09/2016</date>
// <summary>Class that encapsulates a System.Net.Mail object.</summary>

namespace SmartIT.Library.Utility.Mail
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net.Mail;

    /// <summary>
    /// A System.Net.Mail simple implementation class.
    /// </summary>
    public class SystemNetMail : MarshalByRefObject, IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;

        /// <summary>
        /// Dispatches an email.
        /// </summary>
        /// <param name="From">From address.</param>
        /// <param name="To">To address.</param>
        /// <param name="Cc">Cc address.</param>
        /// <param name="Bcc">Bcc address.</param>
        /// <param name="Subject">Mail subject.</param>
        /// <param name="Body">Mail body.</param>
        /// <param name="MailPriority">Mail priority.</param>
        /// <param name="MailFormat">Mail format.</param>
        /// <param name="SmtpServer">Smtp server address.</param>
        /// <param name="SmtpPort">Smtp port.</param>
        /// <param name="SmtpUsername">Smtp username.</param>
        /// <param name="SmtpPassword">Smtp password.</param>
        /// <param name="EnableSsl">Enables or not the ssl.</param>
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
            int SmtpPort,
            string SmtpUsername,
            string SmtpPassword,
            bool EnableSsl)
        {
            SendMail(From, To, Cc, Bcc, Subject, Body, MailPriority, MailFormat, SmtpServer, SmtpPort, SmtpUsername, SmtpPassword, EnableSsl, new List<Attachment>());
        }

        /// <summary>
        /// Dispatches an email.
        /// </summary>
        /// <param name="From">From address.</param>
        /// <param name="To">To address.</param>
        /// <param name="Cc">Cc address.</param>
        /// <param name="Bcc">Bcc address.</param>
        /// <param name="Subject">Mail subject.</param>
        /// <param name="Body">Mail body.</param>
        /// <param name="MailPriority">Mail priority.</param>
        /// <param name="MailFormat">Mail format.</param>
        /// <param name="SmtpServer">Smtp server address.</param>
        /// <param name="SmtpPort">Smtp port.</param>
        /// <param name="SmtpUsername">Smtp username.</param>
        /// <param name="SmtpPassword">Smtp password.</param>
        /// <param name="EnableSsl">Enables or not the ssl.</param>
        /// <param name="Attachments"><seealso cref="Attachment"/> object list.</param>
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
            int SmtpPort,
            string SmtpUsername,
            string SmtpPassword,
            bool EnableSsl,
            List<string> Attachments)
        {
            List<Attachment> list = new List<Attachment>();

            foreach (var file in Attachments)
            {
                list.Add(new Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet));
            }
            SendMail(From, To, Cc, Bcc, Subject, Body, MailPriority, MailFormat, SmtpServer, SmtpPort, SmtpUsername, SmtpPassword, EnableSsl, list);
        }

        /// <summary>
        /// Dispatches an email.
        /// </summary>
        /// <param name="From">From address.</param>
        /// <param name="To">To address.</param>
        /// <param name="Cc">Cc address.</param>
        /// <param name="Bcc">Bcc address.</param>
        /// <param name="Subject">Mail subject.</param>
        /// <param name="Body">Mail body.</param>
        /// <param name="MailPriority">Mail priority.</param>
        /// <param name="MailFormat">Mail format.</param>
        /// <param name="SmtpServer">Smtp server address.</param>
        /// <param name="SmtpPort">Smtp port.</param>
        /// <param name="SmtpUsername">Smtp username.</param>
        /// <param name="SmtpPassword">Smtp password.</param>
        /// <param name="EnableSsl">Enables or not the ssl.</param>
        /// <param name="Attachments"><seealso cref="Attachment"/> object list.</param>
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
            int SmtpPort,
            string SmtpUsername,
            string SmtpPassword,
            bool EnableSsl,
            List<Attachment> Attachments)
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(From);
                message.To.Add(new MailAddress(To));
                message.CC.Add(new MailAddress(Cc));
                message.Bcc.Add(new MailAddress(Bcc));
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = MailFormat.Equals(MailFormat.HtmlFormat);
                message.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                message.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                foreach (Attachment obj in Attachments)
                {
                    message.Attachments.Add(obj);
                }

                using (SmtpClient smtpServer = new SmtpClient())
                {
                    smtpServer.Host = SmtpServer;
                    smtpServer.Port = SmtpPort > 0 ? SmtpPort : 25;
                    smtpServer.EnableSsl = EnableSsl;

                    if (!string.IsNullOrWhiteSpace(SmtpUsername) && !string.IsNullOrWhiteSpace(SmtpPassword))
                    {
                        smtpServer.Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
                    }

                    try
                    {
                        smtpServer.Send(message);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new ArgumentNullException(ex.Message, ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new InvalidOperationException(ex.Message, ex);
                    }
                    catch (SmtpException ex)
                    {
                        throw new SmtpException(ex.Message, ex);
                    }
                }
            }
        }

        /// <summary>
        /// Dispatches an email.
        /// </summary>
        /// <param name="From">From address.</param>
        /// <param name="To">To address list.</param>
        /// <param name="Cc">Cc address list.</param>
        /// <param name="Bcc">Bcc address list.</param>
        /// <param name="Subject">Mail subject.</param>
        /// <param name="Body">Mail body.</param>
        /// <param name="MailPriority">Mail priority.</param>
        /// <param name="MailFormat">Mail format.</param>
        /// <param name="SmtpServer">Smtp server address.</param>
        /// <param name="SmtpPort">Smtp port.</param>
        /// <param name="SmtpUsername">Smtp username.</param>
        /// <param name="SmtpPassword">Smtp password.</param>
        /// <param name="EnableSsl">Enables or not the ssl.</param>
        public static void SendMail(
            string From,
            IEnumerable<MailAddress> To,
            IEnumerable<MailAddress> Cc,
            IEnumerable<MailAddress> Bcc,
            string Subject,
            string Body,
            MailPriority MailPriority,
            MailFormat MailFormat,
            string SmtpServer,
            int SmtpPort,
            string SmtpUsername,
            string SmtpPassword,
            bool EnableSsl)
        {
            SendMail(From, To, Cc, Bcc, Subject, Body, MailPriority, MailFormat, SmtpServer, SmtpPort, SmtpUsername, SmtpPassword, EnableSsl, new List<Attachment>());
        }

        /// <summary>
        /// Dispatches an email.
        /// </summary>
        /// <param name="From">From address.</param>
        /// <param name="To">To address list.</param>
        /// <param name="Cc">Cc address list.</param>
        /// <param name="Bcc">Bcc address list.</param>
        /// <param name="Subject">Mail subject.</param>
        /// <param name="Body">Mail body.</param>
        /// <param name="MailPriority">Mail priority.</param>
        /// <param name="MailFormat">Mail format.</param>
        /// <param name="SmtpServer">Smtp server address.</param>
        /// <param name="SmtpPort">Smtp port.</param>
        /// <param name="SmtpUsername">Smtp username.</param>
        /// <param name="SmtpPassword">Smtp password.</param>
        /// <param name="EnableSsl">Enables or not the ssl.</param>
        /// <param name="Attachments"><see cref="Attachment"/> object list.</param>
        public static void SendMail(
            string From,
            IEnumerable<MailAddress> To,
            IEnumerable<MailAddress> Cc,
            IEnumerable<MailAddress> Bcc,
            string Subject,
            string Body,
            MailPriority MailPriority,
            MailFormat MailFormat,
            string SmtpServer,
            int SmtpPort,
            string SmtpUsername,
            string SmtpPassword,
            bool EnableSsl,
            List<string> Attachments)
        {
            List<Attachment> list = new List<Attachment>();

            foreach (var file in Attachments)
            {
                list.Add(new Attachment(file, mediaType: System.Net.Mime.MediaTypeNames.Application.Octet));
            }
            SendMail(From, To, Cc, Bcc, Subject, Body, MailPriority, MailFormat, SmtpServer, SmtpPort, SmtpUsername, SmtpPassword, EnableSsl, list);
        }

        /// <summary>
        /// Dispatches an email.
        /// </summary>
        /// <param name="From">From address.</param>
        /// <param name="To">To address list.</param>
        /// <param name="Cc">Cc address list.</param>
        /// <param name="Bcc">Bcc address list.</param>
        /// <param name="Subject">Mail subject.</param>
        /// <param name="Body">Mail body.</param>
        /// <param name="MailPriority">Mail priority.</param>
        /// <param name="MailFormat">Mail format.</param>
        /// <param name="SmtpServer">Smtp server address.</param>
        /// <param name="SmtpPort">Smtp port.</param>
        /// <param name="SmtpUsername">Smtp username.</param>
        /// <param name="SmtpPassword">Smtp password.</param>
        /// <param name="EnableSsl">Enables or not the ssl.</param>
        /// <param name="Attachments"><see cref="Attachment"/> object list.</param>
        public static void SendMail(
            string From,
            IEnumerable<MailAddress> To,
            IEnumerable<MailAddress> Cc,
            IEnumerable<MailAddress> Bcc,
            string Subject,
            string Body,
            MailPriority MailPriority,
            MailFormat MailFormat,
            string SmtpServer,
            int SmtpPort,
            string SmtpUsername,
            string SmtpPassword,
            bool EnableSsl,
            List<Attachment> Attachments)
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(From);
                foreach (var addr in To) { message.To.Add(addr); }
                foreach (var addr in Cc) { message.CC.Add(addr); }
                foreach (var addr in Bcc) { message.Bcc.Add(addr); }
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = MailFormat.Equals(MailFormat.HtmlFormat);
                message.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                message.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                foreach (Attachment obj in Attachments)
                {
                    message.Attachments.Add(obj);
                }

                using (SmtpClient smtpServer = new SmtpClient())
                {
                    smtpServer.Host = SmtpServer;
                    smtpServer.Port = SmtpPort > 0 ? SmtpPort : 25;
                    smtpServer.EnableSsl = EnableSsl;

                    if (!string.IsNullOrWhiteSpace(SmtpUsername) && !string.IsNullOrWhiteSpace(SmtpPassword))
                    {
                        smtpServer.Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
                    }

                    try
                    {
                        smtpServer.Send(message);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new ArgumentNullException(ex.Message, ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new InvalidOperationException(ex.Message, ex);
                    }
                    catch (SmtpException ex)
                    {
                        throw new SmtpException(ex.Message, ex);
                    }
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SystemNetMail" /> class.
        /// </summary>
        ~SystemNetMail()
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
                Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }
    }

    /// <summary>
    /// Mail format enumeration.
    /// </summary>
    public enum MailFormat
    {
        /// <summary>
        /// HTML format e-mail.
        /// </summary>
        HtmlFormat,

        /// <summary>
        /// Plain text format email.
        /// </summary>
        PlainText
    }
}