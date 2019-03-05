using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter description in the comments.
	/// </summary>
	public class DotNetCommentTypeParameter : DotNetCommentParameter
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentTypeParameter(DotNetCommentTypeParameterLink link, DotNetComment comment) : base(link, comment)
		{
			Tag = CommentTag.TypeParam;
		}

		/// <summary></summary>
		public DotNetCommentTypeParameter(DotNetCommentTypeParameterLink link, params DotNetComment[] comments) : base(link, comments)
		{
			Tag = CommentTag.TypeParam;
		}

		/// <summary></summary>
		public DotNetCommentTypeParameter(DotNetCommentTypeParameterLink link, List<DotNetComment> comments) : base(link, comments)
		{
			Tag = CommentTag.TypeParam;
		}

		/// <summary>Parses .Net XML documentation for typeparam.</summary>
		/// <example><![CDATA[<typeparam name="T">nested comments</typeparam>]]></example>
		public static new DotNetCommentTypeParameter FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "typeparam");
			return new DotNetCommentTypeParameter(
				new DotNetCommentTypeParameterLink(element.Attribute("name")?.Value),
				ParseSection(element)
			);
		}

		#endregion
	}
}
