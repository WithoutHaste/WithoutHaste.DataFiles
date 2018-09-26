﻿using System;
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
		public void Utilities_ReplaceUnescapedCharacters_Simple()
		{
			//arrange
			string input = "+a+bb+ccc.d+ee+";
			char escapeChar = '\\';
			char searchChar = '+';
			char replacementChar = '.';
			string expectedOutput = ".a.bb.ccc.d.ee.";
			//act
			string result = input.ReplaceUnescapedCharacters(escapeChar, searchChar, replacementChar);
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void Utilities_ReplaceUnescapedCharacters_SingleEscapes()
		{
			//arrange
			string input = @"\+a+bb\+ccc.d+e\e+";
			char escapeChar = '\\';
			char searchChar = '+';
			char replacementChar = '.';
			string expectedOutput = @"\+a.bb\+ccc.d.e\e.";
			//act
			string result = input.ReplaceUnescapedCharacters(escapeChar, searchChar, replacementChar);
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void Utilities_ReplaceUnescapedCharacters_MultipleEscapes()
		{
			//arrange
			string input = @"\\\+a+bb\\+ccc.d+e\e+";
			char escapeChar = '\\';
			char searchChar = '+';
			char replacementChar = '.';
			string expectedOutput = @"\\\+a.bb\\.ccc.d.e\e.";
			//act
			string result = input.ReplaceUnescapedCharacters(escapeChar, searchChar, replacementChar);
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
	}
}
