using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace WithoutHaste.DataFiles
{
	public static class ExcelPackageHelper
	{
		public static ExcelWorksheet AddWorksheet(ExcelPackage excelPackage, string name)
		{
			return excelPackage.Workbook.Worksheets.Add(name);
		}

		public static ExcelWorksheet GetWorksheet(ExcelPackage excelPackage, string name)
		{
			return excelPackage.Workbook.Worksheets[name];
		}

		public static void AppendRow(ExcelWorksheet worksheet, List<object> values)
		{
			int row = CountRows(worksheet) + 1;
			worksheet.InsertRow(row, 1);
			char column = 'A';
			foreach(object value in values)
			{
				worksheet.Cells[String.Format("{0}{1}", column, row)].Value = value;
			}
		}

		public static void SetColumn(ExcelWorksheet worksheet, string column, List<object> values, bool skipFirstRow = true)
		{
			int row = (skipFirstRow) ? 2 : 1;
			foreach(object value in values)
			{
				if(CountRows(worksheet) < row)
					worksheet.InsertRow(row, 1);
				worksheet.Cells[String.Format("{0}{1}", column, row)].Value = value;
			}
		}

		public static string GetColumnCharForHeader(ExcelWorksheet worksheet, string header)
		{
			for(int column = 1; column <= CountColumns(worksheet); column++)
			{
				if(worksheet.Cells[String.Format("{0}0", ColumnChar(column))].Value.ToString() == header)
				{
					return ColumnChar(column);
				}
			}
			return null;
		}

		public static List<object> GetColumnByHeader(ExcelWorksheet worksheet, string header)
		{
			for(int column = 1; column <= CountColumns(worksheet); column++)
			{
				if(worksheet.Cells[String.Format("{0}0", ColumnChar(column))].Value.ToString() == header)
				{
					return GetColumnByChar(worksheet, ColumnChar(column), skipFirstRow:true);
				}
			}
			return new List<object>();
		}

		public static List<object> GetColumnByChar(ExcelWorksheet worksheet, string column, bool skipFirstRow=true)
		{
			List<object> values = new List<object>();
			for(int row = (skipFirstRow ? 2 : 1); row <= CountRows(worksheet); row++)
			{
				values.Add(worksheet.Cells[column + row].Value);
			}
			return values;
		}

		public static int CountRows(ExcelWorksheet excelWorksheet)
		{
			return excelWorksheet.Dimension.End.Row;
		}

		public static int CountColumns(ExcelWorksheet excelWorksheet)
		{
			return excelWorksheet.Dimension.End.Column;
		}

		/// <summary>
		/// Convert column integer index to character index.
		/// </summary>
		/// <example>1 becomes A</example>
		/// <example>26 becomes B</example>
		/// <example>27 becomes AA</example>
		/// <param name="columnNumber">1-based index</param>
		public static string ColumnChar(int columnNumber)
		{
			int dividend = columnNumber;
			string columnName = String.Empty;
			int modulo;

			while(dividend > 0)
			{
			modulo = (dividend - 1) % 26;
			columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
			dividend = (int)((dividend - modulo) / 26);
			}

			return columnName;
		}

		public static void Clear(ExcelWorksheet worksheet)
		{
			worksheet.DeleteRow(rowFrom: 1, rows: CountRows(worksheet), shiftOtherRowsUp: true);
		}
	}
}
