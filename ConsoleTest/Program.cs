using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//System.Diagnostics.Debugger.Launch();
			var arguments = new Arguments();
			if (!arguments.Parse(args)) return;

			Console.WriteLine(arguments.ToString());
		}
	}
}
