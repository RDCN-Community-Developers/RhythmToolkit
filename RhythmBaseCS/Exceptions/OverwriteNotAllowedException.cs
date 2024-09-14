using System;

namespace RhythmBase.Exceptions
{

	public class OverwriteNotAllowedException(string filepath, Type referType) : RhythmBaseException(filepath)
	{

		public string FilePath { get; set; }


		public override string Message
		{
			get
			{
				return string.Format("Cannot save file '{0}' because overwriting is disabled by the settings and a file with the same name already exists.\r\nTo correct this, change the path or filename or set the OverWrite property of {1} to false.", FilePath, _referType.Name);
			}
		}

		private readonly Type _referType = referType;
	}
}
