using SmartIT.Library.Utilities.Mail;
using System.Net.Mail;

namespace SmartIT.Library.Tests.Utilities
{
	[TestFixture]
	internal class SystemNetMailTests
	{
		// Email config
		const string from = "from@localhost.com";
		const string to = "to@localhost.com";
		const string cc = "cc@localhost.com";
		const string bcc = "bcc@localhost.com";
		const string subject = "Subject";
		const string body = "It Worked!";

		// SmtpClient config
		const string smtpServer = "127.0.0.1";
		const int smtpPort = 25;
		const string smtpUsername = "";
		const string smtpPassword = "";

		// Attachments
		readonly List<string> attAsStrings = [];
		readonly List<Attachment> attAsAttachments1 = [];
		readonly List<Attachment> attAsAttachments2 = [];

		// Email with lists config
		readonly List<string> toList = [];
		readonly List<string> ccList = [];
		readonly List<string> bccList = [];

		[SetUp]
		public void SetUp()
		{
			// Clears the list
			attAsStrings.Clear();

			// Adds three attachments as paths to this list
			attAsStrings.Add("E:\\SRC\\ECNSoft\\00. SmartIT.Library\\SmartIT.Library\\SmartIT.Library.Tests\\attachments\\attachment1.txt");
			attAsStrings.Add("E:\\SRC\\ECNSoft\\00. SmartIT.Library\\SmartIT.Library\\SmartIT.Library.Tests\\attachments\\attachment2.txt");
			attAsStrings.Add("E:\\SRC\\ECNSoft\\00. SmartIT.Library\\SmartIT.Library\\SmartIT.Library.Tests\\attachments\\attachment3.txt");

			// Clears the lists
			attAsAttachments1.Clear();
			attAsAttachments2.Clear();

			// Adds the same files as Attachment objects to the second list
			foreach (var attachment in attAsStrings)
			{
				attAsAttachments1.Add(new Attachment(attachment, System.Net.Mime.MediaTypeNames.Application.Octet));
				attAsAttachments2.Add(new Attachment(attachment, System.Net.Mime.MediaTypeNames.Application.Octet));
			}

			// Clears the lists
			toList.Clear();
			ccList.Clear();
			bccList.Clear();

			// Setup the email lists for the second part of the tests
			toList.Add("to1@localhost.com"); toList.Add("to2@localhost.com"); toList.Add("to3@localhost.com");
			ccList.Add("cc1@localhost.com"); ccList.Add("cc2@localhost.com"); ccList.Add("cc3@localhost.com");
			bccList.Add("bcc1@localhost.com"); bccList.Add("bcc2@localhost.com"); bccList.Add("bcc3@localhost.com");
		}

		[Test, Order(1)]
		public void T01_Validate_SystemNetMail_SendMail_InvalidParameters_from()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => SystemNetMail.SendMail("", to, cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false));
		}

		[Test, Order(2)]
		public void T02_Validate_SystemNetMail_SendMail_InvalidParameters_to()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => SystemNetMail.SendMail(from, "", cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false));
		}

		[Test, Order(3)]
		public void T03_Validate_SystemNetMail_SendMail_InvalidParameters_smtpServer()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => SystemNetMail.SendMail(from, to, cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, "", smtpPort, smtpUsername, smtpPassword, false));
		}

		[Test, Order(4)]
		public void T04_Validate_SystemNetMail_SendMail_InvalidParameters_smtpPort()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => SystemNetMail.SendMail(from, to, cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, 0, smtpUsername, smtpPassword, false));
		}

		[Test, Order(5)]
		public void T05_Validate_SystemNetMail_SendMail_NoAttachments()
		{
			// Arrange
			int result;

			// Act
			result = SystemNetMail.SendMail(from, to, cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(6)]
		public void T06_Validate_SystemNetMail_SendMail_AttachmentsAsListOfStrings()
		{
			// Arrange
			int result;

			// Act
			result = SystemNetMail.SendMail(from, to, cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false, attAsStrings);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(7)]
		public void T07_Validate_SystemNetMail_SendMail_AttachmentsAsListOfObjects()
		{
			// Arrange
			int result;

			// Act
			result = SystemNetMail.SendMail(from, to, cc, bcc, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false, attAsAttachments1);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(8)]
		public void T08_Validate_SystemNetMail_SendMailWithLists_InvalidParameters_from()
		{
			// Arrange
			List<MailAddress> localToList = [];
			foreach (var value in toList) { localToList.Add(new MailAddress(value)); }

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => SystemNetMail.SendMail("", localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false));
		}

		[Test, Order(9)]
		public void T09_Validate_SystemNetMail_SendMailWithLists_InvalidParameters_to()
		{
			// Arrange
			List<MailAddress> localToList = [];

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			// Act
			Assert.Throws<ArgumentNullException>(() => SystemNetMail.SendMail(from, localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false));
		}

		[Test, Order(10)]
		public void T10_Validate_SystemNetMail_SendMailWithLists_InvalidParameters_smtpServer()
		{
			// Arrange
			List<MailAddress> localToList = [];
			foreach (var value in toList) { localToList.Add(new MailAddress(value)); }

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => SystemNetMail.SendMail(from, localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, "", smtpPort, smtpUsername, smtpPassword, false));
		}

		[Test, Order(11)]
		public void T11_Validate_SystemNetMail_SendMailWithLists_InvalidParameters_smtpPort()
		{
			// Arrange
			List<MailAddress> localToList = [];
			foreach (var value in toList) { localToList.Add(new MailAddress(value)); }

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			// Act

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => SystemNetMail.SendMail(from, localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, 0, smtpUsername, smtpPassword, false));
		}

		[Test, Order(12)]
		public void T12_Validate_SystemNetMail_SendMailWithLists_NoAttachments()
		{
			// Arrange
			List<MailAddress> localToList = [];
			foreach (var value in toList) { localToList.Add(new MailAddress(value)); }

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			int result;

			// Act
			result = SystemNetMail.SendMail(from, localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(13)]
		public void T13_Validate_SystemNetMail_SendMailWithLists_AttachmentsAsListOfStrings()
		{
			// Arrange
			List<MailAddress> localToList = [];
			foreach (var value in toList) { localToList.Add(new MailAddress(value)); }

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			int result;

			// Act
			result = SystemNetMail.SendMail(from, localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false, attAsStrings);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(14)]
		public void T14_Validate_SystemNetMail_SendMailWithLists_AttachmentsAsListOfObjects()
		{
			// Arrange
			List<MailAddress> localToList = [];
			foreach (var value in toList) { localToList.Add(new MailAddress(value)); }

			List<MailAddress> localCcList = [];
			foreach (var value in ccList) { localCcList.Add(new MailAddress(value)); }

			List<MailAddress> localBccList = [];
			foreach (var value in bccList) { localBccList.Add(new MailAddress(value)); }

			int result;

			// Act
			result = SystemNetMail.SendMail(from, localToList, localCcList, localBccList, subject, body, MailPriority.Normal, MailFormat.Text, smtpServer, smtpPort, smtpUsername, smtpPassword, false, attAsAttachments2);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}
	}
}
