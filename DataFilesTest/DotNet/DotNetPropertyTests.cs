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
	public class DotNetPropertyTests
	{
		public abstract class AbstractClass
		{
			public abstract int AbstractProperty { get; set; }

			public int GetOnlyProperty { get; }
		}

		protected interface IInterfaceA
		{
			int SharedProperty { get; set; }

			int SharedMethod(int a);
		}

		protected interface IInterfaceB
		{
			int SharedProperty { get; set; }

			int SharedMethod(int a);
		}

		protected class ExplicitInterfaceImplementation : IInterfaceA, IInterfaceB
		{
			int IInterfaceA.SharedProperty { get; set; }

			int IInterfaceB.SharedProperty { get; set; }

			int IInterfaceA.SharedMethod(int a) { return 0; }

			int IInterfaceB.SharedMethod(int a) { return 1;  }
		}

		[TestMethod]
		public void DotNetProperty_Assembly_Abstract()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.AbstractProperty'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual("AbstractProperty", dotNetType.Properties[0].Name.LocalName);
			Assert.AreEqual(FieldCategory.Abstract, dotNetType.Properties[0].Category);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsTrue(dotNetType.Properties[0].HasSetterMethod);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_NoSet()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.GetOnlyProperty'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual("GetOnlyProperty", dotNetType.Properties[0].Name.LocalName);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsFalse(dotNetType.Properties[0].HasSetterMethod);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_ExplicitInterfaceImplementation_Properties()
		{
			//arrange
			Type type = typeof(ExplicitInterfaceImplementation);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetPropertyTests.ExplicitInterfaceImplementation'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:DataFilesTest.DotNetPropertyTests.ExplicitInterfaceImplementation.DataFilesTest#DotNetPropertyTests#IInterfaceA#SharedProperty'></member>", LoadOptions.PreserveWhitespace)));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:DataFilesTest.DotNetPropertyTests.ExplicitInterfaceImplementation.DataFilesTest#DotNetPropertyTests#IInterfaceB#SharedProperty'></member>", LoadOptions.PreserveWhitespace)));
			dotNetType.AddMember(DotNetMethod.FromVisualStudioXml(XElement.Parse("<member name='M:DataFilesTest.DotNetPropertyTests.ExplicitInterfaceImplementation.DataFilesTest#DotNetPropertyTests#IInterfaceA#SharedMethod(System.Int32)'></member>", LoadOptions.PreserveWhitespace)));
			dotNetType.AddMember(DotNetMethod.FromVisualStudioXml(XElement.Parse("<member name='M:DataFilesTest.DotNetPropertyTests.ExplicitInterfaceImplementation.DataFilesTest#DotNetPropertyTests#IInterfaceB#SharedMethod(System.Int32)'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual(2, dotNetType.Properties.Count);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceA", dotNetType.Properties[0].Name.ExplicitInterface);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceB", dotNetType.Properties[1].Name.ExplicitInterface);
			Assert.AreEqual(2, dotNetType.Methods.Count);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceA", dotNetType.Methods[0].Name.ExplicitInterface);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceB", dotNetType.Methods[1].Name.ExplicitInterface);
		}
	}
}
