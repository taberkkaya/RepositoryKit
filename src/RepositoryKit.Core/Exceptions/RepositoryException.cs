// File: src/RepositoryKit.Core/Exceptions/RepositoryException.cs

namespace RepositoryKit.Core.Exceptions;

using System;

/// <summary>
/// Represents errors that occur during repository operations and provides context information.
/// </summary>
public enum RepositoryErrorType
{
    Unknown,
    Add,
    Update,
    Delete,
    Query,
    SaveChanges,
    Concurrency,
    ConstraintViolation
}

/// <summary>
/// Represents a repository exception with error type, entity type and operation context.
/// </summary>
public class RepositoryException : Exception
{
    public RepositoryErrorType ErrorType { get; }
    public Type? EntityType { get; }
    public string? Operation { get; }

    public RepositoryException(
        string message,
        RepositoryErrorType errorType = RepositoryErrorType.Unknown,
        Type? entityType = null,
        string? operation = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        ErrorType = errorType;
        EntityType = entityType;
        Operation = operation;
    }
}
