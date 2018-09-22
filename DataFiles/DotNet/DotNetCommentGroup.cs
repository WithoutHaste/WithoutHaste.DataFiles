using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents an ordered collection of comments.
	/// </summary>
	/// <remarks>Groups may include a link to something in the assembly which these comments are documenting.</remarks>
	public class DotNetCommentGroup : DotNetComment
	{
		/// <summary></summary>
		public List<DotNetComment> Comments = new List<DotNetComment>();

		/// <summary>Optional reference link from comments to something in the assembly.</summary>
		public IDotNetCommentLink Link { get; protected set; }

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetCommentGroup()
		{
		}

		/// <summary></summary>
		public DotNetCommentGroup(DotNetComment comment)
		{
			Comments.Add(comment);
		}

		/// <summary></summary>
		public DotNetCommentGroup(params DotNetComment[] comments)
		{
			Comments.AddRange(comments);
		}

		/// <summary></summary>
		public DotNetCommentGroup(List<DotNetComment> comments)
		{
			Comments.AddRange(comments);
		}

		#endregion

		/// <summary></summary>
		public void Add(DotNetComment comment)
		{
			Comments.Add(comment);
		}

		/// <summary></summary>
		public void Add(params DotNetComment[] comments)
		{
			Comments.AddRange(comments);
		}

		/// <summary></summary>
		public void Add(List<DotNetComment> comments)
		{
			Comments.AddRange(comments);
		}

		/// <summary></summary>
		public void SetLink(IDotNetCommentLink link)
		{
			Link = link;
		}
	}
}
