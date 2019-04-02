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

			public int GetOnlyProperty { get { return 0; } }

			public int PublicGetPrivateSet { get; private set; }

			public int PublicGetProtectedSet { get; protected set; }

			public int PublicGetPublicSet { get; set; }

			public int PrivateGetPublicSet { private get; set; }

			public int ProtectedGetPublicSet { protected get; set; }
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
		public void DotNetProperty_Assembly_Abstract()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.AbstractProperty'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
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
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
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
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual(2, dotNetType.Properties.Count);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceA", dotNetType.Properties[0].Name.ExplicitInterface);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceB", dotNetType.Properties[1].Name.ExplicitInterface);
			Assert.AreEqual("System.Int32", dotNetType.Properties[0].TypeName);
			Assert.AreEqual("System.Int32", dotNetType.Properties[1].TypeName);
			Assert.AreEqual(2, dotNetType.Methods.Count);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceA", dotNetType.Methods[0].Name.ExplicitInterface);
			Assert.AreEqual("DataFilesTest.DotNetPropertyTests.IInterfaceB", dotNetType.Methods[1].Name.ExplicitInterface);
			Assert.AreEqual("System.Int32", dotNetType.Methods[0].MethodName.ReturnTypeName);
			Assert.AreEqual("System.Int32", dotNetType.Methods[1].MethodName.ReturnTypeName);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_PublicGetPrivateSet()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.PublicGetPrivateSet'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual("PublicGetPrivateSet", dotNetType.Properties[0].Name.LocalName);
			Assert.AreEqual(FieldCategory.Normal, dotNetType.Properties[0].Category);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsTrue(dotNetType.Properties[0].HasSetterMethod);
			Assert.AreEqual(AccessModifier.Public, dotNetType.Properties[0].GetterMethod.AccessModifier);
			Assert.AreEqual(AccessModifier.Private, dotNetType.Properties[0].SetterMethod.AccessModifier);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_PublicGetProtectedSet()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.PublicGetProtectedSet'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual("PublicGetProtectedSet", dotNetType.Properties[0].Name.LocalName);
			Assert.AreEqual(FieldCategory.Normal, dotNetType.Properties[0].Category);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsTrue(dotNetType.Properties[0].HasSetterMethod);
			Assert.AreEqual(AccessModifier.Public, dotNetType.Properties[0].GetterMethod.AccessModifier);
			Assert.AreEqual(AccessModifier.Protected, dotNetType.Properties[0].SetterMethod.AccessModifier);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_PublicGetPublicSet()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.PublicGetPublicSet'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual("PublicGetPublicSet", dotNetType.Properties[0].Name.LocalName);
			Assert.AreEqual(FieldCategory.Normal, dotNetType.Properties[0].Category);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsTrue(dotNetType.Properties[0].HasSetterMethod);
			Assert.AreEqual(AccessModifier.Public, dotNetType.Properties[0].GetterMethod.AccessModifier);
			Assert.AreEqual(AccessModifier.Public, dotNetType.Properties[0].SetterMethod.AccessModifier);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_PrivateGetPublicSet()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.PrivateGetPublicSet'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual("PrivateGetPublicSet", dotNetType.Properties[0].Name.LocalName);
			Assert.AreEqual(FieldCategory.Normal, dotNetType.Properties[0].Category);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsTrue(dotNetType.Properties[0].HasSetterMethod);
			Assert.AreEqual(AccessModifier.Private, dotNetType.Properties[0].GetterMethod.AccessModifier);
			Assert.AreEqual(AccessModifier.Public, dotNetType.Properties[0].SetterMethod.AccessModifier);
		}

		[TestMethod]
		public void DotNetProperty_Assembly_ProtectedGetPublicSet()
		{
			//arrange
			Type type = typeof(AbstractClass);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:AbstractClass'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:AbstractClass.ProtectedGetPublicSet'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual("ProtectedGetPublicSet", dotNetType.Properties[0].Name.LocalName);
			Assert.AreEqual(FieldCategory.Normal, dotNetType.Properties[0].Category);
			Assert.IsTrue(dotNetType.Properties[0].HasGetterMethod);
			Assert.IsTrue(dotNetType.Properties[0].HasSetterMethod);
			Assert.AreEqual(AccessModifier.Protected, dotNetType.Properties[0].GetterMethod.AccessModifier);
			Assert.AreEqual(AccessModifier.Public, dotNetType.Properties[0].SetterMethod.AccessModifier);
		}
	}
}
