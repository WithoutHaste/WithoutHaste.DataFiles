using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter description in the comments.
	/// </summary>
	public class DotNetCommentTypeParameter : DotNetCommentTypeParameterLink
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentTypeParameter(string name) : base(name)
		{
		}

		/// <summary>Parses .Net XML documentation for typeparam.</summary>
		public static new DotNetComment FromVisualStudioXml(XElement element)
		{
			DotNetCommentGroup group = ParseGroup(element);
			group.SetLink(new DotNetCommentParameter(element.Attribute("name")?.Value));
			return group;
		}

		#endregion
	}
}
