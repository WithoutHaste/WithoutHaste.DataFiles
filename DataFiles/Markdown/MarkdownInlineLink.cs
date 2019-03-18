using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents markdown inline-style link.
	/// </summary>
	/// <example>
	/// <c>new MarkdownInlineLink("google", "www.google.com")</c> is converted to string <c>[google](www.google.com)</c>.
	/// </example>
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

		/// <summary>Accepts formatted text.</summary>
		public MarkdownInlineLink(MarkdownText text, string url) : base(text, url)
		{
		}

		#endregion

		/// <inheritdoc />
		public override string ToMarkdownString(string previousText)
		{
			return String.Format("[{0}]({1})", text.ToMarkdownString(), Url);
		}
	}
}
