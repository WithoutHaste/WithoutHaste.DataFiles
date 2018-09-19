using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace WithoutHaste.DataFiles
{
	/// <summary>Extensions for ExcelPackage.</summary>
	public static class ExcelPackageHelper
	{
		/// <summary>Minimum row index in excel.</summary>
		public const int MIN_ROW_INDEX = 1;
		/// <summary>Minimum column index in excel.</summary>
		public const int MIN_COLUMN_INDEX = 1;
		/// <summary>Minimum column character in excel.</summary>
		public const string MIN_COLUMN_CHAR = "A";

		/// <summary>
		/// Add a new Worksheet to an ExcelPackage.
		/// </summary>
		/// <param name="excelPackage"></param>
		/// <param name="name">The name of the Worksheet.</param>
		/// <returns>The new Worksheet.</returns>
		/// <exception cref="ArgumentException">ExcelPackage cannot be null.</exception>
		public static ExcelWorksheet AddWorksheet(ExcelPackage excelPackage, string name)
		{
			ExcelPackageCannotBeNull(excelPackage);

			return excelPackage.Workbook.Worksheets.Add(name);
		}

		/// <summary>
		/// Searches the ExcelPackage for a Worksheet by name.
		/// </summary>
		/// <param name="excelPackage"></param>
		/// <param name="name">The name of the Worksheet.</param>
		/// <returns>The Worksheet, or null.</returns>
		/// <exception cref="ArgumentException">ExcelPackage cannot be null.</exception>
		public static ExcelWorksheet GetWorksheet(ExcelPackage excelPackage, string name)
		{
			ExcelPackageCannotBeNull(excelPackage);

			return excelPackage.Workbook.Worksheets[name];
		}

		/// <duplicate cref="ExcelPackageHelper.AppendRow(ExcelWorksheet, List{object})" />
		public static void AppendRow(ExcelWorksheet worksheet, List<string> values)
		{
			AppendRow(worksheet, values.Cast<object>().ToList());
		}

		/// <duplicate cref="ExcelPackageHelper.AppendRow(ExcelWorksheet, List{object})" />
		public static void AppendRow(ExcelWorksheet worksheet, List<int> values)
		{
			AppendRow(worksheet, values.Cast<object>().ToList());
		}

		/// <duplicate cref="ExcelPackageHelper.AppendRow(ExcelWorksheet, List{object})" />
		public static void AppendRow(ExcelWorksheet worksheet, List<decimal> values)
		{
			AppendRow(worksheet, values.Cast<object>().ToList());
		}

		/// <summary>
		/// Add a new row of data to the end of the Worksheet.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="values">The data values for the row.</param>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		/// <exception cref="ArgumentException">Values list cannot be null.</exception>
		public static void AppendRow(ExcelWorksheet worksheet, List<object> values)
		{
			WorksheetCannotBeNull(worksheet);
			if(values == null)
				throw new ArgumentException("Values list cannot be null.");

			int row = CountRows(worksheet) + 1;
			worksheet.InsertRow(row, 1);
			int column = MIN_COLUMN_INDEX;
			foreach(object value in values)
			{
				worksheet.Cells[row, column].Value = value;
				column++;
			}
		}

		/// <summary>
		/// Returns all the values from one row, specified by the 0-indexed row number.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="row">Row number, starting at 1.</param>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		/// <exception cref="IndexOutOfRangeException">Row index out of range.</exception>
		public static List<object> GetRow(ExcelWorksheet worksheet, int row)
		{
			WorksheetCannotBeNull(worksheet);
			if(row > CountRows(worksheet))
				throw new IndexOutOfRangeException(String.Format("Row index out of range [1,{0}]: {1}", CountRows(worksheet), row));

			List<object> values = new List<object>();
			for(int i = MIN_COLUMN_INDEX; i <= CountColumns(worksheet); i++)
			{
				values.Add(worksheet.Cells[ColumnChar(i) + row].Value);
			}
			return values;
		}

		/// <summary>
		/// Set an entire column of values at once.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="column">Integer index of column, starting at 1.</param>
		/// <param name="values">Data values for the column.</param>
		/// <param name="skipFirstRow">If true, values[0] is applied to row 2 instead of 1. Intended for indicating the first row is for headers.</param>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		/// <exception cref="ArgumentException">Values list cannot be null.</exception>
		public static void SetColumnByIndex(ExcelWorksheet worksheet, int column, List<object> values, bool skipFirstRow = true)
		{
			SetColumnByChar(worksheet, ColumnChar(column), values, skipFirstRow);
		}

		/// <summary>
		/// Set an entire column of values at once.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="header">Header value of column.</param>
		/// <param name="values">Data values for the column. Should not include the header.</param>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		/// <exception cref="ArgumentException">Values list cannot be null.</exception>
		public static void SetColumnByHeader(ExcelWorksheet worksheet, string header, List<object> values)
		{
			SetColumnByChar(worksheet, GetColumnCharForHeader(worksheet, header), values, skipFirstRow:true);
		}

		/// <summary>
		/// Set an entire column of values at once.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="column">Character name of column. Example: "A", "Z", "AA".</param>
		/// <param name="values">Data values for the column.</param>
		/// <param name="skipFirstRow">If true, values[0] is applied to row 2 instead of 1. Intended for indicating the first row is for headers.</param>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		/// <exception cref="ArgumentException">Values list cannot be null.</exception>
		public static void SetColumnByChar(ExcelWorksheet worksheet, string column, List<object> values, bool skipFirstRow = true)
		{
			WorksheetCannotBeNull(worksheet);
			if(values == null)
				throw new ArgumentException("Values list cannot be null.");

			int row = MIN_ROW_INDEX;
			if(skipFirstRow) row++;
			foreach(object value in values)
			{
				if(CountRows(worksheet) < row)
					worksheet.InsertRow(row, 1);
				worksheet.Cells[String.Format("{0}{1}", column, row)].Value = value;
				row++;
			}
		}

		/// <summary>
		/// Searches first row for a particular value.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="header">Header value.</param>
		/// <returns>Character name of the header's column, or null. Example: "A", "Z", "AA".</returns>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		/// <exception cref="IndexOutOfRangeException">No header row found.</exception>
		public static string GetColumnCharForHeader(ExcelWorksheet worksheet, string header)
		{
			List<object> headers = GetRow(worksheet, MIN_ROW_INDEX);
			for(int i = 0; i < headers.Count; i++)
			{
				if(headers[i] != null && headers[i].ToString() == header)
					return ColumnChar(i + 1);
			}
			return null;
		}

		/// <summary>
		/// Returns all the values from one column, specified by the header value.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="header"></param>
		/// <returns>List of data values, not including the header. Includes all cells to the bottom of the Worksheet.</returns>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		public static List<object> GetColumnByHeader(ExcelWorksheet worksheet, string header)
		{
			return GetColumnByChar(worksheet, GetColumnCharForHeader(worksheet, header), skipFirstRow: true);
		}

		/// <summary>
		/// Returns all values from one column, specified by the integer index of the column.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="column">Integer index of column, starting at 1.</param>
		/// <param name="skipFirstRow">If true, the first value in the column is not included. Intended for skipping the header value.</param>
		/// <returns>List of data values. Includes all cells to the bottom of the Worksheet.</returns>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		public static List<object> GetColumnByIndex(ExcelWorksheet worksheet, int column, bool skipFirstRow = true)
		{
			return GetColumnByChar(worksheet, ColumnChar(column), skipFirstRow);
		}

		/// <summary>
		/// Returns all values from one column, specified by the character name of the column.
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="column">Character name of column. Example: "A", "Z", "AA".</param>
		/// <param name="skipFirstRow">If true, the first value in the column is not included. Intended for skipping the header value.</param>
		/// <returns>List of data values. Includes all cells to the bottom of the Worksheet.</returns>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		public static List<object> GetColumnByChar(ExcelWorksheet worksheet, string column, bool skipFirstRow=true)
		{
			WorksheetCannotBeNull(worksheet);

			List<object> values = new List<object>();
			for(int row = (skipFirstRow ? (MIN_ROW_INDEX+1) : MIN_ROW_INDEX); row <= CountRows(worksheet); row++)
			{
				values.Add(worksheet.Cells[column + row].Value);
			}
			return values;
		}

		/// <summary>
		/// Returns the number of rows in the Worksheet.
		/// </summary>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		public static int CountRows(ExcelWorksheet worksheet)
		{
			WorksheetCannotBeNull(worksheet);

			if(worksheet.Dimension == null)
				return 0;
			return worksheet.Dimension.End.Row;
		}

		/// <summary>
		/// Returns the number of columns in the Worksheet.
		/// </summary>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		public static int CountColumns(ExcelWorksheet worksheet)
		{
			WorksheetCannotBeNull(worksheet);

			if(worksheet.Dimension == null)
				return 0;
			return worksheet.Dimension.End.Column;
		}

		/// <summary>
		/// Returns the number of Worksheets in the ExcelPacakge.
		/// </summary>
		/// <exception cref="ArgumentException">ExcelPackage cannot be null.</exception>
		public static int CountWorksheets(ExcelPackage excelPackage)
		{
			ExcelPackageCannotBeNull(excelPackage);

			return excelPackage.Workbook.Worksheets.Count;
		}

		/// <summary>
		/// Convert column integer index to character index.
		/// </summary>
		/// <example>1 becomes A</example>
		/// <example>26 becomes Z</example>
		/// <example>27 becomes AA</example>
		/// <param name="columnNumber">1-based index of column</param>
		public static string ColumnChar(int columnNumber)
		{
			int dividend = columnNumber;
			string columnName = String.Empty;
			int modulo;

			int ALPHABET_LENGTH = 26;
			int A_CHAR_VALUE = 65;

			while(dividend > 0)
			{
				modulo = (dividend - 1) % ALPHABET_LENGTH;
				columnName = Convert.ToChar(A_CHAR_VALUE + modulo).ToString() + columnName;
				dividend = (int)((dividend - modulo) / ALPHABET_LENGTH);
			}

			return columnName;
		}

		/// <summary>
		/// Remove all rows and columns from Worksheet.
		/// </summary>
		/// <exception cref="ArgumentException">Worksheet cannot be null.</exception>
		public static void Clear(ExcelWorksheet worksheet)
		{
			WorksheetCannotBeNull(worksheet);

			worksheet.DeleteRow(rowFrom: MIN_ROW_INDEX, rows: CountRows(worksheet), shiftOtherRowsUp: true);
		}

		private static void ExcelPackageCannotBeNull(ExcelPackage excelPackage)
		{
			if(excelPackage == null)
				throw new ArgumentException("ExcelPackage cannot be null.");
		}

		private static void WorksheetCannotBeNull(ExcelWorksheet worksheet)
		{
			if(worksheet == null)
				throw new ArgumentException("Worksheet cannot be null.");
		}
	}
}
