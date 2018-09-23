using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a parameter description in the comments.
	/// </summary>
	/// <example><![CDATA[<param name="myParam">nested comments</param>]]></example>
	public class DotNetCommentParameter : DotNetCommentLinkedGroup<DotNetCommentParameterLink>
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentParameter(DotNetCommentParameterLink link, DotNetComment comment) : base(link, comment)
		{
		}

		/// <summary></summary>
		public DotNetCommentParameter(DotNetCommentParameterLink link, params DotNetComment[] comments) : base(link, comments)
		{
		}

		/// <summary></summary>
		public DotNetCommentParameter(DotNetCommentParameterLink link, List<DotNetComment> comments) : base(link, comments)
		{
		}

		/// <summary>Parses .Net XML documentation for param.</summary>
		public static new DotNetCommentParameter FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "param");
			return new DotNetCommentParameter(
				new DotNetCommentParameterLink(element.Attribute("name")?.Value),
				ParseSection(element)
			);
		}

		#endregion
	}
}
