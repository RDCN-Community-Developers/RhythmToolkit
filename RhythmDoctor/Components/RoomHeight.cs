﻿using RhythmBase.RhythmDoctor.Converters;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents the height configuration for a room.
	/// </summary>
	[JsonConverter(typeof(RoomHeightConverter))]
	public struct RoomHeight()
	{
		private int[] _height = new int[4];
		/// <summary>
		/// Gets or sets the height configuration at the specified index.
		/// </summary>
		/// <param name="index">The index of the height configuration (0 to 3).</param>
		/// <returns>The height configuration at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range (not between 0 and 3).</exception>
		public int this[int index]
		{
			get => _height[index is >= 0 and < 4 ? index : throw new IndexOutOfRangeException(nameof(index))];
			set => _height[index is >= 0 and < 4 ? index : throw new IndexOutOfRangeException(nameof(index))] = value;
		}
		/// <summary>
		/// Gets an array of height values for the room.
		/// </summary>
		/// <returns>An array of integers representing the height values.</returns>
		public readonly int[] Heights => (int[])_height.Clone();

		/// <summary>
		/// Normalizes the height values so that their sum equals 100.
		/// </summary>
		/// <returns>A new <see cref="RoomHeight"/> instance with normalized height values.</returns>
		public readonly RoomHeight Normalized(RDRoom room)
		{
			float sum = 0;
			int empty = 0;
			for (byte i = 0; i < _height.Length; i++)
			{
				sum += room[i] ? _height[i] : 0;
				if (!room[i])
					empty++;
			}
			if (sum < 100)
			{
				float h = (100 - sum) / empty;
				for (byte i = 0; i < _height.Length; i++)
				{
					_height[i] = room[i] ? _height[i] : (int)h;
				}
			}
			else
			{
				for (byte i = 0; i < _height.Length; i++)
				{
					_height[i] = room[i] ? (int)(_height[i] / sum * 100f) : 0;
				}
			}
			return this;
		}

		/// <summary>
		/// Gets an array of height percentages for the room.
		/// </summary>
		/// <returns>An array of floats representing the height percentages.</returns>
		public readonly float[] GetHeightPercents(RDRoom room)
		{
			float[] result = new float[4];
			float sum = 0;
			int empty = 0;
			for (byte i = 0; i < _height.Length; i++)
			{
				sum += room[i] ? _height[i] : 0;
				if (!room[i])
					empty++;
			}
			if (sum < 100)
			{
				float h = (100 - sum) / empty;
				for (byte i = 0; i < _height.Length; i++)
				{
					result[i] = room[i] ? _height[i] / 100f : h / 100f;
				}
			}
			else
			{
				for (byte i = 0; i < _height.Length; i++)
				{
					result[i] = room[i] ? _height[i] / sum : 0;
				}
			}
			return result;
		}
	}
}