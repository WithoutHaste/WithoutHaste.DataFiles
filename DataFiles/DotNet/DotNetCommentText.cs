using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a plain text segment of comments.
	/// </summary>
	public class DotNetCommentText : DotNetComment
	{
		/// <summary></summary>
		public string Text { get; protected set; }

		/// <summary></summary>
		public DotNetCommentText(string text)
		{
			Text = text;
		}
	}
}
