using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetQualifiedTypeNameTests
	{
		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_SimpleIntenalType()
		{
			//arrange
			string input = "A.B.MyType";
			string expectedLocalName = "MyType";
			string expectedQualifiedName = "A.B.MyType";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_SimpleExternalType()
		{
			//arrange
			string input = "System.Xml.Linq.XDocument";
			string expectedLocalName = "XDocument";
			string expectedQualifiedName = "System.Xml.Linq.XDocument";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_ClassGeneric()
		{
			//arrange
			string input = "`0";
			string expectedLocalName = "T";
			string expectedQualifiedName = "T";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.IsTrue(result is DotNetReferenceClassGeneric);
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_ClassGenericDoubleDigit()
		{
			//arrange
			string input = "`10";
			string expectedLocalName = "V3";
			string expectedQualifiedName = "V3";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.IsTrue(result is DotNetReferenceClassGeneric);
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_MethodGeneric()
		{
			//arrange
			string input = "``0";
			string expectedLocalName = "A";
			string expectedQualifiedName = "A";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.IsTrue(result is DotNetReferenceMethodGeneric);
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_MethodGenericDoubleDigit()
		{
			//arrange
			string input = "``10";
			string expectedLocalName = "B4";
			string expectedQualifiedName = "B4";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.IsTrue(result is DotNetReferenceMethodGeneric);
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_SpecifiedGeneric()
		{
			//arrange
			string input = "System.Collections.Generic.List{System.Integer}";
			string expectedLocalName = "List<System.Integer>";
			string expectedQualifiedName = "System.Collections.Generic.List<System.Integer>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_SpecifiedClassGeneric()
		{
			//arrange
			string input = "System.Collections.Generic.List{`1}";
			string expectedLocalName = "List<U>";
			string expectedQualifiedName = "System.Collections.Generic.List<U>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_SpecifiedMethodGeneric()
		{
			//arrange
			string input = "System.Collections.Generic.List{``1}";
			string expectedLocalName = "List<B>";
			string expectedQualifiedName = "System.Collections.Generic.List<B>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_NestedSpecifiedGeneric()
		{
			//arrange
			string input = "System.Collections.Generic.List{Test.MyType{System.Integer}}";
			string expectedLocalName = "List<Test.MyType<System.Integer>>";
			string expectedQualifiedName = "System.Collections.Generic.List<Test.MyType<System.Integer>>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_OtherNestedSpecifiedGeneric()
		{
			//arrange
			string input = "Namespace.TypeA{System.Int32}.TypeB{System.String,TypeD}";
			string expectedLocalName = "TypeB<System.String,TypeD>";
			string expectedQualifiedName = "Namespace.TypeA<System.Int32>.TypeB<System.String,TypeD>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_NestedSpecifiedClassGeneric()
		{
			//arrange
			string input = "System.Collections.Generic.List{Test.MyType{`1}}";
			string expectedLocalName = "List<Test.MyType<U>>";
			string expectedQualifiedName = "System.Collections.Generic.List<Test.MyType<U>>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_FromXml_NestedSpecifiedMethodGeneric()
		{
			//arrange
			string input = "System.Collections.Generic.List{Test.MyType{``1}}";
			string expectedLocalName = "List<Test.MyType<B>>";
			string expectedQualifiedName = "System.Collections.Generic.List<Test.MyType<B>>";
			//act
			DotNetQualifiedTypeName result = DotNetQualifiedTypeName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
		}
	}
}
