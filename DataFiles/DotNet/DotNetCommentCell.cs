using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a cell in a table in .Net XML documentation.
	/// </summary>
	/// <remarks>
	/// Does not inherit from DotNetCommentText because a cell cannot appear everywhere text can.
	/// </remarks>
	/// <example><![CDATA[<term>plain text</term>]]></example>
	public class DotNetCommentCell
	{
		/// <summary>Default empty cell.</summary>
		public static readonly DotNetCommentCell EmptyCell = new DotNetCommentCell(null);

		/// <summary>Cell contents.</summary>
		public string Text { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentCell(string text)
		{
			if(text == "")
				text = null;
			Text = text;
		}

		/// <summary>Parses .Net XML documentation term.</summary>
		public static DotNetCommentCell FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, "term");
			return new DotNetCommentCell(element.Value);
		}

		#endregion
	}
}
