using System.Runtime.Serialization;

namespace ManageContacts.Shared.Exceptions;

public class ForbiddenRequestException : Exception
{
    public ForbiddenRequestException()
    {
    }

    public ForbiddenRequestException(string message) : base(message)
    {
    }

    public ForbiddenRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ForbiddenRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}