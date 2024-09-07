namespace SmartIT.Library.Data.Tests.DAL
{
	[TestFixture, SingleThreaded]
	internal class InvoiceBllTests
	{
		const string emptyXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><e/>";

		readonly InvoiceHelper.Invoice invoice1 = new()
		{
			Id = Guid.NewGuid(),
			Data = emptyXml,
			ItemCount = 5,
			CreationDate = DateTime.UtcNow,
			CancellationDate = null,
			IsCancelled = false,
			PageNumber = 1,
			CharFlag = 'A',
			PathLength = long.MaxValue,
			TotalPrice = 12.345678M,
			StarScore = 3.75F,
			Evaluations = short.MaxValue,
			SerialNumber = (long.MaxValue - int.MaxValue)
		};
		readonly InvoiceHelper.Invoice invoice2 = new()
		{
			Id = Guid.NewGuid(),
			Data = string.Empty,
			ItemCount = 10,
			CreationDate = DateTime.UtcNow.AddMinutes(-10),
			CancellationDate = null,
			IsCancelled = false,
			PageNumber = 1,
			CharFlag = 'B',
			PathLength = null,
			TotalPrice = 23.456789M,
			StarScore = 3.75F,
			Evaluations = short.MaxValue,
			SerialNumber = (long.MaxValue - short.MaxValue)
		};
		readonly InvoiceHelper.Invoice invoice3 = new()
		{
			Id = Guid.NewGuid(),
			Data = emptyXml,
			ItemCount = 20,
			CreationDate = DateTime.UtcNow.AddMinutes(-20),
			CancellationDate = null,
			IsCancelled = false,
			PageNumber = 1,
			CharFlag = 'C',
			PathLength = long.MaxValue,
			TotalPrice = 34.567890M,
			StarScore = 3.75F,
			Evaluations = short.MaxValue,
			SerialNumber = (long.MaxValue - byte.MaxValue)
		};

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test, Order(1)]
		public void T01_Validate_ValidateModel_DefaultObject()
		{
			// Arrange
			InvoiceHelper.Invoice defaultInvoice = default!;

			// Act
			var result = InvoiceHelper.InvoiceBll.ValidateModel(defaultInvoice);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test, Order(2)]
		public void T02_Validate_ValidateModel_EmptyObject()
		{
			// Arrange
			InvoiceHelper.Invoice emptyInvoice = new();

			// Act
			var result = InvoiceHelper.InvoiceBll.ValidateModel(emptyInvoice);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test, Order(3)]
		public void T03_Validate_ValidateModel_ValidObject()
		{
			// Arrange

			// Act
			var result = InvoiceHelper.InvoiceBll.ValidateModel(invoice1);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test, Order(4)]
		public void T04_Validate_Insert_EmptyObject()
		{
			// Arrange
			InvoiceHelper.Invoice emptyInvoice = default!;

			// Act
			var result = InvoiceHelper.InvoiceBll.Insert(emptyInvoice);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(5)]
		public void T05_Validate_Insert_UseCase1()
		{
			// Arrange

			// Act
			var result = InvoiceHelper.InvoiceBll.Insert(invoice1);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(6)]
		public void T06_Validate_Insert_UseCase2()
		{
			// Arrange

			// Act
			var result = InvoiceHelper.InvoiceBll.Insert(invoice2);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(7)]
		public void T07_Validate_Insert_UseCase3()
		{
			// Arrange

			// Act
			var result = InvoiceHelper.InvoiceBll.Insert(invoice3);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(8)]
		public void T08_Validate_SearchItem_EmptyObject()
		{
			// Arrange
			InvoiceHelper.Invoice emptyInvoice = default!;

			// Act
			var result = InvoiceHelper.InvoiceBll.SearchItem(emptyInvoice);

			// Assert
			Assert.That(result, Is.Null);
		}

		[Test, Order(9)]
		public void T09_Validate_SearchItem_ValidObject()
		{
			// Arrange

			// Act
			var result = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice { Id = invoice1.Id });

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result?.Id, Is.EqualTo(invoice1.Id));
				Assert.That(result?.CreationDate, Is.GreaterThan(DateTime.MinValue));
			});
		}

		[Test, Order(10)]
		public void T10_Validate_Search_WithObject()
		{
			// Arrange
			var invoice = new InvoiceHelper.Invoice { Id = invoice1.Id };

			// Act
			var result = InvoiceHelper.InvoiceBll.Search(invoice);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Id, Is.EqualTo(invoice1.Id));
			});
		}

		[Test, Order(11)]
		public void T11_Validate_Search_WithSearchCriteria_DbNull()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("CancellationDate", DBNull.Value);

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(3));
			});
		}

		[Test, Order(12)]
		public void T12_Validate_Search_WithSearchCriteria_InOperator()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("ItemCount|IN", "(5,10)");

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(2));
			});
		}

		[Test, Order(13)]
		public void T13_Validate_Search_WithSearchCriteria_DiffOperator()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("ItemCount|<>", 10);

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(2));
			});
		}

		[Test, Order(14)]
		public void T14_Validate_Search_WithSearchCriteria_LikeOperator()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("Data|LIKE", "xml");

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(2));
			});
		}

		[Test, Order(15)]
		public void T15_Validate_Search_WithSearchCriteria_ById()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("Id", invoice1.Id);

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Id, Is.EqualTo(invoice1.Id));
			});
		}

		[Test, Order(16)]
		public void T16_Validate_Search_WithSearchCriteria_ByItemCountRange()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("ItemCountFrom|>=", 1);
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("ItemCountTo|<=", 10);

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(2));
				Assert.That(result[0].ItemCount, Is.EqualTo(invoice1.ItemCount));
				Assert.That(result[1].ItemCount, Is.EqualTo(invoice2.ItemCount));
			});
		}

		[Test, Order(17)]
		public void T17_Validate_Search_WithSearchCriteria_ByCreationDateRange()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("CreationDateFrom|>=", DateTime.UtcNow.AddHours(-1));
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("CreationDateTo|<=", DateTime.UtcNow.AddHours(1));

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(3));
			});
		}

		[Test, Order(18)]
		public void T18_Validate_Search_WithSearchCriteria_ByTotalPriceRange()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("TotalPriceFrom|>=", 15M);
			InvoiceHelper.InvoiceBll.SearchCriteria.Add("TotalPriceTo|<=", 30M);

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Id, Is.EqualTo(invoice2.Id));
			});
		}

		[Test, Order(19)]
		public void T19_Validate_SearchAll()
		{
			// Arrange
			InvoiceHelper.InvoiceBll.SearchCriteria.Clear();

			// Act
			var result = InvoiceHelper.InvoiceBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(3));
			});
		}

		[Test, Order(20)]
		public void T20_Validate_Update_EmptyObject()
		{
			// Arrange
			InvoiceHelper.Invoice emptyObject = default!;

			// Act
			var result = InvoiceHelper.InvoiceBll.Update(emptyObject);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(21)]
		public void T21_Validate_Update_UseCase1()
		{
			// Arrange
			int result = 0;
			var existent = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice() { Id = invoice1.Id });

			// Act
			if (existent != null)
			{
				existent.IsCancelled = true;
				existent.CancellationDate = DateTime.UtcNow;

				result = InvoiceHelper.InvoiceBll.Update(existent);
			}

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(22)]
		public void T22_Validate_Update_UseCase2()
		{
			// Arrange
			int result = 0;
			var existent = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice() { Id = invoice2.Id });

			// Act
			if (existent != null)
			{
				existent.CharFlag = 'D';
				existent.StarScore = 5.0F;

				result = InvoiceHelper.InvoiceBll.Update(existent);
			}

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(23)]
		public void T23_Validate_Update_UseCase3()
		{
			// Arrange
			int result = 0;
			var existent = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice() { Id = invoice3.Id });

			// Act
			if (existent != null)
			{
				existent.TotalPrice = 123.456789M;
				existent.Evaluations = 200;

				result = InvoiceHelper.InvoiceBll.Update(existent);
			}

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(24)]
		public void T24_Validate_Delete_EmptyObject()
		{
			// Arrange
			InvoiceHelper.Invoice emptyInvoice = default!;

			// Act
			var result = InvoiceHelper.InvoiceBll.Delete(emptyInvoice);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(25)]
		public void T25_Validate_Delete_UseCase1()
		{
			// Arrange
			int result = 0;
			var existent = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice() { Id = invoice1.Id });

			// Act
			if (existent != null)
			{
				result = InvoiceHelper.InvoiceBll.Delete(existent);
			}

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(26)]
		public void T26_Validate_Delete_UseCase2()
		{
			// Arrange
			int result = 0;
			var existent = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice() { Id = invoice2.Id });

			// Act
			if (existent != null)
			{
				result = InvoiceHelper.InvoiceBll.Delete(existent);
			}

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(27)]
		public void T27_Validate_Delete_UseCase3()
		{
			// Arrange
			int result = 0;
			var existent = InvoiceHelper.InvoiceBll.SearchItem(new InvoiceHelper.Invoice() { Id = invoice3.Id });

			// Act
			if (existent != null)
			{
				result = InvoiceHelper.InvoiceBll.Delete(existent);
			}

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}
	}
}
