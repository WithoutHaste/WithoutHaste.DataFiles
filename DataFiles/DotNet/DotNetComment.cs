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
	/// Represents a section of documentation, such as the contents of a <![CDATA[<summary></summary>]]> tag.
	/// </summary>
	public abstract class DotNetComment
	{
		/// <summary>Parses top-level .Net XML documentation comments.</summary>
		public static DotNetComment FromVisualStudioXml(XElement element)
		{
			switch(element.Name.LocalName)
			{
				case "summary":
				case "remarks":
				case "example":
				case "para": //paragraph
				case "returns":
				case "value":
					return ParseGroup(element);

				case "exception":
				case "permission":
				case "see": //link only
				case "seealso": //link only
					string cref = element.Attribute("cref")?.Value;
					DotNetCommentGroup group = ParseGroup(element);
					group.SetLink(DotNetCommentQualifiedLink.FromVisualStudioXml(cref));
					return group;

				case "list":
					return DotNetCommentList.FromVisualStudioXml(element);

				case "param":
					return DotNetCommentParameter.FromVisualStudioXml(element);
				case "paramref":
					return DotNetCommentParameterLink.FromVisualStudioXml(element);

				case "typeparam":
					return DotNetCommentTypeParameter.FromVisualStudioXml(element);
				case "typeparamref":
					return DotNetCommentTypeParameterLink.FromVisualStudioXml(element);

				case "c": //inline code
					return new DotNetCommentCode(element.Value);
			}
			return null;
		}

		/// <summary>Creates a plain text comment.</summary>
		public static DotNetCommentText FromVisualStudioXml(string text)
		{
			return new DotNetCommentText(text);
		}

		/// <summary>Parses inner .Net XML documentation comments.</summary>
		public static DotNetCommentGroup ParseGroup(XElement element)
		{
			DotNetCommentGroup group = new DotNetCommentGroup();
			foreach(XNode node in element.Nodes())
			{
				switch(node.NodeType)
				{
					case XmlNodeType.Element:
						group.Add(DotNetComment.FromVisualStudioXml(node as XElement));
						break;
					case XmlNodeType.Text:
						group.Add(DotNetComment.FromVisualStudioXml(node.ToString()));
						break;
				}
			}
			return group;
		}
	}
}
