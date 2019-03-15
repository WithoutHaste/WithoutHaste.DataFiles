using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaterFrameworkTest
{
	public class Program
	{
		public Program()
		{
			//using System.Linq which is not available in .Net 2.0
			List<int> x = new List<int>() { 1, 2, 3, 4 };
			List<int> y = x.Where(num => num < 3).ToList();

			//using C# 6 null conditional
			int? z = (new AnObject())?.FieldA;
		}
	}

	public class AnObject
	{
		public int? FieldA;
	}
}
