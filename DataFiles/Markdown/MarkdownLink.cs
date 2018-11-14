using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary></summary>
	public abstract class MarkdownLink : IMarkdownInLine
	{
		/// <summary>Plain text of link.</summary>
		public string Text { get { return text.Text; } }

		/// <summary>Styled text of link.</summary>
		protected MarkdownText text;

		/// <summary>Url of target.</summary>
		public string Url { get; protected set; }

		#region Constructors

		/// <summary>Link text and url are the same.</summary>
		public MarkdownLink(string text)
		{
			this.text = new MarkdownText(text);
			Url = text;
		}

		/// <summary></summary>
		public MarkdownLink(string text, string url)
		{
			this.text = new MarkdownText(text);
			Url = url;
		}

		/// <summary></summary>
		public MarkdownLink(MarkdownText text, string url)
		{
			this.text = text;
			Url = url;
		}

		#endregion

		/// <inheritdoc />
		public abstract string ToMarkdown(string previousText);
	}
}
