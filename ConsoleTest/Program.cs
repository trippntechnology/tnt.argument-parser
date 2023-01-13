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
