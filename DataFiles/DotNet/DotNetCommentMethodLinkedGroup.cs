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
	public class DotNetCommentMethodLinkedGroup : DotNetCommentQualifiedLinkedGroup
	{
		/// <summary>Strongly-typed link.</summary>
		public DotNetCommentMethodLink MethodLink { get { return Link as DotNetCommentMethodLink; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentMethodLinkedGroup(DotNetCommentMethodLink link, CommentTag tag, DotNetComment comment) : base(link, tag, comment)
		{
		}

		/// <summary></summary>
		public DotNetCommentMethodLinkedGroup(DotNetCommentMethodLink link, CommentTag tag, params DotNetComment[] comments) : base(link, tag, comments)
		{
		}

		/// <summary></summary>
		public DotNetCommentMethodLinkedGroup(DotNetCommentMethodLink link, CommentTag tag, List<DotNetComment> comments) : base(link, tag, comments)
		{
		}

		#endregion
	}
}
