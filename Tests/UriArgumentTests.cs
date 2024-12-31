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
    Assert.That(sut.Name, Is.EqualTo(NAME));
    Assert.That(sut.Description, Is.EqualTo(DESC));
    Assert.That(sut.DefaultValue, Is.Null );
    Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description"));
    Assert.That(sut.IsRequired, Is.False);
    Assert.That(sut.Syntax, Is.EqualTo("[/name <Uri>]"));
    Assert.That(sut.Type, Is.EqualTo("Uri"));
    Assert.That(sut.Value, Is.Null);

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

    Assert.That(sut.Value is Uri, Is.True);
    Assert.That(sut.Value, Is.EqualTo(new Uri(VALID_URI + "/")));
  }
}