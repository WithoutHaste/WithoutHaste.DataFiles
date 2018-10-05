using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents one paragraph of text that will end in a double line break.
	/// </summary>
	/// <remarks>Do not include trailing white space or endline characters.</remarks>
	/// <example>Displays as: The quick brown fox.\\n\\n</example>
	public class MarkdownParagraph : MarkdownLine, IMarkdownIsBlock
	{
		#region Constructors

		/// <summary>
		/// Initialize paragraph with any number of elements.
		/// </summary>
		public MarkdownParagraph(params IMarkdownInLine[] elements) : base(elements)
		{
		}

		/// <summary>
		/// Initialize paragraph with any number of elements.
		/// </summary>
		public MarkdownParagraph(List<IMarkdownInLine> elements) : base(elements)
		{
		}

		/// <summary>
		/// Initialize paragraph with one MarkdownText element.
		/// </summary>
		public MarkdownParagraph(string text) : base(text)
		{
		}

		#endregion

		/// <inheritdoc />
		public override string ToMarkdown()
		{
			return String.Join("", elements.Select(e => e.ToMarkdown()).ToArray()) + "\n\n";
		}
	}
}
