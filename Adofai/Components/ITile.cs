using RhythmBase.Adofai.Events;

namespace RhythmBase.Adofai.Components
{
	public interface ITile
	{
		float Angle { get; set; }
		int Index { get; }
		bool IsHairPin { get; }
		bool IsMidSpin { get; set; }
		ITile Next { get; internal set; }
		internal ADLevel Parent { get; set; }
		ITile? Previous { get; internal set; }
		float Tick { get; }

		ITile Clone();
		void Add(BaseTileEvent item);
	}
}