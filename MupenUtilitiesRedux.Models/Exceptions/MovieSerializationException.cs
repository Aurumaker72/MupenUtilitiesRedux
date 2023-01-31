using System.Runtime.Serialization;

namespace MupenUtilitiesRedux.Models.Exceptions;

[Serializable]
internal class MovieSerializationException : Exception
{
    public MovieSerializationException()
    {
    }

    public MovieSerializationException(string? message) : base(message)
    {
    }

    public MovieSerializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected MovieSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}