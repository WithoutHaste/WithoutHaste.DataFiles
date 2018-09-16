using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFilesTest
{
	[TestClass]
	public class MSExcel2003XmlFileTests
	{
		[TestMethod]
		public void MSExcel2003XmlFile_DefaultFile()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_Default.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_AddWorksheet_GetTableIndex()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_AddWorksheet.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string title0 = "A";
			string title1 = "B";
			string title2 = "C";
			//act
			xmlFile.AddWorksheet(title0);
			xmlFile.AddWorksheet(title1);
			xmlFile.AddWorksheet(title2);
			int index0 = xmlFile.GetTableIndex(title0);
			int index1 = xmlFile.GetTableIndex(title1);
			int index2 = xmlFile.GetTableIndex(title2);
			int indexNone = xmlFile.GetTableIndex("D");
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(0, index0);
			Assert.AreEqual(1, index1);
			Assert.AreEqual(2, index2);
			Assert.AreEqual(-1, indexNone);
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_AddStyle()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_AddStyle.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string styleId = "NewId";
			string childName = "ChildName";
			string childAttributeName = "ChildAttributeName";
			string childAttributeValue = "ChildAttributeValue";
			//act
			xmlFile.AddStyle(styleId, childName, childAttributeName, childAttributeValue);
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_AddStyle_Overwrite()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_AddStyle_Overwrite.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string styleId = "Header";
			string childName = "ChildName";
			string childAttributeName = "ChildAttributeName";
			string childAttributeValue = "ChildAttributeValue";
			//act
			xmlFile.AddStyle(styleId, childName, childAttributeName, childAttributeValue);
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_RemoveStyle()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_RemoveStyle.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string styleId = "header";
			//act
			xmlFile.RemoveStyle(styleId);
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_SetColumnWidths()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_SetColumnWidths.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string title = "title";
			List<int> widths = new List<int>() { 20, 25, 10 };
			//act
			int tableIndex = xmlFile.AddWorksheet(title);
			xmlFile.SetColumnWidths(tableIndex, widths);
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_SetColumnWidths_Overwrite()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_SetColumnWidths_Overwrite.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string title = "title";
			List<int> widthsA = new List<int>() { 20, 25, 10 };
			List<int> widthsB = new List<int>() { 45, 15 };
			//act
			int tableIndex = xmlFile.AddWorksheet(title);
			xmlFile.SetColumnWidths(tableIndex, widthsA);
			xmlFile.SetColumnWidths(tableIndex, widthsB);
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_AddHeaderRow_GetHeaders()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_AddHeaderRow_GetHeaders.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string title = "title";
			List<string> headers = new List<string>() { "Title", "Address", "Last Name" };
			//act
			int tableIndex = xmlFile.AddWorksheet(title);
			xmlFile.AddHeaderRow(tableIndex, headers);
			List<string> savedHeaders = xmlFile.GetHeaders(tableIndex);
			List<int> savedHeaderIndexes = headers.Select(h => xmlFile.GetHeaderIndex(tableIndex, h)).ToList();
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			//assert
			Assert.AreEqual(headers.Count, savedHeaders.Count);
			Assert.AreEqual(headers.Count, savedHeaderIndexes.Count);
			for(int i = 0; i < headers.Count; i++)
			{
				Assert.AreEqual(headers[i], savedHeaders[i]);
				Assert.AreEqual(i, savedHeaderIndexes[i]);
			}
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}
		
		[TestMethod]
		public void MSExcel2003XmlFile_AddRow_GetRowValues()
		{
			//arrange
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string title = "title";
			List<object> valuesA = new List<object>() { "abc", 123, new DateTime(1999, 3, 2) };
			List<object> valuesB = new List<object>() { "def", 456, new DateTime(1777, 5, 4) };
			//act
			int tableIndex = xmlFile.AddWorksheet(title);
			xmlFile.AddRow(tableIndex, valuesA);
			xmlFile.AddRow(tableIndex, valuesB);
			List<string> savedValuesA = xmlFile.GetRowValues(tableIndex, 0);
			List<string> savedValuesB = xmlFile.GetRowValues(tableIndex, 1);
			int savedRowCount = xmlFile.GetRowCount(tableIndex);
			//assert
			Assert.AreEqual(valuesA.Count, savedValuesA.Count);
			for(int i = 0; i < valuesA.Count; i++)
			{
				if(valuesA[i] is DateTime)
				{
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesA[i]), savedValuesA[i]);
				}
				else
				{
					Assert.AreEqual(valuesA[i].ToString(), savedValuesA[i]);
				}
			}
			Assert.AreEqual(valuesB.Count, savedValuesB.Count);
			for(int i = 0; i < valuesB.Count; i++)
			{
				if(valuesB[i] is DateTime)
				{
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesB[i]), savedValuesB[i]);
				}
				else
				{
					Assert.AreEqual(valuesB[i].ToString(), savedValuesB[i]);
				}
			}
			Assert.AreEqual(2, savedRowCount);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_AddColumns_GetColumnValues()
		{
			//arrange
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			string title = "title";
			List<string> headers = new List<string>() { "A", "B", "C" };
			List<object> valuesA = new List<object>() { "abc", 123, new DateTime(1999, 3, 2), 100 };
			List<object> valuesB = new List<object>() { new DateTime(1777, 5, 4), "def", "ghijk", "lm", "nop", "qrstuv" };
			List<object> valuesC = new List<object>() { 456, 400 };
			List<List<object>> columns = new List<List<object>>() { valuesA, valuesB, valuesC };
			int maxRowCount = Math.Max(valuesA.Count, Math.Max(valuesB.Count, valuesC.Count));
			//act
			int tableIndex = xmlFile.AddWorksheet(title);
			xmlFile.AddHeaderRow(tableIndex, headers);
			xmlFile.AddColumns(tableIndex, columns);
			string text = XmlDocumentHelper.XmlToString(xmlFile.XmlDocument);
			List<string> savedValues0 = xmlFile.GetColumnValues(tableIndex, 0, firstRowIsHeader: true);
			List<string> savedValues1 = xmlFile.GetColumnValues(tableIndex, 1, firstRowIsHeader: true);
			List<string> savedValues2 = xmlFile.GetColumnValues(tableIndex, 2, firstRowIsHeader: true);
			List<string> savedValues0WithHeader = xmlFile.GetColumnValues(tableIndex, 0, firstRowIsHeader: false);
			List<string> savedValues1WithHeader = xmlFile.GetColumnValues(tableIndex, 1, firstRowIsHeader: false);
			List<string> savedValues2WithHeader = xmlFile.GetColumnValues(tableIndex, 2, firstRowIsHeader: false);
			List<string> savedValuesA = xmlFile.GetColumnValues(tableIndex, "A");
			List<string> savedValuesB = xmlFile.GetColumnValues(tableIndex, "B");
			List<string> savedValuesC = xmlFile.GetColumnValues(tableIndex, "C");
			int savedColumnCount = xmlFile.GetColumnCount(tableIndex);
			//assert
			Assert.AreEqual(maxRowCount, savedValues0.Count);
			Assert.AreEqual(maxRowCount, savedValues1.Count);
			Assert.AreEqual(maxRowCount, savedValues2.Count);
			Assert.AreEqual(maxRowCount+1, savedValues0WithHeader.Count);
			Assert.AreEqual(maxRowCount+1, savedValues1WithHeader.Count);
			Assert.AreEqual(maxRowCount+1, savedValues2WithHeader.Count);
			Assert.AreEqual(headers[0], savedValues0WithHeader[0]);
			Assert.AreEqual(headers[1], savedValues1WithHeader[0]);
			Assert.AreEqual(headers[2], savedValues2WithHeader[0]);
			for(int i = 0; i < valuesA.Count; i++)
			{
				if(valuesA[i] is DateTime)
				{
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesA[i]), savedValues0[i]);
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesA[i]), savedValues0WithHeader[i + 1]);
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesA[i]), savedValuesA[i]);
				}
				else
				{
					Assert.AreEqual(valuesA[i].ToString(), savedValues0[i]);
					Assert.AreEqual(valuesA[i].ToString(), savedValues0WithHeader[i + 1]);
					Assert.AreEqual(valuesA[i].ToString(), savedValuesA[i]);
				}
			}
			for(int i = 0; i < valuesB.Count; i++)
			{
				if(valuesB[i] is DateTime)
				{
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesB[i]), savedValues1[i]);
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesB[i]), savedValues1WithHeader[i+1]);
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesB[i]), savedValuesB[i]);
				}
				else
				{
					Assert.AreEqual(valuesB[i].ToString(), savedValues1[i]);
					Assert.AreEqual(valuesB[i].ToString(), savedValues1WithHeader[i+1]);
					Assert.AreEqual(valuesB[i].ToString(), savedValuesB[i]);
				}
			}
			for(int i = 0; i < valuesC.Count; i++)
			{
				if(valuesC[i] is DateTime)
				{
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesC[i]), savedValues2[i]);
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesC[i]), savedValues2WithHeader[i+1]);
					Assert.AreEqual(MSExcel2003XmlFile.DateToString((DateTime)valuesC[i]), savedValuesC[i]);
				}
				else
				{
					Assert.AreEqual(valuesC[i].ToString(), savedValues2[i]);
					Assert.AreEqual(valuesC[i].ToString(), savedValues2WithHeader[i+1]);
					Assert.AreEqual(valuesC[i].ToString(), savedValuesC[i]);
				}
			}
			Assert.AreEqual(3, savedColumnCount);
		}

		#region Generate

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateWidthAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_WidthAttribute.txt";
			int width = 50;
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateWidthAttribute(width);
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateStyleIdAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_StyleIdAttribute.txt";
			string styleId = "50";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateStyleIdAttribute(styleId);
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateNameAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_NameAttribute.txt";
			string name = "Bob";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateNameAttribute(name);
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateStringTypeAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_StringTypeAttribute.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateStringTypeAttribute();
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateNumberTypeAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_NumberTypeAttribute.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateNumberTypeAttribute();
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateDateTypeAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_DateTypeAttribute.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateDateTypeAttribute();
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateTypeAttribute()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_TypeAttribute.txt";
			string type = "Word";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateTypeAttribute(type);
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateAttribute_KnownUri()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_Attribute.txt";
			string prefix = "c";
			string name = "Bob";
			string value = "100";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateAttribute(prefix, name, value);
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateAttribute_NewUri()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_Attribute_Uri.txt";
			string prefix = "zz";
			string uri = "google.com";
			string name = "Bob";
			string value = "100";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			XmlNode node = xmlFile.XmlDocument.CreateElement("A", xmlFile.XmlDocument.NamespaceURI);
			//act
			XmlAttribute attribute = xmlFile.GenerateAttribute(prefix, uri, name, value);
			node.Attributes.Append(attribute);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateCell_DateTime()
		{
			//arrange
			DateTime data = new DateTime(1999, 3, 2);
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode nodeA = xmlFile.GenerateCell(data);
			XmlNode nodeB = xmlFile.GenerateDateCell(data);
			string textA = XmlDocumentHelper.XmlToString(nodeA);
			string textB = XmlDocumentHelper.XmlToString(nodeB);
			//assert
			Assert.AreEqual(textA, textB);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateCell_Integer()
		{
			//arrange
			int data = 123;
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode nodeA = xmlFile.GenerateCell(data);
			XmlNode nodeB = xmlFile.GenerateNumberCell(data);
			string textA = XmlDocumentHelper.XmlToString(nodeA);
			string textB = XmlDocumentHelper.XmlToString(nodeB);
			//assert
			Assert.AreEqual(textA, textB);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateCell_String()
		{
			//arrange
			string data = "abc";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode nodeA = xmlFile.GenerateCell(data);
			XmlNode nodeB = xmlFile.GenerateTextCell(data);
			string textA = XmlDocumentHelper.XmlToString(nodeA);
			string textB = XmlDocumentHelper.XmlToString(nodeB);
			//assert
			Assert.AreEqual(textA, textB);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateDateCell()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_DateCell.txt";
			DateTime date = new DateTime(1999, 3, 2);
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateDateCell(date);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateNumberCell()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_NumberCell.txt";
			int number = 100;
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateNumberCell(number);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateHeaderCell()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_HeaderCell.txt";
			string header = "Title";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateHeaderCell(header);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateParagraphCell()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_ParagraphCell.txt";
			string paragraph = "A\nBb\nCcc";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateParagraphCell(paragraph);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateTextCell()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_TextCell.txt";
			string t = "abc";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateTextCell(t);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateTextCell_NoStyleId()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_TextCell.txt";
			string t = "abc";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateTextCell(t, null);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateTextCell_Style()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_TextCell_StyleId.txt";
			string t = "abc";
			string styleId = "italic";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateTextCell(t, styleId);
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		[TestMethod]
		public void MSExcel2003XmlFile_GenerateEmptyCell()
		{
			//arrange
			string comparisonFilename = "data/MSExcel2003XmlFile_EmptyCell.txt";
			MSExcel2003XmlFile xmlFile = new MSExcel2003XmlFile();
			//act
			XmlNode node = xmlFile.GenerateEmptyCell();
			string text = XmlDocumentHelper.XmlToString(node);
			//assert
			Assert.AreEqual(LoadText(comparisonFilename), text);
		}

		#endregion

		private string LoadText(string fullPath)
		{
			using(StreamReader reader = new StreamReader(fullPath))
			{
				return reader.ReadToEnd().Replace("\r", "");
			}
		}
	}
}
