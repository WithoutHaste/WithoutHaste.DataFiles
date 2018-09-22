using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a section of code in the comments.
	/// </summary>
	public class DotNetCommentCode : DotNetCommentText
	{
		/// <summary></summary>
		public DotNetCommentCode(string text) : base(text)
		{
		}
	}
}
