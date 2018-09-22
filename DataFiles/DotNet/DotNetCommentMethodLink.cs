using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a link in the comments to an internal or extenal method.
	/// </summary>
	public class DotNetCommentMethodLink : DotNetCommentLink
	{
		/// <summary></summary>
		public List<DotNetParameter> Parameters = new List<DotNetParameter>();

		#region Constructors

		/// <summary></summary>
		public DotNetCommentMethodLink(DotNetQualifiedName name, List<DotNetParameter> parameters) : base(name)
		{
			Parameters.AddRange(parameters);
		}

		#endregion
	}
}
