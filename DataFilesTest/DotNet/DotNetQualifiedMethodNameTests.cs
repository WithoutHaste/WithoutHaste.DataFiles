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

	}
}
