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
	public class DotNetMethodConstructorTests
	{
		protected class ClassA
		{
			public ClassA() { }

			static ClassA() { }
		}

		[TestMethod]
		public void DotNetMethodConstructor_FromAssembly_StaticConstructor()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetMethodConstructorTests.ClassA.#cctor' />");
			Type type = typeof(ClassA);
			TypeInfo typeInfo = type.GetTypeInfo();
			ConstructorInfo constructorInfo = typeInfo.DeclaredConstructors.First(m => m.Name == ".cctor");
			//act
			DotNetMethod result = DotNetMethod.FromVisualStudioXml(xmlElement);
			DotNetMethodConstructor constructorResult = (result as DotNetMethodConstructor);
			constructorResult.AddAssemblyInfo(constructorInfo);
			//assert
			Assert.AreEqual(MethodCategory.Static, constructorResult.Category);
		}
	}
}
