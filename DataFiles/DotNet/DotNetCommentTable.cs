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

		#region Constructors

		/// <summary></summary>
		public DotNetCommentTable(List<DotNetCommentRow> rows)
		{
			Rows.AddRange(rows);
		}

		/// <summary>Parses .Net XML documentation table.</summary>
		public static new DotNetCommentTable FromVisualStudioXml(XElement element)
		{
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
