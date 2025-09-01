namespace SmartIT.Library.Data.Tests
{
	[TestFixture]
	internal class BizValidationExceptionTests
	{
		internal const string ErrorMessage = "Business Validation Error";
		internal List<string> Errors = [];

		[SetUp]
		public void Setup()
		{
			Errors.Clear();
			Errors.Add("The field 'Name' is mandatory.");
			Errors.Add("The field 'Username' is mandatory.");
			Errors.Add("The field 'Birth Date' is mandatory.");
			Errors.Add("'Birth Date' cannot be over 120 years ago.");
		}

		[Test, Order(1)]
		public void Validate_BizValidationException_Constructor1()
		{
			// Arrange

			// Act
			var result = new BizValidationException(ErrorMessage);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Message, Is.EqualTo(ErrorMessage));
			});
		}

		[Test, Order(2)]
		public void Validate_BizValidationException_Constructor2()
		{
			// Arrange

			// Act
			var result = new BizValidationException(Errors);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Message, Is.EqualTo(ErrorMessage));
				Assert.That(result.Errors, Has.Count.EqualTo(Errors.Count));
			});
		}

		[Test, Order(3)]
		public void Validate_BizValidationException_SetErrors()
		{
			// Arrange

			// Act
			var result = new BizValidationException(ErrorMessage)
			{
				Errors = Errors
			};

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Message, Is.EqualTo(ErrorMessage));
				Assert.That(result.Errors, Has.Count.EqualTo(Errors.Count));
			});
		}

		[Test, Order(4)]
		public void Validate_BizValidationException_GetErrors()
		{
			// Arrange

			// Act
			var result = new BizValidationException(Errors);
			var localErrors = result.Errors;

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Message, Is.EqualTo(ErrorMessage));
				Assert.That(localErrors, Has.Count.EqualTo(Errors.Count));
			});
		}

		[Test, Order(5)]
		public void Validate_BizValidationException_GetTextErrorMessage()
		{
			// Arrange

			// Act
			var result = new BizValidationException(Errors);
			var textErrorMessage = result.GetTextErrorMessage();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Errors, Has.Count.EqualTo(Errors.Count));
				Assert.That(textErrorMessage, Is.Not.Null);
			});
		}

		[Test, Order(6)]
		public void Validate_BizValidationException_GetHtmlErrorMessage()
		{
			// Arrange

			// Act
			var result = new BizValidationException(Errors);
			var htmlErrorMessage = result.GetHtmlErrorMessage();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Errors, Has.Count.EqualTo(Errors.Count));
				Assert.That(htmlErrorMessage, Is.Not.Null);
			});
		}
	}
}
