using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// The type of xml tag that the comment came from.
	/// </summary>
	public enum CommentTag
	{
		/// <summary></summary>
		Unknown = 0,
		/// <summary></summary>
		C,
		/// <summary></summary>
		Code,
		/// <summary></summary>
		Duplicate,
		/// <summary></summary>
		Example,
		/// <summary></summary>
		Exception,
		/// <summary></summary>
		InheritDoc,
		/// <summary></summary>
		List,
		/// <summary></summary>
		Para,
		/// <summary></summary>
		Param,
		/// <summary></summary>
		ParamRef,
		/// <summary></summary>
		Permission,
		/// <summary></summary>
		Remarks,
		/// <summary></summary>
		Returns,
		/// <summary></summary>
		See,
		/// <summary></summary>
		SeeAlso,
		/// <summary></summary>
		Summary,
		/// <summary></summary>
		TypeParam,
		/// <summary></summary>
		TypeParamRef,
		/// <summary></summary>
		Value,
	};

	/// <summary>
	/// Represents a section of documentation, such as the contents of a <![CDATA[<summary></summary>]]> tag.
	/// </summary>
	public abstract class DotNetComment
	{
		/// <summary>The type of xml tag that the comment came from.</summary>
		public CommentTag Tag { get; protected set; }

		/// <summary>Parses top-level .Net XML documentation comments. Returns null if no comments are found.</summary>
		/// <returns>Returns null if the element name is not recognized.</returns>
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
					DotNetCommentGroup group = DotNetCommentGroup.FromVisualStudioXml(element);
					if(group.IsEmpty)
						return null;
					return group;

				case "exception":
				case "permission":
					if(element.Attribute("cref") == null)
						break;
					if(String.IsNullOrEmpty(element.Attribute("cref").Value))
						break;
					return DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);

				case "see":
				case "seealso":
					if(element.Attribute("cref") == null)
						break;
					if(String.IsNullOrEmpty(element.Attribute("cref").Value))
						break;
					if(element.Nodes().Count() == 0)
						return DotNetCommentQualifiedLink.FromVisualStudioXml(element);
					else
						return DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);

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
					return DotNetCommentCode.FromVisualStudioXml(element);
				case "code": //code block
					return DotNetCommentCodeBlock.FromVisualStudioXml(element);

				case "inheritdoc":
					return new DotNetCommentInherit();
				case "duplicate":
					if(element.Attribute("cref") == null)
						break;
					string duplicateCref = element.Attribute("cref").Value;
					if(String.IsNullOrEmpty(duplicateCref))
						break;
					return new DotNetCommentDuplicate(DotNetCommentQualifiedLink.FromVisualStudioXml(duplicateCref));
			}
			return null;
		}

		/// <summary>Creates a plain text comment.</summary>
		public static DotNetCommentText FromVisualStudioXml(string text)
		{
			return new DotNetCommentText(text);
		}

		/// <summary>Parses inner .Net XML documentation comments.</summary>
		protected static List<DotNetComment> ParseSection(XElement element)
		{
			if(element == null)
				return new List<DotNetComment>();

			element = element.CleanWhitespaces();
			List<DotNetComment> comments = new List<DotNetComment>();
			List<CommentTag> nonParagraphTags = new List<CommentTag>() {
				CommentTag.Unknown,
				CommentTag.C,
				CommentTag.ParamRef,
				CommentTag.See,
				CommentTag.SeeAlso,
				CommentTag.TypeParamRef,
			};
			bool previousCommentWasAParagraphTag = false;
			foreach(XNode node in element.Nodes())
			{
				DotNetComment comment;
				switch(node.NodeType)
				{
					case XmlNodeType.CDATA:
						if(node.ToString().Contains("\n"))
						{
							comment = DotNetCommentCodeBlock.FromVisualStudioXml(node as XCData);
							comments.Add(comment);
							previousCommentWasAParagraphTag = true;
						}
						else
						{
							comment = DotNetCommentCode.FromVisualStudioXml(node as XCData);
							comments.Add(comment);
							previousCommentWasAParagraphTag = false;
						}
						break;
					case XmlNodeType.Element:
						comment = DotNetComment.FromVisualStudioXml(node as XElement);
						if(comment == null)
							break;
						comments.Add(comment);
						previousCommentWasAParagraphTag = !nonParagraphTags.Contains(comment.Tag);
						break;
					case XmlNodeType.Text:
						comment = DotNetComment.FromVisualStudioXml(Utilities.XNodeToString(node));
						if(comment == null)
							break;
						if(previousCommentWasAParagraphTag && comment.ToString() == "\n")
							break;
						comments.Add(comment);
						previousCommentWasAParagraphTag = !nonParagraphTags.Contains(comment.Tag);
						break;
				}
			}
			return comments;
		}

		/// <summary>
		/// Throws exception on unexpected xml formats.
		/// </summary>
		/// <exception cref="XmlFormatException">XML tag does not have the expected local name, or is null</exception>
		public static void ValidateXmlTag(XElement element, string localName)
		{
			string elementName = null;
			if(element != null)
				elementName = element.Name.LocalName;

			if(elementName == null || elementName != localName)
			{
				throw new XmlFormatException(String.Format("Unexpected xml element '{0}'. Expecting '{1}'.", elementName, localName));
			}
		}

		/// <summary>
		/// Throws exception on unexpected xml formats.
		/// </summary>
		/// <exception cref="XmlFormatException">XML tag does not have any of the expected local names, or is null</exception>
		public static void ValidateXmlTag(XElement element, string[] localNames)
		{
			string elementName = null;
			if(element != null)
				elementName = element.Name.LocalName;

			if(elementName == null || !localNames.Contains(elementName))
				throw new XmlFormatException(String.Format("Unexpected xml element '{0}'. Expecting any of {1}.", elementName, String.Join(", ", localNames.Select(x => "'"+x+"'").ToArray())));
		}

		/// <summary>
		/// Returns false on unexpected xml formats.
		/// </summary>
		public static bool IsXmlTag(XElement element, string localName)
		{
			return !(element == null || element.Name.LocalName != localName);
		}

		/// <summary>
		/// Returns false on unexpected xml formats.
		/// </summary>
		public static bool IsXmlTag(XElement element, string[] localNames)
		{
			return !(element == null || !localNames.Contains(element.Name.LocalName));
		}

		/// <summary>
		/// Returns the CommentTag value that corresponds to the XElement.
		/// </summary>
		public static CommentTag GetTag(XElement element)
		{
			switch(element.Name.LocalName.ToLower())
			{
				case "c": return CommentTag.C;
				case "code": return CommentTag.Code;
				case "duplicate": return CommentTag.Duplicate;
				case "example": return CommentTag.Example;
				case "exception": return CommentTag.Exception;
				case "inheritdoc": return CommentTag.InheritDoc;
				case "list": return CommentTag.List;
				case "para": return CommentTag.Para;
				case "param": return CommentTag.Param;
				case "paramref": return CommentTag.ParamRef;
				case "permission": return CommentTag.Permission;
				case "remarks": return CommentTag.Remarks;
				case "returns": return CommentTag.Returns;
				case "see": return CommentTag.See;
				case "seealso": return CommentTag.SeeAlso;
				case "summary": return CommentTag.Summary;
				case "typeparam": return CommentTag.TypeParam;
				case "typeparamref": return CommentTag.TypeParamRef;
				case "value": return CommentTag.Value;
			}
			return CommentTag.Unknown;
		}

		#region Low Level

		/// <summary>Defaults to the CommentTag text.</summary>
		public override string ToString()
		{
			return Tag.ToString();
		}

		#endregion
	}
}
