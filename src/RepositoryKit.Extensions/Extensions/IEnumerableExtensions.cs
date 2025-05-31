// IEnumerableExtensions.cs
namespace RepositoryKit.Extensions;

/// <summary>
/// Provides extension methods for IEnumerable.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Chunks a collection into smaller groups of specified size.
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        if (chunkSize <= 0)
            throw new ArgumentException("Chunk size must be greater than zero.", nameof(chunkSize));

        var chunk = new List<T>(chunkSize);

        foreach (var item in source)
        {
            chunk.Add(item);
            if (chunk.Count == chunkSize)
            {
                yield return chunk;
                chunk = new List<T>(chunkSize);
            }
        }

        if (chunk.Any())
            yield return chunk;
    }

    /// <summary>
    /// Applies an action to each element in the collection.
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }
}
