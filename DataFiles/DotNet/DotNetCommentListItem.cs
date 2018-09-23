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
	public class DotNetCommentListItem
	{
		/// <summary></summary>
		public bool IsHeader { get; protected set; }

		/// <summary></summary>
		public string Term { get; protected set; }

		/// <summary></summary>
		public string Description { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentListItem(string term, string description = null, bool isHeader = false)
		{
			Term = term;
			Description = description;
			IsHeader = isHeader;
		}

		/// <summary>Parses .Net XML documentation listheader or item.</summary>
		public static DotNetCommentListItem FromVisualStudioXml(XElement element)
		{
			DotNetComment.ValidateXmlTag(element, new string[] { "listheader", "item" });

			bool isHeader = (element.Name.LocalName == "listheader");
			string term = null;
			string description = null;

			foreach(XNode node in element.Nodes())
			{
				if(node.NodeType == XmlNodeType.Text)
				{
					term = node.ToString();
					break;
				}
				if(node.NodeType == XmlNodeType.Element)
				{
					XElement child = (node as XElement);
					switch(child.Name.LocalName)
					{
						case "term": term = child.Value; break;
						case "description": description = child.Value; break;
					}
				}
			}

			return new DotNetCommentListItem(term, description, isHeader);
		}

		#endregion
	}
}
