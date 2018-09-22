using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a list in the comments.
	/// </summary>
	/// <example>
	/// <![CDATA[
	///  <list type="bullet"> <!-- type can also be "number" -->
	///   <listheader>
	///    <term>Term</term>
	///    <description>Description</description>
	///   </listheader>
	///   <item>
	///    <term>Term</term>
	///    <description>Description</description>
	///   </item>
	///  </list>
	/// ]]>
	/// </example>
	public class DotNetCommentList : DotNetComment
	{
		/// <summary>
		/// True for numbered lists (numbering starts at 1).
		/// False for bulleted lists.
		/// </summary>
		public bool IsNumbered { get; protected set; }

		/// <summary>
		/// Items in the list.
		/// </summary>
		public List<DotNetCommentListItem> Items = new List<DotNetCommentListItem>();

		#region Constructors

		/// <summary></summary>
		public DotNetCommentList(List<DotNetCommentListItem> items, bool isNumbered=false)
		{
			Items.AddRange(items);
			IsNumbered = isNumbered;
		}
		
		/// <summary>Parses .Net XML documentation list (which may actually be a table).</summary>
		public static new DotNetComment FromVisualStudioXml(XElement element)
		{
			string type = element.Attribute("type")?.Value;
			if(type == "table")
			{
				return DotNetCommentTable.FromVisualStudioXml(element);
			}

			bool isNumbered = (type == "number"); //none=bullet, bullet, number, table

			List<DotNetCommentListItem> items = new List<DotNetCommentListItem>();
			foreach(XElement item in element.Elements())
			{
				items.Add(DotNetCommentListItem.FromVisualStudioXml(item));
			}

			return new DotNetCommentList(items, isNumbered);
		}

		#endregion
	}
}
