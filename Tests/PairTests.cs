using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class PairTests
{
	private const string Key = "key";
	private const string Value = "value";

	[Test]
	public void Constructor_Single_Argument()
	{
		var sut = new Pair<string, string>(Key);
		Assert.That(sut.Key, Is.EqualTo("key"));
		Assert.That(sut.Value, Is.Null);
	}

	[Test]
	public void Constructor_Double_Argument()
	{
		var sut = new Pair<string, string>(Key, Value);
		Assert.That(sut.Key, Is.EqualTo("key"));
		Assert.That(sut.Value, Is.EqualTo("value"));
	}

	[Test]
	public void Pair_ToString()
	{
		var sut = new Pair<string, string>(Key, Value);
		Assert.That(sut.ToString(), Is.EqualTo("(key, value)"));
	}

	[Test]
	public void ToTuple()
	{
		var sut = new Pair<string, string>(Key, Value);
		Assert.That(sut.ToTuple(), Is.EqualTo((Key, Value)));
	}
}
