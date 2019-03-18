using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class UtilitiesTests
	{
		[TestMethod]
		public void Utilities_TrimAllowOneSpace_None()
		{
			//arrange
			string input = "abc def\nghi";
			//act
			string result = input.TrimAllowOneSpace();
			//assert
			Assert.AreEqual(input, result);
		}

		[TestMethod]
		public void Utilities_TrimAllowOneSpace_NoSpaces()
		{
			//arrange
			string input = " \tabc def\nghi\r\n ";
			string expectedOutput = "abc def\nghi";
			//act
			string result = input.TrimAllowOneSpace();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void Utilities_TrimAllowOneSpace_StartSpace()
		{
			//arrange
			string input = "\t abc def\nghi\r\n ";
			string expectedOutput = " abc def\nghi";
			//act
			string result = input.TrimAllowOneSpace();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void Utilities_TrimAllowOneSpace_EndSpace()
		{
			//arrange
			string input = " \tabc def\nghi \r\n ";
			string expectedOutput = "abc def\nghi ";
			//act
			string result = input.TrimAllowOneSpace();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void Utilities_TrimAllowOneSpace_BothSpaces()
		{
			//arrange
			string input = "        abc def\nghi \r\n ";
			string expectedOutput = " abc def\nghi ";
			//act
			string result = input.TrimAllowOneSpace();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsAbstract()
		{
			//arrange
			Type type = typeof(System.IO.Stream);
			//act
			bool result = type.Attributes.IsAbstract();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsNotAbstract()
		{
			//arrange
			Type type = typeof(System.IO.FileStream);
			//act
			bool result = type.Attributes.IsAbstract();
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsStatic()
		{
			//arrange
			Type type = typeof(System.IO.File);
			//act
			bool result = type.Attributes.IsStatic();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsNotStatic()
		{
			//arrange
			Type type = typeof(System.IO.FileStream);
			//act
			bool result = type.Attributes.IsStatic();
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsInterface()
		{
			//arrange
			Type type = typeof(System.IComparable);
			//act
			bool result = type.Attributes.IsInterface();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsNotInterface()
		{
			//arrange
			Type type = typeof(System.IO.FileStream);
			//act
			bool result = type.Attributes.IsInterface();
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsEnum()
		{
			//arrange
			Type type = typeof(System.Reflection.TypeAttributes);
			//act
			bool result = type.IsEnum();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsNotEnum()
		{
			//arrange
			Type type = typeof(System.IO.FileStream);
			//act
			bool result = type.IsEnum();
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsException()
		{
			//arrange
			Type type = typeof(System.Exception);
			//act
			bool result = type.IsException();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsExceptionChild()
		{
			//arrange
			Type type = typeof(System.ArgumentException);
			//act
			bool result = type.IsException();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsExceptionGrandChild()
		{
			//arrange
			Type type = typeof(System.ArgumentOutOfRangeException);
			//act
			bool result = type.IsException();
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Utilities_TypeInfo_IsNotException()
		{
			//arrange
			Type type = typeof(System.IO.FileStream);
			//act
			bool result = type.IsException();
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Utilities_XNodeToString_AttributeValueWithDoubleQuotes()
		{
			//arrange
			string elementString = "<tag attr='\"with quotes\"'></tag>";
			string expectedResult = "<tag attr=\"\"with quotes\"\" ></tag>";
			XElement element = XElement.Parse(elementString, LoadOptions.PreserveWhitespace);
			//act
			string result = WithoutHaste.DataFiles.DotNet.Utilities.XNodeToString(element);
			//assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void Utilities_XNodeToString_AttributeValueWithSingleQuotes()
		{
			//arrange
			string elementString = "<tag attr=\"'with quotes'\"></tag>";
			string expectedResult = "<tag attr=\"'with quotes'\" ></tag>";
			XElement element = XElement.Parse(elementString, LoadOptions.PreserveWhitespace);
			//act
			string result = WithoutHaste.DataFiles.DotNet.Utilities.XNodeToString(element);
			//assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void Utilities_XNodeToString_PlainTextWithLessThanCode()
		{
			//arrange
			string elementString = "<tag>some plain &lt; text</tag>";
			string expectedResult = "<tag >some plain &lt; text</tag>";
			XElement element = XElement.Parse(elementString, LoadOptions.PreserveWhitespace);
			//act
			string result = WithoutHaste.DataFiles.DotNet.Utilities.XNodeToString(element);
			//assert
			Assert.AreEqual(expectedResult, result);
		}
	}
}
