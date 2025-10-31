namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Specifies the type of render filter to be used.
	/// </summary>
	/// 
	[RDJsonEnumSerializable]
	public enum Filters
	{
		/// <summary>
		/// Nearest neighbor filtering.
		/// </summary>
		NearestNeighbor,
		/// <summary>
		/// Bilinear filtering.
		/// </summary>
		BiliNear
	}
}
