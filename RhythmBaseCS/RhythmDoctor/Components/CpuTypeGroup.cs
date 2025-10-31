using RhythmBase.RhythmDoctor.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents a fixed-size group of CPU types, accessible by index.
	/// </summary>
	/// <remarks>
	/// This structure provides indexed access to a collection of CPU types, where the valid index range is 0 to 15.
	/// Attempting to access an index outside this range will result in an <see cref="IndexOutOfRangeException"/>.
	/// </remarks>
	public readonly struct CpuTypeGroup()
	{
		/// <summary>
		/// Gets or sets the <see cref="CpuType"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the CPU type to get or set. Valid range is 0 to 15.</param>
		/// <returns>The <see cref="CpuType"/> at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException">
		/// Thrown when the index is less than 0 or greater than 15.
		/// </exception>
		public readonly CpuType this[int index]
		{
			get => _cpuTypes[index];
			set => _cpuTypes[index] = value;
		}

		/// <summary>
		/// The internal array storing CPU types.
		/// </summary>
		private readonly CpuType[] _cpuTypes = new CpuType[16];
	}
}
