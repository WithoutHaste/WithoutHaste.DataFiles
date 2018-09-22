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
	/// Represents either an item in a list, or a cell in a table, in .Net XML documentation.
	/// </summary>
	public class DotNetCommentCell
	{
		/// <summary></summary>
		public bool IsHeader { get; protected set; }

		/// <summary></summary>
		public string Term { get; protected set; }

		/// <summary></summary>
		public string Description { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentCell(string term, string description=null, bool isHeader=false)
		{
			Term = term;
			Description = description;
			IsHeader = isHeader;
		}

		/// <summary>Parses .Net XML documentation listheader or item.</summary>
		public static DotNetCommentCell FromVisualStudioXml(XElement element)
		{
			bool isHeader = (element.Name.LocalName == "listheader");
			string term = null;
			string description = null;

			foreach(XNode node in element.Nodes())
			{
				if(node.NodeType == XmlNodeType.Text)
				{
					term = node.ToString();
					continue;
				}
				else if(node.NodeType == XmlNodeType.Element)
				{
					XElement child = (node as XElement);
					switch(child.Name.LocalName)
					{
						case "term": term = child.Value; break;
						case "description": description = child.Value; break;
					}
				}
			}

			return new DotNetCommentCell(term, description, isHeader);
		}

		#endregion
	}
}
