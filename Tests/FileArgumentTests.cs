using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class FileArgumentTests
{
  const string NAME = "f";
  const string DESCRIPTION = "description";
  const string VALID_FILE_NAME = "file1.txt";
  const string INVALID_FILE_NAME = "bogus.txt";

  [Test]
  public void Constructor()
  {
    var sut = new FileArgument(NAME, DESCRIPTION);
    Assert.That(sut.MustExist, Is.False);
  }

  [Test]
  public void Constructor_MustExist()
  {
    var sut = new FileArgument(NAME, DESCRIPTION, mustExist: true);
    Assert.That(sut.MustExist, Is.True);
  }

  [Test]
  public void Syntax()
  {
    var sut = new FileArgument(NAME, DESCRIPTION, true);
    Assert.That(sut.Syntax, Is.EqualTo("/f <File>"));
  }

  [Test]
  public void Setvalue()
  {
    var sut = new FileArgument(NAME, DESCRIPTION);
    sut.SetValue(VALID_FILE_NAME);
    Assert.That(sut.Value, Is.EqualTo(VALID_FILE_NAME));
  }

  [Test]
  public void Setvalue_InvalidFileName()
  {
    Assert.Throws<ArgumentException>(() =>
    {
      try
      {
        var sut = new FileArgument(NAME, DESCRIPTION, mustExist: true);
        sut.SetValue(INVALID_FILE_NAME);
      }
      catch (Exception ex)
      {
        Assert.That(ex.Message, Is.EqualTo("Argument 'f' is invalid"));
        throw;
      }
    });
  }
}
