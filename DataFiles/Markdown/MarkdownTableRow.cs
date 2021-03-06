﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents one row in a Markdown table.
	/// </summary>
	public class MarkdownTableRow
	{
		/// <summary>List of the cells in the row.</summary>
		public List<string> Cells = new List<string>();

		/// <summary></summary>
		public MarkdownTableRow()
		{
		}

		/// <summary></summary>
		public MarkdownTableRow(List<string> cells)
		{
			Cells.AddRange(cells);
		}

		/// <summary></summary>
		public MarkdownTableRow(params string[] cells)
		{
			Cells.AddRange(cells);
		}

		/// <summary>Add a cell to the end of the row.</summary>
		/// <param name="cell">Contents of cell.</param>
		public void Add(string cell)
		{
			Cells.Add(cell);
		}

		/// <summary>
		/// Return markdown-formatted text.
		/// </summary>
		/// <remarks>Column widths are padded an additional 1 space on left and right, per Markdown formatting.</remarks>
		/// <remarks>Line feed, new line, and tab characters are removed.</remarks>
		/// <exception cref="ArgumentException">ColumnWidths cannot be null or shorter than the row.</exception>
		public string ToMarkdownString(List<int> columnWidths)
		{
			if(columnWidths == null || columnWidths.Count < Cells.Count)
				throw new ArgumentException("ColumnWidths cannot be null or shorter than the row.");

			StringBuilder builder = new StringBuilder();

			for(int i = 0; i < Cells.Count; i++)
			{
				string text = Cells[i];
				text = (text == null) ? "" : text.Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ');
				builder.Append("| ");
				builder.Append(text.PadRight(columnWidths[i], ' '));
				builder.Append(" ");
			}

			builder.Append("|\n");
			return builder.ToString();
		}
	}
}
