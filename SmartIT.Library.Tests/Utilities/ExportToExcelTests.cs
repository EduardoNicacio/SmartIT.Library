using SmartIT.Library.Utilities;
using System.Data;

namespace SmartIT.Library.Tests.Utilities
{
	[TestFixture]
	internal class ExportToExcelTests
	{
		internal class StubExcelClass
		{
			public StubExcelClass() { }
			public int Id { get; set; }
			public string Name { get; set; } = string.Empty;
			public double Height { get; set; }
			public DateTime BirthDate { get; set; }
		}

		internal List<StubExcelClass> dataList = [];

		[SetUp]
		public void Setup()
		{
			dataList.Add(new StubExcelClass { Id = 1, Name = "Eduardo", Height = 1.66, BirthDate = new DateTime(1978, 12, 2, 13, 0, 0, DateTimeKind.Utc) });
			dataList.Add(new StubExcelClass { Id = 2, Name = "Claudio", Height = 1.67, BirthDate = new DateTime(1978, 12, 2, 13, 41, 0, DateTimeKind.Utc) });
			dataList.Add(new StubExcelClass { Id = 3, Name = "Nicacio", Height = 1.68, BirthDate = new DateTime(1978, 12, 2, 13, 41, 47, DateTimeKind.Utc) });
		}

		[Test, Order(1)]
		public void Validate_ExportToExcel_ListToDataTable()
		{
			// Arrange
			DataTable result;

			// Act
			result = ExportToExcel.ListToDataTable(dataList);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Rows, Has.Count.GreaterThan(0));
			});
		}

		[Test, Order(2)]
		public void Validate_ExportToExcel_CreateExcelDocument_FromList()
		{
			// Arrange
			string currDateTime = DateTime.Now.ToString("MMddyyyy-HHmmss");
			string pathExcel = $"E:\\SRC\\ECNSoft\\00. SmartIT.Library\\SmartIT.Library\\SmartIT.Library.Tests\\spreadsheets\\CreateExcelDocument_FromList_{currDateTime}.xlsx";

			// Act
			bool result = ExportToExcel.CreateExcelDocument(dataList, pathExcel, "DataList");

			// Assert
			Assert.That(result, Is.True);
		}

		[Test, Order(3)]
		public void Validate_ExportToExcel_CreateExcelDocument_FromDataTable()
		{
			// Arrange
			string currDateTime = DateTime.Now.ToString("MMddyyyy-HHmmss");
			string pathExcel = $"E:\\SRC\\ECNSoft\\00. SmartIT.Library\\SmartIT.Library\\SmartIT.Library.Tests\\spreadsheets\\CreateExcelDocument_FromDataTable_{currDateTime}.xlsx";
			var dataTable = ExportToExcel.ListToDataTable(dataList);

			// Act
			bool result = ExportToExcel.CreateExcelDocument(dataTable, pathExcel);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test, Order(4)]
		public void Validate_ExportToExcel_CreateExcelDocument_FromDataSet()
		{
			// Arrange
			string currDateTime = DateTime.Now.ToString("MMddyyyy-HHmmss");
			string pathExcel = $"E:\\SRC\\ECNSoft\\00. SmartIT.Library\\SmartIT.Library\\SmartIT.Library.Tests\\spreadsheets\\CreateExcelDocument_FromDataSet_{currDateTime}.xlsx";
			var dataSet = new DataSet();
			dataSet.Tables.Add(ExportToExcel.ListToDataTable(dataList));
			dataSet.Tables[0].TableName = "DataList";

			// Act
			bool result = ExportToExcel.CreateExcelDocument(dataSet, pathExcel);

			// Assert
			Assert.That(result, Is.True);
		}
	}
}
