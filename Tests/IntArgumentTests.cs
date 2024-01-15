using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class IntArgumentTests
{
	const string NAME = "name";
	const string DESCRIPTION = "description";

	[Test]
	public void Constructor_Defaults()
	{
		var sut = new IntArgument(NAME, DESCRIPTION);
		Assert.That(sut.Name, Is.EqualTo(NAME));
		Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
		Assert.IsNull(sut.DefaultValue);
		Assert.IsFalse(sut.IsRequired);
		Assert.That(sut.Type, Is.EqualTo(typeof(Int32).Name));
	}

	[Test]
	public void SetValue_Valid()
	{
		var sut = new IntArgument(NAME, DESCRIPTION);
		sut.SetValue("13");
		Assert.That(sut.Value, Is.EqualTo(13));
	}

	[Test]
	public void SetValue_Invalid()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			try
			{
				var sut = new IntArgument(NAME, DESCRIPTION);
				sut.SetValue("ab");
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 'name' is invalid"));
				Assert.That(ex.InnerException?.Message, Is.EqualTo("The input string 'ab' was not in a correct format."));
				throw;
			}
		});
	}
}
