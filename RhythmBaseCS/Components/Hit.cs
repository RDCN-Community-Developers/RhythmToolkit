using System;
using RhythmBase.Events;

namespace RhythmBase.Components
{
	/// <summary>
	/// The moment the beat is hit.
	/// </summary>

	public struct Hit
	{
		/// <summary>
		/// The moment of pressing.
		/// </summary>

		public Beat Beat { get; }

		/// <summary>
		/// The length of time player held it.
		/// </summary>

		public float Hold { get; }

		/// <summary>
		/// The source event for this hit.
		/// </summary>

		public BaseBeat Parent { get; }

		/// <summary>
		/// Indicates whether this hit needs to be held down continuously.
		/// </summary>

		public bool Holdable
		{
			get
			{
				return Hold > 0f;
			}
		}

		/// <summary>
		/// Construct a hit.
		/// </summary>
		/// <param name="parent">The source event for this hit.</param>
		/// <param name="beat">The moment of pressing.</param>
		/// <param name="hold">The source event for this hit.</param>

		public Hit(BaseBeat parent, Beat beat, float hold = 0f)
		{
			this = default;
			this.Parent = parent;
			this.Beat = beat;
			this.Hold = hold;
		}


		public override string ToString() => string.Format("{{{0}, {1}}}", Beat, Parent);
	}
}
