using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class PairTests
	{
		private const string Key = "key";
		private const string Value = "value";

		[TestMethod]
		public void Constructor_Single_Argument()
		{
			var sut = new Pair<string, string>(Key);
			Assert.AreEqual("key", sut.Key);
			Assert.IsNull(sut.Value);
		}

		[TestMethod]
		public void Constructor_Double_Argument()
		{
			var sut = new Pair<string, string>(Key, Value);
			Assert.AreEqual("key", sut.Key);
			Assert.AreEqual("value", sut.Value);
		}

		[TestMethod]
		public void Pair_ToString()
		{
			var sut = new Pair<string, string>(Key, Value);
			Assert.AreEqual("(key, value)", sut.ToString());
		}

		[TestMethod]
		public void ToTuple()
		{
			var sut = new Pair<string, string>(Key, Value);
			Assert.AreEqual((Key, Value), sut.ToTuple());
		}
	}
}
