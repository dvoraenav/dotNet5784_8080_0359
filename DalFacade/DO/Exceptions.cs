namespace DO;
[Serializable]
public class DalDoesNotExistException : Exception
{
    /// <summary>
    /// ctr for new DalDoesNotExistException object
    /// </summary>
    /// <param name="message">the error message</param>
    public DalDoesNotExistException(string? message) : base(message) { }
}
/// <summary>
/// ctr for new DalDoesNotExistException object
/// </summary>
/// <param name="message">the error message</param>
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
/// <summary>
/// ctr for new DalCantBeEraseException object
/// </summary>
/// <param name="message">the error message</param>
public class DalCantBeEraseException : Exception
{
    public DalCantBeEraseException(string? message) : base(message) { }
}

public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}