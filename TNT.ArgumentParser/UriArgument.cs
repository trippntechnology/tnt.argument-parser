namespace TNT.ArgumentParser;

/// <summary>
/// An <see cref="Argument"/> that represents a <see cref="Uri"/>
/// </summary>
public class UriArgument : Argument
{
  /// <summary>
  /// <see cref="Value"/> represented as a <see cref="Uri"/> type
  /// </summary>
  public new Uri? Value { get { return (Uri?)base.Value; } }

  /// <summary>
  /// Indicates the type of this <see cref="Argument"/>
  /// </summary>
  public override string Type => typeof(Uri).Name;

  /// <summary>
  /// Initializes a <see cref="UriArgument"/>
  /// </summary>
  /// <param name="name">Argument's name</param>
  /// <param name="description">Argument's description</param>
  /// <param name="required">Indicate if the argument is required. Default is false.</param>
  /// <param name="defaultValue">The default value to use if not specified. Default is null</param>
  public UriArgument(string name, string description, bool required = false, string? defaultValue = null)
: base(name, description, required, defaultValue)
  {
  }

  /// <summary>
  /// Transforms <paramref name="value"/> into a <see cref="Uri"/>
  /// </summary>
  /// <param name="value">Value that should represent a <see cref="Uri"/></param>
  /// <returns><see cref="Uri"/> created with <paramref name="value"/></returns>
  protected override object Transform(string? value) => new Uri(value!);
}
