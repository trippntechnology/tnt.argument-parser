using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class UriArgumentTests
	{
		private const string NAME = "name";
		private const string DESC = "description";
		private const string VALID_URI = "https://valid.domain.com";

		[TestMethod]
		public void Constructor_Defaults()
		{
			var sut = new UriArgument(NAME, DESC);
			Assert.AreEqual(sut.Name, NAME);
			Assert.AreEqual(sut.Description, DESC);
			Assert.IsNull(sut.DefaultValue);
			Assert.AreEqual("  /name      description", sut.GetUsage());
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual("[/name <Uri>]", sut.Syntax);
			Assert.AreEqual("Uri", sut.Type);
			Assert.IsNull(sut.Value);

		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void Invalid_Uri()
		{
			try
			{
				var sut = new UriArgument(NAME, DESC);
				sut.SetValue("foo");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'name' is invalid", ex.Message);
				throw;
			}
		}

		[TestMethod]
		public void Valid_Uri()
		{
			var sut = new UriArgument(NAME, DESC);
			sut.SetValue(VALID_URI);

			Assert.IsTrue(sut.Value is Uri);
			Assert.AreEqual(new Uri(VALID_URI + "/"), sut.Value);
		}
	}
}