using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTextTests
	{
		[TestMethod]
		public void DotNetCommentText_FromXml_Empty()
		{
			//arrange
			string text = "";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(text);
			//assert
			Assert.AreEqual(null, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_Short()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(text);
			//assert
			Assert.AreEqual(text, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_Formatted()
		{
			//arrange
			string text = @" Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ultricies hendrerit vehicula. Vivamus odio justo, bibendum non rutrum ac, interdum in metus. In tempus id diam eu mollis. Donec cursus non quam at egestas. Sed dictum, justo a maximus vulputate, nunc massa bibendum purus, nec placerat leo est quis elit. Sed porta porta tellus sit amet dignissim. Morbi id nisi eget dolor volutpat commodo nec vitae tortor. Pellentesque faucibus faucibus imperdiet.

Pellentesque ac tincidunt purus. Mauris id metus commodo, efficitur justo non, ultricies leo. Mauris eget eros velit. Mauris ultrices elementum purus, ac ullamcorper erat sodales sit amet. Etiam congue hendrerit neque, sed fringilla turpis euismod ut. Sed lobortis nulla in consectetur suscipit. Donec euismod dui dolor, eu sodales turpis ultrices sit amet. ";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(text);
			//assert
			Assert.AreEqual(text, result.Text);
		}
	}
}
