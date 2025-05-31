// RepositoryException.cs
namespace RepositoryKit.Core.Exceptions;

/// <summary>
/// Represents errors that occur during repository operations.
/// </summary>
public class RepositoryException : Exception
{
    public RepositoryException()
    {
    }

    public RepositoryException(string message)
        : base(message)
    {
    }

    public RepositoryException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
