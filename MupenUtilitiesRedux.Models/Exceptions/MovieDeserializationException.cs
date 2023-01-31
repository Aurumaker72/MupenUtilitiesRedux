using System.Runtime.Serialization;

namespace MupenUtilitiesRedux.Models.Exceptions;

[Serializable]
internal class MovieDeserializationException : Exception
{
    public MovieDeserializationException()
    {
    }

    public MovieDeserializationException(string? message) : base(message)
    {
    }

    public MovieDeserializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected MovieDeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}