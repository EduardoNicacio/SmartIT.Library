namespace SmartIT.Library.Tests.Helpers
{
	using SmartIT.Library.Helpers;

	[TestFixture]
	public class EventViewerHelperTests
	{
		protected const string machineName = "Ryzen-5900X";
		protected const string source = "Application";
		protected const string log = "SmartIT.Library";
		protected const string message = "SmartIT.Library.Utilities.EventViewerTests";
		protected byte typeError = 1;
		protected byte typeWarning = 2;
		protected byte typeInformation = 4;
		protected byte typeSuccessAudit = 8;
		protected byte typeFailureAudit = 16;

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_Error()
		{
			// Arrange
			var callResult = EventViewerHelper.SetEventLog(source, log, message, typeError, 1001);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, message, typeError, 1001);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_Warning()
		{
			// Arrange
			var callResult = EventViewerHelper.SetEventLog(source, log, message, typeWarning, 1002);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, message, typeWarning, 1002);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_Information()
		{
			// Arrange
			var callResult = EventViewerHelper.SetEventLog(source, log, message, typeInformation, 1003);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, message, typeInformation, 1003);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_SuccessAudit()
		{
			// Arrange
			var callResult = EventViewerHelper.SetEventLog(source, log, message, typeSuccessAudit, 1004);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, message, typeSuccessAudit, 1004);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_FailureAudit()
		{
			// Arrange
			var callResult = EventViewerHelper.SetEventLog(source, log, message, typeFailureAudit, 1005);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, message, typeFailureAudit, 1005);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_DefaultCondition()
		{
			// Arrange
			const byte typeDefaultCondition = 32;
			var callResult = EventViewerHelper.SetEventLog(source, log, message, typeDefaultCondition, 1006);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, message, typeInformation, 1006);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_SetEventLog_SourceDoesNotExit()
		{
			// Arrange
			const string smartIT = "SmartIT.Library.2";
			var callResult = EventViewerHelper.SetEventLog(source, smartIT, message, typeInformation, 1007);

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, smartIT, message, typeInformation, 1007);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public void Validate_GetEventLog_EventDoesNotExit()
		{
			// Arrange

			// Act
			var result = EventViewerHelper.GetEventLog(machineName, source, "Eduardo", typeInformation, 99999);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(result, Is.Null);
			});
		}

		[Test]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public async Task Validate_SetEventLogAsync_Error()
		{
			// Arrange
			var callResult = await EventViewerHelper.SetEventLogAsync(source, log, message, typeError, 1010);

			// Act
			var result = await EventViewerHelper.GetEventLogAsync(machineName, source, message, typeError, 1010);

			Assert.Multiple(() =>
			{
				// Assert
				Assert.That(callResult, Is.EqualTo(0));
				Assert.That(result, Is.Not.Null);
			});
		}
	}
}
