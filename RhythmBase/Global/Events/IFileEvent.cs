using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Events
{
	public interface IFileEvent : IEvent
	{
		public IEnumerable<FileReference> Files { get; }
	}
	public interface IAudioFileEvent : IFileEvent
	{
		public IEnumerable<FileReference> AudioFiles { get; }
	}
	public interface IImageFileEvent : IFileEvent
	{
		public IEnumerable<FileReference> ImageFiles { get; }
	}
}
