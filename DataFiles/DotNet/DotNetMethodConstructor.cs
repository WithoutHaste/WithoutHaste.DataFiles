using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method that is a constructor.
	/// </summary>
	public class DotNetMethodConstructor : DotNetMethod
	{
		/// <summary></summary>
		public DotNetMethodConstructor(DotNetQualifiedName name, List<DotNetBaseParameter> parameters) : base(name, parameters)
		{
		}
	}
}
