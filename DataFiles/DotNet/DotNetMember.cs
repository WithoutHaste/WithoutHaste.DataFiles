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
	/// Represents anything that a class/struct/interface may contain.
	/// </summary>
	public abstract class DotNetMember
	{
		/// <summary></summary>
		public DotNetQualifiedName Name { get; protected set; }

		/// <summary>Returns the number of top level comments, which may contain more nested comments.</summary>
		public int CommentCount {
			get {
				return summaryComments.Count
					+ remarksComments.Count
					+ permissionsComments.Count
					+ exampleComments.Count
					+ exceptionComments.Count
					+ parameterComments.Count
					+ (valueComment != null ? 1 : 0)
					+ (returnsComment != null ? 1 : 0)
					+ floatingComments.Count;
			}
		}

		/// <summary>True when there are no comments.</summary>
		public bool IsEmpty { get { return (CommentCount == 0); } }

		private List<DotNetComment> summaryComments = new List<DotNetComment>();
		private List<DotNetComment> remarksComments = new List<DotNetComment>();
		private List<DotNetComment> permissionsComments = new List<DotNetComment>();
		private List<DotNetComment> exampleComments = new List<DotNetComment>();
		private List<DotNetCommentGroup> exceptionComments = new List<DotNetCommentGroup>();
		private List<DotNetCommentGroup> parameterComments = new List<DotNetCommentGroup>(); //for <param> and <typeparam>
		private DotNetComment valueComment; //properties only
		private DotNetComment returnsComment; //methods only
		private List<DotNetComment> floatingComments = new List<DotNetComment>(); //anything not inside main tags

		/// <summary></summary>
		public DotNetMember(DotNetQualifiedName name)
		{
			Name = name;
		}

		/// <summary>
		/// Parse .Net XML documentation about this member.
		/// </summary>
		/// <param name="parent">Expects the tag containing all documentation for this member.</param>
		public void ParseVisualStudioXmlDocumentation(XElement parent)
		{
			foreach(XNode node in parent.Nodes())
			{
				switch(node.NodeType)
				{
					case XmlNodeType.Element:
						XElement element = (node as XElement);
						switch(element.Name.LocalName)
						{
							case "summary":
								summaryComments.Add(DotNetComment.FromVisualStudioXml(element));
								break;
							case "remarks":
								remarksComments.Add(DotNetComment.FromVisualStudioXml(element));
								break;
							case "example":
								exampleComments.Add(DotNetComment.FromVisualStudioXml(element));
								break;
							case "exception":
								exceptionComments.Add(DotNetComment.FromVisualStudioXml(element) as DotNetCommentGroup);
								break;
							case "value":
								valueComment = DotNetComment.FromVisualStudioXml(element);
								break;
							case "returns":
								returnsComment = DotNetComment.FromVisualStudioXml(element);
								break;
							case "param":
							case "typeparam":
								parameterComments.Add(DotNetComment.FromVisualStudioXml(element) as DotNetCommentGroup);
								break;
							default:
								floatingComments.Add(DotNetComment.FromVisualStudioXml(element));
								break;
						}
						break;
					case XmlNodeType.Text:
						floatingComments.Add(DotNetComment.FromVisualStudioXml(node.ToString()));
						break;
				}
			}
		}

	}
}
