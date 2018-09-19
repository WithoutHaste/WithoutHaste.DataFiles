using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents one line of text that will end in a line break.
	/// </summary>
	/// <remarks>Do not include the white space or endline character.</remarks>
	/// <example>Displays as: The quick brown fox.  \\n</example>
	public class MarkdownLine : IMarkdownInsection
	{
		/// <summary>
		/// Ordered inline elements that make up this line.
		/// </summary>
		/// <remarks>Expect mostly one plain text element.</remarks>
		public IMarkdownInline[] Elements { get { return elements.ToArray(); } }

		/// <summary>
		/// Ordered inline elements that make up this line.
		/// </summary>
		protected List<IMarkdownInline> elements = new List<IMarkdownInline>();

		#region Constructors

		/// <summary>
		/// Initialize line with any number of elements.
		/// </summary>
		public MarkdownLine(params IMarkdownInline[] elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Initialize line with one MarkdownText element.
		/// </summary>
		public MarkdownLine(string text)
		{
			this.elements.Add(new MarkdownText(text));
		}

		#endregion

		/// <inheritdoc />
		public virtual string ToMarkdown()
		{
			return String.Join("", elements.Select(e => e.ToMarkdown()).ToArray()) + "  \n";
		}

	}
}
