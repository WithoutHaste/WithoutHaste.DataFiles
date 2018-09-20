using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	internal class Utilities
	{
		/// <summary>
		/// Ensure that string is null or ends with two end line characters.
		/// Does not add unneccessary characters.
		/// </summary>
		public static string EnsureTwoEndlines(string text)
		{
			if(text == null) return text;
			if(text.EndsWith("\n\n")) return text;
			if(text.EndsWith("\n")) return text + "\n";
			return text + "\n\n";
		}
	}
}
