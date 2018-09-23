using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
