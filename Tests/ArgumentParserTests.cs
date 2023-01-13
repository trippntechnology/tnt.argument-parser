using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class ArgumentParserTests
{
	const string STRING_ARG = "s";
	const string STRING_DESC = "The string description";
	const string FLAG_ARG = "f";
	const string FLAG_DESC = "The flag description";
	const string INT_ARG = "i";
	const string INT_DESC = "The int description";

	[Test]
	public void Add_UniqueArguments()
	{
		var sut = new TestParser();
		Assert.That(sut.Count, Is.EqualTo(4));
	}

	[Test]
	public void Add_DuplicateArguments()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			try
			{
				var sut = new TestParser();
				sut.Add(new IntArgument(INT_ARG, INT_DESC));
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 'i' already exists"));
				throw;
			}
		});
	}

	[Test]
	public void ToPairs()
	{
		var args = new string[] { "/s", "https://AppleSauce.com", "/f", "/i", "57" };
		var sut = new TestParser();

		var result = sut.ToPairs(args);
		var expected = new List<(string name, string value)>() { ("s", "https://AppleSauce.com"), ("f", null), ("i", "57") };
		CollectionAssert.AreEqual(expected, result);
	}

	[Test]
	public void Parse_HappyPath()
	{
		var sut = new TestParser();
		var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57" };

		Assert.IsTrue(sut.Parse(args));
	}

	[Test]
	public void Parse_InvalidArgument()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			try
			{
				var sut = new TestParser();
				var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57", "/j" };
				sut.Parse(args, false);
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 'j' is invalid"));
				throw;
			}
		});
	}

	[Test]
	public void Parse_InvalidArgument_Swallow_Exception()
	{
		var sut = new TestParser();
		var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57", "/j" };
		Assert.IsFalse(sut.Parse(args));
	}

	[Test]
	public void Parse_ShowUsage()
	{
		var sut = new TestParser();
		var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57", "/h" };
		Assert.IsFalse(sut.UsageShown);
		sut.Parse(args);
		Assert.IsTrue(sut.UsageShown);
	}

	[Test]
	public void Parse_NullArgs()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			try
			{
				var sut = new TestParser();
				sut.Parse(null, false);
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'source')"));
				throw;
			}
		});
	}

	[Test]
	public void Parse_UnassignedRequiredArgs()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			try
			{
				var sut = new TestParser();
				sut["s"].IsRequired = true;
				var args = new string[] { "/f", "/i", "57" };
				sut.Parse(args, false);
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 's' is required"));
				throw;
			}
		});
	}
}