namespace SmartIT.Library.Tests.Utilities
{
	using SmartIT.Library.Utilities;
	using System.ComponentModel;

	[TestFixture]
	public class EnumToListTests
	{
		protected enum EnumBasic
		{
			None = 0,
			Value1,
			Value2,
			Value3
		}

		protected enum EnumBoolean
		{
			False = 0,
			True
		}

		protected enum EnumWithDescriptionAttribute
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

		protected enum EnumWithStringValueAttribute
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

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test]
		public void Validate_GetList()
		{
			// Arrange

			// Act
			var result = EnumToList.GetList<EnumBasic>();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Count.GreaterThan(0));
		}

		[Test]
		public void Validate_GetDescriptionList()
		{
			// Arrange

			// Act
			var result = EnumToList.GetDescriptionList<EnumWithDescriptionAttribute>();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Count.GreaterThan(0));
		}

		[Test]
		public void Validate_GetDescriptionBooleanList()
		{
			// Arrange

			// Act
			var result = EnumToList.GetDescriptionBooleanList<EnumBoolean>();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Count.GreaterThan(0));
		}

		[Test]
		public void Validate_GetEnumDescriptionIntegerValue()
		{
			// Arrange

			// Act
			var none = EnumToList.GetEnumDescription((EnumWithDescriptionAttribute)0);
			var value1 = EnumToList.GetEnumDescription((EnumWithDescriptionAttribute)1);
			var value2 = EnumToList.GetEnumDescription((EnumWithDescriptionAttribute)2);
			var value3 = EnumToList.GetEnumDescription((EnumWithDescriptionAttribute)3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(none, Is.Not.Null);
				Assert.That(value1, Is.Not.Null);
				Assert.That(value2, Is.Not.Null);
				Assert.That(value3, Is.Not.Null);

				// Values
				Assert.That(none, Is.EqualTo("None"));
				Assert.That(value1, Is.EqualTo("Value 1"));
				Assert.That(value2, Is.EqualTo("Value 2"));
				Assert.That(value3, Is.EqualTo("Value 3"));
			});
		}

		[Test]
		public void Validate_GetEnumDescriptionEnumElement()
		{
			// Arrange

			// Act
			var none = EnumToList.GetEnumDescription(EnumWithDescriptionAttribute.None);
			var value1 = EnumToList.GetEnumDescription(EnumWithDescriptionAttribute.Value1);
			var value2 = EnumToList.GetEnumDescription(EnumWithDescriptionAttribute.Value2);
			var value3 = EnumToList.GetEnumDescription(EnumWithDescriptionAttribute.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(none, Is.Not.Null);
				Assert.That(value1, Is.Not.Null);
				Assert.That(value2, Is.Not.Null);
				Assert.That(value3, Is.Not.Null);

				// Values
				Assert.That(none, Is.EqualTo("None"));
				Assert.That(value1, Is.EqualTo("Value 1"));
				Assert.That(value2, Is.EqualTo("Value 2"));
				Assert.That(value3, Is.EqualTo("Value 3"));
			});
		}

		[Test]
		public void Validate_GetEnumStringValueIntegerValue()
		{
			// Arrange

			// Act
			var none = EnumToList.GetEnumStringValue((EnumWithStringValueAttribute)0);
			var value1 = EnumToList.GetEnumStringValue((EnumWithStringValueAttribute)1);
			var value2 = EnumToList.GetEnumStringValue((EnumWithStringValueAttribute)2);
			var value3 = EnumToList.GetEnumStringValue((EnumWithStringValueAttribute)3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(none, Is.Not.Null);
				Assert.That(value1, Is.Not.Null);
				Assert.That(value2, Is.Not.Null);
				Assert.That(value3, Is.Not.Null);

				// Values
				Assert.That(none, Is.EqualTo("None"));
				Assert.That(value1, Is.EqualTo("Value 1"));
				Assert.That(value2, Is.EqualTo("Value 2"));
				Assert.That(value3, Is.EqualTo("Value 3"));
			});
		}

		[Test]
		public void Validate_GetEnumStringValueEnumElement()
		{
			// Arrange

			// Act
			var none = EnumToList.GetEnumStringValue(EnumWithStringValueAttribute.None);
			var value1 = EnumToList.GetEnumStringValue(EnumWithStringValueAttribute.Value1);
			var value2 = EnumToList.GetEnumStringValue(EnumWithStringValueAttribute.Value2);
			var value3 = EnumToList.GetEnumStringValue(EnumWithStringValueAttribute.Value3);

			// Assert
			Assert.Multiple(() =>
			{
				// Nullability
				Assert.That(none, Is.Not.Null);
				Assert.That(value1, Is.Not.Null);
				Assert.That(value2, Is.Not.Null);
				Assert.That(value3, Is.Not.Null);

				// Values
				Assert.That(none, Is.EqualTo("None"));
				Assert.That(value1, Is.EqualTo("Value 1"));
				Assert.That(value2, Is.EqualTo("Value 2"));
				Assert.That(value3, Is.EqualTo("Value 3"));
			});
		}
	}
}
