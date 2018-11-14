using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a link in the comments to an internal or extenal type or type.method().
	/// </summary>
	/// <example><![CDATA[<exception cref="Namespace.ExceptionType">nested comments and/or plain text</exception>]]></example>
	/// <example><![CDATA[<permission cref="Namespace.Type">nested comments and/or plain text</permission>]]></example>
	public class DotNetCommentQualifiedLink : DotNetComment, IDotNetCommentLink
	{
		/// <summary>Name of type or member.</summary>
		public DotNetQualifiedName Name { get; protected set; }

		/// <inheritdoc />
		public string FullName { get { return Name?.FullName; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentQualifiedLink(DotNetQualifiedName name)
		{
			Name = name;
		}

		/// <summary></summary>
		public DotNetCommentQualifiedLink(DotNetQualifiedName name, CommentTag tag)
		{
			Name = name;
			Tag = tag;
		}

		/// <summary>Parses .Net XML documentation tag that contains attribute cref.</summary>
		public static new DotNetCommentQualifiedLink FromVisualStudioXml(XElement element)
		{
			DotNetCommentQualifiedLink link = FromVisualStudioXml(element.Attribute("cref")?.Value);
			link.Tag = DotNetComment.GetTag(element);
			return link;
		}

		/// <summary>Parses .Net XML documentation cref.</summary>
		public static new DotNetCommentQualifiedLink FromVisualStudioXml(string cref)
		{
			int divider = cref.IndexOf("(");
			if(divider > -1 || cref.StartsWith("M:"))
				return DotNetCommentMethodLink.FromVisualStudioXml(cref);

			return new DotNetCommentQualifiedLink(DotNetQualifiedName.FromVisualStudioXml(cref));
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
