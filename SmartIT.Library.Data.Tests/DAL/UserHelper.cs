using SmartIT.Library.Data.DAL;
using SmartIT.Library.Data.ModelManager;
using System.Data.SqlClient;

namespace SmartIT.Library.Data.Tests.DAL
{
	internal static class UserHelper
	{
		// Declares a series of classes to be used in the tests

		[Serializable]
		public class User
		{
			/// <summary>
			/// Tests SafeDataReader.GetInt32()
			/// </summary>
			public int Id { get; set; }
			/// <summary>
			/// Tests SafeDataReader.GetString()
			/// </summary>
			public string Name { get; set; } = string.Empty;
			/// <summary>
			/// Tests SafeDataReader.GetString()
			/// </summary>
			public string Email { get; set; } = string.Empty;
			/// <summary>
			/// Tests SafeDataReader.GetDateTime()
			/// </summary>
			public DateTime CreationDate { get; set; }
		}

		[ActiveConnection("NUnitTests")]
		public class UserDao : DaoBase<UserDao>
		{
			public UserDao() : base() { }

			public int Insert(User user)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_UserInsert"
				};

				cmd.Parameters.AddWithValue("@Name", DbNullHelper.GetValue(user.Name));
				cmd.Parameters.AddWithValue("@Email", DbNullHelper.GetValue(user.Email));
				cmd.Parameters.AddWithValue("@CreationDate", DbNullHelper.GetValue(user.CreationDate));

				return DbHelper.ExecuteNonQuery(cmd);
			}

			public int Update(User user)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_UserUpdate"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(user.Id));
				cmd.Parameters.AddWithValue("@Name", DbNullHelper.GetValue(user.Name));
				cmd.Parameters.AddWithValue("@Email", DbNullHelper.GetValue(user.Email));
				cmd.Parameters.AddWithValue("@CreationDate", DbNullHelper.GetValue(user.CreationDate));

				return DbHelper.ExecuteNonQuery(cmd);
			}

			public int Delete(User user)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_UserDelete"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(user.Id));

				return DbHelper.ExecuteNonQuery(cmd);
			}

			public List<User> Search(Dictionary<string, object> criteria)
			{
				string SQL = @" SELECT DISTINCT
									   [Id]
									  ,[Name]
									  ,[Email]
									  ,[CreationDate]
								  FROM dbo.[User]
									   {0}
							  ORDER BY [Name] ";

				Dictionary<string, string> alias = new()
				{
					{ "Id", "Id" },
					{ "Name", "Name" },
					{ "Email", "Email" },
					{ "CreationDateFrom", "CreationDate" },
					{ "CreationDateTo", "CreationDate" }
				};

				return (List<User>)SqlSearch(criteria, alias, SQL);
			}

			public List<User> Search(User user)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_UserRetrieve"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(user.Id));
				cmd.Parameters.AddWithValue("@Name", DbNullHelper.GetValue(user.Name));
				cmd.Parameters.AddWithValue("@Email", DbNullHelper.GetValue(user.Email));
				cmd.Parameters.AddWithValue("@CreationDate", DbNullHelper.GetValue(user.CreationDate));

				return (List<User>)SqlSearch(cmd);
			}

			protected override object GetList(ref SafeDataReader reader)
			{
				List<User> list = [];

				while (reader.Read())
				{
					User obj = new()
					{
						Id = reader.GetInt32("Id"),
						Name = reader.GetString("Name"),
						Email = reader.GetString("Email"),
						CreationDate = reader.GetDateTime("CreationDate")
					};

					list.Add(obj);
				}

				return list;
			}
		}

		public class UserBll : ManagerBase<UserDao>
		{
			public static int Insert(User user)
			{
				if (ValidateModel(user))
				{
					return EntityDb.Insert(user);
				}
				return 0;
			}

			public static int Update(User user)
			{
				if (ValidateModel(user))
				{
					return EntityDb.Update(user);
				}
				return 0;
			}

			public static int Delete(User user)
			{
				if (user.Id > 0)
				{
					return EntityDb.Delete(user);
				}
				return 0;
			}

			public static User? SearchItem(User user)
			{
				List<User> users = EntityDb.Search(user);
				foreach (var obj in users)
				{
					if (obj.Id == user.Id || obj.Name == user.Name || obj.Email == user.Email || obj.CreationDate == user.CreationDate)
					{
						return obj;
					}
				}
				return null;
			}

			public static List<User> Search()
			{
				return EntityDb.Search(SearchCriteria);
			}

			public static List<User> Search(User user)
			{
				return EntityDb.Search(user);
			}

			public static bool ValidateModel(User user)
			{
				return user is not null &&
					!string.IsNullOrWhiteSpace(user.Name) &&
					!string.IsNullOrWhiteSpace(user.Email) &&
					user.CreationDate > DateTime.MinValue &&
					user.CreationDate < DateTime.MaxValue;
			}
		}
	}
}
