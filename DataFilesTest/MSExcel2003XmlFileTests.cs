using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFormatsTest
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

		private string LoadText(string fullPath)
		{
			using(StreamReader reader = new StreamReader(fullPath))
			{
				return reader.ReadToEnd().Replace("\r", "");
			}
		}
	}
}
