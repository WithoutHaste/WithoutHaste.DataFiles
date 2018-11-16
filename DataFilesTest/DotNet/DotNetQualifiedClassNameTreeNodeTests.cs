using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetQualifiedClassNameTreeNodeTests
	{
		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_Null()
		{
			//arrange
			List<DotNetQualifiedClassName> names = null;
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(names);
			//assert
			Assert.IsNull(root.Value);
			Assert.AreEqual(0, root.Children.Count);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_Empty()
		{
			//arrange
			List<DotNetQualifiedClassName> names = new List<DotNetQualifiedClassName>();
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(names);
			//assert
			Assert.IsNull(root.Value);
			Assert.AreEqual(0, root.Children.Count);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_One()
		{
			//arrange
			List<DotNetQualifiedClassName> names = new List<DotNetQualifiedClassName>() {
				DotNetQualifiedClassName.FromVisualStudioXml("NameA.ClassA")
			};
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(names);
			//assert
			Assert.AreEqual("NameA.ClassA", root.Value);
			Assert.AreEqual(0, root.Children.Count);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_FirstRoot()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.ClassA");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			//assert
			Assert.AreEqual(a, root.Value);
			Assert.AreEqual(0, root.Children.Count);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_PushDownRoot()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.ClassA");
			DotNetQualifiedClassName b = DotNetQualifiedClassName.FromVisualStudioXml("NameA");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(b);
			//assert
			Assert.AreEqual(b, root.Value);
			Assert.AreEqual(1, root.Children.Count);
			Assert.AreEqual(a, root.Children[0].Value);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_ChildOfRoot()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB");
			DotNetQualifiedClassName b = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler.NameC");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(b);
			//assert
			Assert.AreEqual(a, root.Value);
			Assert.AreEqual(1, root.Children.Count);
			Assert.AreEqual(b, root.Children[0].Value);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_DescendentOfRoot()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB");
			DotNetQualifiedClassName c = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler.NameC");
			DotNetQualifiedClassName d = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler.NameD");
			DotNetQualifiedClassName e = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler.NameD.NameE");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(c);
			root.Insert(d);
			root.Insert(e);
			//assert
			Assert.AreEqual(a, root.Value);
			Assert.AreEqual(2, root.Children.Count);
			Assert.AreEqual(c, root.Children[0].Value);
			Assert.AreEqual(d, root.Children[1].Value);
			Assert.AreEqual(1, root.Children[1].Children.Count);
			Assert.AreEqual(e, root.Children[1].Children[0].Value);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_BetweenRootAndChild()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB");
			DotNetQualifiedClassName c = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler.NameC");
			DotNetQualifiedClassName d = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler.NameD");
			DotNetQualifiedClassName e = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB.Filler");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(c);
			root.Insert(d);
			root.Insert(e);
			//assert
			Assert.AreEqual(a, root.Value);
			Assert.AreEqual(1, root.Children.Count);
			Assert.AreEqual(e, root.Children[0].Value);
			Assert.AreEqual(2, root.Children[0].Children.Count);
			Assert.AreEqual(c, root.Children[0].Children[0].Value);
			Assert.AreEqual(d, root.Children[0].Children[1].Value);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_MultipleRoots()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB");
			DotNetQualifiedClassName c = DotNetQualifiedClassName.FromVisualStudioXml("NameC");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(c);
			//assert
			Assert.AreEqual(null, root.Value);
			Assert.AreEqual(2, root.Children.Count);
			Assert.AreEqual(a, root.Children[0].Value);
			Assert.AreEqual(0, root.Children[0].Children.Count);
			Assert.AreEqual(c, root.Children[1].Value);
			Assert.AreEqual(0, root.Children[1].Children.Count);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_MultipleRootsChild()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB");
			DotNetQualifiedClassName c = DotNetQualifiedClassName.FromVisualStudioXml("NameC");
			DotNetQualifiedClassName d = DotNetQualifiedClassName.FromVisualStudioXml("NameC.NameD");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(c);
			root.Insert(d);
			//assert
			Assert.AreEqual(null, root.Value);
			Assert.AreEqual(2, root.Children.Count);
			Assert.AreEqual(a, root.Children[0].Value);
			Assert.AreEqual(0, root.Children[0].Children.Count);
			Assert.AreEqual(c, root.Children[1].Value);
			Assert.AreEqual(1, root.Children[1].Children.Count);
			Assert.AreEqual(d, root.Children[1].Children[0].Value);
		}

		[TestMethod]
		public void DotNetQualifiedClassNameTreeNode_Generate_MultipleRootsChildBetween()
		{
			//arrange
			DotNetQualifiedClassName a = DotNetQualifiedClassName.FromVisualStudioXml("NameA.NameB");
			DotNetQualifiedClassName d = DotNetQualifiedClassName.FromVisualStudioXml("NameC.NameD");
			DotNetQualifiedClassName c = DotNetQualifiedClassName.FromVisualStudioXml("NameC");
			//act
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(null);
			root.Insert(a);
			root.Insert(d);
			root.Insert(c);
			//assert
			Assert.AreEqual(null, root.Value);
			Assert.AreEqual(2, root.Children.Count);
			Assert.AreEqual(a, root.Children[0].Value);
			Assert.AreEqual(0, root.Children[0].Children.Count);
			Assert.AreEqual(c, root.Children[1].Value);
			Assert.AreEqual(1, root.Children[1].Children.Count);
			Assert.AreEqual(d, root.Children[1].Children[0].Value);
		}
	}
}
