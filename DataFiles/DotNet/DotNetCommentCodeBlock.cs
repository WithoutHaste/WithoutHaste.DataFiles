using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a section of code in the comments.
	/// </summary>
	/// <example><![CDATA[<code>code statements</code>]]></example>
	public class DotNetCommentCodeBlock : DotNetCommentCode
	{
		/// <summary></summary>
		public DotNetCommentCodeBlock(string text) : base(text)
		{
		}

		/// <summary>Parses .Net XML documentation code.</summary>
		public static new DotNetCommentCode FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, "code");
			return new DotNetCommentCode(element.Value);
		}
	}
}
