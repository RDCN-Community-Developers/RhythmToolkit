using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Events;

/// <summary>
/// Represents an event that carries one or more file references.
/// </summary>
/// <remarks>
/// Implementations of this interface provide access to a collection of <see cref="FileReference"/>
/// instances associated with the event. The referenced files may represent any file types
/// (audio, image, data, etc.). Consumers can enumerate the <see cref="Files"/> property to
/// inspect or resolve the referenced file paths.
/// </remarks>
public interface IFileEvent : IEvent
{
	/// <summary>
	/// Gets the collection of file references that are associated with this event.
	/// </summary>
	/// <value>
	/// An <see cref="IEnumerable{T}"/> of <see cref="FileReference"/> instances. The sequence
	/// may be empty but should never be <c>null</c>.
	/// </value>
	public IEnumerable<FileReference> Files { get; }
}

/// <summary>
/// Represents an event that specifically carries one or more audio file references.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IFileEvent"/> and exposes an explicit sequence of
/// audio file references via the <see cref="AudioFiles"/> property. Implementations should
/// ensure that <see cref="AudioFiles"/> refers to the subset of <see cref="IFileEvent.Files"/>
/// that are audio files (for example, by extension or metadata).
/// </remarks>
public interface IAudioFileEvent : IFileEvent
{
	/// <summary>
	/// Gets the collection of audio file references associated with this event.
	/// </summary>
	/// <value>
	/// An <see cref="IEnumerable{T}"/> of <see cref="FileReference"/> instances representing
	/// audio files. The sequence may be empty but should never be <c>null</c>.
	/// </value>
	public IEnumerable<FileReference> AudioFiles { get; }
}

/// <summary>
/// Represents an event that specifically carries one or more image file references.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IFileEvent"/> and exposes an explicit sequence of
/// image file references via the <see cref="ImageFiles"/> property. Implementations should
/// ensure that <see cref="ImageFiles"/> refers to the subset of <see cref="IFileEvent.Files"/>
/// that are image files (for example, by extension or metadata).
/// </remarks>
public interface IImageFileEvent : IFileEvent
{
	/// <summary>
	/// Gets the collection of image file references associated with this event.
	/// </summary>
	/// <value>
	/// An <see cref="IEnumerable{T}"/> of <see cref="FileReference"/> instances representing
	/// image files. The sequence may be empty but should never be <c>null</c>.
	/// </value>
	public IEnumerable<FileReference> ImageFiles { get; }
}
