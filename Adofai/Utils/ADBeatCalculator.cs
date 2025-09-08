using RhythmBase.Adofai.Components;
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
			////this._SetSpeeds = this.Collection.EventsWhere<ADSetSpeed>().ToList<ADSetSpeed>();
			////this._Twirls = this.Collection.EventsWhere<ADTwirl>().ToList<ADTwirl>();
			////this._Pauses = this.Collection.EventsWhere<ADPause>().ToList<ADPause>();
			////this._Holds = this.Collection.EventsWhere<ADHold>().ToList<ADHold>();
			////this._Freeroams = this.Collection.EventsWhere<ADFreeRoam>().ToList<ADFreeRoam>();
		}
#pragma warning disable IDE0052 // 删除未读的私有成员
		internal ADLevel Collection;
		private float _DefaultBpm = 100;
		private readonly List<Tile> _SetSpeeds = [];
		private readonly List<Tile> _Twirls = [];
		private readonly List<Tile> _Pauses = [];
		private readonly List<Tile> _Holds = [];
		private readonly List<Tile> _Freeroams = [];
		private readonly List<Tile> _MultiPlanets = [];
		private readonly List<Tile> _RadiusScales = [];
#pragma warning restore IDE0052 // 删除未读的私有成员
	}
}
