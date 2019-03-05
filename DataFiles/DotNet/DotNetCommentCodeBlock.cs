using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a section of code in the comments.
	/// </summary>
	public class DotNetCommentCodeBlock : DotNetCommentCode
	{
		/// <summary>Specify the language of the code block. Null if not known.</summary>
		public string Language { get; protected set; }

		/// <summary></summary>
		public DotNetCommentCodeBlock(string text) : base(text)
		{
			Tag = CommentTag.Code;
		}

		/// <summary></summary>
		public DotNetCommentCodeBlock(string text, string language) : base(text)
		{
			Tag = CommentTag.Code;
			Language = language;
		}

		/// <summary>Parses .Net XML documentation code tag.</summary>
		/// <example><![CDATA[<code>code statements</code>]]></example>
		public static new DotNetCommentCode FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, "code");
			if(element.Attribute("lang") != null)
				return new DotNetCommentCodeBlock(element.Value, element.Attribute("lang").Value);
			return new DotNetCommentCodeBlock(element.Value);
		}

		/// <summary>Parses .Net XML documentation CDATA tag.</summary>
		/// <remarks>Sets language to "xml".</remarks>
		/// <example><![CDATA[<![CDATA[xml statements\]\]>]]></example>
		public new static DotNetCommentCode FromVisualStudioXml(XCData element)
		{
			return new DotNetCommentCodeBlock(element.Value, "xml");
		}
	}
}
