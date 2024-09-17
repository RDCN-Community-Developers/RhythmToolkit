namespace RhythmBase.Exceptions
{
    [Serializable]
    internal class TypeNotSupportedException : Exception
    {
        public TypeNotSupportedException()
        {
        }
        public TypeNotSupportedException(string? message) : base(message)
        {
        }
        public TypeNotSupportedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}