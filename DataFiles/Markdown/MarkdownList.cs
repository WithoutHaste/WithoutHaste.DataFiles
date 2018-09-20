using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a markdown list.
	/// </summary>
	public class MarkdownList : IMarkdownInSection, IMarkdownInList
	{
		/// <summary>
		/// 0-indexed nesting depth of list.
		/// </summary>
		public int Depth {
			get {
				return depth;
			}
			protected set {
				if(value < 0) throw new Exception("Depth cannot be less than 0.");

				depth = value;
				UpdateDepths(elements.OfType<MarkdownList>().ToArray());
			}
		}

		/// <summary>
		/// True means the list will be numbered. 
		/// False means the list will be bulleted.
		/// </summary>
		public bool IsNumbered { get; protected set; }

		/// <summary>
		/// The length of the list. Nested lists count as 1 each.
		/// </summary>
		public int Length { get { return elements.Count; } }

		/// <summary>
		/// Get an element from the list by 0-based index.
		/// </summary>
		public IMarkdownInList this[int i] {
			get {
				if(i < 0 || i >= elements.Count)
					throw new IndexOutOfRangeException(String.Format("List index out of range: [0, {0}].", elements.Count - 1));
				return elements[i];
			}
		}

		private int depth;
		private List<IMarkdownInList> elements = new List<IMarkdownInList>();
		private string Margin { get { return new String(' ', Depth * 2); } }

		#region Constructors

		/// <summary>
		/// Creates an empty list.
		/// </summary>
		public MarkdownList(bool isNumbered = false)
		{
			Init(isNumbered);
		}

		/// <summary>
		/// Creates a list of the specified MarkdownLines.
		/// </summary>
		public MarkdownList(bool isNumbered = false, params IMarkdownInList[] inList)
		{
			Init(isNumbered);
			UpdateDepths(inList.OfType<MarkdownList>().ToArray());
			elements.AddRange(inList);
		}

		/// <summary>
		/// Creates a list MarkdownLines containing the specified IMarkdownInline elements.
		/// </summary>
		public MarkdownList(bool isNumbered = false, params IMarkdownInLine[] lines)
		{
			Init(isNumbered);
			elements.AddRange(lines.Select(line => new MarkdownLine(line)).Cast<IMarkdownInList>());
		}

		private void Init(bool isNumbered)
		{
			IsNumbered = isNumbered;
			Depth = 0;
		}

		#endregion

		/// <summary>
		/// Adds element to the end of the list.
		/// </summary>
		public void Add(IMarkdownInList line)
		{
			if(line is MarkdownList) UpdateDepth(line as MarkdownList);
			elements.Add(line);
		}

		/// <summary>
		/// Adds new MarkdownLine containing the specified element to the end of the list.
		/// </summary>
		public void Add(IMarkdownInLine element)
		{
			elements.Add(new MarkdownLine(element));
		}

		/// <inheritdoc />
		public string ToMarkdown()
		{
			if(elements.Count == 0)
				return "";

			StringBuilder builder = new StringBuilder();

			if(IsNumbered) ToMarkdownNumbered(builder);
			else ToMarkdownBulleted(builder);

			return builder.ToString();
		}

		private void ToMarkdownNumbered(StringBuilder builder)
		{
			int count = 1;
			foreach(IMarkdownInList line in elements)
			{
				builder.Append(String.Format("{0}{1}. {2}", Margin, count, line.ToMarkdown()));
				count++;
			}
		}

		private void ToMarkdownBulleted(StringBuilder builder)
		{
			foreach(IMarkdownInList line in elements)
			{
				builder.Append(String.Format("{0}* {1}", Margin, line.ToMarkdown()));
			}
		}

		private void UpdateDepths(MarkdownList[] sublists)
		{
			foreach(MarkdownList sublist in sublists)
			{
				UpdateDepth(sublist);
			}
		}

		private void UpdateDepth(MarkdownList sublist)
		{
			sublist.Depth = this.Depth + 1;
		}
	}
}
