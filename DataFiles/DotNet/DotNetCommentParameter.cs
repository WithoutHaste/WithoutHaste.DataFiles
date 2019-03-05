using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a parameter description in the comments.
	/// </summary>
	public class DotNetCommentParameter : DotNetCommentLinkedGroup
	{
		/// <summary>Strongly-typed link.</summary>
		public DotNetCommentParameterLink ParameterLink { get { return Link as DotNetCommentParameterLink; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentParameter(DotNetCommentParameterLink link, DotNetComment comment) : base(link, comment)
		{
			Tag = CommentTag.Param;
		}

		/// <summary></summary>
		public DotNetCommentParameter(DotNetCommentParameterLink link, params DotNetComment[] comments) : base(link, comments)
		{
			Tag = CommentTag.Param;
		}

		/// <summary></summary>
		public DotNetCommentParameter(DotNetCommentParameterLink link, List<DotNetComment> comments) : base(link, comments)
		{
			Tag = CommentTag.Param;
		}

		/// <summary>Parses .Net XML documentation for param.</summary>
		/// <example><![CDATA[<param name="myParam">nested comments</param>]]></example>
		public static new DotNetCommentParameter FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "param");
			return new DotNetCommentParameter(
				new DotNetCommentParameterLink(element.Attribute("name")?.Value),
				ParseSection(element)
			);
		}

		#endregion
	}
}
