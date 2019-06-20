using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// An <see cref="Argument"/> that represents an enumeration of <typeparamref name="T"/>
	/// </summary>
	/// <typeparam name="T">Enumerated type</typeparam>
	public class EnumArgument<T> : Argument
	{
		/// <summary>
		/// Gets the type of the <see cref="Argument"/>
		/// </summary>
		public override string Type => typeof(T).Name;

		/// <summary>
		/// Casts the <see cref="Argument.Value"/> to <typeparamref name="T"/>
		/// </summary>
		public new T Value => (T)base.Value;

		/// <summary>
		/// Initializes the <see cref="EnumArgument{T}"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false). Note, if a <paramref name="defaultValue"/>
		/// is provided, the argument will not be required, i.e. set to false.</param>
		/// <param name="defaultValue">Indicates the default value of the argument (default = null)</param>
		public EnumArgument(string name, string description, bool required = false)
			: base(name, description, required, null)
		{
		}

		/// <summary>
		/// Initializes the <see cref="EnumArgument{T}"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false). Note, if a <paramref name="defaultValue"/>
		/// is provided, the argument will not be required, i.e. set to false.</param>
		/// <param name="defaultValue">Indicates the default value of the argument (default = null)</param>
		public EnumArgument(string name, string description, T defaultValue)
			: base(name, description, false, defaultValue)
		{
		}

		/// <summary>
		/// Transforms <paramref name="value"/> into an enumerated value of <typeparamref name="T"/>
		/// </summary>
		/// <param name="value"><see cref="string"/> to transform</param>
		/// <returns>Enumerated value of <typeparamref name="T"/></returns>
		/// <exception cref="ArgumentException">Thrown if unable to transform <paramref name="value"/> to an 
		/// enumerated value of <typeparamref name="T"/></exception>
		protected override object Transform(string value)
		{
			try
			{
				T t = (T)Enum.Parse(typeof(T), value.ToString());
				return t;
			}
			catch
			{
				throw new ArgumentException(string.Format(Resources.INVALID_ARGUMENT_NAME, this.Name));
			}
		}
	}
}
