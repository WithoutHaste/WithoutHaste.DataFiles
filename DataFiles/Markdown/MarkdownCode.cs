using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents inline code or CDATA.
	/// </summary>
	public class MarkdownCode : IMarkdownInLine
	{
		/// <summary>
		/// Full text of code.
		/// </summary>
		public readonly string Text;

		/// <summary></summary>
		public MarkdownCode(string text)
		{
			Text = text;
		}

		/// <inheritdoc />
		public string ToMarkdown(string previousText)
		{
			return String.Format("`{0}`", Text);
		}
	}
}
