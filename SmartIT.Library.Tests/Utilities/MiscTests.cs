namespace SmartIT.Library.Tests.Utilities
{
	using SmartIT.Library.Utilities;
	using System.ComponentModel;

	[TestFixture]
	public class MiscTests
	{
		protected const string limitStringTestCase = "Eduardo Claudio Nicacio";
		protected const string specialCharsString = "aâãáÁeêéÉiîíÍoôõóÓuûúÚ";
		protected DateTime constantDateTime = new(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		protected DateTime constantDateTime2 = new(2001, 9, 9, 1, 46, 40, DateTimeKind.Utc); // valid date for 1G seconds.

		public enum EnumBasic
		{
			None = 0,
			Value1,
			Value2,
			Value3
		}

		public enum EnumWithDescriptionAttribute
		{
			[Description("None")]
			None = 0,
			[Description("Value 1")]
			Value1,
			[Description("Value 2")]
			Value2,
			[Description("Value 3")]
			Value3
		}

		public enum EnumWithStringValueAttribute
		{
			[StringValue("None")]
			None = 0,
			[StringValue("Value 1")]
			Value1,
			[StringValue("Value 2")]
			Value2,
			[StringValue("Value 3")]
			Value3
		}

		public class MockedClassForTests
		{
			public int Id { get; set; }
			public string Name { get; set; } = string.Empty;
			public DateTime CreationDate { get; set; }
		}

		public struct MockedStructForTests
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public DateTime CreationDate { get; set; }
		}

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		#region LimiString tests

		[Test]
		public void Validate_LimitString_TestCase1()
		{
			// Arrange

			// Act
			var result = Misc.LimitString(limitStringTestCase, 0);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo("..."));
		}

		[Test]
		public void Validate_LimitString_TestCase2()
		{
			// Arrange

			// Act
			var result = Misc.LimitString(limitStringTestCase, 7);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(limitStringTestCase[..7] + "..."));
		}

		[Test]
		public void Validate_LimitString_TestCase3()
		{
			// Arrange

			// Act
			var result = Misc.LimitString(limitStringTestCase, 32);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(limitStringTestCase));
		}

		[Test(ExpectedResult = "...")]
		public async Task<string> Validate_LimitStringAsync_TestCase1()
		{
			// Arrange

			// Act
			var result = await Misc.LimitStringAsync(limitStringTestCase, 0);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo("..."));

			return result;
		}

		[Test(ExpectedResult = "Eduardo...")]
		public async Task<string> Validate_LimitStringAsync_TestCase2()
		{
			// Arrange

			// Act
			var result = await Misc.LimitStringAsync(limitStringTestCase, 7);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(limitStringTestCase[..7] + "..."));

			return result;
		}

		[Test(ExpectedResult = limitStringTestCase)]
		public async Task<string> Validate_LimitStringAsync_TestCase3()
		{
			// Arrange

			// Act
			var result = await Misc.LimitStringAsync(limitStringTestCase, 32);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(limitStringTestCase));

			return result;
		}

		#endregion

		#region GetEnumDescription tests

		[Test]
		public void Validate_GetEnumDescription_EnumBasic()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumDescription(EnumBasic.None);
			var result2 = Misc.GetEnumDescription(EnumBasic.Value1);
			var result3 = Misc.GetEnumDescription(EnumBasic.Value2);
			var result4 = Misc.GetEnumDescription(EnumBasic.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value1"));
				Assert.That(result3, Is.EqualTo("Value2"));
				Assert.That(result4, Is.EqualTo("Value3"));
			});
		}

		[Test]
		public void Validate_GetEnumDescription_EnumWithDescriptionAttribute()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumDescription(EnumWithDescriptionAttribute.None);
			var result2 = Misc.GetEnumDescription(EnumWithDescriptionAttribute.Value1);
			var result3 = Misc.GetEnumDescription(EnumWithDescriptionAttribute.Value2);
			var result4 = Misc.GetEnumDescription(EnumWithDescriptionAttribute.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value 1"));
				Assert.That(result3, Is.EqualTo("Value 2"));
				Assert.That(result4, Is.EqualTo("Value 3"));
			});
		}

		[Test]
		public async Task Validate_GetEnumDescriptionAsync_EnumBasic()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumDescriptionAsync(EnumBasic.None);
			var result2 = await Misc.GetEnumDescriptionAsync(EnumBasic.Value1);
			var result3 = await Misc.GetEnumDescriptionAsync(EnumBasic.Value2);
			var result4 = await Misc.GetEnumDescriptionAsync(EnumBasic.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value1"));
				Assert.That(result3, Is.EqualTo("Value2"));
				Assert.That(result4, Is.EqualTo("Value3"));
			});
		}

		[Test]
		public async Task Validate_GetEnumDescriptionAsync_EnumWithDescriptionAttribute()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumDescriptionAsync(EnumWithDescriptionAttribute.None);
			var result2 = await Misc.GetEnumDescriptionAsync(EnumWithDescriptionAttribute.Value1);
			var result3 = await Misc.GetEnumDescriptionAsync(EnumWithDescriptionAttribute.Value2);
			var result4 = await Misc.GetEnumDescriptionAsync(EnumWithDescriptionAttribute.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value 1"));
				Assert.That(result3, Is.EqualTo("Value 2"));
				Assert.That(result4, Is.EqualTo("Value 3"));
			});
		}

		#endregion

		#region GetEnumStringValue tests

		[Test]
		public void Validate_GetEnumStringValue_EnumBasic()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumStringValue(EnumBasic.None);
			var result2 = Misc.GetEnumStringValue(EnumBasic.Value1);
			var result3 = Misc.GetEnumStringValue(EnumBasic.Value2);
			var result4 = Misc.GetEnumStringValue(EnumBasic.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value1"));
				Assert.That(result3, Is.EqualTo("Value2"));
				Assert.That(result4, Is.EqualTo("Value3"));
			});
		}

		[Test]
		public void Validate_GetEnumStringValue_EnumWithStringValueAttribute()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumStringValue(EnumWithStringValueAttribute.None);
			var result2 = Misc.GetEnumStringValue(EnumWithStringValueAttribute.Value1);
			var result3 = Misc.GetEnumStringValue(EnumWithStringValueAttribute.Value2);
			var result4 = Misc.GetEnumStringValue(EnumWithStringValueAttribute.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value 1"));
				Assert.That(result3, Is.EqualTo("Value 2"));
				Assert.That(result4, Is.EqualTo("Value 3"));
			});
		}

		[Test]
		public async Task Validate_GetEnumStringValueAsync_EnumBasic()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumStringValueAsync(EnumBasic.None);
			var result2 = await Misc.GetEnumStringValueAsync(EnumBasic.Value1);
			var result3 = await Misc.GetEnumStringValueAsync(EnumBasic.Value2);
			var result4 = await Misc.GetEnumStringValueAsync(EnumBasic.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value1"));
				Assert.That(result3, Is.EqualTo("Value2"));
				Assert.That(result4, Is.EqualTo("Value3"));
			});
		}

		[Test]
		public async Task Validate_GetEnumStringValueAsync_EnumWithStringValueAttribute()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumStringValueAsync(EnumWithStringValueAttribute.None);
			var result2 = await Misc.GetEnumStringValueAsync(EnumWithStringValueAttribute.Value1);
			var result3 = await Misc.GetEnumStringValueAsync(EnumWithStringValueAttribute.Value2);
			var result4 = await Misc.GetEnumStringValueAsync(EnumWithStringValueAttribute.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo("None"));
				Assert.That(result2, Is.EqualTo("Value 1"));
				Assert.That(result3, Is.EqualTo("Value 2"));
				Assert.That(result4, Is.EqualTo("Value 3"));
			});
		}

		#endregion

		#region GetEnumValue tests

		[Test]
		public void Validate_GetEnumValue_EnumBasic()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumValue("None", typeof(EnumBasic));
			var result2 = Misc.GetEnumValue("Value1", typeof(EnumBasic));
			var result3 = Misc.GetEnumValue("Value2", typeof(EnumBasic));
			var result4 = Misc.GetEnumValue("Value3", typeof(EnumBasic));

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Null);
				Assert.That(result2, Is.Null);
				Assert.That(result3, Is.Null);
				Assert.That(result4, Is.Null);
			});
		}

		[Test]
		public void Validate_GetEnumValue_EnumWithDescriptionAttribute()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumValue("None", typeof(EnumWithDescriptionAttribute));
			var result2 = Misc.GetEnumValue("Value 1", typeof(EnumWithDescriptionAttribute));
			var result3 = Misc.GetEnumValue("Value 2", typeof(EnumWithDescriptionAttribute));
			var result4 = Misc.GetEnumValue("Value 3", typeof(EnumWithDescriptionAttribute));

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo(EnumWithDescriptionAttribute.None));
				Assert.That(result2, Is.EqualTo(EnumWithDescriptionAttribute.Value1));
				Assert.That(result3, Is.EqualTo(EnumWithDescriptionAttribute.Value2));
				Assert.That(result4, Is.EqualTo(EnumWithDescriptionAttribute.Value3));
			});
		}

		[Test]
		public void Validate_GetEnumValue_EnumWithStringValueAttribute()
		{
			// Arrange

			// Act
			var result1 = Misc.GetEnumValue("None", typeof(EnumWithStringValueAttribute));
			var result2 = Misc.GetEnumValue("Value 1", typeof(EnumWithStringValueAttribute));
			var result3 = Misc.GetEnumValue("Value 2", typeof(EnumWithStringValueAttribute));
			var result4 = Misc.GetEnumValue("Value 3", typeof(EnumWithStringValueAttribute));

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo(EnumWithStringValueAttribute.None));
				Assert.That(result2, Is.EqualTo(EnumWithStringValueAttribute.Value1));
				Assert.That(result3, Is.EqualTo(EnumWithStringValueAttribute.Value2));
				Assert.That(result4, Is.EqualTo(EnumWithStringValueAttribute.Value3));
			});
		}

		[Test]
		public async Task Validate_GetEnumValueAsync_EnumBasic()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumValueAsync("None", typeof(EnumBasic));
			var result2 = await Misc.GetEnumValueAsync("Value1", typeof(EnumBasic));
			var result3 = await Misc.GetEnumValueAsync("Value2", typeof(EnumBasic));
			var result4 = await Misc.GetEnumValueAsync("Value3", typeof(EnumBasic));

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Null);
				Assert.That(result2, Is.Null);
				Assert.That(result3, Is.Null);
				Assert.That(result4, Is.Null);
			});
		}

		[Test]
		public async Task Validate_GetEnumValueAsync_EnumWithDescriptionAttribute()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumValueAsync("None", typeof(EnumWithDescriptionAttribute));
			var result2 = await Misc.GetEnumValueAsync("Value 1", typeof(EnumWithDescriptionAttribute));
			var result3 = await Misc.GetEnumValueAsync("Value 2", typeof(EnumWithDescriptionAttribute));
			var result4 = await Misc.GetEnumValueAsync("Value 3", typeof(EnumWithDescriptionAttribute));

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo(EnumWithDescriptionAttribute.None));
				Assert.That(result2, Is.EqualTo(EnumWithDescriptionAttribute.Value1));
				Assert.That(result3, Is.EqualTo(EnumWithDescriptionAttribute.Value2));
				Assert.That(result4, Is.EqualTo(EnumWithDescriptionAttribute.Value3));
			});
		}

		[Test]
		public async Task Validate_GetEnumValueAsync_EnumWithStringValueAttribute()
		{
			// Arrange

			// Act
			var result1 = await Misc.GetEnumValueAsync("None", typeof(EnumWithStringValueAttribute));
			var result2 = await Misc.GetEnumValueAsync("Value 1", typeof(EnumWithStringValueAttribute));
			var result3 = await Misc.GetEnumValueAsync("Value 2", typeof(EnumWithStringValueAttribute));
			var result4 = await Misc.GetEnumValueAsync("Value 3", typeof(EnumWithStringValueAttribute));

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(result1, Is.Not.Null);
				Assert.That(result2, Is.Not.Null);
				Assert.That(result3, Is.Not.Null);
				Assert.That(result4, Is.Not.Null);

				// Values
				Assert.That(result1, Is.EqualTo(EnumWithStringValueAttribute.None));
				Assert.That(result2, Is.EqualTo(EnumWithStringValueAttribute.Value1));
				Assert.That(result3, Is.EqualTo(EnumWithStringValueAttribute.Value2));
				Assert.That(result4, Is.EqualTo(EnumWithStringValueAttribute.Value3));
			});
		}

		#endregion

		#region GetBooleanToStatus tests

		[Test]
		public void Validate_GetBooleanToStatus()
		{
			// Arrange
			bool trueValue = true;
			bool falseValue = false;

			// Act
			var resultTrue = Misc.GetBooleanToStatus(trueValue);
			var resultFalse = Misc.GetBooleanToStatus(falseValue);
			var resultNull = Misc.GetBooleanToStatus(null);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(resultTrue, Is.Not.Null);
				Assert.That(resultFalse, Is.Not.Null);
				Assert.That(resultNull, Is.Null);

				Assert.That(resultTrue, Is.EqualTo("Active"));
				Assert.That(resultFalse, Is.EqualTo("Inactive"));
			});
		}

		[Test]
		public async Task Validate_GetBooleanToStatusAsync()
		{
			// Arrange
			const bool trueValue = true;
			const bool falseValue = false;

			// Act
			var resultTrue = await Misc.GetBooleanToStatusAsync(trueValue);
			var resultFalse = await Misc.GetBooleanToStatusAsync(falseValue);
			var resultNull = await Misc.GetBooleanToStatusAsync(null);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(resultTrue, Is.Not.Null);
				Assert.That(resultFalse, Is.Not.Null);
				Assert.That(resultNull, Is.Null);

				Assert.That(resultTrue, Is.EqualTo("Active"));
				Assert.That(resultFalse, Is.EqualTo("Inactive"));
			});
		}

		#endregion

		#region GetStatusToBoolean tests

		[Test]
		public void Validate_GetStatusToBoolean()
		{
			// Arrange
			const string trueValue = "Active";
			const string falseValue = "Inactive";

			// Act
			var resultTrue = Misc.GetStatusToBoolean(trueValue);
			var resultFalse = Misc.GetStatusToBoolean(falseValue);
			var resultEmpty = Misc.GetStatusToBoolean(string.Empty);
			var resultNull = Misc.GetStatusToBoolean(null);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(resultTrue, Is.True);
				Assert.That(resultFalse, Is.False);
				Assert.That(resultEmpty, Is.False);
				Assert.That(resultNull, Is.False);
			});
		}

		[Test]
		public async Task Validate_GetStatusToBooleanAsync()
		{
			// Arrange
			const string trueValue = "Active";
			const string falseValue = "Inactive";

			// Act
			var resultTrue = await Misc.GetStatusToBooleanAsync(trueValue);
			var resultFalse = await Misc.GetStatusToBooleanAsync(falseValue);
			var resultEmpty = await Misc.GetStatusToBooleanAsync(string.Empty);
			var resultNull = await Misc.GetStatusToBooleanAsync(null);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(resultTrue, Is.True);
				Assert.That(resultFalse, Is.False);
				Assert.That(resultEmpty, Is.False);
				Assert.That(resultNull, Is.False);
			});
		}

		#endregion

		#region RemoveAccents tests

		[Test]
		public void Validate_RemoveAccents()
		{
			// Arrange

			// Act
			var result = Misc.RemoveAccents(specialCharsString);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result, Is.EqualTo("aaaaAeeeEiiiIooooOuuuU"));
			});
		}

		[Test]
		public async Task Validate_RemoveAccentsAsync()
		{
			// Arrange

			// Act
			var result = await Misc.RemoveAccentsAsync(specialCharsString);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result, Is.EqualTo("aaaaAeeeEiiiIooooOuuuU"));
			});
		}

		#endregion

		#region DateTimeToUnixTimestamp and UnixTimestampToDateTime tests

		[Test]
		public void Validate_DateTimeToUnixTimestamp()
		{
			// Arrange

			// Act
			var result = Misc.DateTimeToUnixTimestamp(constantDateTime);

			// Assert
			Assert.That(result, Is.Not.NaN);
			Assert.That(result, Is.EqualTo(946706400D));
		}

		[Test]
		public async Task Validate_DateTimeToUnixTimestampAsync()
		{
			// Arrange

			// Act
			var result = await Misc.DateTimeToUnixTimestampAsync(constantDateTime);

			// Assert
			Assert.That(result, Is.Not.NaN);
			Assert.That(result, Is.EqualTo(946706400D));
		}

		[Test]
		public void Validate_UnixTimestampToDateTime()
		{
			// Arrange

			// Act
			var result = Misc.UnixTimestampToDateTime(1000000000D);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.GreaterThan(DateTime.MinValue));
				Assert.That(result, Is.LessThan(DateTime.MaxValue));
				Assert.That(result, Is.EqualTo(constantDateTime2));
			});
		}

		[Test]
		public async Task Validate_UnixTimestampToDateTimeAsync()
		{
			// Arrange

			// Act
			var result = await Misc.UnixTimestampToDateTimeAsync(1000000000D);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.GreaterThan(DateTime.MinValue));
				Assert.That(result, Is.LessThan(DateTime.MaxValue));
				Assert.That(result, Is.EqualTo(constantDateTime2));
			});
		}

		#endregion

		#region GetClassProperties tests

		[Test]
		public void Validate_GetClassProperties_FromStruct()
		{
			// Arrange

			// Act
			var result = Misc.GetClassProperties(new MockedStructForTests());

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result, Does.Contain("MockedStructForTests"));
				Assert.That(result, Does.Contain("Id"));
				Assert.That(result, Does.Contain("Name"));
				Assert.That(result, Does.Contain("CreationDate"));
			});
		}

		[Test]
		public void Validate_GetClassProperties_FromClass()
		{
			// Arrange

			// Act
			var result = Misc.GetClassProperties(new MockedClassForTests());

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result, Does.Contain("MockedClassForTests"));
				Assert.That(result, Does.Contain("Id"));
				Assert.That(result, Does.Contain("Name"));
				Assert.That(result, Does.Contain("CreationDate"));
			});
		}

		[Test]
		public async Task Validate_GetClassPropertiesAsync_FromStruct()
		{
			// Arrange

			// Act
			var result = await Misc.GetClassPropertiesAsync(new MockedStructForTests());

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result, Does.Contain("MockedStructForTests"));
				Assert.That(result, Does.Contain("Id"));
				Assert.That(result, Does.Contain("Name"));
				Assert.That(result, Does.Contain("CreationDate"));
			});
		}

		[Test]
		public async Task Validate_GetClassPropertiesAsync_FromClass()
		{
			// Arrange

			// Act
			var result = await Misc.GetClassPropertiesAsync(new MockedClassForTests());

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result, Does.Contain("MockedClassForTests"));
				Assert.That(result, Does.Contain("Id"));
				Assert.That(result, Does.Contain("Name"));
				Assert.That(result, Does.Contain("CreationDate"));
			});
		}

		#endregion

		#region ToAge tests

		[Test]
		public void Validate_ToAge_WithBirthDate()
		{
			// Arrange
			var birthdate = new DateTime(1978, 12, 2, 13, 40, 00, DateTimeKind.Utc);
			var dateDiff = DateTime.Today.Subtract(birthdate).TotalDays / 365;

			// Act
			var result = Misc.ToAge(birthdate);

			// Assert
			Assert.That(result, Is.EqualTo(Math.Truncate(dateDiff)));
		}

		[Test]
		public void Validate_ToAge_WithBirthDateAndReferenceDate()
		{
			// Arrange
			var birthdate = new DateTime(1978, 12, 2, 13, 40, 00, DateTimeKind.Utc);
			var dateDiff = DateTime.Today.Subtract(birthdate).TotalDays / 365;

			// Act
			var result1 = Misc.ToAge(birthdate, DateTime.Today);
			var result2 = Misc.ToAge(birthdate, birthdate.AddYears(40));

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.EqualTo(Math.Truncate(dateDiff)));
				Assert.That(result2, Is.EqualTo(40));
			});
		}

		[Test]
		public async Task Validate_ToAgeAsync_WithBirthDate()
		{
			// Arrange
			var birthdate = new DateTime(1978, 12, 2, 13, 40, 00, DateTimeKind.Utc);
			var dateDiff = DateTime.Today.Subtract(birthdate).TotalDays / 365;

			// Act
			var result = await Misc.ToAgeAsync(birthdate);

			// Assert
			Assert.That(result, Is.EqualTo(Math.Truncate(dateDiff)));
		}

		[Test]
		public async Task Validate_ToAgeAsync_WithBirthDateAndReferenceDate()
		{
			// Arrange
			var birthdate = new DateTime(1978, 12, 2, 13, 40, 00, DateTimeKind.Utc);
			var dateDiff = DateTime.Today.Subtract(birthdate).TotalDays / 365;

			// Act
			var result1 = await Misc.ToAgeAsync(birthdate, DateTime.Today);
			var result2 = await Misc.ToAgeAsync(birthdate, birthdate.AddYears(40));

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result1, Is.EqualTo(Math.Truncate(dateDiff)));
				Assert.That(result2, Is.EqualTo(40));
			});
		}

		#endregion

		#region GenericComparer tests

		[Test]
		public void Validate_GenericComparer_ForClass()
		{
			// Arrange
			var class1 = new MockedClassForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
			var class2 = new MockedClassForTests() { Id = 2, Name = "Name2", CreationDate = new DateTime(1981, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			// Needs to be identical to class1
			var class3 = new MockedClassForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			var idComparerAsc = new Misc.GenericComparer<MockedClassForTests>("Id", Misc.SortDirection.Ascending);
			var idComparerDesc = new Misc.GenericComparer<MockedClassForTests>("Id", Misc.SortDirection.Descending);

			var nameComparerAsc = new Misc.GenericComparer<MockedClassForTests>("Name", Misc.SortDirection.Ascending);
			var nameComparerDesc = new Misc.GenericComparer<MockedClassForTests>("Name", Misc.SortDirection.Descending);

			var dateComparerAsc = new Misc.GenericComparer<MockedClassForTests>("CreationDate", Misc.SortDirection.Ascending);
			var dateComparerDesc = new Misc.GenericComparer<MockedClassForTests>("CreationDate", Misc.SortDirection.Descending);

			// Act
			var result1 = idComparerAsc.Compare(class1, class2);
			var result2 = idComparerDesc.Compare(class1, class2);

			var result3 = nameComparerAsc.Compare(class1, class2);
			var result4 = nameComparerDesc.Compare(class1, class2);

			var result5 = dateComparerAsc.Compare(class1, class2);
			var result6 = dateComparerDesc.Compare(class1, class2);

			// Compares classes with the same values
			var result7 = idComparerAsc.Compare(class1, class3);
			var result8 = nameComparerAsc.Compare(class1, class3);
			var result9 = dateComparerAsc.Compare(class1, class3);

			// Assert
			Assert.Multiple(() =>
			{
				// Id check
				Assert.That(result1, Is.EqualTo(-1));
				Assert.That(result2, Is.EqualTo(1));

				// Name check
				Assert.That(result3, Is.EqualTo(-1));
				Assert.That(result4, Is.EqualTo(1));

				// CreationDate check
				Assert.That(result5, Is.EqualTo(-1));
				Assert.That(result6, Is.EqualTo(1));

				// Equality check
				Assert.That(result7, Is.EqualTo(0));
				Assert.That(result8, Is.EqualTo(0));
				Assert.That(result9, Is.EqualTo(0));
			});
		}

		[Test]
		public void Validate_GenericComparer_ForStruct()
		{
			// Arrange
			var struct1 = new MockedStructForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
			var struct2 = new MockedStructForTests() { Id = 2, Name = "Name2", CreationDate = new DateTime(1981, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			// Needs to be identical to struct1
			var struct3 = new MockedStructForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			var idComparerAsc = new Misc.GenericComparer<MockedStructForTests>("Id", Misc.SortDirection.Ascending);
			var idComparerDesc = new Misc.GenericComparer<MockedStructForTests>("Id", Misc.SortDirection.Descending);

			var nameComparerAsc = new Misc.GenericComparer<MockedStructForTests>("Name", Misc.SortDirection.Ascending);
			var nameComparerDesc = new Misc.GenericComparer<MockedStructForTests>("Name", Misc.SortDirection.Descending);

			var dateComparerAsc = new Misc.GenericComparer<MockedStructForTests>("CreationDate", Misc.SortDirection.Ascending);
			var dateComparerDesc = new Misc.GenericComparer<MockedStructForTests>("CreationDate", Misc.SortDirection.Descending);

			// Act
			var result1 = idComparerAsc.Compare(struct1, struct2);
			var result2 = idComparerDesc.Compare(struct1, struct2);

			var result3 = nameComparerAsc.Compare(struct1, struct2);
			var result4 = nameComparerDesc.Compare(struct1, struct2);

			var result5 = dateComparerAsc.Compare(struct1, struct2);
			var result6 = dateComparerDesc.Compare(struct1, struct2);

			// Compares structs with the same values
			var result7 = idComparerAsc.Compare(struct1, struct3);
			var result8 = nameComparerAsc.Compare(struct1, struct3);
			var result9 = dateComparerAsc.Compare(struct1, struct3);

			// Assert
			Assert.Multiple(() =>
			{
				// Id check
				Assert.That(result1, Is.EqualTo(-1));
				Assert.That(result2, Is.EqualTo(1));

				// Name check
				Assert.That(result3, Is.EqualTo(-1));
				Assert.That(result4, Is.EqualTo(1));

				// CreationDate check
				Assert.That(result5, Is.EqualTo(-1));
				Assert.That(result6, Is.EqualTo(1));

				// Equality check
				Assert.That(result7, Is.EqualTo(0));
				Assert.That(result8, Is.EqualTo(0));
				Assert.That(result9, Is.EqualTo(0));
			});
		}

		[Test]
		public void Validate_GenericComparer_ForClass_EmptyConstructor()
		{
			// Arrange
			var class1 = new MockedClassForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
			var class2 = new MockedClassForTests() { Id = 2, Name = "Name2", CreationDate = new DateTime(1981, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			var idComparerAsc = new Misc.GenericComparer<MockedClassForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Ascending };
			var idComparerDesc = new Misc.GenericComparer<MockedClassForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Descending };

			// Act
			var result1 = idComparerAsc.Compare(class1, class2);
			var result2 = idComparerDesc.Compare(class1, class2);

			// Assert
			Assert.Multiple(() =>
			{
				// Id check
				Assert.That(result1, Is.EqualTo(-1));
				Assert.That(result2, Is.EqualTo(1));
			});
		}

		[Test]
		public void Validate_GenericComparer_ForStruct_EmptyConstructor()
		{
			// Arrange
			var struct1 = new MockedStructForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
			var struct2 = new MockedStructForTests() { Id = 2, Name = "Name2", CreationDate = new DateTime(1981, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			var idComparerAsc = new Misc.GenericComparer<MockedStructForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Ascending };
			var idComparerDesc = new Misc.GenericComparer<MockedStructForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Descending };

			// Act
			var result1 = idComparerAsc.Compare(struct1, struct2);
			var result2 = idComparerDesc.Compare(struct1, struct2);

			// Assert
			Assert.Multiple(() =>
			{
				// Id check
				Assert.That(result1, Is.EqualTo(-1));
				Assert.That(result2, Is.EqualTo(1));
			});
		}

		[Test]
		public async Task Validate_GenericComparerAsync_ForClass()
		{
			// Arrange
			var class1 = new MockedClassForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
			var class2 = new MockedClassForTests() { Id = 2, Name = "Name2", CreationDate = new DateTime(1981, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			var idComparerAsc = new Misc.GenericComparer<MockedClassForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Ascending };
			var idComparerDesc = new Misc.GenericComparer<MockedClassForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Descending };

			// Act
			var result1 = await idComparerAsc.CompareAsync(class1, class2);
			var result2 = await idComparerDesc.CompareAsync(class1, class2);

			// Assert
			Assert.Multiple(() =>
			{
				// Id check
				Assert.That(result1, Is.EqualTo(-1));
				Assert.That(result2, Is.EqualTo(1));
			});
		}

		[Test]
		public async Task Validate_GenericComparerAsync_ForStruct()
		{
			// Arrange
			var struct1 = new MockedStructForTests() { Id = 1, Name = "Name1", CreationDate = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
			var struct2 = new MockedStructForTests() { Id = 2, Name = "Name2", CreationDate = new DateTime(1981, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

			var idComparerAsc = new Misc.GenericComparer<MockedStructForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Ascending };
			var idComparerDesc = new Misc.GenericComparer<MockedStructForTests>() { GenericSortExpression = "Id", GenericSortDirection = Misc.SortDirection.Descending };

			// Act
			var result1 = await idComparerAsc.CompareAsync(struct1, struct2);
			var result2 = await idComparerDesc.CompareAsync(struct1, struct2);

			// Assert
			Assert.Multiple(() =>
			{
				// Id check
				Assert.That(result1, Is.EqualTo(-1));
				Assert.That(result2, Is.EqualTo(1));
			});
		}

		#endregion
	}
}
