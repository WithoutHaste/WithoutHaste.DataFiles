using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a header and all contents until the next header of the same depth.
	/// </summary>
	public class MarkdownSection : IMarkdownInsection
	{
		/// <summary>
		/// 0-indexed nesting depth of section.
		/// </summary>
		/// <example>"# Header" is depth 1</example>
		/// <example>"## Header" is depth 2</example>
		public int Depth { get; protected set; }

		/// <summary>
		/// Displayed header text.
		/// </summary>
		public string Header { get; set; }

		private List<IMarkdownInsection> elements = new List<IMarkdownInsection>();

		#region Constructors

		/// <summary></summary>
		public MarkdownSection(string header, int depth)
		{
			Depth = depth;
			Header = header;
		}

		#endregion

		/// <summary>
		/// Creates new section and adds it to the end of this section. Defaults to depth of parent + 1.
		/// </summary>
		/// <param name="header">Section header</param>
		/// <returns>The new section</returns>
		public MarkdownSection AddSection(string header)
		{
			MarkdownSection section = new MarkdownSection(header, Depth + 1);
			elements.Add(section);
			return section;
		}

		/// <summary>
		/// Adds the element to the end of this section.
		/// </summary>
		public void Add(IMarkdownInsection element)
		{
			elements.Add(element);
		}

		/// <summary>
		/// Adds the element in a new markdownLine at the end of this section.
		/// </summary>
		public void AddInline(IMarkdownInline element)
		{
			elements.Add(new MarkdownLine(element));
		}

		/// <inheritdoc />
		public string ToMarkdown()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(new String('#', Depth) + " " + Header + "\n\n");
			foreach(IMarkdownInsection element in elements)
			{
				builder.Append(element.ToMarkdown());
			}

			return builder.ToString();
		}
	}
}
