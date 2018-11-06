using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a code block or CDATA block.
	/// </summary>
	public class MarkdownCodeBlock : IMarkdownInSection, IMarkdownIsBlock
	{
		/// <summary>
		/// Full text of code, with endline characters between lines.
		/// </summary>
		public readonly string Text;

		/// <summary>
		/// Language tag supported by highlight.js for syntax highlighting.
		/// </summary>
		public readonly string Language;

		/// <summary></summary>
		public MarkdownCodeBlock(string text)
		{
			Text = text;
		}

		/// <summary></summary>
		public MarkdownCodeBlock(string text, string language)
		{
			Text = text;
			Language = language;
		}

		/// <inheritdoc />
		public string ToMarkdown(string previousText)
		{
			string text = Text;
			if(!text.EndsWith("\n"))
				text += "\n";
			return String.Format("\n```{0}\n{1}```\n\n", Language, text);
		}
	}
}
