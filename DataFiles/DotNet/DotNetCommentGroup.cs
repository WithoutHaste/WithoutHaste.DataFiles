using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

		/// <summary>The number of comments in the group. Does not count nested comments.</summary>
		public int Count { get { return Comments.Count; } }

		/// <summary>Access comments by zero-based index.</summary>
		public DotNetComment this[int index] {
			get {
				return Comments[index];
			}
		}

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetCommentGroup()
		{
		}

		/// <summary></summary>
		public DotNetCommentGroup(CommentTag tag)
		{
			Tag = tag;
		}

		/// <summary></summary>
		public DotNetCommentGroup(DotNetComment comment)
		{
			Comments.Add(comment);
		}

		/// <summary></summary>
		public DotNetCommentGroup(CommentTag tag, DotNetComment comment)
		{
			Tag = tag;
			Comments.Add(comment);
		}

		/// <summary></summary>
		public DotNetCommentGroup(params DotNetComment[] comments)
		{
			Comments.AddRange(comments);
		}

		/// <summary></summary>
		public DotNetCommentGroup(CommentTag tag, params DotNetComment[] comments)
		{
			Tag = tag;
			Comments.AddRange(comments);
		}

		/// <summary></summary>
		public DotNetCommentGroup(List<DotNetComment> comments)
		{
			Comments.AddRange(comments);
		}

		/// <summary></summary>
		public DotNetCommentGroup(CommentTag tag, List<DotNetComment> comments)
		{
			Tag = tag;
			Comments.AddRange(comments);
		}

		/// <summary>Parses .Net XML documentation for any "grouping" tag.</summary>
		public new static DotNetCommentGroup FromVisualStudioXml(XElement element)
		{
			return new DotNetCommentGroup(DotNetComment.GetTag(element), DotNetComment.ParseSection(element));
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
