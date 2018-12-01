using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a row of data in a .Net XML documentation table.
	/// </summary>
	public class DotNetCommentRow
	{
		/// <summary></summary>
		public bool IsHeader { get; protected set; }

		/// <summary>Number of columns (cells) in the row.</summary>
		public int ColumnCount { get { return Cells.Count; } }

		/// <summary></summary>
		public List<DotNetCommentCell> Cells = new List<DotNetCommentCell>();

		/// <summary>
		/// Returns the selected cell of the row. Returns an empty cell if no cell is found.
		/// </summary>
		/// <remarks>Returns an empty cell because Row does not know the number of columns in the Table, just how many cells are filled on this row.</remarks>
		/// <param name="columnIndex">0-based index of table column.</param>
		/// <exception cref="IndexOutOfRangeException">Column index is negative.</exception>
		public DotNetCommentCell this[int columnIndex] {
			get {
				if(columnIndex < 0)
					throw new IndexOutOfRangeException("Table column index cannot be negative.");
				if(ColumnCount <= columnIndex)
					return DotNetCommentCell.EmptyCell;
				return Cells[columnIndex];
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetCommentRow(List<DotNetCommentCell> cells, bool isHeader=false)
		{
			Cells.AddRange(cells);
			IsHeader = isHeader;
		}

		/// <summary>Parses .Net XML documentation "listheader" or "item", expecting one "term" per cell.</summary>
		/// <example><![CDATA[<listheader><term>Header 1</term><term>Header 2</term></listheader>]]></example>
		/// <example><![CDATA[<item><term>Cell 1</term><term>Cell 2</term></item>]]></example>
		public static DotNetCommentRow FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, new string[] { "listheader", "item" });

			bool isHeader = (element.Name.LocalName == "listheader");
			List<DotNetCommentCell> cells = new List<DotNetCommentCell>();

			foreach(XElement cell in element.Elements())
			{
				if(cell.Name.LocalName != "term") continue;
				cells.Add(new DotNetCommentCell(cell.Value));
			}

			return new DotNetCommentRow(cells, isHeader);
		}

		#endregion
	}
}
