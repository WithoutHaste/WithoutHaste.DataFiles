﻿using System;
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
		/// <summary>Specify the language of the code block. Null if not known.</summary>
		public string Language { get; protected set; }

		/// <summary></summary>
		public DotNetCommentCodeBlock(string text) : base(text)
		{
			Tag = CommentTag.Code;
		}

		/// <summary>Parses .Net XML documentation code.</summary>
		public static new DotNetCommentCode FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, "code");
			return new DotNetCommentCodeBlock(element.Value);
		}
	}
}
