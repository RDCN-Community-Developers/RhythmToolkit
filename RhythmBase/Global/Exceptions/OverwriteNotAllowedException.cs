namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Exception thrown when an attempt to overwrite a file is not allowed.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="OverwriteNotAllowedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </remarks>
	/// <param name="filepath">The file path that caused the exception.</param>
	/// <param name="referType">The type that caused the exception.</param>
	public class OverwriteNotAllowedException(string filepath, Type referType) : RhythmBaseException(filepath)
	{
		/// <summary>
		/// Gets or sets the file path that caused the exception.
		/// </summary>
		public string FilePath { get; set; } = filepath;
		/// <summary>
		/// Gets the message that describes the current exception.
		/// </summary>
		public override string Message =>
			$"Cannot save file '{FilePath}' because overwriting is disabled by the settings and a file with the same name already exists.\r\nTo correct this, change the path or filename or set the OverWrite property of {_referType.Name} to false.";
		private readonly Type _referType = referType;
	}
}
