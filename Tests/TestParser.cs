using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class TestParser : ArgumentParser
{
	const string STRING_ARG = "s";
	const string STRING_DESC = "The string description";
	const string FLAG_ARG = "f";
	const string FLAG_DESC = "The flag description";
	const string INT_ARG = "i";
	const string INT_DESC = "The int description";

	public bool UsageShown { get; set; } = false;

	public TestParser() : base(true)
	{
		Add(new StringArgument(STRING_ARG, STRING_DESC));
		Add(new FlagArgument(FLAG_ARG, FLAG_DESC));
		Add(new IntArgument(INT_ARG, INT_DESC));
	}

	public new List<(string name, string value)> ToPairs(string[] args) => base.ToPairs(args);

	protected override string Usage()
	{
		UsageShown = true;
		return base.Usage();
	}
}
