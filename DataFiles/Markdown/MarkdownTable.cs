using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a markdown table.
	/// </summary>
	/// <remarks>
	/// Markdown requires each table to have exactly 1 header row, so the first row is assumed to be the header.
	/// </remarks>
	public class MarkdownTable : IMarkdownIsBlock, IMarkdownInSection
	{
		/// <summary>Minimum column width is 3 to allow for minimum "---" contents indicating header/data divider.</summary>
		public const int MINIMUM_COLUMN_WIDTH = 3;

		/// <summary></summary>
		public List<MarkdownTableRow> Rows = new List<MarkdownTableRow>();

		/// <summary></summary>
		public MarkdownTable(List<MarkdownTableRow> rows)
		{
			Rows.AddRange(rows);
		}

		/// <summary></summary>
		public MarkdownTable(params MarkdownTableRow[] rows)
		{
			Rows.AddRange(rows);
		}

		/// <summary>Add a row to the end of the table.</summary>
		public void Add(MarkdownTableRow row)
		{
			Rows.Add(row);
		}

		/// <summary>
		/// Return markdown-formatted text.
		/// </summary>
		public string ToMarkdownString(string previousText)
		{
			List<int> columnWidths = GetColumnWidths();
			StringBuilder builder = new StringBuilder();

			if(!String.IsNullOrEmpty(previousText))
			{
				if(previousText.EndsWith("\n\n"))
				{
					//no action
				}
				else if(previousText.StartsWith("#"))
				{
					//no action
				}
				else if(previousText.EndsWith("\n"))
				{
					if(previousText.Length > 1)
					{
						builder.Append("\n");
					}
				}
				else
				{
					builder.Append("\n\n");
				}
			}

			bool finishedHeader = false;
			foreach(MarkdownTableRow row in Rows)
			{
				builder.Append(row.ToMarkdownString(columnWidths));
				if(!finishedHeader)
				{
					builder.Append(DividerToMarkdown(columnWidths));
					finishedHeader = true;
				}
			}

			return builder.ToString();
		}

		private string DividerToMarkdown(List<int> columnWidths)
		{
			StringBuilder builder = new StringBuilder();
			foreach(int width in columnWidths)
			{
				builder.Append("| ");
				builder.Append(new String('-', width));
				builder.Append(" ");
			}
			builder.Append("|\n");
			return builder.ToString();
		}

		/// <summary>
		/// Returns the width of the widest cell in each column.
		/// </summary>
		private List<int> GetColumnWidths()
		{
			List<int> widths = new List<int>();
			foreach(MarkdownTableRow row in Rows)
			{
				for(int i = 0; i < row.Cells.Count; i++)
				{
					while(widths.Count <= i)
					{
						widths.Add(MINIMUM_COLUMN_WIDTH);
					}
					widths[i] = Math.Max(widths[i], row.Cells[i].Length);
				}
			}
			return widths;
		}
	}
}
