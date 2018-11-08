using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents an inline section of code in the comments.
	/// </summary>
	/// <example><![CDATA[<c>code fragment</c>]]></example>
	public class DotNetCommentCode : DotNetCommentText
	{
		/// <summary></summary>
		public DotNetCommentCode(string text) : base(text)
		{
			Tag = CommentTag.C;
		}

		/// <summary>Parses .Net XML documentation c tag.</summary>
		public new static DotNetCommentCode FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, "c");
			return new DotNetCommentCode(element.Value.Trim());
		}

		/// <summary>Parses .Net XML documentation CDATA tag.</summary>
		public static DotNetCommentCode FromVisualStudioXml(XCData element)
		{
			return new DotNetCommentCode(element.Value.Trim());
		}
	}
}
