using System;
using System.Runtime.Serialization;

public class PlayerProfileMissingException : Exception {
	public PlayerProfileMissingException()
    {
    }

    public PlayerProfileMissingException(string message) : base(message)
    {
    }

    public PlayerProfileMissingException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected PlayerProfileMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
