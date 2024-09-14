using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Extensions;

namespace RhythmBase.Adofai.Utils
{
	/// <summary>
	/// Beat Calculator.
	/// </summary>

	public class ADBeatCalculator
	{
		internal ADBeatCalculator(ADLevel level)
		{
			Collection = level;
			Refresh();
		}
		private void Refresh()
		{
			_DefaultBpm = Collection.Settings.Bpm;
			_MidSpins = Collection.Where((i) => i.IsMidSpin).ToList();
			////this._SetSpeeds = this.Collection.EventsWhere<ADSetSpeed>().ToList<ADSetSpeed>();
			////this._Twirls = this.Collection.EventsWhere<ADTwirl>().ToList<ADTwirl>();
			////this._Pauses = this.Collection.EventsWhere<ADPause>().ToList<ADPause>();
			////this._Holds = this.Collection.EventsWhere<ADHold>().ToList<ADHold>();
			////this._Freeroams = this.Collection.EventsWhere<ADFreeRoam>().ToList<ADFreeRoam>();
		}
#pragma warning disable IDE0052 // 删除未读的私有成员
		internal ADLevel Collection;
		private float _DefaultBpm = 100;
		private List<ADTile> _MidSpins = [];
		private readonly List<ADSetSpeed> _SetSpeeds = [];
		private readonly List<ADTwirl> _Twirls = [];
		private readonly List<ADPause> _Pauses = [];
		private readonly List<ADHold> _Holds = [];
		private readonly List<ADFreeRoam> _Freeroams = [];
#pragma warning restore IDE0052 // 删除未读的私有成员
	}
}
