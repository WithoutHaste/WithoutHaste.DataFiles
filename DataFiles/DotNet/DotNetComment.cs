﻿using System;
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
					return new DotNetCommentGroup(ParseSection(element));

				case "exception":
				case "permission":
					return DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);

				case "see": //link only
				case "seealso": //link only
					string cref = element.Attribute("cref")?.Value;
					return DotNetCommentQualifiedLink.FromVisualStudioXml(cref);

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
			List<DotNetComment> comments = new List<DotNetComment>();
			foreach(XNode node in element.Nodes())
			{
				switch(node.NodeType)
				{
					case XmlNodeType.Element:
						comments.Add(DotNetComment.FromVisualStudioXml(node as XElement));
						break;
					case XmlNodeType.Text:
						comments.Add(DotNetComment.FromVisualStudioXml(node.ToString()));
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
			if(element == null || element.Name.LocalName != localName)
				throw new XmlFormatException(String.Format("Unexpected xml element '{0}'. Expecting '{1}'.", element?.Name.LocalName, localName));
		}

		/// <summary>
		/// Throws exception on unexpected xml formats.
		/// </summary>
		/// <exception cref="XmlFormatException">XML tag does not have any of the expected local names, or is null</exception>
		public static void ValidateXmlTag(XElement element, string[] localNames)
		{
			if(element == null || !localNames.Contains(element.Name.LocalName))
				throw new XmlFormatException(String.Format("Unexpected xml element '{0}'. Expecting any of {1}.", element?.Name.LocalName, String.Join(", ", localNames.Select(x => "'"+x+"'").ToArray())));
		}
	}
}