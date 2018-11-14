using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents anything that a class/struct/interface may contain.
	/// </summary>
	public abstract class DotNetMember
	{
		/// <summary></summary>
		public DotNetQualifiedName Name { get; protected set; }

		/// <summary>True when there's at least one comment on this member.</summary>
		public bool HasComments { get { return !HasNoComments; } }

		/// <summary>True when there are no comments on this member.</summary>
		public bool HasNoComments {
			get {
				if(!SummaryComments.IsEmpty) return false;
				if(!RemarksComments.IsEmpty) return false;
				if(PermissionComments.Count > 0) return false;
				if(ExampleComments.Count > 0) return false;
				if(ExceptionComments.Count > 0) return false;
				if(ParameterComments.Count > 0) return false;
				if(TypeParameterComments.Count > 0) return false;
				if(!ValueComments.IsEmpty) return false;
				if(!ReturnsComments.IsEmpty) return false;
				if(!FloatingComments.IsEmpty) return false;
				return true;
			}
		}

		internal bool InheritsDocumentation {
			get {
				return (!FloatingComments.IsEmpty && FloatingComments.Comments.OfType<DotNetCommentInherit>().Count() > 0);
			}
		}

		internal bool DuplicatesDocumentation {
			get {
				return (!FloatingComments.IsEmpty && FloatingComments.Comments.OfType<DotNetCommentDuplicate>().Count() > 0);
			}
		}

		internal DotNetQualifiedName DuplicatesFrom {
			get {
				DotNetCommentDuplicate comment = FloatingComments.Comments.OfType<DotNetCommentDuplicate>().FirstOrDefault();
				return comment?.Name;
			}
		}

		/// <summary>Comments from "summary" xml tags. Only expected as a top-level tag.</summary>
		/// <remarks>If there are multiple "summary" tags, their contents will be concatenated as if they were one tag.</remarks>
		public DotNetCommentGroup SummaryComments = new DotNetCommentGroup();

		/// <summary>Comments from "remarks" xml tags. Only expected as a top-level tag.</summary>
		/// <remarks>If there are multiple "remarks" tags, their contents will be concatenated as if they were one tag.</remarks>
		public DotNetCommentGroup RemarksComments = new DotNetCommentGroup();

		/// <summary>Comments from "permission" xml tags. Only expected as top-level tags.</summary>
		public List<DotNetCommentQualifiedLinkedGroup> PermissionComments = new List<DotNetCommentQualifiedLinkedGroup>();

		/// <summary>Comments from "example" xml tags.</summary>
		public List<DotNetComment> ExampleComments = new List<DotNetComment>();

		/// <summary>Comments from "exception" xml tags.  Only expected as top-level tags.</summary>
		public List<DotNetCommentQualifiedLinkedGroup> ExceptionComments = new List<DotNetCommentQualifiedLinkedGroup>();

		/// <summary>Comments from "param" xml tags. Only expected as top-level tags.</summary>
		public List<DotNetCommentParameter> ParameterComments = new List<DotNetCommentParameter>();

		/// <summary>Comments from "typeparam" xml tags. Only expected as top-level tags.</summary>
		public List<DotNetCommentParameter> TypeParameterComments = new List<DotNetCommentParameter>();

		/// <summary>Comments from "value" xml tags. Only expected as a top-level tag.</summary>
		/// <remarks>If there are multiple "value" tags, their contents will be concatenated as if they were one tag.</remarks>
		public DotNetCommentGroup ValueComments = new DotNetCommentGroup();

		/// <summary>Comments from "returns" xml tags. Only expected as a top-level tag.</summary>
		/// <remarks>If there are multiple "returns" tags, their contents will be concatenated as if they were one tag.</remarks>
		public DotNetCommentGroup ReturnsComments = new DotNetCommentGroup();

		/// <summary>Any comments not within expected top-level tags.</summary>
		public DotNetCommentGroup FloatingComments = new DotNetCommentGroup();

		/// <summary></summary>
		public DotNetMember(DotNetQualifiedName name)
		{
			Name = name;
		}

		/// <summary>
		/// Parse .Net XML documentation about this member.
		/// </summary>
		/// <param name="parent">Expects the tag containing all documentation for this member.</param>
		public void ParseVisualStudioXmlDocumentation(XElement parent)
		{
			parent = parent.CleanWhitespaces();

			//todo: should this start by clearing all the comment lists? so running it multiple times does not duplicate data?

			//todo: refactor: using DotNetComment.Tag, alot of this duplication can be removed

			bool previousCommentWasAParagraphTag = false;
			foreach(XNode node in parent.Nodes())
			{
				switch(node.NodeType)
				{
					case XmlNodeType.Element:
						XElement element = (node as XElement);
						DotNetComment comment = null;
						switch(element.Name.LocalName)
						{
							case "example":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								ExampleComments.Add(comment);
								previousCommentWasAParagraphTag = true;
								break;
							case "exception":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								ExceptionComments.Add(comment as DotNetCommentQualifiedLinkedGroup);
								previousCommentWasAParagraphTag = true;
								break;
							case "param":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								ParameterComments.Add(comment as DotNetCommentParameter);
								previousCommentWasAParagraphTag = true;
								break;
							case "permission":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								PermissionComments.Add(comment as DotNetCommentQualifiedLinkedGroup);
								previousCommentWasAParagraphTag = true;
								break;
							case "remarks":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								RemarksComments.Add(comment);
								previousCommentWasAParagraphTag = true;
								break;
							case "returns":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								ReturnsComments.Add(comment);
								previousCommentWasAParagraphTag = true;
								break;
							case "summary":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								SummaryComments.Add(comment);
								previousCommentWasAParagraphTag = true;
								break;
							case "typeparam":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								TypeParameterComments.Add(comment as DotNetCommentParameter);
								previousCommentWasAParagraphTag = true;
								break;
							case "value":
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								ValueComments.Add(comment);
								previousCommentWasAParagraphTag = true;
								break;
							default:
								comment = DotNetComment.FromVisualStudioXml(element);
								if(comment == null)
									break;
								if(previousCommentWasAParagraphTag && comment.ToString() == "\n")
									break;
								FloatingComments.Add(comment);
								previousCommentWasAParagraphTag = (comment.Tag == CommentTag.Para || comment.Tag == CommentTag.List || comment.Tag == CommentTag.Code);
								break;
						}
						break;
					case XmlNodeType.Text:
						comment = DotNetComment.FromVisualStudioXml(node.ToString());
						if(comment == null)
							break;
						if(previousCommentWasAParagraphTag && comment.ToString() == "\n")
							break;
						FloatingComments.Add(comment);
						previousCommentWasAParagraphTag = false;
						break;
				}
			}
		}

		/// <summary>
		/// For all "duplicate" comments, replace the comment with the duplicated comments.
		/// </summary>
		/// <param name="FindMember">Function that returns the selected member from all known members in the assembly.</param>
		public virtual void ResolveDuplicatedComments(Func<DotNetQualifiedName, DotNetMember> FindMember)
		{
			//todo: the duplicated member itself duplicates - watch out for loops
			if(!DuplicatesDocumentation)
				return;
			DotNetMember copyFrom = FindMember(DuplicatesFrom);
			CopyComments(copyFrom);
		}

		/// <summary>
		/// Shallow-copies all comments from the <paramref name="original"/> member to this member.
		/// </summary>
		public void CopyComments(DotNetMember original)
		{
			if(original == null)
				return;

			SummaryComments.Comments.Clear();
			SummaryComments.Comments.AddRange(original.SummaryComments.Comments);

			RemarksComments.Comments.Clear();
			RemarksComments.Comments.AddRange(original.RemarksComments.Comments);

			PermissionComments.Clear();
			PermissionComments.AddRange(original.PermissionComments);

			ExampleComments.Clear();
			ExampleComments.AddRange(original.ExampleComments);

			ExceptionComments.Clear();
			ExceptionComments.AddRange(original.ExceptionComments);

			ParameterComments.Clear();
			ParameterComments.AddRange(original.ParameterComments);

			TypeParameterComments.Clear();
			TypeParameterComments.AddRange(original.TypeParameterComments);

			ValueComments.Comments.Clear();
			ValueComments.Comments.AddRange(original.ValueComments.Comments);

			ReturnsComments.Comments.Clear();
			ReturnsComments.Comments.AddRange(original.ReturnsComments.Comments);

			FloatingComments.Comments.Clear();
			FloatingComments.Comments.AddRange(original.FloatingComments.Comments);
		}

		#region Low Level

		/// <summary>Full name of member.</summary>
		public override string ToString()
		{
			return Name.FullName;
		}

		/// <summary>
		/// Returns true if member name matches the link name.
		/// </summary>
		public bool Matches(DotNetCommentQualifiedLink link)
		{
			return (this.Name == link.Name);
		}

		/// <summary>
		/// Returns true if member name matches the link name.
		/// </summary>
		public bool Matches(DotNetCommentQualifiedLinkedGroup group)
		{
			return (this.Name == group.QualifiedLink.Name);
		}

		#endregion

	}
}
