// <copyright file="ExportToExcel.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>02/09/2016</date>
// <summary>Utility class that generates Excel files.</summary>

#define INCLUDE_WEB_FUNCTIONS

namespace SmartIT.Library.Utilities
{
	using DocumentFormat.OpenXml;
	using DocumentFormat.OpenXml.Packaging;
	using DocumentFormat.OpenXml.Spreadsheet;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Diagnostics;
	using System.Globalization;
	using System.Text;

	/// <summary>
	/// Utility class to export to Excel a set of data. 
	/// </summary>
	public static class ExportToExcel
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static DataTable ListToDataTable<T>(List<T> list)
		{
			var dt = new DataTable();

			foreach (var info in typeof(T).GetProperties())
			{
				dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
			}
			foreach (var t in list)
			{
				var row = dt.NewRow();
				foreach (var info in typeof(T).GetProperties())
				{
					if (!IsNullableType(info.PropertyType))
						row[info.Name] = info.GetValue(t, null);
					else
						row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
				}
				dt.Rows.Add(row);
			}
			return dt;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		private static Type GetNullableType(Type t)
		{
			var returnType = t;
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				returnType = Nullable.GetUnderlyingType(t);
			}
			return returnType;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private static bool IsNullableType(Type type)
		{
			return (type == typeof(string)
					|| type.IsArray
					|| (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="xlsxFilePath"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath, string tableName)
		{
			var ds = new DataSet();
			ds.Tables.Add(ListToDataTable(list));
			ds.Tables[0].TableName = tableName;
			return CreateExcelDocument(ds, xlsxFilePath);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="xlsxFilePath"></param>
		/// <returns></returns>
		public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
		{
			var ds = new DataSet();
			ds.Tables.Add(dt);
			var result = CreateExcelDocument(ds, xlsxFilePath);
			ds.Tables.Remove(dt);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="excelFilename"></param>
		/// <returns></returns>
		public static bool CreateExcelDocument(DataSet ds, string excelFilename)
		{
			try
			{
				using (var document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
				{
					WriteExcelFile(ds, document);
				}
				Trace.WriteLine("Successfully created: " + excelFilename);
				return true;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Failed, exception thrown: " + ex.Message);
				return false;
			}
		}

#if INCLUDE_WEB_FUNCTIONS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="filename"></param>
		/// <param name="Response"></param>
		/// <returns></returns>
		public static bool CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response)
		{
			try
			{
				var ds = new DataSet();
				ds.Tables.Add(dt);
				CreateExcelDocumentAsStream(ds, filename, Response);
				ds.Tables.Remove(dt);
				return true;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Failed, exception thrown: " + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="filename"></param>
		/// <param name="Response"></param>
		/// <returns></returns>
		public static bool CreateExcelDocument<T>(List<T> list, string filename, System.Web.HttpResponse Response)
		{
			try
			{
				var ds = new DataSet();
				ds.Tables.Add(ListToDataTable(list));
				CreateExcelDocumentAsStream(ds, filename, Response);
				return true;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Failed, exception thrown: " + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="filename"></param>
		/// <param name="response"></param>
		/// <returns></returns>
		public static bool CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse response)
		{
			try
			{
				var stream = new System.IO.MemoryStream();
				using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
				{
					WriteExcelFile(ds, document);
				}
				stream.Flush();
				stream.Position = 0;

				response.ClearContent();
				response.Clear();
				response.Buffer = true;
				response.Charset = "";

				response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
				response.AddHeader("content-disposition", "attachment; filename=" + filename);
				response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				var data1 = new byte[stream.Length];
				stream.Read(data1, 0, data1.Length);
				response.BinaryWrite(data1);
				response.Flush();
				response.End();

				return true;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Failed, exception thrown: " + ex.Message);
				return false;
			}
		}

#endif

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="spreadsheet"></param>
		private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
		{
			spreadsheet.AddWorkbookPart();
			spreadsheet.WorkbookPart.Workbook = new Workbook();

			spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

			var workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
			var stylesheet = new Stylesheet();
			workbookStylesPart.Stylesheet = stylesheet;

			uint worksheetNumber = 1;
			var sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
			foreach (DataTable dt in ds.Tables)
			{
				var worksheetName = dt.TableName;

				var newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
				var sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart), SheetId = worksheetNumber, Name = worksheetName };

				sheets.Append(sheet);

				WriteDataTableToExcelWorksheet(dt, newWorksheetPart);

				worksheetNumber++;
			}

			spreadsheet.WorkbookPart.Workbook.Save();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="worksheetPart"></param>
		private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
		{
			var writer = OpenXmlWriter.Create(worksheetPart, Encoding.ASCII);
			writer.WriteStartElement(new Worksheet());
			writer.WriteStartElement(new SheetData());

			string cellValue;

			var numberOfColumns = dt.Columns.Count;
			var IsNumericColumn = new bool[numberOfColumns];
			var IsDateColumn = new bool[numberOfColumns];

			var excelColumnNames = new string[numberOfColumns];
			for (var n = 0; n < numberOfColumns; n++)
				excelColumnNames[n] = GetExcelColumnName(n);

			uint rowIndex = 1;

			writer.WriteStartElement(new Row { RowIndex = rowIndex });
			for (var colInx = 0; colInx < numberOfColumns; colInx++)
			{
				var col = dt.Columns[colInx];
				AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, ref writer);
				IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32") || (col.DataType.FullName == "System.Double") || (col.DataType.FullName == "System.Single");
				IsDateColumn[colInx] = (col.DataType.FullName == "System.DateTime");
			}
			writer.WriteEndElement();

			double cellNumericValue;
			foreach (DataRow dr in dt.Rows)
			{
				++rowIndex;

				writer.WriteStartElement(new Row { RowIndex = rowIndex });

				for (var colInx = 0; colInx < numberOfColumns; colInx++)
				{
					cellValue = dr.ItemArray[colInx].ToString();

					if (IsNumericColumn[colInx])
					{
						if (!double.TryParse(cellValue, out cellNumericValue)) continue;
						cellValue = cellNumericValue.ToString(CultureInfo.InvariantCulture);
						AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
					}
					else if (IsDateColumn[colInx])
					{
						//  This is a date value.
						var dtValue = DateTime.Parse(cellValue);
						var strValue = dtValue.ToShortDateString();
						AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), strValue, ref writer);
					}
					else
					{
						AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
					}
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndElement();

			writer.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellReference"></param>
		/// <param name="cellStringValue"></param>
		/// <param name="writer"></param>
		private static void AppendTextCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
		{
			writer.WriteElement(new Cell { CellValue = new CellValue(cellStringValue), CellReference = cellReference, DataType = CellValues.String });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellReference"></param>
		/// <param name="cellStringValue"></param>
		/// <param name="writer"></param>
		private static void AppendDateCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
		{
			writer.WriteElement(new Cell
			{
				CellValue = new CellValue(cellStringValue),
				CellReference = cellReference,
				DataType = new EnumValue<CellValues>(CellValues.Number),
				StyleIndex = 0
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellReference"></param>
		/// <param name="cellStringValue"></param>
		/// <param name="writer"></param>
		private static void AppendNumericCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
		{
			writer.WriteElement(new Cell { CellValue = new CellValue(cellStringValue), CellReference = cellReference, DataType = CellValues.Number });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="columnIndex"></param>
		/// <returns></returns>
		private static string GetExcelColumnName(int columnIndex)
		{
			if (columnIndex < 26)
				return ((char)('A' + columnIndex)).ToString();

			var firstChar = (char)('A' + (columnIndex / 26) - 1);
			var secondChar = (char)('A' + (columnIndex % 26));

			return string.Format("{0}{1}", firstChar, secondChar);
		}
	}
}