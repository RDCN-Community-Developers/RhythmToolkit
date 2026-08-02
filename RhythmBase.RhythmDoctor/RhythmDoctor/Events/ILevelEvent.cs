using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	public interface IChartFileEvent : IFileEvent
	{
		/// <summary>
		/// Gets the collection of chart file references associated with this event.
		/// </summary>
		/// <value>
		/// An <see cref="IEnumerable{T}"/> of <see cref="FileReference"/> instances representing
		/// cahrt files. The sequence may be empty but should never be <c>null</c>.
		/// </value>
		public IEnumerable<FileReference> ChartFiles { get; }
	}
	public interface IFontFileEvent : IFileEvent
	{
		/// <summary>
		/// Gets the collection of font file references associated with this event.
		/// </summary>
		/// <value>
		/// An <see cref="IEnumerable{T}"/> of <see cref="FileReference"/> instances representing
		/// font files. The sequence may be empty but should never be <c>null</c>.
		/// </value>
		public IEnumerable<FileReference> FontFiles { get; }
	}
}
