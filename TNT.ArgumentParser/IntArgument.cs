using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// An <see cref="Argument"/> that represents an <see cref="Int32"/>
	/// </summary>
	public class IntArgument : Argument
	{
		/// <summary>
		/// The name of the argument type
		/// </summary>
		public override string Type => typeof(Int32).Name;

		/// <summary>
		/// The <see cref="Int32"/> associated with this <see cref="Argument"/>
		/// </summary>
		public new int Value => (int)base.Value;

		/// <summary>
		/// Initializes an <see cref="Argument"/> that parses a string the represents a
		/// <see cref="Int32"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false). Note, if a <paramref name="defaultValue"/>
		/// is provided, the argument will not be required, i.e. set to false.</param>
		/// <param name="defaultValue">Indicates the default value of the argument (default = null)</param>
		public IntArgument(string name, string description, bool required = false, int? defaultValue = null)
			: base(name, description, required, defaultValue)
		{
		}

		/// <summary>
		/// Transforms the value represented by <paramref name="value"/> to an <see cref="Int32"/>
		/// </summary>
		/// <param name="value"><see cref="string"/> that should represent an <see cref="int"/></param>
		/// <returns>An <see cref="Object"/> representing an <see cref="int"/></returns>
		/// <exception cref="FormatException">Thrown by <see cref="Convert.ToInt32(string)"/></exception>
		/// <exception cref="OverflowException">Thrown by <see cref="Convert.ToInt32(string)"/></exception>
		protected override object Transform(string value) => Convert.ToInt32(value);
	}
}
