using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetMethodOperatorTests
	{
		protected class ClassA
		{
			public static implicit operator double(ClassA a)
			{
				return 0;
			}

			public static explicit operator float(ClassA a)
			{
				return 0;
			}
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
		public void DotNetMethodOperator_FromAssembly_ImplicitOperator()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodOperatorTests.ClassA.op_Implicit(DataFilesTest.DotNetMethodOperatorTests.ClassA)~System.Double' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(ClassA);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "op_Implicit");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual("System.Double", result.MethodName.ReturnTypeName);
			Assert.AreEqual("DataFilesTest.DotNetMethodOperatorTests.ClassA", result.MethodName.Parameters[0].TypeName);
		}

		[TestMethod]
		public void DotNetMethodOperator_FromAssembly_ExplicitOperator()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodOperatorTests.ClassA.op_Explicit(DataFilesTest.DotNetMethodOperatorTests.ClassA)~System.Int64' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(ClassA);
			MethodInfo methodInfo = type.GetMethods().First(m => m.Name == "op_Explicit");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			result.AddAssemblyInfo(methodInfo);
			//assert
			Assert.AreEqual("System.Single", result.MethodName.ReturnTypeName);
			Assert.AreEqual("DataFilesTest.DotNetMethodOperatorTests.ClassA", result.MethodName.Parameters[0].TypeName);
		}

		[TestMethod]
		public void DotNetMethodOperator_Equals_True()
		{
			//arrange
			string xml = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			XElement element = XElement.Parse(xml, LoadOptions.PreserveWhitespace);
			DotNetMethodOperator a = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(element);
			DotNetMethodOperator b = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(element);
			//act
			bool result = (a == b);
			//assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DotNetMethodOperator_Equals_DifferentNamespace_False()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlB = "<member name='M:MyNamespaceB.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			XElement elementA = XElement.Parse(xmlA, LoadOptions.PreserveWhitespace);
			XElement elementB = XElement.Parse(xmlB, LoadOptions.PreserveWhitespace);
			DotNetMethodOperator a = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementA);
			DotNetMethodOperator b = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementB);
			//act
			bool result = (a == b);
			//assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void DotNetMethodOperator_Equals_DifferentClass_False()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlB = "<member name='M:MyNamespace.MyClassB.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			XElement elementA = XElement.Parse(xmlA, LoadOptions.PreserveWhitespace);
			XElement elementB = XElement.Parse(xmlB, LoadOptions.PreserveWhitespace);
			DotNetMethodOperator a = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementA);
			DotNetMethodOperator b = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementB);
			//act
			bool result = (a == b);
			//assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void DotNetMethodOperator_Equals_DifferentMethod_False()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlB = "<member name='M:MyNamespace.MyClass.op_Subtraction(MyNamespace.MyClass,System.Int32)' />";
			XElement elementA = XElement.Parse(xmlA, LoadOptions.PreserveWhitespace);
			XElement elementB = XElement.Parse(xmlB, LoadOptions.PreserveWhitespace);
			DotNetMethodOperator a = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementA);
			DotNetMethodOperator b = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementB);
			//act
			bool result = (a == b);
			//assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void DotNetMethodOperator_Equals_DifferentParameters_False()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlB = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Decimal)' />";
			XElement elementA = XElement.Parse(xmlA, LoadOptions.PreserveWhitespace);
			XElement elementB = XElement.Parse(xmlB, LoadOptions.PreserveWhitespace);
			DotNetMethodOperator a = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementA);
			DotNetMethodOperator b = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementB);
			//act
			bool result = (a == b);
			//assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void DotNetMethodOperator_Equals_DifferentParameterCounts_False()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlB = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass)' />";
			XElement elementA = XElement.Parse(xmlA, LoadOptions.PreserveWhitespace);
			XElement elementB = XElement.Parse(xmlB, LoadOptions.PreserveWhitespace);
			DotNetMethodOperator a = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementA);
			DotNetMethodOperator b = (DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(elementB);
			//act
			bool result = (a == b);
			//assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void DotNetMethodOperator_Sort_OperatorOrder_RegardlessOfParameters()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlB = "<member name='M:MyNamespace.MyClass.op_Subtraction(MyNamespace.MyClass,System.Byte)' />";
			string xmlC = "<member name='M:MyNamespace.MyClass.op_Multiply(MyNamespace.MyClass,System.Int64)' />";
			string xmlD = "<member name='M:MyNamespace.MyClass.op_Division(MyNamespace.MyClass,System.Decimal)' />";
			List<DotNetMethodOperator> list = new List<DotNetMethodOperator>() {
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlC, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlA, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlD, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlB, LoadOptions.PreserveWhitespace)),
			};
			//act
			list.Sort();
			//assert
			Assert.AreEqual("op_Addition", list[0].Name.LocalName);
			Assert.AreEqual("op_Subtraction", list[1].Name.LocalName);
			Assert.AreEqual("op_Multiply", list[2].Name.LocalName);
			Assert.AreEqual("op_Division", list[3].Name.LocalName);
		}

		[TestMethod]
		public void DotNetMethodOperator_Sort_OperatorOrder_ByParameters()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Byte)' />";
			string xmlB = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Decimal)' />";
			string xmlC = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlD = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int64)' />";
			List<DotNetMethodOperator> list = new List<DotNetMethodOperator>() {
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlC, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlA, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlD, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlB, LoadOptions.PreserveWhitespace)),
			};
			//act
			list.Sort();
			//assert
			Assert.AreEqual("System.Byte", list[0].MethodName.Parameters[1].TypeName);
			Assert.AreEqual("System.Decimal", list[1].MethodName.Parameters[1].TypeName);
			Assert.AreEqual("System.Int32", list[2].MethodName.Parameters[1].TypeName);
			Assert.AreEqual("System.Int64", list[3].MethodName.Parameters[1].TypeName);
		}

		[TestMethod]
		public void DotNetMethodOperator_SortLINQ_OperatorOrder_ByParameters()
		{
			//arrange
			string xmlA = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Byte)' />";
			string xmlB = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Decimal)' />";
			string xmlC = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int32)' />";
			string xmlD = "<member name='M:MyNamespace.MyClass.op_Addition(MyNamespace.MyClass,System.Int64)' />";
			List<DotNetMethodOperator> list = new List<DotNetMethodOperator>() {
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlC, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlA, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlD, LoadOptions.PreserveWhitespace)),
				(DotNetMethodOperator)DotNetMethod.FromVisualStudioXml(XElement.Parse(xmlB, LoadOptions.PreserveWhitespace)),
			};
			//act
			list = list.OrderBy(x => x).ToList();
			//assert
			Assert.AreEqual("System.Byte", list[0].MethodName.Parameters[1].TypeName);
			Assert.AreEqual("System.Decimal", list[1].MethodName.Parameters[1].TypeName);
			Assert.AreEqual("System.Int32", list[2].MethodName.Parameters[1].TypeName);
			Assert.AreEqual("System.Int64", list[3].MethodName.Parameters[1].TypeName);
		}
	}
}
