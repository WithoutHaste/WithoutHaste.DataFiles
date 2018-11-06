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
	}
}
