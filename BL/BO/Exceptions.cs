
namespace BO;

public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
               : base(message, innerException) { }
}

public class BlCantBeEraseException : Exception
{
    public BlCantBeEraseException(string? message) : base(message) { }
    public BlCantBeEraseException(string message, Exception innerException)
               : base(message, innerException) { }
}




