using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
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
			public void MethodReturnVoid() { }

			public int MethodReturnNormal() { return 0; }

			public List<int> MethodReturnGeneric() { return new List<int>(); }

			public GenericOne<string>.NestedGeneric<int> MethodReturnNestedGeneric() { return new GenericOne<string>.NestedGeneric<int>(); }
		}

		protected class ParameterClass
		{
			public void MethodZeroParameters() { }

			public void MethodOneNormalParameter(int a) { }

			public void MethodTwoNormalParameters(int a, string b) { }

			public void MethodGeneric<A>(A a) { }
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnVoid()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnVoid' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnVoid";
			string expectedReturnFullName = "System.Void";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods()[0];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnNormal()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNormal' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNormal";
			string expectedReturnFullName = "System.Int32";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods()[1];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnGeneric()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnGeneric' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnGeneric";
			string expectedReturnFullName = "System.Collections.Generic.List<System.Int32>";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods()[2];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ReturnNestedGeneric()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNestedGeneric' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.NormalClass.MethodReturnNestedGeneric";
			string expectedReturnFullName = "DataFilesTest.DotNetMethodTests.GenericOne<System.String>.NestedGeneric<System.Int32>";
			Type type = typeof(NormalClass);
			MethodInfo methodInfo = type.GetMethods()[3];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(expectedReturnFullName, result.ReturnTypeName.FullName);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_ZeroParameters()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodZeroParameters' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodZeroParameters";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods()[0];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(0, result.Parameters.Count);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_OneNormalParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodOneNormalParameter(System.Int32)' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodOneNormalParameter";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods()[1];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.Parameters.Count);
			Assert.AreEqual("System.Int32", result.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_TwoNormalParameters()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodTwoNormalParameters(System.Int32,System.String)' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodTwoNormalParameters";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods()[2];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(2, result.Parameters.Count);
			Assert.AreEqual("System.Int32", result.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.Parameters[0].Name);
			Assert.AreEqual("System.String", result.Parameters[1].FullTypeName);
			Assert.AreEqual("b", result.Parameters[1].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericMethodOneParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.ParameterClass.MethodGeneric``1(``0)' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.ParameterClass.MethodGeneric<A>";
			Type type = typeof(ParameterClass);
			MethodInfo methodInfo = type.GetMethods()[3];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.Parameters.Count);
			Assert.AreEqual("A", result.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericClassOneParameter()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.GenericOne`1.MethodClassGeneric(`0)' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.GenericOne<T>.MethodClassGeneric";
			Type type = typeof(GenericOne<>);
			MethodInfo methodInfo = type.GetMethods()[0];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.Parameters.Count);
			Assert.AreEqual("T", result.Parameters[0].FullTypeName);
			Assert.AreEqual("t", result.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericClassGenericMethodOneParameterFromEach()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.GenericOne`1.MethodMethodAndClassGeneric``1(``0,`0)' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.GenericOne<T>.MethodMethodAndClassGeneric<A>";
			Type type = typeof(GenericOne<>);
			MethodInfo methodInfo = type.GetMethods()[1];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(2, result.Parameters.Count);
			Assert.AreEqual("A", result.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.Parameters[0].Name);
			Assert.AreEqual("T", result.Parameters[1].FullTypeName);
			Assert.AreEqual("t", result.Parameters[1].Name);
		}

		[TestMethod]
		public void DotNetMethod_FromAssembly_GenericClassGenericMethodOneParameterFromMethod()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodTests.GenericOne`1.MethodMethodGeneric``1(``0)' />");
			string expectedFullName = "DataFilesTest.DotNetMethodTests.GenericOne<T>.MethodMethodGeneric<A>";
			Type type = typeof(GenericOne<>);
			MethodInfo methodInfo = type.GetMethods()[2];
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.Name.FullName);
			Assert.AreEqual(1, result.Parameters.Count);
			Assert.AreEqual("A", result.Parameters[0].FullTypeName);
			Assert.AreEqual("a", result.Parameters[0].Name);
		}
	}
}
