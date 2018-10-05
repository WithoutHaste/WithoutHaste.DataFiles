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
	public class DotNetTypeTests
	{
		protected class ChildOfObject { }

		protected struct ChildOfValue { }

		protected class GrandChildOfObject : ChildOfObject { }

		protected interface I1 { }

		protected interface I2 { }

		protected class ChildOfOneInterface : I1 { }

		protected class ChildOfTwoInterfaces : I1, I2 { }

		protected interface I3 : I1 { }

		protected class ChildOfChildInterface : I3 { }

		[TestMethod]
		public void DotNetType_Assembly_Object()
		{
			//arrange
			Type type = typeof(System.Object);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("System.Object"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNull(dotNetType.BaseType);
		}

		[TestMethod]
		public void DotNetType_Assembly_ChildOfObject()
		{
			//arrange
			Type type = typeof(ChildOfObject);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("ChildOfObject"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.BaseType);
			Assert.AreEqual(1, dotNetType.BaseType.Depth);
			Assert.AreEqual("System.Object", dotNetType.BaseType.Name.FullName);
		}

		[TestMethod]
		public void DotNetType_Assembly_ChildOfValue()
		{
			//arrange
			Type type = typeof(ChildOfValue);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("ChildOfValue"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.BaseType);
			Assert.AreEqual(2, dotNetType.BaseType.Depth);
			Assert.AreEqual("System.ValueType", dotNetType.BaseType.Name.FullName);
			Assert.AreEqual("System.Object", dotNetType.BaseType.BaseType.Name.FullName);
		}

		[TestMethod]
		public void DotNetType_Assembly_GrandChildOfValue()
		{
			//arrange
			Type type = typeof(GrandChildOfObject);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("GrandChildOfObject"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.BaseType);
			Assert.AreEqual(2, dotNetType.BaseType.Depth);
			Assert.AreEqual("ChildOfObject", dotNetType.BaseType.Name.LocalName);
			Assert.AreEqual("System.Object", dotNetType.BaseType.BaseType.Name.FullName);
		}

		[TestMethod]
		public void DotNetType_Assembly_ChildOfOneInterface()
		{
			//arrange
			Type type = typeof(ChildOfOneInterface);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("ChildOfOneInterface"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual(1, dotNetType.ImplementedInterfaces.Count);
			Assert.AreEqual("I1", dotNetType.ImplementedInterfaces[0].Name.LocalName);
		}

		[TestMethod]
		public void DotNetType_Assembly_ChildOfTwoInterfaces()
		{
			//arrange
			Type type = typeof(ChildOfTwoInterfaces);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("ChildOfTwoInterfaces"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual(2, dotNetType.ImplementedInterfaces.Count);
			Assert.AreEqual("I1", dotNetType.ImplementedInterfaces[0].Name.LocalName);
			Assert.AreEqual("I2", dotNetType.ImplementedInterfaces[1].Name.LocalName);
		}

		[TestMethod]
		public void DotNetType_Assembly_ChildOfChildInterface()
		{
			//arrange
			Type type = typeof(ChildOfChildInterface);
			DotNetType dotNetType = new DotNetType(new DotNetQualifiedName("ChildOfChildInterface"));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual(2, dotNetType.ImplementedInterfaces.Count);
			Assert.AreEqual(1, dotNetType.ImplementedInterfaces.Count(i => i.Name.LocalName == "I1"));
			Assert.AreEqual(1, dotNetType.ImplementedInterfaces.Count(i => i.Name.LocalName == "I3"));
		}

	}
}
