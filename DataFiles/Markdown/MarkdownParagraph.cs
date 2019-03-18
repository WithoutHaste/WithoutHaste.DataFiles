using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a grouping of elements that will end in a single double-line-break.
	/// </summary>
	/// <remarks>Nesting paragraphs inside paragraphs will still result in just one double-line-break at the end.</remarks>
	/// <example>Displays as: The quick brown fox.\\n\\n</example>
	public class MarkdownParagraph : IMarkdownIsBlock, IMarkdownInSection
	{
		/// <summary>
		/// Ordered elements that make up this paragraph.
		/// </summary>
		/// <remarks>Expect mostly one plain text element.</remarks>
		public IMarkdownInSection[] Elements { get { return elements.ToArray(); } }

		/// <summary>
		/// True when there are no elements in the line.
		/// </summary>
		public bool IsEmpty { get { return (elements.Count == 0); } }

		/// <summary>
		/// Ordered inline elements that make up this line.
		/// </summary>
		protected List<IMarkdownInSection> elements = new List<IMarkdownInSection>();

		#region Constructors

		/// <summary>
		/// Initialize paragraph with any number of elements.
		/// </summary>
		public MarkdownParagraph(params IMarkdownInSection[] elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Initialize paragraph with any number of elements.
		/// </summary>
		public MarkdownParagraph(List<IMarkdownInSection> elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Initialize paragraph with one MarkdownText element.
		/// </summary>
		public MarkdownParagraph(string text)
		{
			this.elements.Add(new MarkdownText(text));
		}

		#endregion

		/// <summary>
		/// Add a new MarkdownText containing the text to the end of the paragraph.
		/// </summary>
		public void Add(string text)
		{
			elements.Add(new MarkdownText(text));
		}

		/// <summary>
		/// Add an element to the end of the paragraph.
		/// </summary>
		public void Add(IMarkdownInSection element)
		{
			elements.Add(element);
		}

		/// <summary>
		/// Add elements to the end of the paragraph.
		/// </summary>
		public void Add(List<IMarkdownInSection> elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Add elements to the end of the paragraph.
		/// </summary>
		public void Add(params IMarkdownInSection[] elements)
		{
			this.elements.AddRange(elements);
		}

		/// <summary>
		/// Add a new MarkdownText containing the text to the beginning of the paragraph.
		/// </summary>
		public void Prepend(string text)
		{
			elements.Insert(0, new MarkdownText(text));
		}

		/// <summary>
		/// Add an element to the beginning of the paragraph.
		/// </summary>
		public void Prepend(IMarkdownInSection element)
		{
			elements.Insert(0, element);
		}

		/// <summary>
		/// Convert the paragraph to markdown-formatted text.
		/// </summary>
		public string ToMarkdownString(string previousText)
		{
			StringBuilder builder = new StringBuilder();
			string thisPreviousText = null;
			foreach(IMarkdownInSection element in elements)
			{
				thisPreviousText = element.ToMarkdownString(thisPreviousText);
				builder.Append(thisPreviousText);
			}

			string result = builder.ToString();
			result = result.TrimEnd();
			return result + "  \n\n";
		}
	}
}
