using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace RhythmBase.RhythmDoctor.Components;

public class SoundCollection : IReadOnlyDictionary<SoundType, Audio?>
{
	internal protected SoundType[] _keys;
	internal protected Audio?[] _values;
	public SoundCollection(params SoundType[] types)
	{
		if (types.Length == 0)
			throw new ArgumentException("At least one SoundType must be provided.", nameof(types));
		_keys = types;
		_values = new Audio[types.Length];
	}
	///<inheritdoc/>
	public Audio? this[SoundType key]
	{
		get
		{
			int index = Array.IndexOf(_keys, key);
			if (index >= 0)
				return _values[index];
			throw new KeyNotFoundException($"The given key '{key}' was not present in the collection.");
		}
		set
		{
			int index = Array.IndexOf(_keys, key);
			if (index >= 0)
				_values[index] = value;
			else
				throw new KeyNotFoundException($"The given key '{key}' was not present in the collection.");
		}
	}
	///<inheritdoc/>
	internal ref Audio? First => ref _values[0];
	///<inheritdoc/>
	public IEnumerable<SoundType> Keys => Array.AsReadOnly(_keys);
	///<inheritdoc/>
	public IEnumerable<Audio?> Values => Array.AsReadOnly(_values);
	///<inheritdoc/>
	public int Count => _keys.Length;
	///<inheritdoc/>
	public bool ContainsKey(SoundType key) => _keys.Contains(key);
	///<inheritdoc/>
	public IEnumerator<KeyValuePair<SoundType, Audio?>> GetEnumerator() => _keys.Zip(_values, (k, v) => new KeyValuePair<SoundType, Audio?>(k, v)).GetEnumerator();
	///<inheritdoc/>
	public bool TryGetValue(SoundType key, [MaybeNullWhen(false)] out Audio? value)
	{
		if (_keys.Contains(key))
		{
			value = this[key];
			return true;
		}
		value = default;
		return false;
	}
	/*
			"ClapSoundHold": ["ClapSoundHoldLongEnd", "ClapSoundHoldLongStart", "ClapSoundHoldShortEnd", "ClapSoundHoldShortStart"],
			"PulseSoundHold": ["PulseSoundHoldStart", "PulseSoundHoldShortEnd", "PulseSoundHoldEnd", "PulseSoundHoldStartAlt", "PulseSoundHoldShortEndAlt", "PulseSoundHoldEndAlt"],
			"ClapSoundHoldP2": ["ClapSoundHoldLongEndP2", "ClapSoundHoldLongStartP2", "ClapSoundHoldShortEndP2", "ClapSoundHoldShortStartP2"],
			"PulseSoundHoldP2": ["PulseSoundHoldStartP2", "PulseSoundHoldShortEndP2", "PulseSoundHoldEndP2", "PulseSoundHoldStartAltP2", "PulseSoundHoldShortEndAltP2", "PulseSoundHoldEndAltP2"],
			"FreezeshotSound": ["FreezeshotSoundCueLow", "FreezeshotSoundCueHigh", "FreezeshotSoundRiser", "FreezeshotSoundCymbal"],
			"BurnshotSound": ["BurnshotSoundCueLow", "BurnshotSoundCueHigh", "BurnshotSoundRiser", "BurnshotSoundCymbal"],
			"HoldshotSound": ["HoldshotSoundCue", "HoldshotSoundClapStart", "HoldshotSoundClapShortEnd", "HoldshotSoundClapLongEnd"]

	 */

	///<inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	public class SingleAudioSoundCollection : SoundCollection
	{
		public SingleAudioSoundCollection(SoundType type) : base(type) { }
		public Audio? Audio { get => _values[0]; set => _values[0] = value; }
	}
	public class ClapSound : SoundCollection
	{
		public Audio? LongEnd { get => _values[0]; set => _values[0] = value; }
		public Audio? LongStart { get => _values[1]; set => _values[1] = value; }
		public Audio? ShortEnd { get => _values[2]; set => _values[2] = value; }
		public Audio? ShortStart { get => _values[3]; set => _values[3] = value; }
		public ClapSound() : base(
			SoundType.ClapSoundHoldLongEnd,
			SoundType.ClapSoundHoldLongStart,
			SoundType.ClapSoundHoldShortEnd,
			SoundType.ClapSoundHoldShortStart
		)
		{ }
	}
	public class PulseSound : SoundCollection
	{
		public Audio? Start { get => _values[0]; set => _values[0] = value; }
		public Audio? ShortEnd { get => _values[1]; set => _values[1] = value; }
		public Audio? End { get => _values[2]; set => _values[2] = value; }
		public Audio? StartAlt { get => _values[3]; set => _values[3] = value; }
		public Audio? ShortEndAlt { get => _values[4]; set => _values[4] = value; }
		public Audio? EndAlt { get => _values[5]; set => _values[5] = value; }
		public PulseSound() : base(
			SoundType.PulseSoundHoldStart,
			SoundType.PulseSoundHoldShortEnd,
			SoundType.PulseSoundHoldEnd,
			SoundType.PulseSoundHoldStartAlt,
			SoundType.PulseSoundHoldShortEndAlt,
			SoundType.PulseSoundHoldEndAlt)
		{ }
	}
	public class ClapSoundP2 : SoundCollection
	{
		public Audio? LongEnd { get => _values[0]; set => _values[0] = value; }
		public Audio? LongStart { get => _values[1]; set => _values[1] = value; }
		public Audio? ShortEnd { get => _values[2]; set => _values[2] = value; }
		public Audio? ShortStart { get => _values[3]; set => _values[3] = value; }
		public ClapSoundP2() : base(
			SoundType.ClapSoundHoldLongEndP2,
			SoundType.ClapSoundHoldLongStartP2,
			SoundType.ClapSoundHoldShortEndP2,
			SoundType.ClapSoundHoldShortStartP2
		)
		{ }
	}
	public class PulseSoundP2 : SoundCollection
	{
		public Audio? Start { get => _values[0]; set => _values[0] = value; }
		public Audio? ShortEnd { get => _values[1]; set => _values[1] = value; }
		public Audio? End { get => _values[2]; set => _values[2] = value; }
		public Audio? StartAlt { get => _values[3]; set => _values[3] = value; }
		public Audio? ShortEndAlt { get => _values[4]; set => _values[4] = value; }
		public Audio? EndAlt { get => _values[5]; set => _values[5] = value; }
		public PulseSoundP2() : base(
			SoundType.PulseSoundHoldStartP2,
			SoundType.PulseSoundHoldShortEndP2,
			SoundType.PulseSoundHoldEndP2,
			SoundType.PulseSoundHoldStartAltP2,
			SoundType.PulseSoundHoldShortEndAltP2,
			SoundType.PulseSoundHoldEndAltP2
		)
		{ }
	}
	public class FreezeshotSound : SoundCollection
	{
		public Audio? CueLow { get => _values[0]; set => _values[0] = value; }
		public Audio? CueHigh { get => _values[1]; set => _values[1] = value; }
		public Audio? Riser { get => _values[2]; set => _values[2] = value; }
		public Audio? Cymbal { get => _values[3]; set => _values[3] = value; }
		public FreezeshotSound() : base(
			SoundType.FreezeshotSoundCueLow,
			SoundType.FreezeshotSoundCueHigh,
			SoundType.FreezeshotSoundRiser,
			SoundType.FreezeshotSoundCymbal
		)
		{ }
	}
	public class BurnshotSound : SoundCollection
	{
		public Audio? CueLow { get => _values[0]; set => _values[0] = value; }
		public Audio? CueHigh { get => _values[1]; set => _values[1] = value; }
		public Audio? Riser { get => _values[2]; set => _values[2] = value; }
		public Audio? Cymbal { get => _values[3]; set => _values[3] = value; }
		public BurnshotSound() : base(
			SoundType.BurnshotSoundCueLow,
			SoundType.BurnshotSoundCueHigh,
			SoundType.BurnshotSoundRiser,
			SoundType.BurnshotSoundCymbal
		)
		{ }
	}
	public class HoldshotSound : SoundCollection
	{
		public Audio? Cue { get => _values[0]; set => _values[0] = value; }
		public Audio? ClapStart { get => _values[1]; set => _values[1] = value; }
		public Audio? ClapShortEnd { get => _values[2]; set => _values[2] = value; }
		public Audio? ClapLongEnd { get => _values[3]; set => _values[3] = value; }
		public HoldshotSound() : base(
			SoundType.HoldshotSoundCue,
			SoundType.HoldshotSoundClapStart,
			SoundType.HoldshotSoundClapShortEnd,
			SoundType.HoldshotSoundClapLongEnd
		)
		{ }
	}
}
