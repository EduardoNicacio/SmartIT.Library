using SmartIT.Library.Data.DAL;
using SmartIT.Library.Data.ModelManager;
using System.Data.SqlClient;

namespace SmartIT.Library.Data.Tests.DAL
{
	internal static class InvoiceHelper
	{
		// Declares a series of classes to be used in the tests

		[Serializable]
		public class Invoice
		{
			/// <summary>
			/// Tests SafeDataReader.GetGuid()
			/// </summary>
			public Guid? Id { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetValue()
			/// </summary>
			public string Data { get; set; } = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><e/>";
			/// <summary>
			/// Tests SafeDataReader.GetDouble()
			/// </summary>
			public double? ItemCount { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetDateTime()
			/// </summary>
			public DateTime? CreationDate { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetNullableDateTime()
			/// </summary>
			public DateTime? CancellationDate { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetBoolean()
			/// </summary>
			public bool? IsCancelled { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetByte()
			/// </summary>
			public byte? PageNumber { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetChar()
			/// </summary>
			public char? CharFlag { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetChars()
			/// </summary>
			public long? PathLength { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetDecimal()
			/// </summary>
			public decimal? TotalPrice { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetFloat()
			/// </summary>
			public double? StarScore { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetInt16()
			/// </summary>
			public short? Evaluations { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetInt64()
			/// </summary>
			public long? SerialNumber { get; set; } = default!;
			/// <summary>
			/// Tests SafeDataReader.GetBytes()
			/// </summary>
			public byte[]? SysTimestamp { get; set; } = default!;
		}

		[ActiveConnection("NUnitTests")]
		public class InvoiceDao : DaoBase<InvoiceDao>
		{
			public InvoiceDao() : base() { }

			public int Insert(Invoice invoice)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_InvoiceInsert"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(invoice.Id));
				cmd.Parameters.AddWithValue("@Data", DbNullHelper.GetValue(invoice.Data?.ToString()));
				cmd.Parameters.AddWithValue("@ItemCount", DbNullHelper.GetValue(invoice.ItemCount));
				cmd.Parameters.AddWithValue("@CreationDate", DbNullHelper.GetValue(invoice.CreationDate));
				cmd.Parameters.AddWithValue("@CancellationDate", DbNullHelper.GetValue(invoice.CancellationDate));
				cmd.Parameters.AddWithValue("@IsCancelled", DbNullHelper.GetValue(invoice.IsCancelled));
				cmd.Parameters.AddWithValue("@PageNumber", DbNullHelper.GetValue(invoice.PageNumber));
				cmd.Parameters.AddWithValue("@CharFlag", DbNullHelper.GetValue(invoice.CharFlag));
				cmd.Parameters.AddWithValue("@PathLength", DbNullHelper.GetValue(invoice.PathLength));
				cmd.Parameters.AddWithValue("@TotalPrice", DbNullHelper.GetValue(invoice.TotalPrice));
				cmd.Parameters.AddWithValue("@StarScore", DbNullHelper.GetValue(invoice.StarScore));
				cmd.Parameters.AddWithValue("@Evaluations", DbNullHelper.GetValue(invoice.Evaluations));
				cmd.Parameters.AddWithValue("@SerialNumber", DbNullHelper.GetValue(invoice.SerialNumber));

				return DbHelper.ExecuteNonQuery(cmd);
			}

			public int Update(Invoice invoice)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_InvoiceUpdate"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(invoice.Id));
				cmd.Parameters.AddWithValue("@Data", DbNullHelper.GetValue(invoice.Data));
				cmd.Parameters.AddWithValue("@ItemCount", DbNullHelper.GetValue(invoice.ItemCount));
				cmd.Parameters.AddWithValue("@CreationDate", DbNullHelper.GetValue(invoice.CreationDate));
				cmd.Parameters.AddWithValue("@CancellationDate", DbNullHelper.GetValue(invoice.CancellationDate));
				cmd.Parameters.AddWithValue("@IsCancelled", DbNullHelper.GetValue(invoice.IsCancelled));
				cmd.Parameters.AddWithValue("@PageNumber", DbNullHelper.GetValue(invoice.PageNumber));
				cmd.Parameters.AddWithValue("@CharFlag", DbNullHelper.GetValue(invoice.CharFlag));
				cmd.Parameters.AddWithValue("@PathLength", DbNullHelper.GetValue(invoice.PathLength));
				cmd.Parameters.AddWithValue("@TotalPrice", DbNullHelper.GetValue(invoice.TotalPrice));
				cmd.Parameters.AddWithValue("@StarScore", DbNullHelper.GetValue(invoice.StarScore));
				cmd.Parameters.AddWithValue("@Evaluations", DbNullHelper.GetValue(invoice.Evaluations));
				cmd.Parameters.AddWithValue("@SerialNumber", DbNullHelper.GetValue(invoice.SerialNumber));
				cmd.Parameters.AddWithValue("@SysTimestamp", DbNullHelper.GetValue(invoice.SysTimestamp));

				return DbHelper.ExecuteNonQuery(cmd);
			}

			public int Delete(Invoice invoice)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_InvoiceDelete"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(invoice.Id));
				cmd.Parameters.AddWithValue("@SysTimestamp", DbNullHelper.GetValue(invoice.SysTimestamp));

				return DbHelper.ExecuteNonQuery(cmd);
			}

			public List<Invoice> Search(Dictionary<string, object> criteria)
			{
				string SQL = @"  SELECT [Id],
										[Data],
										[ItemCount],
										[CreationDate],
										[CancellationDate],
										[IsCancelled],
										[PageNumber],
										[CharFlag],
										[PathLength],
										[TotalPrice],
										[StarScore],
										[Evaluations],
										[SerialNumber],
										[SysTimeStamp]
								   FROM dbo.[Invoice]
										{0}
							   ORDER BY [CreationDate] DESC, [TotalPrice] DESC ";

				Dictionary<string, string> alias = new()
				{
					{ "Id", "Id"},
					{ "Data", "Data"},
					{ "ItemCount", "ItemCount"},
					{ "ItemCountFrom", "ItemCount"},
					{ "ItemCountTo", "ItemCount"},
					{ "CreationDateFrom", "CreationDate"},
					{ "CreationDateTo", "CreationDate"},
					{ "CancellationDate", "CancellationDate" },
					{ "CancellationDateFrom", "CancellationDate"},
					{ "CancellationDateTo", "CancellationDate"},
					{ "IsCancelled", "IsCancelled"},
					{ "PageNumberFrom", "PageNumber"},
					{ "PageNumberTo", "PageNumber"},
					{ "CharFlag", "CharFlag"},
					{ "PathLengthFrom", "PathLength"},
					{ "PathLengthTo", "PathLength"},
					{ "TotalPriceFrom", "TotalPrice"},
					{ "TotalPriceTo", "TotalPrice"},
					{ "StarScoreFrom", "StarScore"},
					{ "StarScoreTo", "StarScore"},
					{ "EvaluationsFrom", "Evaluations"},
					{ "EvaluationsTo", "Evaluations"},
					{ "SerialNumber", "SerialNumber"}
				};

				return (List<Invoice>)SqlSearch(criteria, alias, SQL);
			}

			public List<Invoice> Search(Invoice invoice)
			{
				SqlCommand cmd = new()
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandText = "dbo.usp_InvoiceRetrieve"
				};

				cmd.Parameters.AddWithValue("@Id", DbNullHelper.GetValue(invoice.Id));
				cmd.Parameters.AddWithValue("@ItemCount", DbNullHelper.GetValue(invoice.ItemCount));
				cmd.Parameters.AddWithValue("@CreationDate", DbNullHelper.GetValue(invoice.CreationDate));
				cmd.Parameters.AddWithValue("@CancellationDate", DbNullHelper.GetValue(invoice.CancellationDate));
				cmd.Parameters.AddWithValue("@IsCancelled", DbNullHelper.GetValue(invoice.IsCancelled));
				cmd.Parameters.AddWithValue("@PageNumber", DbNullHelper.GetValue(invoice.PageNumber));
				cmd.Parameters.AddWithValue("@CharFlag", DbNullHelper.GetValue(invoice.CharFlag));
				cmd.Parameters.AddWithValue("@PathLength", DbNullHelper.GetValue(invoice.PathLength));
				cmd.Parameters.AddWithValue("@TotalPrice", DbNullHelper.GetValue(invoice.TotalPrice));
				cmd.Parameters.AddWithValue("@StarScore", DbNullHelper.GetValue(invoice.StarScore));
				cmd.Parameters.AddWithValue("@Evaluations", DbNullHelper.GetValue(invoice.Evaluations));
				cmd.Parameters.AddWithValue("@SerialNumber", DbNullHelper.GetValue(invoice.SerialNumber));

				return (List<Invoice>)SqlSearch(cmd);
			}

			protected override object GetList(ref SafeDataReader reader)
			{
				List<Invoice> list = [];

				while (reader.Read())
				{
					Invoice invoice = new()
					{
						Id = reader.GetGuid("Id"),
						Data = reader.GetString("Data"),
						ItemCount = reader.GetDouble("ItemCount"),
						CreationDate = reader.GetDateTime("CreationDate"),
						CancellationDate = reader.GetNullableDateTime("CancellationDate"),
						IsCancelled = reader.GetBoolean("IsCancelled"),
						PageNumber = reader.GetByte("PageNumber"),
						CharFlag = reader.GetChar("CharFlag"),
						PathLength = reader.GetInt64("PathLength"),
						TotalPrice = reader.GetDecimal("TotalPrice"),
						StarScore = reader.GetDouble("StarScore"),
						Evaluations = reader.GetInt16("Evaluations"),
						SerialNumber = reader.GetInt64("SerialNumber"),
						SysTimestamp = reader.GetValue("SysTimestamp") as byte[]
					};

					list.Add(invoice);
				}

				return list;
			}
		}

		public class InvoiceBll : ManagerBase<InvoiceDao>
		{
			public static int Insert(Invoice invoice)
			{
				if (ValidateModel(invoice))
				{
					return EntityDb.Insert(invoice);
				}
				return 0;
			}

			public static int Update(Invoice invoice)
			{
				if (ValidateModel(invoice))
				{
					return EntityDb.Update(invoice);
				}
				return 0;
			}

			public static int Delete(Invoice invoice)
			{
				if (invoice != null && invoice.Id != Guid.Empty)
				{
					return EntityDb.Delete(invoice);
				}
				return 0;
			}

			public static Invoice? SearchItem(Invoice invoice)
			{
				if (invoice == null) { return null; }

				List<Invoice> invoices = EntityDb.Search(invoice);
				foreach (var obj in invoices)
				{
					if (obj.Id == invoice.Id || obj.SerialNumber == invoice.SerialNumber)
					{
						return obj;
					}
				}
				return null;
			}

			public static List<Invoice> Search()
			{
				return EntityDb.Search(SearchCriteria);
			}

			public static List<Invoice> Search(Invoice invoice)
			{
				return EntityDb.Search(invoice);
			}

			public static bool ValidateModel(Invoice invoice)
			{
				return invoice is not null &&
					invoice.Id != Guid.Empty &&
					invoice.CreationDate > DateTime.MinValue &&
					invoice.SerialNumber > 0;
			}
		}
	}
}
