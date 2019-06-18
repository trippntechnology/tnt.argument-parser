using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class FlagArgumentTests
	{
		const string NAME = "name";
		const string DESCRIPTION = "description";

		[TestMethod]
		public void Constructor()
		{
			var sut = new FlagArgument(NAME, DESCRIPTION);
			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual(DESCRIPTION, sut.Description);
			Assert.IsTrue(sut.Value == false);
		}

		[TestMethod]
		public void SetValue()
		{
			var sut = new FlagArgument(NAME, DESCRIPTION);
			Assert.IsTrue(sut.Value == false);
			sut.SetValue();
			Assert.IsTrue(sut.Value == true);
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void SetValue_AlreadyExists()
		{
			try
			{
				var sut = new FlagArgument(NAME, DESCRIPTION);
				sut.SetValue();
				sut.SetValue();
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument was already provided", ex.Message);
				throw;
			}
		}
	}
}
