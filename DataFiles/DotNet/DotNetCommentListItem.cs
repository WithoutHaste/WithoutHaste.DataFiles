using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a listheader or item in a .Net XML documentation list.
	/// </summary>
	public class DotNetCommentListItem
	{
		/// <summary></summary>
		public bool IsHeader { get; protected set; }

		/// <summary></summary>
		public DotNetCommentGroup Term { get; protected set; }

		/// <summary></summary>
		public DotNetCommentGroup Description { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentListItem()
		{
			Term = null;
			Description = null;
			IsHeader = false;
		}

		/// <summary>Plain text <paramref name='term'/> and <paramref name='description'/>.</summary>
		public DotNetCommentListItem(string term, string description = null, bool isHeader = false)
		{
			Term = new DotNetCommentGroup(new DotNetCommentText(term));
			Description = new DotNetCommentGroup(new DotNetCommentText(description));
			IsHeader = isHeader;
		}

		/// <summary><paramref name='term'/> and <paramref name='description'/> containing more than plain text, such as a <c>see</c> tag.</summary>
		public DotNetCommentListItem(DotNetCommentGroup term, DotNetCommentGroup description = null, bool isHeader = false)
		{
			Term = term;
			Description = description;
			IsHeader = isHeader;
		}

		/// <summary>Parses .Net XML documentation listheader or item.</summary>
		/// <example>
		/// Format options:
		/// <![CDATA[
		///   <listheader>
		///     plain text
		///   </listheader>
		///   <listheader>
		///     <term>Term</term>
		///   </listheader>
		///   <listheader>
		///     <description>Description</description>
		///   </listheader>
		///   <listheader>
		///     <term>Term</term>
		///     <description>Description</description>
		///   </listheader>
		/// ]]>
		/// </example>
		/// <example>
		/// Format options:
		/// <![CDATA[
		///   <item>
		///     plain text
		///   </item>
		///   <item>
		///     <term>Term</term>
		///   </item>
		///   <item>
		///     <description>Description</description>
		///   </item>
		///   <item>
		///     <term>Term</term>
		///     <description>Description</description>
		///   </item>
		/// ]]>
		/// </example>
		public static DotNetCommentListItem FromVisualStudioXml(XElement element)
		{
			if(!DotNetComment.IsXmlTag(element, new string[] { "listheader", "item" }))
			{
				return new DotNetCommentListItem();
			}

			bool isHeader = (element.Name.LocalName == "listheader");
			DotNetCommentGroup term = null;
			DotNetCommentGroup description = null;

			foreach(XNode node in element.Nodes())
			{
				if(node.NodeType == XmlNodeType.Text)
				{
					term = DotNetCommentGroup.FromVisualStudioXml(element);
					break;
				}
				if(node.NodeType == XmlNodeType.Element)
				{
					XElement child = (node as XElement);
					switch(child.Name.LocalName)
					{
						case "term": term = DotNetCommentGroup.FromVisualStudioXml(child); break;
						case "description": description = DotNetCommentGroup.FromVisualStudioXml(child); break;
					}
				}
			}

			return new DotNetCommentListItem(term, description, isHeader);
		}

		#endregion
	}
}
