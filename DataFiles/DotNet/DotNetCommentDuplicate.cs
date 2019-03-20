using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a the <![CDATA[<duplicate cref="" />]]> tag, which means that documentation should be copied from the specified (cref) class, interface, struct, or member.
	/// </summary>
	/// <remarks>This is just a marker class. If the cref can be resolved, the duplicated comments will automatically replace this comment.</remarks>
	public class DotNetCommentDuplicate : DotNetCommentQualifiedLink
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentDuplicate(DotNetQualifiedName name) : base(name)
		{
			Tag = CommentTag.Duplicate;
		}

		/// <summary></summary>
		public DotNetCommentDuplicate(DotNetCommentQualifiedLink link) : base(link.Name)
		{
			Tag = CommentTag.Duplicate;
		}

		#endregion
	}
}
