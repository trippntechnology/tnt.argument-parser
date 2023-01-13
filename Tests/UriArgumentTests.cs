using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class UriArgumentTests
{
	private const string NAME = "name";
	private const string DESC = "description";
	private const string VALID_URI = "https://valid.domain.com";

	[Test]
	public void Constructor_Defaults()
	{
		var sut = new UriArgument(NAME, DESC);
		Assert.That(NAME, Is.EqualTo(sut.Name));
		Assert.That(DESC, Is.EqualTo(sut.Description));
		Assert.IsNull(sut.DefaultValue);
		Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description"));
		Assert.IsFalse(sut.IsRequired);
		Assert.That(sut.Syntax, Is.EqualTo("[/name <Uri>]"));
		Assert.That(sut.Type, Is.EqualTo("Uri"));
		Assert.IsNull(sut.Value);

	}

	[Test]
	public void Invalid_Uri()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			try
			{
				var sut = new UriArgument(NAME, DESC);
				sut.SetValue("foo");
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 'name' is invalid"));
				throw;
			}
		});
	}

	[Test]
	public void Valid_Uri()
	{
		var sut = new UriArgument(NAME, DESC);
		sut.SetValue(VALID_URI);

		Assert.IsTrue(sut.Value is Uri);
		Assert.That(sut.Value, Is.EqualTo(new Uri(VALID_URI + "/")));
	}
}