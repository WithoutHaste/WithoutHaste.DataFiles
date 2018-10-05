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
	/// <remarks>Text is allowed to have a single space at beginning and end. Anymore leading/trailing whitespace will be removed.</remarks>
	public class DotNetCommentText : DotNetComment
	{
		/// <summary></summary>
		public string Text { get; protected set; }

		/// <summary></summary>
		public DotNetCommentText(string text)
		{
			if(text == "") text = null;
			else Text = text.TrimAllowOneSpace();
		}
	}
}
