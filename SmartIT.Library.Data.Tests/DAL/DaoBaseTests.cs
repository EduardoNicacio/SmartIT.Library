namespace SmartIT.Library.Data.Tests.DAL
{
	[TestFixture, SingleThreaded]
	internal class DaoBaseTests
	{
		readonly Helpers.User user1 = new() { Id = 1, Name = "Eduardo", Email = "eduardo@email.com", CreationDate = DateTime.Now };
		readonly Helpers.User user2 = new() { Id = 2, Name = "Claudio", Email = "claudio@email.com", CreationDate = DateTime.Now.AddDays(-1) };
		readonly Helpers.User user3 = new() { Id = 3, Name = "Nicacio", Email = "nicacio@email.com", CreationDate = DateTime.Now.AddDays(-2) };

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test, Order(1)]
		public void T01_Validate_ValidateModel_NullObject()
		{
			// Arrange
			Helpers.User emptyUser = null;

			// Act
			var result = Helpers.UserBll.ValidateModel(emptyUser);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test, Order(2)]
		public void T02_Validate_ValidateModel_InvalidModel()
		{
			// Arrange
			var emptyUser = new Helpers.User();

			// Act
			var result = Helpers.UserBll.ValidateModel(emptyUser);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test, Order(3)]
		public void T03_Validate_ValidateModel_ValidModel()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.ValidateModel(user1);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test, Order(4)]
		public void T04_Validate_Insert_EmptyObject()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.Insert(new Helpers.User());

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(5)]
		public void T05_Validate_Insert_UseCase1()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.Insert(user1);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(6)]
		public void T06_Validate_Insert_UseCase2()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.Insert(user2);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(7)]
		public void T07_Validate_Insert_UseCase3()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.Insert(user3);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(8)]
		public void T08_Validate_SearchItem_EmptyObject()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.SearchItem(new Helpers.User());

			// Assert
			Assert.That(result, Is.Null);
		}

		[Test, Order(9)]
		public void T09_Validate_SearchItem_ValidObject()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.SearchItem(new Helpers.User { Email = user1.Email });

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result?.Name, Is.EqualTo(user1.Name));
				Assert.That(result?.Email, Is.EqualTo(user1.Email));
				Assert.That(result?.CreationDate, Is.GreaterThan(DateTime.MinValue));
			});
		}

		[Test, Order(10)]
		public void T10_Validate_Search_WithObject()
		{
			// Arrange
			var user = new Helpers.User { Email = user2.Email };

			// Act
			var result = Helpers.UserBll.Search(user);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Email, Is.EqualTo(user2.Email));
			});
		}

		[Test, Order(11)]
		public void T11_Validate_Search_WithSearchCriteria()
		{
			// Arrange
			Helpers.UserBll.SearchCriteria.Clear();
			Helpers.UserBll.SearchCriteria.Add("Email", user3.Email);

			// Act
			var result = Helpers.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Email, Is.EqualTo(user3.Email));
			});
		}

		[Test, Order(12)]
		public void T12_Validate_SearchAll()
		{
			// Arrange
			Helpers.UserBll.SearchCriteria.Clear();

			// Act
			var result = Helpers.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.GreaterThan(1));
			});
		}

		[Test, Order(13)]
		public void T13_Validate_Update_EmptyObject()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.Update(new Helpers.User());

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(14)]
		public void T14_Validate_Update_UseCase1()
		{
			// Arrange
			var existent = Helpers.UserBll.SearchItem(new Helpers.User { Email = user1.Email });
			var user = new Helpers.User { Id = existent.Id, Name = "Eduardo after update", Email = "eduardo@email.com", CreationDate = existent.CreationDate };

			// Act
			var result = Helpers.UserBll.Update(user);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(15)]
		public void T15_Validate_Update_UseCase2()
		{
			// Arrange
			var existent = Helpers.UserBll.SearchItem(new Helpers.User { Email = user2.Email });
			var user = new Helpers.User { Id = existent.Id, Name = "Claudio after update", Email = "claudio@email.com", CreationDate = existent.CreationDate };

			// Act
			var result = Helpers.UserBll.Update(user);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(16)]
		public void T16_Validate_Update_UseCase3()
		{
			// Arrange
			var existent = Helpers.UserBll.SearchItem(new Helpers.User { Email = user3.Email });
			var user = new Helpers.User { Id = existent.Id, Name = "Nicacio after update", Email = "nicacio@email.com", CreationDate = existent.CreationDate };

			// Act
			var result = Helpers.UserBll.Update(user);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(17)]
		public void T17_Validate_Delete_EmptyObject()
		{
			// Arrange

			// Act
			var result = Helpers.UserBll.Delete(new Helpers.User());

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(18)]
		public void T18_Validate_Delete_UseCase1()
		{
			// Arrange
			var existent = Helpers.UserBll.SearchItem(new Helpers.User { Email = user1.Email });

			// Act
			var result = Helpers.UserBll.Delete(existent);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(19)]
		public void T19_Validate_Delete_UseCase2()
		{
			// Arrange
			var existent = Helpers.UserBll.SearchItem(new Helpers.User { Email = user2.Email });

			// Act
			var result = Helpers.UserBll.Delete(existent);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(20)]
		public void T20_Validate_Delete_UseCase3()
		{
			// Arrange
			var existent = Helpers.UserBll.SearchItem(new Helpers.User { Email = user3.Email });

			// Act
			var result = Helpers.UserBll.Delete(existent);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}
	}
}