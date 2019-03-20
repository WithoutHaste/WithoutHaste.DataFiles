using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetSettingsTests
	{
		[TestMethod]
		public void DotNetSettings_DefaultQualifiedNameConverter_AliasesType()
		{
			//arrange
			DotNetQualifiedTypeName qualifiedName = DotNetQualifiedTypeName.FromVisualStudioXml("System.Int32");
			//act
			string fullName = qualifiedName.FullName;
			//assert
			Assert.AreEqual("int", fullName);
		}

		[TestMethod]
		public void DotNetSettings_DefaultQualifiedNameConverter_GenericTypeParameter()
		{
			//arrange
			DotNetQualifiedTypeName qualifiedName = DotNetQualifiedTypeName.FromVisualStudioXml("A.B{System.Int32}.C{System.String,D,System.Byte}");
			//act
			string fullName = qualifiedName.FullName;
			//assert
			Assert.AreEqual("A.B<int>.C<string,D,byte>", fullName);
		}
	}
}
