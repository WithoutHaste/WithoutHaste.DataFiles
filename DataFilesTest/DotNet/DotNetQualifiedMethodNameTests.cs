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
	public class DotNetQualifiedMethodNameTests
	{
		protected class OneGenericClass<CustomT>
		{
			public void MethodEmpty() { }

			public void MethodOne<CustomA>(CustomA a) { }
		}

		protected class TwoGenericClass<CustomT, CustomU>
		{
			public void MethodEmpty() { }

			public void MethodTwo<CustomA, CustomB>(CustomA a, CustomB b) { }
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromXml_ClassGenericParameters_OneGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass`1.MethodEmpty";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass<T>.MethodEmpty";
			Type type = typeof(OneGenericClass<>);
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromAssembly_ClassGenericParameters_OneGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass`1.MethodEmpty";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass<CustomT>.MethodEmpty";
			Type type = typeof(OneGenericClass<>);
			MethodInfo methodInfo = type.GetMethods()[0];
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromXml_ClassGenericParameters_TwoGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass`2.MethodEmpty";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass<T,U>.MethodEmpty";
			Type type = typeof(TwoGenericClass<,>);
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromAssembly_ClassGenericParameters_TwoGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass`2.MethodEmpty";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass<CustomT,CustomU>.MethodEmpty";
			Type type = typeof(TwoGenericClass<,>);
			MethodInfo methodInfo = type.GetMethods()[0];
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromXml_MethodGenericParameters_OneGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass`1.MethodOne``1(``0)";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass<T>.MethodOne<A>";
			Type type = typeof(OneGenericClass<>);
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromAssembly_MethodGenericParameters_OneGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass`1.MethodOne``1(``0)";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.OneGenericClass<CustomT>.MethodOne<CustomA>";
			Type type = typeof(OneGenericClass<>);
			MethodInfo methodInfo = type.GetMethods()[1];
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromXml_MethodGenericParameters_TwoGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass`2.MethodTwo``2(``0,``1)";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass<T,U>.MethodTwo<A,B>";
			Type type = typeof(TwoGenericClass<,>);
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_FromAssembly_MethodGenericParameters_TwoGeneric()
		{
			//arrange
			string xmlFormatName = "M:DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass`2.MethodTwo``2(``0,``1)";
			string expectedFullName = "DataFilesTest.DotNetQualifiedMethodNameTests.TwoGenericClass<CustomT,CustomU>.MethodTwo<CustomA,CustomB>";
			Type type = typeof(TwoGenericClass<,>);
			MethodInfo methodInfo = type.GetMethods()[1];
			//act
			DotNetQualifiedMethodName result = DotNetQualifiedName.FromVisualStudioXml(xmlFormatName) as DotNetQualifiedMethodName;
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual(expectedFullName, result.FullName);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_NullFullNamespace()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("MyMethod(System.Int32)");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_ManySegments()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("A.B.C.MyMethod(System.Int32)");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_GenericMethod()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("A.B.C.MyMethod``2(System.Int32)");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual(2, result.GenericTypeCount);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_GenericClassGenericMethod()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("A.B.C`1.MyMethod``2(System.Int32)");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual(2, result.GenericTypeCount);
			Assert.AreEqual(1, result.FullClassNamespace.GenericTypeCount);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_ExplicitInterface()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("A.B.C.D#E#F#MyMethod(System.Int32)");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual("D.E.F", result.ExplicitInterface);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_ZeroParameters()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("A.B.C.MyMethod");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual(0, result.Parameters.Count);
		}

		[TestMethod]
		public void DotNetQualifiedMethodName_Clone_TwoParameters()
		{
			//arrange
			DotNetQualifiedMethodName a = DotNetQualifiedMethodName.FromVisualStudioXml("A.B.C.MyMethod(System.Int32,System.Collections.Generic.List{System.String})");
			//act
			DotNetQualifiedMethodName result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual(2, result.Parameters.Count);
			Assert.AreEqual("System.Int32", result.Parameters[0].TypeName.ToString());
			Assert.AreEqual("System.Collections.Generic.List<System.String>", result.Parameters[1].TypeName.ToString());
		}

	}
}
