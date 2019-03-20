using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	//todo: class needs a better name

	/// <summary>
	/// Represents a section of comments that is linked to a fully qualified type or member.
	/// </summary>
	public class DotNetCommentQualifiedLinkedGroup : DotNetCommentLinkedGroup
	{
		/// <summary>Strongly-typed link.</summary>
		public DotNetCommentQualifiedLink QualifiedLink { get { return Link as DotNetCommentQualifiedLink; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentQualifiedLinkedGroup(DotNetCommentQualifiedLink link, CommentTag tag, DotNetComment comment) : base(link, comment)
		{
			Tag = tag;
		}

		/// <summary></summary>
		public DotNetCommentQualifiedLinkedGroup(DotNetCommentQualifiedLink link, CommentTag tag, params DotNetComment[] comments) : base(link, comments)
		{
			Tag = tag;
		}

		/// <summary></summary>
		public DotNetCommentQualifiedLinkedGroup(DotNetCommentQualifiedLink link, CommentTag tag, List<DotNetComment> comments) : base(link, comments)
		{
			Tag = tag;
		}

		/// <summary>Parses .Net XML documentation for permission or exception.</summary>
		/// <example><![CDATA[<permission cref="Namespace.Type.Member">nested comments</permission>]]></example>
		/// <example><![CDATA[<exception cref="Namespace.ExceptionType">nested comments</exception>]]></example>
		public static new DotNetCommentQualifiedLinkedGroup FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, new string[] { "permission", "exception", "see", "seealso" });
			CommentTag tag = DotNetComment.GetTag(element);

			DotNetCommentQualifiedLink link = DotNetCommentQualifiedLink.FromVisualStudioXml(element.GetAttributeValue("cref"));
			List<DotNetComment> comments = ParseSection(element);

			if(link is DotNetCommentMethodLink)
			{
				return new DotNetCommentMethodLinkedGroup(link as DotNetCommentMethodLink, tag, comments);
			}
			return new DotNetCommentQualifiedLinkedGroup(link, tag, comments);
		}

		#endregion

		#region Low Level

		/// <summary>
		/// Returns true if link name matches the member name.
		/// </summary>
		public bool Matches(DotNetMember member)
		{
			return member.Matches(this);
		}

		#endregion
	}
}
