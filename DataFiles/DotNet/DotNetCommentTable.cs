using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a table in the comments.
	/// </summary>
	/// <example>
	/// <![CDATA[
	///  <list type="table">
	///   <listheader>
	///    <term>Column 1</term>
	///    <term>Column 2</term>
	///    <term>Column 3</term>
	///   </listheader>
	///   <item>
	///    <term>Row 1, Cell 1</term>
	///    <term>Row 1, Cell 2</term>
	///    <term>Row 1, Cell 3</term>
	///   </item>
	///  </list>
	/// ]]>
	/// </example>
	public class DotNetCommentTable : DotNetComment
	{
		/// <summary></summary>
		public List<DotNetCommentRow> Rows = new List<DotNetCommentRow>();

		/// <summary>Number of rows in the table. Includes header rows and normal rows.</summary>
		public int RowCount { get { return Rows.Count; } }

		/// <summary>Number of header rows in the table.</summary>
		public int HeaderRowCount { get { return Rows.Count(row => row.IsHeader); } }

		/// <summary>Number of data (non-header) rows in the table.</summary>
		public int DataRowCount { get { return Rows.Count(row => !row.IsHeader); } }

		/// <summary>Maximum number of columns in the table.</summary>
		public int ColumnCount {
			get {
				if(Rows.Count == 0)
					return 0;
				return Rows.Max(row => row.ColumnCount);
			}
		}

		/// <summary>
		/// Returns the selected <see cref="DotNetCommentCell"/> of the table. Will return an empty <see cref="DotNetCommentCell"/> if the cell within range but does not actually exist.
		/// </summary>
		/// <param name="rowIndex">0-based index of table row.</param>
		/// <param name="columnIndex">0-based index of table column.</param>
		/// <exception cref="IndexOutOfRangeException">Either the row or column index is out of range.</exception>
		public DotNetCommentCell this[int rowIndex, int columnIndex] {
			get {
				if(rowIndex < 0 || RowCount <= rowIndex)
					throw new IndexOutOfRangeException(String.Format("Table row index out of range [0,{0}]: index {1}.", RowCount-1, rowIndex));
				if(columnIndex < 0 || ColumnCount <= columnIndex)
					throw new IndexOutOfRangeException(String.Format("Table column index out of range [0,{0}]: index {1}.", ColumnCount - 1, columnIndex));
				return Rows[rowIndex][columnIndex];
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetCommentTable(List<DotNetCommentRow> rows)
		{
			Rows.AddRange(rows);
			Tag = CommentTag.List;
		}

		/// <summary>Parses .Net XML documentation table.</summary>
		public static new DotNetCommentTable FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "list");

			List<DotNetCommentRow> rows = new List<DotNetCommentRow>();
			foreach(XElement row in element.Elements())
			{
				if(row.Name.LocalName == "listheader" || row.Name.LocalName == "item")
				{
					rows.Add(DotNetCommentRow.FromVisualStudioXml(row));
				}
			}

			return new DotNetCommentTable(rows);
		}

		#endregion
	}
}
