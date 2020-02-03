using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// <see cref="Argument"/> that represents a version
	/// </summary>
	public class VersionArgument : Argument
	{
		/// <summary>
		/// Name of the underlying type
		/// </summary>
		public override string Type => typeof(Version).Name;

		/// <summary>
		/// Value of the argument
		/// </summary>
		public new Version Value => (Version)base.Value;

		/// <summary>
		/// Initializes an <see cref="Argument"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false). Note, if a <paramref name="defaultValue"/>
		/// is provided, the argument will not be required, i.e. set to false.</param>
		/// <param name="defaultValue">Indicates the default value of the argument (default = null)</param>
		public VersionArgument(string name, string description, bool required = false, Version defaultValue = null)
			: base(name, description, required, defaultValue) { }

		/// <summary>
		/// Transforms <paramref name="value"/> provided by command-line argument int a <see cref="Version"/>.
		/// </summary>
		/// <param name="value">Value to transform</param>
		/// <returns><see cref="Version"/> that represents <see cref="Value"/> if transformation is success</returns>
		/// <exception cref="ArgumentException">When <see cref="Value"/> can not be tranformed into a <see cref="Version"/></exception>
		protected override object Transform(string value) => Version.Parse(value);
	}
}
