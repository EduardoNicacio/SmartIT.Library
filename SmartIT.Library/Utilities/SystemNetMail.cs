// <copyright file="SystemNetMail.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>02/09/2016</date>
// <summary>A simple wrapper for the System.Net.Mail class.</summary>

namespace SmartIT.Library.Utilities.Mail
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Mail;
	using System.Text;

	/// <summary>
	/// A simple wrapper for the System.Net.Mail class.
	/// </summary>
	public class SystemNetMail : MarshalByRefObject, IDisposable
	{
		// Flag: Has Dispose already been called?
		bool disposed = false;

		/// <summary>
		/// Sends an email.
		/// </summary>
		/// <param name="from">From address.</param>
		/// <param name="to">To address.</param>
		/// <param name="cc">Cc address.</param>
		/// <param name="bcc">Bcc address.</param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="body">Mail body.</param>
		/// <param name="mailPriority">Mail priority.</param>
		/// <param name="mailFormat">Mail body format.</param>
		/// <param name="smtpServer">Smtp server address.</param>
		/// <param name="smtpPort">Smtp port.</param>
		/// <param name="smtpUsername">Smtp username.</param>
		/// <param name="smtpPassword">Smtp password.</param>
		/// <param name="enableSsl">Enables or not the SSL.</param>
		/// <returns>0 in case of success, or an exception.</returns>
		public static int SendMail(
			string from,
			string to,
			string cc,
			string bcc,
			string subject,
			string body,
			MailPriority mailPriority,
			MailFormat mailFormat,
			string smtpServer,
			int smtpPort,
			string smtpUsername,
			string smtpPassword,
			bool enableSsl = false)
		{
			SendMail(from, to, cc, bcc, subject, body, mailPriority, mailFormat, smtpServer, smtpPort, smtpUsername, smtpPassword, enableSsl, new List<Attachment>());
			return 0;
		}

		/// <summary>
		/// Sends an email.
		/// </summary>
		/// <param name="from">From address.</param>
		/// <param name="to">To address.</param>
		/// <param name="cc">Cc address.</param>
		/// <param name="bcc">Bcc address.</param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="body">Mail body.</param>
		/// <param name="mailPriority">Mail priority.</param>
		/// <param name="mailFormat">Mail format.</param>
		/// <param name="smtpServer">Smtp server address.</param>
		/// <param name="smtpPort">Smtp port.</param>
		/// <param name="smtpUsername">Smtp username.</param>
		/// <param name="smtpPassword">Smtp password.</param>
		/// <param name="enableSsl">Enables or not the ssl.</param>
		/// <param name="attachments"><seealso cref="Attachment"/> object list.</param>
		/// <returns>0 in case of success, or an exception.</returns>
		public static int SendMail(
			string from,
			string to,
			string cc,
			string bcc,
			string subject,
			string body,
			MailPriority mailPriority,
			MailFormat mailFormat,
			string smtpServer,
			int smtpPort,
			string smtpUsername,
			string smtpPassword,
			bool enableSsl,
			List<string> attachments)
		{
			List<Attachment> list = new List<Attachment>();

			foreach (var attachment in attachments)
			{
				list.Add(new Attachment(attachment, System.Net.Mime.MediaTypeNames.Application.Octet));
			}

			SendMail(from, to, cc, bcc, subject, body, mailPriority, mailFormat, smtpServer, smtpPort, smtpUsername, smtpPassword, enableSsl, list);
			return 0;
		}

		/// <summary>
		/// Sends an email.
		/// </summary>
		/// <param name="from">From address.</param>
		/// <param name="to">To address.</param>
		/// <param name="cc">Cc address.</param>
		/// <param name="bcc">Bcc address.</param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="body">Mail body.</param>
		/// <param name="mailPriority">Mail priority.</param>
		/// <param name="mailFormat">Mail format.</param>
		/// <param name="smtpServer">Smtp server address.</param>
		/// <param name="smtpPort">Smtp port.</param>
		/// <param name="smtpUsername">Smtp username.</param>
		/// <param name="smtpPassword">Smtp password.</param>
		/// <param name="enableSsl">Enables or not the ssl.</param>
		/// <param name="attachments"><seealso cref="Attachment"/> object list.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="SmtpException"></exception>
		/// <returns>0 in case of success, or an exception.</returns>
		public static int SendMail(
			string from,
			string to,
			string cc,
			string bcc,
			string subject,
			string body,
			MailPriority mailPriority,
			MailFormat mailFormat,
			string smtpServer,
			int smtpPort,
			string smtpUsername,
			string smtpPassword,
			bool enableSsl,
			List<Attachment> attachments)
		{
			using (MailMessage message = new MailMessage())
			{
				if (string.IsNullOrWhiteSpace(from))
				{
					throw new ArgumentNullException(nameof(from));
				}
				if (string.IsNullOrWhiteSpace(to))
				{
					throw new ArgumentNullException(nameof(to));
				}

				message.From = new MailAddress(from);
				message.To.Add(new MailAddress(to));
				if (!string.IsNullOrWhiteSpace(cc)) { message.CC.Add(new MailAddress(cc)); }
				if (!string.IsNullOrWhiteSpace(bcc)) { message.Bcc.Add(new MailAddress(bcc)); }
				message.Subject = subject;
				message.Body = body;
				message.IsBodyHtml = mailFormat.Equals(MailFormat.Html);
				message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
				message.BodyEncoding = Encoding.GetEncoding("UTF-8");
				message.Priority = mailPriority;

				foreach (Attachment obj in attachments)
				{
					message.Attachments.Add(obj);
				}

				using (SmtpClient smtpClient = new SmtpClient())
				{
					if (string.IsNullOrEmpty(smtpServer))
					{
						throw new ArgumentNullException(nameof(smtpServer));
					}
					if (smtpPort <= 0 || smtpPort > 65535)
					{
						throw new ArgumentOutOfRangeException(nameof(smtpPort));
					}

					smtpClient.Host = smtpServer;
					smtpClient.Port = smtpPort;
					smtpClient.EnableSsl = enableSsl;

					if (!string.IsNullOrWhiteSpace(smtpUsername) && !string.IsNullOrWhiteSpace(smtpPassword))
					{
						smtpClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
					}

					try
					{
						smtpClient.Send(message);
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

			return 0;
		}

		/// <summary>
		/// Sends an email.
		/// </summary>
		/// <param name="from">From address.</param>
		/// <param name="to">To address list.</param>
		/// <param name="cc">Cc address list.</param>
		/// <param name="bcc">Bcc address list.</param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="body">Mail body.</param>
		/// <param name="mailPriority">Mail priority.</param>
		/// <param name="mailFormat">Mail format.</param>
		/// <param name="smtpServer">Smtp server address.</param>
		/// <param name="smtpPort">Smtp port.</param>
		/// <param name="smtpUsername">Smtp username.</param>
		/// <param name="smtpPassword">Smtp password.</param>
		/// <param name="enableSsl">Enables or not the ssl.</param>
		/// <returns>0 in case of success, or an exception.</returns>
		public static int SendMail(
			string from,
			IEnumerable<MailAddress> to,
			IEnumerable<MailAddress> cc,
			IEnumerable<MailAddress> bcc,
			string subject,
			string body,
			MailPriority mailPriority,
			MailFormat mailFormat,
			string smtpServer,
			int smtpPort,
			string smtpUsername,
			string smtpPassword,
			bool enableSsl = false)
		{
			SendMail(from, to, cc, bcc, subject, body, mailPriority, mailFormat, smtpServer, smtpPort, smtpUsername, smtpPassword, enableSsl, new List<Attachment>());
			return 0;
		}

		/// <summary>
		/// Sends an email.
		/// </summary>
		/// <param name="from">From address.</param>
		/// <param name="to">To address list.</param>
		/// <param name="cc">Cc address list.</param>
		/// <param name="bcc">Bcc address list.</param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="body">Mail body.</param>
		/// <param name="mailPriority">Mail priority.</param>
		/// <param name="mailFormat">Mail format.</param>
		/// <param name="smtpServer">Smtp server address.</param>
		/// <param name="smtpPort">Smtp port.</param>
		/// <param name="smtpUsername">Smtp username.</param>
		/// <param name="smtpPassword">Smtp password.</param>
		/// <param name="enableSsl">Enables or not the ssl.</param>
		/// <param name="attachments"><see cref="Attachment"/> object list.</param>
		/// <returns>0 in case of success, or an exception.</returns>
		public static int SendMail(
			string from,
			IEnumerable<MailAddress> to,
			IEnumerable<MailAddress> cc,
			IEnumerable<MailAddress> bcc,
			string subject,
			string body,
			MailPriority mailPriority,
			MailFormat mailFormat,
			string smtpServer,
			int smtpPort,
			string smtpUsername,
			string smtpPassword,
			bool enableSsl,
			List<string> attachments)
		{
			List<Attachment> list = new List<Attachment>();

			foreach (var attachment in attachments)
			{
				list.Add(new Attachment(attachment, mediaType: System.Net.Mime.MediaTypeNames.Application.Octet));
			}

			SendMail(from, to, cc, bcc, subject, body, mailPriority, mailFormat, smtpServer, smtpPort, smtpUsername, smtpPassword, enableSsl, list);
			return 0;
		}

		/// <summary>
		/// Sends an email.
		/// </summary>
		/// <param name="from">From address.</param>
		/// <param name="to">To address list.</param>
		/// <param name="cc">Cc address list.</param>
		/// <param name="bcc">Bcc address list.</param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="body">Mail body.</param>
		/// <param name="mailPriority">Mail priority.</param>
		/// <param name="mailFormat">Mail format.</param>
		/// <param name="smtpServer">Smtp server address.</param>
		/// <param name="smtpPort">Smtp port.</param>
		/// <param name="smtpUsername">Smtp username.</param>
		/// <param name="smtpPassword">Smtp password.</param>
		/// <param name="enableSsl">Enables or not the ssl.</param>
		/// <param name="attachments"><see cref="Attachment"/> object list.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="SmtpException"></exception>
		/// <returns>0 in case of success, or an exception.</returns>
		public static int SendMail(
			string from,
			IEnumerable<MailAddress> to,
			IEnumerable<MailAddress> cc,
			IEnumerable<MailAddress> bcc,
			string subject,
			string body,
			MailPriority mailPriority,
			MailFormat mailFormat,
			string smtpServer,
			int smtpPort,
			string smtpUsername,
			string smtpPassword,
			bool enableSsl,
			List<Attachment> attachments)
		{
			using (MailMessage message = new MailMessage())
			{
				if (string.IsNullOrWhiteSpace(from))
				{
					throw new ArgumentNullException(nameof(from));
				}
				if (to is null || !to.Any())
				{
					throw new ArgumentNullException(nameof(to));
				}

				message.From = new MailAddress(from);
				foreach (var addr in to) { message.To.Add(addr); }
				foreach (var addr in cc) { message.CC.Add(addr); }
				foreach (var addr in bcc) { message.Bcc.Add(addr); }
				message.Subject = subject;
				message.Body = body;
				message.IsBodyHtml = mailFormat.Equals(MailFormat.Html);
				message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
				message.BodyEncoding = Encoding.GetEncoding("UTF-8");
				message.Priority = mailPriority;

				foreach (var attachment in attachments)
				{
					message.Attachments.Add(attachment);
				}

				using (SmtpClient smtpClient = new SmtpClient())
				{
					if (string.IsNullOrEmpty(smtpServer))
					{
						throw new ArgumentNullException(nameof(smtpServer));
					}
					if (smtpPort <= 0 || smtpPort > 65535)
					{
						throw new ArgumentOutOfRangeException(nameof(smtpPort));
					}

					smtpClient.Host = smtpServer;
					smtpClient.Port = smtpPort;
					smtpClient.EnableSsl = enableSsl;

					if (!string.IsNullOrWhiteSpace(smtpUsername) && !string.IsNullOrWhiteSpace(smtpPassword))
					{
						smtpClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
					}

					try
					{
						smtpClient.Send(message);
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

			return 0;
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
	/// Provides enumerated values for e-mail format. Recommended alternative: System.Net.Mail.
	/// </summary>
	public enum MailFormat
	{
		/// <summary>
		/// Specifies that the e-mail format is plain text.
		/// </summary>
		Text,
		/// <summary>
		/// Specifies that the e-mail format is HTML.
		/// </summary>
		Html
	}
}