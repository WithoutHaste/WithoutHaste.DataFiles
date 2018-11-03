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
	public class DotNetIndexerTests
	{
		protected class A
		{
			public int this[string key] { get { return 0; } }

			public double this[int i] { get { return 0; } }
		}
		
		[TestMethod]
		public void DotNetIndexer_Assembly_MultipleIndexers()
		{
			//arrange
			Type type = typeof(A);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:A'></member>"));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:A.Item(System.String)'></member>")));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:A.Item(System.Int32)'></member>")));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsTrue(dotNetType.Properties[0] is DotNetIndexer);
			DotNetIndexer indexer = (dotNetType.Properties[0] as DotNetIndexer);
			Assert.AreEqual(1, indexer.Parameters.Count());
			Assert.AreEqual("key", indexer.Parameters[0].Name);

			Assert.IsTrue(dotNetType.Properties[1] is DotNetIndexer);
			indexer = (dotNetType.Properties[1] as DotNetIndexer);
			Assert.AreEqual(1, indexer.Parameters.Count());
			Assert.AreEqual("i", indexer.Parameters[0].Name);
		}

		[TestMethod]
		public void DotNetIndexer_Assembly_MatchesPermission()
		{
			//arrange
			Type type = typeof(A);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetIndexerTests.A'></member>"));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:DataFilesTest.DotNetIndexerTests.A.Item(System.String)'></member>")));
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			XElement permissionElement = XElement.Parse("<permission cref='P:DataFilesTest.DotNetIndexerTests.A.Item(System.String)'></permission>");
			DotNetCommentQualifiedLinkedGroup permissionComment = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(permissionElement);
			//act
			DotNetIndexer indexerResult = dotNetType.Properties.OfType<DotNetIndexer>().Cast<DotNetIndexer>().First();
			bool matchesResult = indexerResult.Matches(permissionComment);
			//assert
			Assert.IsTrue(matchesResult);
		}
	}
}
