using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a list in the comments.
	/// </summary>
	public class DotNetCommentList : DotNetComment
	{
		/// <summary>
		/// True for numbered lists.
		/// False for bulleted lists.
		/// </summary>
		public bool IsNumbered { get; protected set; }

		/// <summary>Items in the list.</summary>
		public List<DotNetCommentListItem> Items = new List<DotNetCommentListItem>();

		/// <summary>Access list items by 0-based index.</summary>
		/// <exception cref="IndexOutOfRangeException"></exception>
		public DotNetCommentListItem this[int index] {
			get {
				if(index < 0 || index >= Items.Count)
					throw new IndexOutOfRangeException("Index out of range: " + index);
				return Items[index];
			}
		}

		/// <summary>Number of items in the list. Includes headers.</summary>
		public int Length { get { return Items.Count; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentList(List<DotNetCommentListItem> items, bool isNumbered=false)
		{
			Items.AddRange(items);
			IsNumbered = isNumbered;
			Tag = CommentTag.List;
		}

		/// <summary>Parses .Net XML documentation list (which may actually be a table).</summary>
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
		public static new DotNetComment FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "list");

			string type = element.GetAttributeValue("type");
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
