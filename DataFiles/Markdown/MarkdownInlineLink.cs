using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents markdown inline-style link.
	/// </summary>
	/// <example>Displays as: The quick brown fox [jumped over](https://www.google.com) the lazy dog.</example>
	public class MarkdownInlineLink : MarkdownLink
	{
		#region Constructors

		/// <inheritdoc/>
		public MarkdownInlineLink(string text) : base(text)
		{
		}

		/// <summary></summary>
		public MarkdownInlineLink(string text, string url) : base(text, url)
		{
		}

		/// <summary></summary>
		public MarkdownInlineLink(MarkdownText text, string url) : base(text, url)
		{
		}

		#endregion

		/// <inheritdoc />
		public override string ToMarkdown(string previousText)
		{
			return String.Format("[{0}]({1})", text.ToMarkdown(), Url);
		}
	}
}
