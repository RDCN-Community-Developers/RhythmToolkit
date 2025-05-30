﻿using RhythmBase.Adofai.Components;

namespace RhythmBase.Adofai.Extensions
{
	/// <summary>  
	/// Provides extension methods for the <see cref="ADLevel"/> class.  
	/// </summary>  
	public static class Extensions
	{
		/// <summary>  
		/// Adds a range of <see cref="Tile"/> objects to the specified <see cref="ADLevel"/>.  
		/// </summary>  
		/// <param name="level">The <see cref="ADLevel"/> to which the tiles will be added.</param>  
		/// <param name="tiles">The collection of <see cref="Tile"/> objects to add to the level.</param>  
		public static void AddRange(this ADLevel level, IEnumerable<Tile> tiles)
		{
			foreach (var item in tiles)
			{
				level.Insert(level.Count, item);
			}
		}
		/// <summary>  
		/// Inserts a range of <see cref="Tile"/> objects into the specified <see cref="ADLevel"/> at the given index.  
		/// </summary>  
		/// <param name="level">The <see cref="ADLevel"/> to which the tiles will be inserted.</param>  
		/// <param name="index">The zero-based index at which the tiles should be inserted.</param>  
		/// <param name="tiles">The collection of <see cref="Tile"/> objects to insert into the level.</param>  
		public static void InsertRange(this ADLevel level, int index, IEnumerable<Tile> tiles)
		{
			foreach (var item in tiles)
			{
				level.Insert(index, item);
				index++;
			}
		}
	}
}
