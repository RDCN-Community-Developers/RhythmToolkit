using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest
{
	public class Timer : IDisposable
	{
		private TimeSpan time = DateTime.Now.TimeOfDay;
		private readonly string? name;
		public Timer() { }
		public Timer(string processName)
		{
			name = processName;
		}
		public void Dispose() => TickTime();
		public void TickTime()
		{
			TimeSpan t = DateTime.Now.TimeOfDay;
			Console.WriteLine($"{name ?? "It"} takes {(t - time).TotalMilliseconds}ms");
			time = t;
		}
	}

	public static class Extensions
	{
		public static void RemoveBookmarks(this RDLevel e) =>
			e.Bookmarks.Clear();
		public static void RemoveComments(this RDLevel e) =>
			e.RemoveRange(new List<IBaseEvent>(e.Where<Comment>()));
		public static void RenameTags(this RDLevel e, string oldName, string newName)
		{
			foreach (var item in e.Where(i => i.Tag == oldName))
				item.Tag = newName;
			foreach (var item in e.Where<TagAction>(i => i.ActionTag == oldName))
				item.Tag = newName;
		}
		public static void RenameConditionals(this RDLevel e, string oldName, string newName)
		{
			foreach (var item in e.Conditionals.Where(i => i.Name == oldName))
				item.Name = newName;
		}
		public static void MoveY(this RDLevel e, int height)
		{
			foreach (var item in e)
				item.Y += height;
		}
	}
}
