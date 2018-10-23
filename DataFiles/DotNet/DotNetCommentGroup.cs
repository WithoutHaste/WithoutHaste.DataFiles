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
	/// <example><![CDATA[<summary>nested comments and/or plain text</summary>]]></example>
	/// <example><![CDATA[<remarks>nested comments and/or plain text</remarks>]]></example>
	/// <example><![CDATA[<example>nested comments and/or plain text</example>]]></example>
	/// <example><![CDATA[<para>nested comments and/or plain text</para>]]></example>
	/// <example><![CDATA[<returns>nested comments and/or plain text</returns>]]></example>
	/// <example><![CDATA[<value>nested comments and/or plain text</value>]]></example>
	public class DotNetCommentGroup : DotNetComment
	{
		/// <summary></summary>
		public List<DotNetComment> Comments = new List<DotNetComment>();

		/// <summary></summary>
		public bool IsEmpty {
			get {
				if(Comments.Count == 0)
					return true;
				if(Comments.All(c => c is DotNetCommentText && (c as DotNetCommentText).IsEmpty))
					return true;
				return false;
			}
		}

		/// <summary></summary>
		public int Count { get { return Comments.Count; } }

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
	}
}
