using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary></summary>
	public abstract class MarkdownLink : IMarkdownInline
	{
		/// <summary>Plain text of link.</summary>
		public string Text { get; protected set; }

		/// <summary>Url of target.</summary>
		public string Url { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public MarkdownLink(string text, string url)
		{
			Text = text;
			Url = url;
		}

		#endregion

		/// <inheritdoc />
		public abstract string ToMarkdown();
	}
}
