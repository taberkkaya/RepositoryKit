// File: RepositoryKit.Extensions/Extensions/IEnumerableExtensions.cs

namespace RepositoryKit.Extensions.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Provides extension methods for <see cref="IEnumerable{T}"/> sequences.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/> sequence.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="source">The enumerable source.</param>
    /// <param name="action">The action to perform on each element.</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    /// <summary>
    /// Returns a distinct set based on a key selector.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <param name="source">The enumerable source.</param>
    /// <param name="keySelector">Function to extract the key for each element.</param>
    /// <returns>Distinct elements by key.</returns>
    public static IEnumerable<T> SafeDistinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    {
        var set = new HashSet<TKey>();
        foreach (var item in source)
            if (set.Add(keySelector(item)))
                yield return item;
    }

    /// <summary>
    /// Randomly shuffles the elements in the sequence.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="source">The enumerable source.</param>
    /// <returns>Shuffled enumerable.</returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var rand = new Random();
        return source.OrderBy(_ => rand.Next());
    }

    /// <summary>
    /// Groups elements by a key selector and projects the results.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="source">The enumerable source.</param>
    /// <param name="keySelector">Function to extract the key for each element.</param>
    /// <param name="resultSelector">Function to create a result value from each group.</param>
    /// <returns>Projected groups as an enumerable.</returns>
    public static IEnumerable<TResult> GroupBySelect<T, TKey, TResult>(
        this IEnumerable<T> source,
        Func<T, TKey> keySelector,
        Func<TKey, IEnumerable<T>, TResult> resultSelector)
    {
        return source.GroupBy(keySelector).Select(g => resultSelector(g.Key, g));
    }
}
