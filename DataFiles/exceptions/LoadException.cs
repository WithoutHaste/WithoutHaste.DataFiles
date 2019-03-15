using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles
{
	/// <summary>Error loading a file.</summary>
	public class LoadException : Exception
	{
		/// <summary></summary>
		public LoadException(string message) : base(message)
		{
		}
	}
}
