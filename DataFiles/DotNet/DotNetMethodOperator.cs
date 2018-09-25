using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method that is an operator.
	/// </summary>
	public class DotNetMethodOperator : DotNetMethod
	{
		/// <summary></summary>
		public DotNetMethodOperator(DotNetQualifiedName name, List<DotNetBaseParameter> parameters) : base(name, parameters)
		{
		}
	}
}
