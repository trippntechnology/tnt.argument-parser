using System;
using System.Collections.Generic;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Argument parser
	/// </summary>
	public abstract class ArgumentParser : List<Argument>
	{
		/// <summary>
		/// Delimiter used to indicate an <see cref="Argument.Name"/>
		/// </summary>
		public virtual char Delimiter => '/';

		/// <summary>
		/// Adds a new <see cref="Argument"/>
		/// </summary>
		/// <typeparam name="T">Type of <see cref="Argument"/></typeparam>
		/// <param name="argument"><see cref="Argument"/> to add</param>
		/// <exception cref="ArgumentException">Argument already exists</exception>
		public virtual void Add<T>(Argument argument)
		{
			// Check that this parameter has a unique Name
			var existingArgument = this.Find(argument.Name);

			if (existingArgument != null)
			{
				throw new ArgumentException(string.Format(Resources.ARGUMENT_ALREADY_EXISTS, argument.Name));
			}

			base.Add(argument);
		}

		public virtual bool Parse(string[] args)
		{
			var pairs = ToPairs(args);

			pairs.ForEach(p =>
			{
				// Find argument
				var argument = this.Find(p.name);

				if (argument == null) throw new ArgumentException();
				argument.SetValue(p.value);
			});

			return true;
		}

		private List<(string name, string value)> ToPairs(string[] args)
		{
			var joinedArgs = string.Join(" ", args);
			string[] splitArgs = joinedArgs.Split(new char[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries);
			var pairs = new List<(string name, string value)>();

			foreach (var arg in splitArgs)
			{
				var keyValue = arg.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
				var key = keyValue[0].Trim();
				var value = keyValue.Length == 2 ? keyValue[1].Trim() : null;

				pairs.Add((key, value));
			}

			return pairs;
		}

		private Argument Find(string name) => this.Find(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
	}
}
