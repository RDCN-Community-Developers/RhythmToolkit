using System;
namespace RhythmBase.Exceptions
{
	public class VersionTooLowException : RhythmBaseException
	{
		public override string Message { get; }

		public VersionTooLowException(int version)
		{
			this.Message = string.Format("Might not support. The version {0} is too low. Save this level with the latest version of the game to update the level version.", LevelVersion);
			LevelVersion = version;
		}

		public VersionTooLowException(int version, Exception innerException) : base(string.Empty, innerException)
		{
			this.Message = string.Format("Might not support. The version {0} is too low. Save this level with the latest version of the game to update the level version.", LevelVersion);
			LevelVersion = version;
		}

		public int LevelVersion;
	}
}
