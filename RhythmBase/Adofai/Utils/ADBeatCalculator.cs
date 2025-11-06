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
#pragma warning restore IDE0052 // 删除未读的私有成员

		/*
		 * 更新：
		 * - 事件添加进砖块
		 * - 事件移除出砖块
		 * - 砖块添加进关卡
		 * - 砖块移除出关卡
		 * - 砖块中旋
		 * - 砖块发卡弯
		 * - 事件属性更新
		 */
	}
}
