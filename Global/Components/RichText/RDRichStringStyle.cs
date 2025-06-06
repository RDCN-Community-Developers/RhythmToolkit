﻿using System.Diagnostics.CodeAnalysis;
using static RhythmBase.RhythmDoctor.Extensions.Extensions;

namespace RhythmBase.Global.Components.RichText
{
	/// <summary>
	/// Represents a rich string style.
	/// </summary>
	public struct RDRichStringStyle : IRDRichStringStyle<RDRichStringStyle>
	{
		/// <summary>
		/// 获取或设置文本的颜色。
		/// </summary>
		public RDColor? Color { get; set; }
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
		static
#endif
		public bool HasPhrase => false;
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
		static
#endif
		public string GetXmlTag(RDRichStringStyle before, RDRichStringStyle after)
		{
			string tag = "";
			TryAddTag(ref tag, "color",
				before.Color?.TryGetName(out string[] namesbefore) == true
				? namesbefore[0].ToLower()
				: before.Color?.ToString(before.Color?.A == 255 ? "#RRGGBB" : "#RRGGBBAA"),
				after.Color?.TryGetName(out string[] namesafter) == true
				? namesafter[0].ToLower()
				: after.Color?.ToString(after.Color?.A == 255 ? "#RRGGBB" : "#RRGGBBAA"));
			return tag;
		}
		/// <inheritdoc/>
		public readonly bool Equals(RDRichStringStyle other) => this == other;
		/// <inheritdoc/>
#if NETSTANDARD
		public readonly override bool Equals(object? obj) => obj is RDRichStringStyle e && Equals(e);
#else
		public readonly override bool Equals([NotNullWhen(true)] object? obj) => obj is RDRichStringStyle e && Equals(e);
#endif
		/// <inheritdoc/>
		public bool ResetProperty(string name)
		{
			switch (name)
			{
				case "color":
					Color = null;
					break;
				default:
					return false;
			}
			return true;
		}
		/// <inheritdoc/>
		public bool SetProperty(string name, string value)
		{
			switch (name)
			{
				case "color":
					if (RDColor.TryFromName(value, out RDColor color))
						Color = color;
					else if (RDColor.TryFromRgba(value, out color))
						Color = color;
					else
						return false;
					break;
				default:
					return false;
			}
			return true;
		}
#if NETSTANDARD
		/// <inheritdoc/>
		public readonly string GetCloseTag(string name) => $"</{name}>";
		/// <inheritdoc/>
		public readonly string GetOpenTag(string name, string? arg = null) => arg is null ? $"<{name}>" : $"<{name}={arg}>";
#endif
		/// <inheritdoc/>
		public static bool operator ==(RDRichStringStyle left, RDRichStringStyle right) => left.Color == right.Color;
		/// <inheritdoc/>
		public static bool operator !=(RDRichStringStyle left, RDRichStringStyle right) => !(left == right);
		/// <inheritdoc/>
		public readonly override int GetHashCode() => Color.GetHashCode();
	}
}
