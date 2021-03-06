﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a plain text segment of comments.
	/// </summary>
	/// <remarks>
	/// Endline characters (\n) are preserved.
	/// 
	/// A multiline block of text will have leading white-space removed as a block.
	/// So if each line starts with two tabs, two tabs will be removed from the beginning of each line.
	/// </remarks>
	public class DotNetCommentText : DotNetComment
	{
		/// <summary></summary>
		public string Text { get; protected set; }

		/// <summary>True if text is null or empty string.</summary>
		public bool IsEmpty {
			get {
				return String.IsNullOrEmpty(Text);
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetCommentText(string text)
		{
			if(text == "") text = null;
			else Text = text.TrimFromStartAsBlock();
		}

		#endregion

		#region Low Level

		/// <summary>Returns full text.</summary>
		public override string ToString()
		{
			return Text;
		}

		#endregion
	}
}
