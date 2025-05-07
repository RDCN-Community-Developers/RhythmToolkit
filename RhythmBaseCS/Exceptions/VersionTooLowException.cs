namespace RhythmBase.Exceptions
{
	/// <summary>
	/// Exception thrown when the version is too low.
	/// </summary>
	public class VersionTooLowException : RhythmBaseException
	{
		/// <summary>
		/// Gets the error message.
		/// </summary>
		public override string Message { get; }		/// <summary>
		/// Gets the level version that caused the exception.
		/// </summary>
		public int LevelVersion;		/// <summary>
		/// Initializes a new instance of the <see cref="VersionTooLowException"/> class with the specified version.
		/// </summary>
		/// <param name="version">The version that is too low.</param>
		public VersionTooLowException(int version)
		{
			Message = string.Format("Might not support. The version {0} is too low. Save this level with the latest version of the game to update the level version.", LevelVersion);
			LevelVersion = version;
		}		/// <summary>
		/// Initializes a new instance of the <see cref="VersionTooLowException"/> class with the specified version and inner exception.
		/// </summary>
		/// <param name="version">The version that is too low.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public VersionTooLowException(int version, Exception innerException) : base(string.Empty, innerException)
		{
			Message = string.Format("Might not support. The version {0} is too low. Save this level with the latest version of the game to update the level version.", version);
			LevelVersion = version;
		}
	}
}
