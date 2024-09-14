using Newtonsoft.Json;
using RhythmBase.Adofai.Components;

namespace RhythmBase.Adofai.Events
{

	public class ADTile : ADTypedList<ADBaseTileEvent>
	{
		public float Angle
		{
			get => _angle;
			set => _angle = value is > (-360f) and < 360f ? ((value + 540f) % 360f) - 180f : -999f;
		}
		public bool IsMidSpin => _angle < -180f || _angle > 180f;
		public ADBeat Beat => new(Parent?.Calculator!, Index + (_angle / 180f));
		[JsonIgnore]
		public ADLevel? Parent { get; set; }
		public ADTile() { }
		public ADTile(IEnumerable<ADBaseTileEvent> actions)
		{
			foreach (ADBaseTileEvent i in actions)
			{
				i.Parent = this;
				Add(i);
			}
		}
		public int Index => Parent?.IndexOf(this) ?? -1;
		public override string ToString() => string.Format("[{0}]{1}<{2}>{3}",
			[
				Index,
				Beat,
				IsMidSpin ? "MS".PadRight(4) : _angle.ToString().PadLeft(4),
				((IEnumerable<ADBaseTileEvent>)this).Any<ADBaseTileEvent>() ? string.Format(", Count = {0}", ((IEnumerable<ADBaseTileEvent>)this).Count<ADBaseTileEvent>()) : string.Empty
			]);
		private float _angle = 0;
	}
}
