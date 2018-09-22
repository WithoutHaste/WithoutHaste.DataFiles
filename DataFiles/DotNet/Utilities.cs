using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	internal static class Utilities
	{
		internal static string RemoveFromEnd(this string text, string end)
		{
			if(text.EndsWith(end))
				return text.Substring(0, text.Length - end.Length);
			return text;
		}

		internal static string RemoveFromStart(this string text, string start)
		{
			if(text.StartsWith(start))
				return text.Substring(start.Length);
			return text;
		}
	}
}
