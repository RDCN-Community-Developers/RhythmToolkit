namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents the height configuration for a room.
	/// </summary>
	public class RoomHeight()
	{
		/// <summary>
		/// Represents a single height configuration.
		/// </summary>
		public struct Height
		{
			/// <summary>
			/// Gets or sets a value indicating whether the height is set to auto (value is 0).
			/// </summary>
			public bool IsAuto
			{
				readonly get => Value == 0;
				set => Value = 0;
			}

			/// <summary>
			/// Gets or sets a value indicating whether the height is enabled.
			/// </summary>
			public bool IsEnabled { get; set; }

			/// <summary>
			/// Gets or sets the value of the height.
			/// </summary>
			public int Value { get; set; }
		}
		private Height[] _height = new Height[4];
		/// <summary>
		/// Gets or sets the height configuration at the specified index.
		/// </summary>
		/// <param name="index">The index of the height configuration (0 to 3).</param>
		/// <returns>The height configuration at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range (not between 0 and 3).</exception>
		public Height this[int index]
		{
			get => _height[index is >= 0 and < 4 ? index : throw new IndexOutOfRangeException(nameof(index))];
			set => _height[index is >= 0 and < 4 ? index : throw new IndexOutOfRangeException(nameof(index))] = value;
		}

		/// <summary>
		/// Gets or sets the room configuration based on the enabled heights.
		/// </summary>
		public RDRoom Room
		{
			get
			{
				RDRoom room = new(false);
				for (int i = 0; i < _height.Length; i++)
				{
					if (_height[i].IsEnabled)
						room[(byte)i] = true;
				}
				return room;
			}
			set
			{
				for (int i = 0; i < _height.Length; i++)
				{
					_height[i].IsEnabled = value[(byte)i];
				}
			}
		}

		/// <summary>
		/// Gets an array of height values for the room.
		/// </summary>
		/// <returns>An array of integers representing the height values.</returns>
		public int[] Heights
		{
			get
			{
				int[] h = new int[4];
				for (int i = 0; i < _height.Length; i++)
				{
					h[i] = _height[i].IsEnabled ? _height[i].Value : 0;
				}
				return h;
			}
		}

		/// <summary>
		/// Normalizes the height values so that their sum equals 100.
		/// </summary>
		/// <returns>A new <see cref="RoomHeight"/> instance with normalized height values.</returns>
		public RoomHeight Normalized()
		{
			float sum = 0;
			int empty = 0;
			for (int i = 0; i < _height.Length; i++)
			{
				sum += _height[i].IsEnabled ? _height[i].Value : 0;
				if (!_height[i].IsEnabled)
					empty++;
			}
			if (sum < 100)
			{
				float h = (100 - sum) / empty;
				for (int i = 0; i < _height.Length; i++)
				{
					_height[i].Value = _height[i].IsEnabled ? _height[i].Value : (int)h;
				}
			}
			else
			{
				for (int i = 0; i < _height.Length; i++)
				{
					_height[i].Value = _height[i].IsEnabled ? (int)(_height[i].Value / sum * 100f) : 0;
				}
			}
			return this;
		}

		/// <summary>
		/// Gets an array of height percentages for the room.
		/// </summary>
		/// <returns>An array of floats representing the height percentages.</returns>
		public float[] HeightPercents
		{
			get
			{
				float[] result = new float[4];
				float sum = 0;
				int empty = 0;
				for (int i = 0; i < _height.Length; i++)
				{
					sum += _height[i].IsEnabled ? _height[i].Value : 0;
					if (!_height[i].IsEnabled)
						empty++;
				}
				if (sum < 100)
				{
					float h = (100 - sum) / empty;
					for (int i = 0; i < _height.Length; i++)
					{
						result[i] = _height[i].IsEnabled ? _height[i].Value / 100f : h / 100f;
					}
				}
				else
				{
					for (int i = 0; i < _height.Length; i++)
					{
						result[i] = _height[i].IsEnabled ? _height[i].Value / sum : 0;
					}
				}
				return result;
			}
		}
	}
}