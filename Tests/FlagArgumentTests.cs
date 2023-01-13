using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class FlagArgumentTests
{
	const string NAME = "name";
	const string DESCRIPTION = "description";

	[Test]
	public void Constructor()
	{
		var sut = new FlagArgument(NAME, DESCRIPTION);
		Assert.That(sut.Name, Is.EqualTo(NAME));
		Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
		Assert.IsTrue(sut.Value == false);
	}

	[Test]
	public void SetValue()
	{
		var sut = new FlagArgument(NAME, DESCRIPTION);
		Assert.IsTrue(sut.Value == false);
		sut.SetValue();
		Assert.IsTrue(sut.Value == true);
	}

	[Test]
	public void SetValue_AlreadyExists()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			try
			{
				var sut = new FlagArgument(NAME, DESCRIPTION);
				sut.SetValue();
				sut.SetValue();
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument was already provided"));
				throw;
			}
		});
	}

	[Test]
	public void Syntax()
	{
		var sut = new FlagArgument(NAME, DESCRIPTION);
		Assert.That(sut.Syntax, Is.EqualTo("[/name]"));
	}

	[Test]
	public void Type()
	{
		var sut = new FlagArgument(NAME, DESCRIPTION);
		Assert.That(sut.Type, Is.EqualTo(typeof(bool).Name));
	}
}
