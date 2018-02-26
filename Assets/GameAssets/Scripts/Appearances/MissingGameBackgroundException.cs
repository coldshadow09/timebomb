using System;
using System.Runtime.Serialization;

[Serializable]
public class MissingGameBackgroundException : Exception
{
    public MissingGameBackgroundException()
    {
    }

    public MissingGameBackgroundException(string message) : base(message)
    {
    }

    public MissingGameBackgroundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MissingGameBackgroundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}