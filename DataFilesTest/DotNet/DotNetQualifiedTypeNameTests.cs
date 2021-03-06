﻿using System;
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

		[TestInitialize]
		public void Initialize()
		{
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			DotNetSettings.UseDefaultQualifiedNameConverter(false);
#else
			DotNetSettings.QualifiedNameConverter = null;
#endif
		}

		[TestCleanup]
		public void Cleanup()
		{
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			DotNetSettings.UseDefaultQualifiedNameConverter(true);
#else
			DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
#endif
		}

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
			string expectedLocalName = "C3";
			string expectedQualifiedName = "C3";
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

		[TestMethod]
		public void DotNetQualifiedTypeName_Clone_NullFullNamespace()
		{
			//arrange
			DotNetQualifiedTypeName a = DotNetQualifiedTypeName.FromVisualStudioXml("MyType");
			//act
			DotNetQualifiedTypeName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_Clone_ManySegments()
		{
			//arrange
			DotNetQualifiedTypeName a = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C.MyType");
			//act
			DotNetQualifiedTypeName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_Clone_GenericType()
		{
			//arrange
			DotNetQualifiedTypeName a = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C.MyType{System.Int32}");
			//act
			DotNetQualifiedTypeName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual("System.Int32", result.GenericTypeParameters[0].ToString());
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_Clone_NestedGenericType()
		{
			//arrange
			DotNetQualifiedTypeName a = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C{System.String}.MyType{System.Int32}");
			//act
			DotNetQualifiedTypeName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual("System.Int32", result.GenericTypeParameters[0].ToString());
			Assert.AreEqual("System.String", result.FullTypeNamespace.GenericTypeParameters[0].ToString());
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_GetLocalized_GenericType_NotLocal()
		{
			//arrange
			DotNetQualifiedTypeName a = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C.MyType{System.Int32}");
			DotNetQualifiedTypeName b = DotNetQualifiedTypeName.FromVisualStudioXml("D.E");
			//act
			DotNetQualifiedTypeName result = a.GetLocalized(b);
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual("System.Int32", result.GenericTypeParameters[0].ToString());
			Assert.AreEqual("A.B.C.MyType<System.Int32>", result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedTypeName_GetLocalized_GenericType_LocalizeParameter()
		{
			//arrange
			DotNetQualifiedTypeName a = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C.MyType{A.B.C.OtherType}");
			DotNetQualifiedTypeName b = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C");
			//act
			DotNetQualifiedTypeName result = a.GetLocalized(b);
			//assert
			Assert.AreEqual("MyType<OtherType>", result.FullName);
			Assert.AreEqual("OtherType", result.GenericTypeParameters[0].ToString());
		}
	}
}
