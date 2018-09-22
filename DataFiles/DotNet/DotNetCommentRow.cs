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

		/// <summary></summary>
		public List<DotNetCommentCell> Cells = new List<DotNetCommentCell>();

		#region Constructors

		/// <summary></summary>
		public DotNetCommentRow(List<DotNetCommentCell> cells, bool isHeader=false)
		{
			Cells.AddRange(cells);
			IsHeader = isHeader;
		}
		
		/// <summary>Parses .Net XML documentation "listheader" or "item", expecting one "term" per cell.</summary>
		public static DotNetCommentRow FromVisualStudioXml(XElement element)
		{
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
