using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that renames or modifies the name of a game window.
/// </summary>
public class RenameWindow : BaseWindowEvent
{
	/// <summary>
	/// The text to set, append, or use when renaming the window.
	/// </summary>
	public string Text { get; set; } = "";

	/// <summary>
	/// Describes how <see cref="Text"/> should be applied to the target window's name.
	/// </summary>
	public WindowNameAction Action { get; set; } = WindowNameAction.Set;

	/// <inheritdoc/>
	public override EventType Type => EventType.RenameWindow;
}

/// <summary>
/// Indicates how the window name should be modified by a <see cref="RenameWindow"/> event.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowNameAction
{
	/// <summary>
	/// Replace the current window name with the provided <see cref="RenameWindow.Text"/>.
	/// </summary>
	Set,

	/// <summary>
	/// Append the provided <see cref="RenameWindow.Text"/> to the existing window name.
	/// </summary>
	Append,

	/// <summary>
	/// Reset the window name to its default value. Any provided <see cref="RenameWindow.Text"/>
	/// is ignored when this action is used.
	/// </summary>
	Reset,
}