using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Adofai.Components.Filters
{
	/// <summary>
	/// Defines a contract for a filter with an associated name.
	/// </summary>
	/// <remarks>The <see cref="IFilter"/> interface provides a mechanism to retrieve the name of a filter instance.
	/// Implementations may define specific behaviors or additional properties and methods.</remarks>
	public interface IFilter
	{
		/// <summary>
		/// Gets the name associated with the current instance.
		/// </summary>
#if NETSTANDARD2_0
		public abstract string Name { get; }
#else
		public static virtual string Name { get; } = "Unknown";
#endif
	}
}
