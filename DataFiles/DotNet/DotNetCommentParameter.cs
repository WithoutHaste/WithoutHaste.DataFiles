using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a parameter description in the comments.
	/// </summary>
	public class DotNetCommentParameter : DotNetCommentParameterLink
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentParameter(string name) : base(name)
		{
		}

		/// <summary>Parses .Net XML documentation for param.</summary>
		public static new DotNetComment FromVisualStudioXml(XElement element)
		{
			DotNetCommentGroup group = ParseGroup(element);
			group.SetLink(new DotNetCommentParameter(element.Attribute("name")?.Value));
			return group;
		}

		#endregion
	}
}
