using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Components.RichText
{
	/// <summary>
	/// Represents the style of a rich string.
	/// </summary>
	public struct RDDialoguePhraseStyle : IRDRichStringStyle<RDDialoguePhraseStyle>
	{
		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		public RDColor? Color { get; set; }
		/// <summary>
		/// Gets or sets the speed of the text animation.
		/// </summary>
		public float? Speed { get; set; }
		/// <summary>
		/// Gets or sets the volume of the text.
		/// </summary>
		public float? Volume { get; set; }
		/// <summary>
		/// Gets or sets the pitch of the text.
		/// </summary>
		public float? Pitch { get; set; }
		/// <summary>
		/// Gets or sets the pitch range of the text.
		/// </summary>
		public float? PitchRange { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should shake.
		/// </summary>
		public bool Shake { get; set; }
		/// <summary>
		/// Gets or sets the radius of the shake effect.
		/// </summary>
		public float? ShakeRadius { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should have a wave effect.
		/// </summary>
		public bool Wave { get; set; }
		/// <summary>
		/// Gets or sets the height of the wave effect.
		/// </summary>
		public float? WaveHeight { get; set; }
		/// <summary>
		/// Gets or sets the speed of the wave effect.
		/// </summary>
		public float? WaveSpeed { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should have a swirl effect.
		/// </summary>
		public bool Swirl { get; set; }
		/// <summary>
		/// Gets or sets the radius of the swirl effect.
		/// </summary>
		public float? SwirlRadius { get; set; }
		/// <summary>
		/// Gets or sets the speed of the swirl effect.
		/// </summary>
		public float? SwirlSpeed { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should be sticky.
		/// </summary>
		public bool Sticky { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should be loud.
		/// </summary>
		public bool Loud { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should be bold.
		/// </summary>
		public bool Bold { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the text should be whispered.
		/// </summary>
		public bool Whisper { get; set; }
		/// <inheritdoc/>
		public static bool HasPhrase => true;
		/// <summary>
		/// Sets the property of the rich string style based on the provided name and value.
		/// </summary>
		/// <param name="name">The name of the property to set.</param>
		/// <param name="value">The value to set for the property.</param>
		/// <returns>True if the property was successfully set; otherwise, false.</returns>
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
				case "speed":
					Speed = float.Parse(value);
					break;
				case "volume":
					Volume = float.Parse(value);
					break;
				case "pitch":
					Pitch = float.Parse(value);
					break;
				case "pitchRange":
					PitchRange = float.Parse(value);
					break;
				case "shake":
					Shake = bool.Parse(value);
					break;
				case "shakeRadius":
					ShakeRadius = float.Parse(value);
					break;
				case "wave":
					Wave = bool.Parse(value);
					break;
				case "waveHeight":
					WaveHeight = float.Parse(value);
					break;
				case "waveSpeed":
					WaveSpeed = float.Parse(value);
					break;
				case "swirl":
					Swirl = bool.Parse(value);
					break;
				case "swirlRadius":
					SwirlRadius = float.Parse(value);
					break;
				case "swirlSpeed":
					SwirlSpeed = float.Parse(value);
					break;
				case "sticky":
					Sticky = bool.Parse(value);
					break;
				case "loud":
					Loud = bool.Parse(value);
					break;
				case "bold":
					Bold = bool.Parse(value);
					break;
				case "whisper":
					Whisper = bool.Parse(value);
					break;
				default:
					return false;
			}
			return true;
		}
		/// <summary>
		/// Removes the property of the rich string style based on the provided name.
		/// </summary>
		/// <param name="name">The name of the property to remove.</param>
		/// <returns>True if the property was successfully removed; otherwise, false.</returns>
		public bool ResetProperty(string name)
		{
			switch (name)
			{
				case "color":
					Color = null;
					break;
				case "speed":
					Speed = null;
					break;
				case "volume":
					Volume = null;
					break;
				case "pitch":
					Pitch = null;
					break;
				case "pitchRange":
					PitchRange = null;
					break;
				case "shake":
					Shake = false;
					break;
				case "shakeRadius":
					ShakeRadius = null;
					break;
				case "wave":
					Wave = false;
					break;
				case "waveHeight":
					WaveHeight = null;
					break;
				case "waveSpeed":
					WaveSpeed = null;
					break;
				case "swirl":
					Swirl = false;
					break;
				case "swirlRadius":
					SwirlRadius = null;
					break;
				case "swirlSpeed":
					SwirlSpeed = null;
					break;
				case "sticky":
					Sticky = false;
					break;
				case "loud":
					Loud = false;
					break;
				case "bold":
					Bold = false;
					break;
				case "whisper":
					Whisper = false;
					break;
				default:
					return false;
			}
			return true;
		}
		/// <inheritdoc/>
		public static string GetXmlTag(RDDialoguePhraseStyle before, RDDialoguePhraseStyle after)
		{
			string tag = "";
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "color",
				before.Color?.TryGetName(out string[] namesbefore) == true
				? namesbefore[0].ToLower()
				: before.Color?.ToString(before.Color?.A == 255 ? "#RRGGBB" : "#RRGGBBAA"),
				after.Color?.TryGetName(out string[] namesafter) == true
				? namesafter[0].ToLower()
				: after.Color?.ToString(after.Color?.A == 255 ? "#RRGGBB" : "#RRGGBBAA"));
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "speed", before.Speed?.ToString(), after.Speed?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "volume", before.Volume?.ToString(), after.Volume?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "pitch", before.Pitch?.ToString(), after.Pitch?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "pitchRange", before.PitchRange?.ToString(), after.PitchRange?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "shake", before.Shake, after.Shake);
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "shakeRadius", before.ShakeRadius?.ToString(), after.ShakeRadius?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "wave", before.Wave, after.Wave);
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "waveHeight", before.WaveHeight?.ToString(), after.WaveHeight?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "waveSpeed", before.WaveSpeed?.ToString(), after.WaveSpeed?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "swirl", before.Swirl, after.Swirl);
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "swirlRadius", before.SwirlRadius?.ToString(), after.SwirlRadius?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "swirlSpeed", before.SwirlSpeed?.ToString(), after.SwirlSpeed?.ToString());
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "sticky", before.Sticky, after.Sticky);
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "loud", before.Loud, after.Loud);
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "bold", before.Bold, after.Bold);
			IRDRichStringStyle<RDDialoguePhraseStyle>.TryAddTag(ref tag, "whisper", before.Whisper, after.Whisper);
			return tag;
		}
		/// <inheritdoc/>
		public static bool operator ==(RDDialoguePhraseStyle left, RDDialoguePhraseStyle right) =>
				   left.Color == right.Color
				&& left.Speed == right.Speed
				&& left.Volume == right.Volume
				&& left.Pitch == right.Pitch
				&& left.PitchRange == right.PitchRange
				&& left.Shake == right.Shake
				&& left.ShakeRadius == right.ShakeRadius
				&& left.Wave == right.Wave
				&& left.WaveHeight == right.WaveHeight
				&& left.WaveSpeed == right.WaveSpeed
				&& left.Swirl == right.Swirl
				&& left.SwirlRadius == right.SwirlRadius
				&& left.SwirlSpeed == right.SwirlSpeed
				&& left.Sticky == right.Sticky
				&& left.Loud == right.Loud
				&& left.Bold == right.Bold
				&& left.Whisper == right.Whisper;
		/// <inheritdoc/>
		public static bool operator !=(RDDialoguePhraseStyle left, RDDialoguePhraseStyle right) => !(left == right);
		/// <inheritdoc/>
		public readonly override bool Equals([NotNullWhen(true)] object? obj) => obj is RDDialoguePhraseStyle e && base.Equals(e);
		/// <inheritdoc/>
		public readonly bool Equals(RDDialoguePhraseStyle other) => this == other;
		/// <inheritdoc/>
		public readonly override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
