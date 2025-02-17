namespace SmartIT.Library.Data.Tests.DAL
{
	[TestFixture, SingleThreaded]
	internal class UserBllTests
	{
		readonly UserHelper.User user1 = new() { Id = 1, Name = "Eduardo", Email = "eduardo@email.com", CreationDate = DateTime.Now };
		readonly UserHelper.User user2 = new() { Id = 2, Name = "Claudio", Email = "claudio@email.com", CreationDate = DateTime.Now.AddDays(-1) };
		readonly UserHelper.User user3 = new() { Id = 3, Name = "Nicacio", Email = "nicacio@email.com", CreationDate = DateTime.Now.AddDays(-2) };

		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test, Order(1)]
		public void T01_Validate_ValidateModel_DefaultObject()
		{
			// Arrange
			UserHelper.User emptyUser = default!;

			// Act
			var result = UserHelper.UserBll.ValidateModel(emptyUser);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test, Order(2)]
		public void T02_Validate_ValidateModel_InvalidModel()
		{
			// Arrange
			var emptyUser = new UserHelper.User();

			// Act
			var result = UserHelper.UserBll.ValidateModel(emptyUser);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test, Order(3)]
		public void T03_Validate_ValidateModel_ValidModel()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.ValidateModel(user1);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test, Order(4)]
		public void T04_Validate_Insert_EmptyObject()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.Insert(new UserHelper.User());

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(5)]
		public void T05_Validate_Insert_UseCase1()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.Insert(user1);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(6)]
		public void T06_Validate_Insert_UseCase2()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.Insert(user2);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(7)]
		public void T07_Validate_Insert_UseCase3()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.Insert(user3);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(8)]
		public void T08_Validate_SearchItem_EmptyObject()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.SearchItem(new UserHelper.User());

			// Assert
			Assert.That(result, Is.Null);
		}

		[Test, Order(9)]
		public void T09_Validate_SearchItem_ValidObject()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user1.Email });

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
			var user = new UserHelper.User { Email = user2.Email };

			// Act
			var result = UserHelper.UserBll.Search(user);

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
			UserHelper.UserBll.SearchCriteria.Clear();
			UserHelper.UserBll.SearchCriteria.Add("Email", user3.Email);

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Email, Is.EqualTo(user3.Email));
			});
		}

		[Test, Order(12)]
		public void T12_Validate_Search_WithSearchCriteria_WithLogicalOperator1() 
		{
			// Arrange
			UserHelper.UserBll.SearchCriteria.Clear();
			UserHelper.UserBll.SearchCriteria.Add("CreationDateFrom|>=", DateTime.Today);
			UserHelper.UserBll.SearchCriteria.Add("CreationDateTo|<=", DateTime.Today.AddDays(1));

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.GreaterThanOrEqualTo(1));
				Assert.That(result[0].Name, Is.EqualTo(user1.Name));
			});
		}

		[Test, Order(13)]
		public void T13_Validate_Search_WithSearchCriteria_WithLogicalOperator2()
		{
			// Arrange
			UserHelper.UserBll.SearchCriteria.Clear();
			UserHelper.UserBll.SearchCriteria.Add("Name|IN", "('" + user2.Name + "')");

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Name, Is.EqualTo(user2.Name));
			});
		}

		[Test, Order(14)]
		public void T14_Validate_Search_WithSearchCriteria_WithLogicalOperator3()
		{
			// Arrange
			UserHelper.UserBll.SearchCriteria.Clear();
			UserHelper.UserBll.SearchCriteria.Add("Name|<>", user3.Name);

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(2));
				Assert.That(result[0].Name, Is.EqualTo(user2.Name));
				Assert.That(result[1].Name, Is.EqualTo(user1.Name));
			});
		}


		[Test, Order(15)]
		public void T15_Validate_Search_WithSearchCriteria_WithLogicalOperator4()
		{
			// Arrange
			UserHelper.UserBll.SearchCriteria.Clear();
			UserHelper.UserBll.SearchCriteria.Add("Name|LIKE", user1.Name);

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.EqualTo(1));
				Assert.That(result[0].Name, Is.EqualTo(user1.Name));
			});
		}

		[Test, Order(16)]
		public void T16_Validate_Search_WithSearchCriteria_WithLogicalOperator5()
		{
			// Arrange
			UserHelper.UserBll.SearchCriteria.Clear();
			UserHelper.UserBll.SearchCriteria.Add("Name|IS", DBNull.Value);

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Empty);
			});
		}

		[Test, Order(17)]
		public void T17_Validate_SearchAll()
		{
			// Arrange
			UserHelper.UserBll.SearchCriteria.Clear();

			// Act
			var result = UserHelper.UserBll.Search();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Empty);
				Assert.That(result, Has.Count.GreaterThan(1));
			});
		}

		[Test, Order(18)]
		public void T18_Validate_Update_EmptyObject()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.Update(new UserHelper.User());

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(19)]
		public void T19_Validate_Update_UseCase1()
		{
			// Arrange
			var existent = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user1.Email });
			int id = existent != null ? existent.Id : 0;
			DateTime creationDate = existent != null ? existent.CreationDate : DateTime.Now;
			var user = new UserHelper.User { Id = id, Name = "Eduardo after update", Email = "eduardo@email.com", CreationDate = creationDate };

			// Act
			var result = UserHelper.UserBll.Update(user);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(20)]
		public void T20_Validate_Update_UseCase2()
		{
			// Arrange
			var existent = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user2.Email });
			int id = existent != null ? existent.Id : 0;
			DateTime creationDate = existent != null ? existent.CreationDate : DateTime.Now;
			var user = new UserHelper.User { Id = id, Name = "Claudio after update", Email = "claudio@email.com", CreationDate = creationDate };

			// Act
			var result = UserHelper.UserBll.Update(user);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(21)]
		public void T21_Validate_Update_UseCase3()
		{
			// Arrange
			var existent = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user3.Email });
			int id = existent != null ? existent.Id : 0;
			DateTime creationDate = existent != null ? existent.CreationDate : DateTime.Now;
			var user = new UserHelper.User { Id = id, Name = "Nicacio after update", Email = "nicacio@email.com", CreationDate = creationDate };

			// Act
			var result = UserHelper.UserBll.Update(user);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(22)]
		public void T22_Validate_Delete_EmptyObject()
		{
			// Arrange

			// Act
			var result = UserHelper.UserBll.Delete(new UserHelper.User());

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(23)]
		public void T23_Validate_Delete_UseCase1()
		{
			// Arrange
			var existent = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user1.Email });
			int id = existent != null ? existent.Id : 0;

			// Act
			var result = UserHelper.UserBll.Delete(new UserHelper.User { Id = id });

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(24)]
		public void T24_Validate_Delete_UseCase2()
		{
			// Arrange
			var existent = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user2.Email });
			int id = existent != null ? existent.Id : 0;

			// Act
			var result = UserHelper.UserBll.Delete(new UserHelper.User { Id = id });

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(25)]
		public void T25_Validate_Delete_UseCase3()
		{
			// Arrange
			var existent = UserHelper.UserBll.SearchItem(new UserHelper.User { Email = user3.Email });
			int id = existent != null ? existent.Id : 0;

			// Act
			var result = UserHelper.UserBll.Delete(new UserHelper.User { Id = id });

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}
	}
}