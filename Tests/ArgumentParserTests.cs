using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class ArgumentParserTests
	{
		[TestMethod]
		public void Add_UniqueArguments()
		{
			var sut = new TestParser();

			sut.Add(new StringArgument("string", "string description"));
			sut.Add(new FlagArgument("flag", "flag description"));

			Assert.AreEqual(2, sut.Count());
		}
	}

	public class TestParser : ArgumentParser
	{
	}
}
