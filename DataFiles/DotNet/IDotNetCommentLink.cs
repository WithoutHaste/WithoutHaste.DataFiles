using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents anything in the comments that links to something in the assembly.
	/// </summary>
	public interface IDotNetCommentLink
	{
		/// <summary>Return the fully qualified name of the referenced assembly element.</summary>
		string FullName { get; }
	}
}
