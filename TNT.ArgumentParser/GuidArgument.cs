namespace TNT.ArgumentParser;

/// <summary>
/// An <see cref="Argument"/> that represents a <see cref="Guid"/>
/// </summary>
public class GuidArgument : Argument
{
  /// <summary>
  /// Value of the argument
  /// </summary>
  public new Guid? Value => (Guid?)base.Value;

  /// <summary>
  /// Indicates the type of this <see cref="Argument"/>
  /// </summary>
  public override string Type => typeof(Guid).Name;

  /// <summary>
  /// Initializes a <see cref="UriArgument"/>
  /// </summary>
  /// <param name="name">Argument's name</param>
  /// <param name="description">Argument's description</param>
  /// <param name="required">Indicate if the argument is required. Default is false.</param>
  /// <param name="defaultValue">The default value to use if not specified. Default is null</param>
  public GuidArgument(string name, string description, bool required = false, Guid? defaultValue = null) :
    base(name, description, required, defaultValue)
  {
  }

  /// <summary>
  /// Transforms <paramref name="value"/> into a <see cref="Guid"/>
  /// </summary>
  /// <param name="value">Value that should represent a <see cref="Guid"/></param>
  /// <returns><see cref="Guid"/> created with <paramref name="value"/></returns>
  protected override object Transform(string? value) => Guid.Parse(value!);
}
