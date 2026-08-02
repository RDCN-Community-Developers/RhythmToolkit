using RhythmBase.Global.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct FontName
{
	[JsonEnumSerializable]
	public enum BuiltInFontType
	{
		Default,
		Pixel,
		Vector,
		Flash
	}
	/// <summary>
	/// Uses the default project font, typically optimized for general UI text.
	/// </summary>
	public static FontName Default => new(BuiltInFontType.Default);
	/// <summary>
	/// Renders text with pixel-perfect precision, ideal for retro aesthetics.
	/// </summary>
	public static FontName Pixel => new(BuiltInFontType.Pixel);
	/// <summary>
	/// Utilizes vector-based rendering to keep text crisp at any scale.
	/// </summary>
	public static FontName Vector => new(BuiltInFontType.Vector);
	/// <summary>
	/// Applies a Flash-inspired font style for legacy content compatibility.
	/// </summary>
	public static FontName Flash => new(BuiltInFontType.Flash);
	private readonly BuiltInFontType _type;
	private readonly FileReference? _fileReference;
	[MemberNotNullWhen(true, nameof(_fileReference))]
	public readonly bool IsCustom { get; }
	public readonly string Value =>IsCustom ? _fileReference : _type.ToEnumString();
	public FontName(string fontName)
	{
		if (EnumConverter.TryParse(fontName, out BuiltInFontType type))
		{
			_type = type;
			_fileReference = null;
			IsCustom = false;
		}
		else
		{
			_type = BuiltInFontType.Default;
			_fileReference = fontName;
			IsCustom = true;
		}
	}
	internal FontName(BuiltInFontType type)
	{
		_type = type;
		_fileReference = null;
		IsCustom = false;
	}
	public static implicit operator FontName(string fontName) => new(fontName);
	public override string ToString() => Value;
	private string GetDebuggerDisplay() => IsCustom ? _fileReference : $"*{_type}";
}
