using System;
using System.Collections.Generic;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Argument parser
	/// </summary>
	public abstract class ArgumentParser : List<IArgument>
	{
		/// <summary>
		/// Delimiter used to indicate an <see cref="Argument{T}.Name"/>
		/// </summary>
		public virtual string Delimiter => "/";

		/// <summary>
		/// Adds a new <see cref="Argument{T}"/>
		/// </summary>
		/// <typeparam name="T">Type of <see cref="Argument{T}"/></typeparam>
		/// <param name="argument"><see cref="Argument{T}"/> to add</param>
		/// <exception cref="ArgumentException">Argument already exists</exception>
		public virtual void Add<T>(Argument<T> argument)
		{
			// Check that this parameter has a unique Name
			var existingArgument = this.Find(a => a.Name.Equals(argument.Name, StringComparison.CurrentCultureIgnoreCase));

			if (existingArgument != null)
			{
				throw new ArgumentException(string.Format(Resources.ARGUMENT_ALREADY_EXISTS, argument.Name));
			}

			base.Add(argument);
		}
	}
}
