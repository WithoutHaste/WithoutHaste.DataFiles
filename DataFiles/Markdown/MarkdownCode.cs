using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents inline code or CDATA.
	/// </summary>
	public class MarkdownCode : IMarkdownInLine
	{
		/// <summary>
		/// Full text of code.
		/// </summary>
		public readonly string Text;

		/// <summary></summary>
		public MarkdownCode(string text)
		{
			Text = text;
		}

		/// <inheritdoc />
		public string ToMarkdownString(string previousText)
		{
			//Markdown does not allow escaping backtics.
			//Instead, surround the code with more backtics than occur contiguous in the code.
			//Also, insert spaces if text starts or ends with a backtic.

			string longestBacktics = GetLongestBacktics();

			string text = Text;
			if(text.StartsWith("`"))
				text = " " + text;
			if(text.EndsWith("`"))
				text = text + " ";

			return String.Format("{0}`{1}{0}`", longestBacktics, text);
		}

		/// <summary>
		/// Returns the longest substring in the code that is all backtics (`).
		/// </summary>
		private string GetLongestBacktics()
		{
			char c = '`';
			int length = 0;
			while(Text.Contains(new String(c, length + 1)))
				length++;
			return new String(c, length);
		}
	}
}
