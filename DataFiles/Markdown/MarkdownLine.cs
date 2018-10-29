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
	/// <remarks>Do not include the trailing white space or endline character.</remarks>
	/// <example>Displays as: The quick brown fox.  \\n</example>
	public class MarkdownLine : IMarkdownInSection, IMarkdownInList
	{
		/// <summary>
		/// Ordered inline elements that make up this line.
		/// </summary>
		/// <remarks>Expect mostly one plain text element.</remarks>
		public IMarkdownInLine[] Elements { get { return elements.ToArray(); } }

		/// <summary>
		/// True when there are no elements in the line.
		/// </summary>
		public bool IsEmpty { get { return (elements.Count == 0); } }

		/// <summary>
		/// Ordered inline elements that make up this line.
		/// </summary>
		protected List<IMarkdownInLine> elements = new List<IMarkdownInLine>();

		#region Constructors

		/// <summary>
		/// Initialize line with any number of elements.
		/// </summary>
		public MarkdownLine(params IMarkdownInLine[] elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Initialize line with any number of elements.
		/// </summary>
		public MarkdownLine(List<IMarkdownInLine> elements)
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

		/// <summary>
		/// Add a new MarkdownText containing the text to the end of the line.
		/// </summary>
		public void Add(string text)
		{
			elements.Add(new MarkdownText(text));
		}

		/// <summary>
		/// Add an element to the end of the line.
		/// </summary>
		public void Add(IMarkdownInLine element)
		{
			elements.Add(element);
		}

		/// <summary>
		/// Add elements to the end of the line.
		/// </summary>
		public void Add(List<IMarkdownInLine> elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Add elements to the end of the line.
		/// </summary>
		public void Add(params IMarkdownInLine[] elements)
		{
			this.elements.AddRange(elements);
		}
		
		/// <summary>
		/// Add a new MarkdownText containing the text to the beginning of the line.
		/// </summary>
		public void Prepend(string text)
		{
			elements.Insert(0, new MarkdownText(text));
		}

		/// <summary>
		/// Add an element to the beginning of the line.
		/// </summary>
		public void Prepend(IMarkdownInLine element)
		{
			elements.Insert(0, element);
		}
		
		/// <inheritdoc />
		public virtual string ToMarkdown(string previousText)
		{
			return String.Join("", elements.Select(e => e.ToMarkdown(null)).ToArray()) + "  \n";
		}

	}
}
