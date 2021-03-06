﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	public static class StaticTestClass
	{
		public static void MethodExtension(this string a, string b) { }
	}

	[TestClass]
	public class DotNetMethodTests
	{
		protected class GenericOne<T>
		{
			public void MethodClassGeneric(T t) { }

			public void MethodMethodAndClassGeneric<A>(A a, T t) { }

			public void MethodMethodGeneric<A>(A a) { }

			public class NestedGeneric<U>
			{
			}
		}

		protected class NormalClass
		{
			static NormalClass() { }

			public void MethodReturnVoid() { }

			public int MethodReturnNormal() { return 0; }

			public List<int> MethodReturnGeneric() { return new List<int>(); }

			public GenericOne<string>.NestedGeneric<int> MethodReturnNestedGeneric() { return new GenericOne<string>.NestedGeneric<int>(); }

			public void MethodOneGeneric<CustomA>() { }

			public void MethodOut(out int a) { a = 0; }

			public void MethodRef(ref int a) { a = 0; }

			public void MethodOptional(int a = 5, string b = null) { }
		}

		protected class ParameterClass
		{
			public void MethodZeroParameters() { }

			public void MethodOneNormalParameter(int a) { }

			public void MethodTwoNormalParameters(int a, string b) { }

			public void MethodGeneric<A>(A a) { }
		}

		public abstract class AbstractClass
		{
			public abstract void MethodAbstract();

			protected void MethodProtected() { }
		}

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
		public void DotNetMethod_FromXml_GenericParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:Demo.DoubleGenericClass`2.op_Addition(`0,Demo.DoubleGenericClass{`0,`1})' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "Demo.DoubleGenericClass<T,U>.op_Addition";
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnVoid()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnVoid' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnVoid";
			string expectedReturnFullName = "System.Void";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodReturnVoid");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.MethodName.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnNormal()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNormal' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNormal";
			string expectedReturnFullName = "System.Int32";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodReturnNormal");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.MethodName.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnGeneric()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnGeneric' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnGeneric";
			string expectedReturnFullName = "System.Collections.Generic.List<System.Int32>";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodReturnGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.MethodName.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnNestedGeneric()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNestedGeneric' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNestedGeneric";
			string expectedReturnFullName = "DataFilesTest.DotNetMethodTests.GenericOne<System.String>.NestedGeneric<System.Int32>";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodReturnNestedGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.MethodName.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_MethodOneGeneric_AddedToMethod()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodOneGeneric``1' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodOneGeneric<CustomA>";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodOneGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_MethodOneGeneric_AddedToType()
		{
			//arrange
			XElement typeXmlElement = XElement.Parse("<member name='T:DataFilesTest.DotNetMethodTests.NormalClass' />", LoadOptions.PreserveWhitespace);
			XElement methodXmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodOneGeneric``1' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodOneGeneric<CustomA>";
			Type type = typeof(NormalClass);
			//act
			DotNetType typeResult = DotNetType.FromVisualStudioXml(typeXmlElement);
			DotNetMethod methodResult = DotNetMethod.FromVisualStudioXml(methodXmlElement);
			typeResult.AddMember(methodResult);

			Assert.IsTrue(methodResult.MatchesSignature(type.GetDeclaredMethods().First(m => m.Name == "MethodOneGeneric")));
			typeResult.AddAssemblyInfo(type, typeResult.Name);
			//assert
			Assert.AreEqual(expectedFullName, typeResult.Methods[0].Name.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ZeroParameters()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodZeroParameters' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodZeroParameters";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodZeroParameters");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(0, result.MethodName.Parameters.Count);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_OneNormalParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodOneNormalParameter(System.Int32)' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodOneNormalParameter";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodOneNormalParameter");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
			Assert.AreEqual("System.Int32", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_OutParameter()
		{
			//arrange
			XElement xmlTypeElement = XElement.Parse("<member name='T:DataFilesTest.DotNetMethodTests.NormalClass' />", LoadOptions.PreserveWhitespace);
			XElement xmlMemberElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodOut(System.Int32@)' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(NormalClass);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedClassName("DataFilesTest.DotNetMethodTests.NormalClass"));
			dotNetType.AddMember(DotNetMethod.FromVisualStudioXml(xmlMemberElement));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			DotNetMethod result = dotNetType.Methods[0];
			//assert
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
			Assert.AreEqual("System.Int32", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
			Assert.AreEqual(ParameterCategory.Out, result.MethodName.Parameters[0].Category);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_RefParameter()
		{
			//arrange
			XElement xmlTypeElement = XElement.Parse("<member name='T:DataFilesTest.DotNetMethodTests.NormalClass' />", LoadOptions.PreserveWhitespace);
			XElement xmlMemberElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodRef(System.Int32@)' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(NormalClass);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedClassName("DataFilesTest.DotNetMethodTests.NormalClass"));
			dotNetType.AddMember(DotNetMethod.FromVisualStudioXml(xmlMemberElement));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			DotNetMethod result = dotNetType.Methods[0];
			//assert
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
			Assert.AreEqual("System.Int32", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
			Assert.AreEqual(ParameterCategory.Ref, result.MethodName.Parameters[0].Category);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_OptionalParameter()
		{
			//arrange
			XElement xmlTypeElement = XElement.Parse("<member name='T:DataFilesTest.DotNetMethodTests.NormalClass' />", LoadOptions.PreserveWhitespace);
			XElement xmlMemberElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodOptional(System.Int32,System.String)' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(NormalClass);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedClassName("DataFilesTest.DotNetMethodTests.NormalClass"));
			dotNetType.AddMember(DotNetMethod.FromVisualStudioXml(xmlMemberElement));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			DotNetMethod result = dotNetType.Methods[0];
			//assert
			Assert.AreEqual(2, result.MethodName.Parameters.Count);
			Assert.AreEqual("System.Int32", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
			Assert.AreEqual(ParameterCategory.Optional, result.MethodName.Parameters[0].Category);
			Assert.AreEqual(5, result.MethodName.Parameters[0].DefaultValue);
			Assert.AreEqual("System.String", result.MethodName.Parameters[1].FullTypeName);
			Assert.AreEqual("b", result.MethodName.Parameters[1].Name);
			Assert.AreEqual(ParameterCategory.Optional, result.MethodName.Parameters[1].Category);
			Assert.IsNull(result.MethodName.Parameters[1].DefaultValue);
			Assert.AreEqual("System.String b = null", result.MethodName.Parameters[1].SignatureWithName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_TwoNormalParameters()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodTwoNormalParameters(System.Int32,System.String)' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodTwoNormalParameters";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodTwoNormalParameters");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(2, result.MethodName.Parameters.Count);
			Assert.AreEqual("System.Int32", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
			Assert.AreEqual("System.String", result.MethodName.Parameters[1].FullTypeName);
			Assert.AreEqual("b", result.MethodName.Parameters[1].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericMethodOneParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodGeneric``1(``0)' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodGeneric<A>";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
			Assert.AreEqual("A", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericClassOneParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.GenericOne`1.MethodClassGeneric(`0)' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.GenericOne<T>.MethodClassGeneric";
			Type type = typeof(GenericOne<>);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodClassGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
			Assert.AreEqual("T", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("t", result.MethodName.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericClassGenericMethodOneParameterFromEach()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.GenericOne`1.MethodMethodAndClassGeneric``1(``0,`0)' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.GenericOne<T>.MethodMethodAndClassGeneric<A>";
			Type type = typeof(GenericOne<>);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodMethodAndClassGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(2, result.MethodName.Parameters.Count);
			Assert.AreEqual("A", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
			Assert.AreEqual("T", result.MethodName.Parameters[1].FullTypeName);
			Assert.AreEqual("t", result.MethodName.Parameters[1].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericClassGenericMethodOneParameterFromMethod()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.GenericOne`1.MethodMethodGeneric``1(``0)' />", LoadOptions.PreserveWhitespace);
			string expectedFullName = "DataFilesTest.DotNetMethodTests.GenericOne<T>.MethodMethodGeneric<A>";
			Type type = typeof(GenericOne<>);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodMethodGeneric");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
			Assert.AreEqual("A", result.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.MethodName.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ParamComment()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodOneNormalParameter(System.Int32)'><param name='a'>Comments</param></member>", LoadOptions.PreserveWhitespace);
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodOneNormalParameter");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(1, result.ParameterComments.Count);
			Assert.IsNotNull(result.ParameterComments[0]);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_AbstractMethod()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.AbstractClass.MethodAbstract()'></member>", LoadOptions.PreserveWhitespace);
			Type type = typeof(AbstractClass);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "MethodAbstract");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(MethodCategory.Abstract, result.Category);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_AbstractClassProtectedMethod()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.AbstractClass.MethodProtected()'></member>", LoadOptions.PreserveWhitespace);
			Type type = typeof(AbstractClass);
			MethodInfo methodInfo = type.GetDeclaredMethods().First(m => m.Name == "MethodProtected");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(MethodCategory.Protected, result.Category);
		}

		[TestMethod]
		public void DotNetMethod_ThirdPartyTypes()
		{
			//arrange
			string xmlDocumentationFilename = Path.Combine(Utilities.GetProjectDirectory(), "../ThirdPartyTest/bin/Debug/ThirdPartyTest.XML");
			string dllFilename = Path.Combine(Utilities.GetProjectDirectory(), "../ThirdPartyTest/bin/Debug/ThirdPartyTest.dll");
			string thirdPartyDllFilename = Path.Combine(Utilities.GetProjectDirectory(), "../ThirdPartyTest/bin/Debug/Markdown.dll");
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename, thirdPartyDllFilename);
			//assert
			DotNetType type = xmlDocumentation.Types[0];
			DotNetMethod methodA = type.Methods.First(m => m.Name.LocalName == "MethodA");
			DotNetMethod methodB = type.Methods.First(m => m.Name.LocalName == "MethodB");
			DotNetMethod methodC = type.Methods.First(m => m.Name.LocalName == "MethodC");
			Assert.AreEqual("HeyRed.MarkdownSharp.Markdown", methodA.MethodName.ReturnTypeName);
			Assert.AreEqual(0, methodA.MethodName.Parameters.Count);
			Assert.AreEqual("System.Void", methodB.MethodName.ReturnTypeName);
			Assert.AreEqual(1, methodB.MethodName.Parameters.Count);
			Assert.AreEqual("HeyRed.MarkdownSharp.Markdown", methodC.MethodName.ReturnTypeName);
			Assert.AreEqual(1, methodC.MethodName.Parameters.Count);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ExtensionMethodParameters()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.StaticTestClass.MethodExtension(System.String,System.String)'></member>", LoadOptions.PreserveWhitespace);
			Type type = typeof(StaticTestClass);
			MethodInfo methodInfo = type.GetDeclaredMethods().First(m => m.Name == "MethodExtension");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			Assert.AreEqual(MethodCategory.Static, result.Category);
			Assert.AreEqual(ParameterCategory.Normal, result.MethodName.Parameters[0].Category);
#else
			Assert.AreEqual(MethodCategory.Extension, result.Category);
			Assert.AreEqual(ParameterCategory.Extension, result.MethodName.Parameters[0].Category);
#endif
			Assert.AreEqual(ParameterCategory.Normal, result.MethodName.Parameters[1].Category);
		}
	}
}
