using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	//todo: class needs a better name

	/// <summary>
	/// Represents a section of comments that is linked to a fully qualified type or member.
	/// </summary>
	/// <example><![CDATA[<permission cref="Namespace.Type.Member">nested comments</permission>]]></example>
	/// <example><![CDATA[<exception cref="Namespace.ExceptionType">nested comments</exception>]]></example>
	public class DotNetCommentQualifiedLinkedGroup : DotNetCommentLinkedGroup
	{
		/// <summary>Strongly-typed link.</summary>
		public DotNetCommentQualifiedLink QualifiedLink { get { return Link as DotNetCommentQualifiedLink; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentQualifiedLinkedGroup(DotNetCommentQualifiedLink link, DotNetComment comment) : base(link, comment)
		{
		}

		/// <summary></summary>
		public DotNetCommentQualifiedLinkedGroup(DotNetCommentQualifiedLink link, params DotNetComment[] comments) : base(link, comments)
		{
		}

		/// <summary></summary>
		public DotNetCommentQualifiedLinkedGroup(DotNetCommentQualifiedLink link, List<DotNetComment> comments) : base(link, comments)
		{
		}

		/// <summary>Parses .Net XML documentation for permission or exception.</summary>
		public static new DotNetCommentQualifiedLinkedGroup FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, new string[] { "permission", "exception" });
			DotNetCommentQualifiedLink link = DotNetCommentQualifiedLink.FromVisualStudioXml(element.Attribute("cref")?.Value);
			List<DotNetComment> comments = ParseSection(element);

			if(link is DotNetCommentMethodLink)
			{
				return new DotNetCommentMethodLinkedGroup(link as DotNetCommentMethodLink, comments);
			}
			return new DotNetCommentQualifiedLinkedGroup(link, comments);
		}

		#endregion
	}
}
