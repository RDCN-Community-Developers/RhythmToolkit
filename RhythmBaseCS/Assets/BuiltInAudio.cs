using System;

namespace RhythmBase.Assets
{

	public class BuiltInAudio(string name) : IAssetFile
	{

		public string Name
		{
			get
			{
				return _name;
			}
		}


		public void Load(string directory)
		{
		}

		private readonly string _name = name;
	}
}
