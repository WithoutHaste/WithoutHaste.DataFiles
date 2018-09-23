using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents an ordered collection of comments that is linked to something in the assembly which it is documenting.
	/// </summary>
	public class DotNetCommentLinkedGroup<T> : DotNetCommentGroup where T : IDotNetCommentLink
	{
		/// <summary>Reference link from comments to something in the assembly.</summary>
		public T Link { get; protected set; }

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetCommentLinkedGroup()
		{
		}

		/// <summary></summary>
		public DotNetCommentLinkedGroup(T link, DotNetComment comment) : base(comment)
		{
			Link = link;
		}

		/// <summary></summary>
		public DotNetCommentLinkedGroup(T link, params DotNetComment[] comments) : base(comments)
		{
			Link = link;
		}

		/// <summary></summary>
		public DotNetCommentLinkedGroup(T link, List<DotNetComment> comments) : base(comments)
		{
			Link = link;
		}

		#endregion
		
	}
}
