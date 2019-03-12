using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using WithoutHaste.DataFiles.Excel;

namespace DataFilesTest
{
	[TestClass]
	public class ExcelPackageHelperTests
	{
		[TestMethod]
		public void ExcelPackageHelper_AddWorksheet()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			//act
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			//assert
			Assert.AreEqual(name, worksheet.Name);
			Assert.AreEqual(1, ExcelPackageHelper.CountWorksheets(package));
			Assert.AreEqual(0, ExcelPackageHelper.CountRows(worksheet));
			Assert.AreEqual(0, ExcelPackageHelper.CountColumns(worksheet));
		}

		[TestMethod]
		public void ExcelPackageHelper_GetWorksheet()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheetA = ExcelPackageHelper.AddWorksheet(package, name);
			//act
			ExcelWorksheet worksheetB = ExcelPackageHelper.GetWorksheet(package, name);
			//assert
			Assert.AreEqual(worksheetA, worksheetB);
		}

		[TestMethod]
		public void ExcelPackageHelper_GetWorksheet_Null()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			//act
			ExcelWorksheet worksheet = ExcelPackageHelper.GetWorksheet(package, name);
			//assert
			Assert.AreEqual(null, worksheet);
		}

		[TestMethod]
		public void ExcelPackageHelper_AppendRow_GetRow()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<object> row1 = new List<object>() { null, 123, "abc", "", new DateTime(1999, 3, 2) };
			List<object> row2 = new List<object>() { 456, "", null, null, null, null, 789 };
			//act
			ExcelPackageHelper.AppendRow(worksheet, row1);
			ExcelPackageHelper.AppendRow(worksheet, row2);
			List<object> result1 = ExcelPackageHelper.GetRow(worksheet, 1);
			List<object> result2 = ExcelPackageHelper.GetRow(worksheet, 2);
			//assert
			Assert.AreEqual(2, ExcelPackageHelper.CountRows(worksheet));
			Assert.AreEqual(Math.Max(row1.Count, row2.Count), ExcelPackageHelper.CountColumns(worksheet));
			for(int i = 0; i < row1.Count; i++)
			{
				Assert.AreEqual(row1[i], result1[i]);
			}
			for(int i = 0; i < row2.Count; i++)
			{
				Assert.AreEqual(row2[i], result2[i]);
			}
		}

		[TestMethod]
		public void ExcelPackageHelper_SetColumnByIndex_GetColumnByIndex()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<object> col1 = new List<object>() { null, 123, "abc", "", new DateTime(1999, 3, 2) };
			List<object> col2 = new List<object>() { 456, "", null, null, null, null, 789 };
			//act
			ExcelPackageHelper.SetColumnByIndex(worksheet, 1, col1, skipFirstRow:false);
			ExcelPackageHelper.SetColumnByIndex(worksheet, 2, col2, skipFirstRow:false);
			List<object> result1 = ExcelPackageHelper.GetColumnByIndex(worksheet, 1, skipFirstRow:false);
			List<object> result2 = ExcelPackageHelper.GetColumnByIndex(worksheet, 2, skipFirstRow:false);
			//assert
			Assert.AreEqual(2, ExcelPackageHelper.CountColumns(worksheet));
			Assert.AreEqual(Math.Max(col1.Count, col2.Count), ExcelPackageHelper.CountRows(worksheet));
			for(int i = 0; i < col1.Count; i++)
			{
				Assert.AreEqual(col1[i], result1[i]);
			}
			for(int i = 0; i < col2.Count; i++)
			{
				Assert.AreEqual(col2[i], result2[i]);
			}
		}

		[TestMethod]
		public void ExcelPackageHelper_SetColumnByChar_GetColumnByChar()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<object> col1 = new List<object>() { null, 123, "abc", "", new DateTime(1999, 3, 2) };
			List<object> col2 = new List<object>() { 456, "", null, null, null, null, 789 };
			//act
			ExcelPackageHelper.SetColumnByChar(worksheet, "A", col1, skipFirstRow: false);
			ExcelPackageHelper.SetColumnByChar(worksheet, "B", col2, skipFirstRow: false);
			List<object> result1 = ExcelPackageHelper.GetColumnByChar(worksheet, "A", skipFirstRow: false);
			List<object> result2 = ExcelPackageHelper.GetColumnByChar(worksheet, "B", skipFirstRow: false);
			//assert
			Assert.AreEqual(2, ExcelPackageHelper.CountColumns(worksheet));
			Assert.AreEqual(Math.Max(col1.Count, col2.Count), ExcelPackageHelper.CountRows(worksheet));
			for(int i = 0; i < col1.Count; i++)
			{
				Assert.AreEqual(col1[i], result1[i]);
			}
			for(int i = 0; i < col2.Count; i++)
			{
				Assert.AreEqual(col2[i], result2[i]);
			}
		}

		[TestMethod]
		public void ExcelPackageHelper_SetColumnByHeader_GetColumnByHeader()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<string> headers = new List<string>() { "ColA", "ColB" };
			List<object> col1 = new List<object>() { null, 123, "abc", "", new DateTime(1999, 3, 2) };
			List<object> col2 = new List<object>() { 456, "", null, null, null, null, 789 };
			//act
			ExcelPackageHelper.AppendRow(worksheet, headers);
			ExcelPackageHelper.SetColumnByHeader(worksheet, headers[0], col1);
			ExcelPackageHelper.SetColumnByHeader(worksheet, headers[1], col2);
			List<object> result1 = ExcelPackageHelper.GetColumnByHeader(worksheet, headers[0]);
			List<object> result2 = ExcelPackageHelper.GetColumnByHeader(worksheet, headers[1]);
			//assert
			Assert.AreEqual(headers[0], worksheet.Cells["A1"].Value.ToString());
			Assert.AreEqual(headers[1], worksheet.Cells["B1"].Value.ToString());
			Assert.AreEqual(2, ExcelPackageHelper.CountColumns(worksheet));
			Assert.AreEqual(Math.Max(col1.Count, col2.Count) + 1, ExcelPackageHelper.CountRows(worksheet));
			for(int i = 0; i < col1.Count; i++)
			{
				Assert.AreEqual(col1[i], result1[i]);
			}
			for(int i = 0; i < col2.Count; i++)
			{
				Assert.AreEqual(col2[i], result2[i]);
			}
		}

		[TestMethod]
		public void ExcelPackageHelper_GetColumnCharForHeader()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<string> headers = new List<string>() { "ColA", "ColB", "ColC" };
			//act
			ExcelPackageHelper.AppendRow(worksheet, headers);
			string a = ExcelPackageHelper.GetColumnCharForHeader(worksheet, headers[0]);
			string b = ExcelPackageHelper.GetColumnCharForHeader(worksheet, headers[1]);
			string c = ExcelPackageHelper.GetColumnCharForHeader(worksheet, headers[2]);
			string d = ExcelPackageHelper.GetColumnCharForHeader(worksheet, "Other");
			//assert
			Assert.AreEqual("A", a);
			Assert.AreEqual("B", b);
			Assert.AreEqual("C", c);
			Assert.IsNull(d);
		}

		[TestMethod]
		public void ExcelPackageHelper_CountRows_FromRows()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<string> row = new List<string>() { "ColA", "ColB", "ColC" };
			//act
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.AppendRow(worksheet, row);
			int count = ExcelPackageHelper.CountRows(worksheet);
			//assert
			Assert.AreEqual(4, count);
		}

		[TestMethod]
		public void ExcelPackageHelper_CountRows_FromColumns()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<object> colA = new List<object>() { "ColA", "ColB", "ColC" };
			List<object> colB = new List<object>() { "ColA" };
			List<object> colC = new List<object>() { "ColA", "ColB", "ColC", "ColD", "ColE" };
			//act
			ExcelPackageHelper.SetColumnByIndex(worksheet, 1, colA, skipFirstRow:false);
			ExcelPackageHelper.SetColumnByIndex(worksheet, 2, colB, skipFirstRow:false);
			ExcelPackageHelper.SetColumnByIndex(worksheet, 3, colC, skipFirstRow:false);
			int count = ExcelPackageHelper.CountRows(worksheet);
			//assert
			Assert.AreEqual(5, count);
		}

		[TestMethod]
		public void ExcelPackageHelper_CountColumns_FromRows()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<object> rowA = new List<object>() { "ColA", "ColB", "ColC" };
			List<object> rowB = new List<object>() { "ColA" };
			List<object> rowC = new List<object>() { "ColA", "ColB", "ColC", "ColD", "ColE" };
			//act
			ExcelPackageHelper.AppendRow(worksheet, rowA);
			ExcelPackageHelper.AppendRow(worksheet, rowB);
			ExcelPackageHelper.AppendRow(worksheet, rowC);
			int count = ExcelPackageHelper.CountColumns(worksheet);
			//assert
			Assert.AreEqual(5, count);
		}

		[TestMethod]
		public void ExcelPackageHelper_CountColumns_FromColumns()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<object> colB = new List<object>() { "ColA", "ColB", "ColC" };
			List<object> colD = new List<object>() { "ColA" };
			List<object> colF = new List<object>() { "ColA", "ColB", "ColC", "ColD", "ColE" };
			//act
			ExcelPackageHelper.SetColumnByChar(worksheet, "B", colB, skipFirstRow:false);
			ExcelPackageHelper.SetColumnByChar(worksheet, "D", colD, skipFirstRow:false);
			ExcelPackageHelper.SetColumnByChar(worksheet, "F", colF, skipFirstRow:false);
			int count = ExcelPackageHelper.CountColumns(worksheet);
			//assert
			Assert.AreEqual(6, count);
		}

		[TestMethod]
		public void ExcelPackageHelper_CountWorksheets()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			//act
			ExcelPackageHelper.AddWorksheet(package, name+1);
			ExcelPackageHelper.AddWorksheet(package, name+2);
			ExcelPackageHelper.AddWorksheet(package, name+3);
			int count = ExcelPackageHelper.CountWorksheets(package);
			//assert
			Assert.AreEqual(3, count);
		}

		[TestMethod]
		public void ExcelPackageHelper_ColumnChar()
		{
			//arrange
			Dictionary<int, string> expectedValues = new Dictionary<int, string>() {
				{ 1, "A" },
				{ 2, "B" },
				{ 25, "Y" },
				{ 26, "Z" },
				{ 27, "AA" },
				{ 28, "AB" }
			};
			//act
			//assert
			foreach(KeyValuePair<int, string> expectedValue in expectedValues)
			{
				Assert.AreEqual(expectedValue.Value, ExcelPackageHelper.ColumnChar(expectedValue.Key));
			}
		}

		[TestMethod]
		public void ExcelPackageHelper_Clear()
		{
			//arrange
			ExcelPackage package = new ExcelPackage();
			string name = "Title";
			ExcelWorksheet worksheet = ExcelPackageHelper.AddWorksheet(package, name);
			List<string> row = new List<string>() { "ColA", "ColB", "ColC" };
			//act
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.AppendRow(worksheet, row);
			ExcelPackageHelper.Clear(worksheet);
			int rowCount = ExcelPackageHelper.CountRows(worksheet);
			int columnCount = ExcelPackageHelper.CountColumns(worksheet);
			//assert
			Assert.AreEqual(0, rowCount);
			Assert.AreEqual(0, columnCount);
		}
	}
}
