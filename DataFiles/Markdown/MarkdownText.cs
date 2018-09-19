using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents plain text.
	/// </summary>
	public class MarkdownText : IMarkdownInline, IMarkdownInsection
	{
		/// <summary></summary>
		public string Text { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public MarkdownText(string text)
		{
			Text = text;
		}

		#endregion

		/// <inheritdoc />
		public string ToMarkdown()
		{
			return Text;
		}
	}
}
