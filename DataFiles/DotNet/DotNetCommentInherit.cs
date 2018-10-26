using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a the <![CDATA[<inheritdoc />]]> tag, which means that documentation should be inherited for the base class, interface, struct, or member.
	/// </summary>
	/// <remarks>This is just a marker class. If the inheritance can be resolved, the inherited comments will automatically replace this comment.</remarks>
	public class DotNetCommentInherit : DotNetComment
	{
		/// <summary></summary>
		public DotNetCommentInherit()
		{
		}
	}
}
