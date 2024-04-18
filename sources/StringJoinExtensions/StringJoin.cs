namespace StringJoinExtensions;

public static class StringJoin
{
    /// <summary>
    /// Concatenates the result of the specified predicate applied to each member of a collection,
    /// using the specified separator between each member.
    /// </summary>
    /// <typeparam name="T">The type of the members of <paramref name="collectionToJoin"/>.</typeparam>
    /// <param name="separator">The separator to use for the concatenation.</param>
    /// <param name="collectionToJoin">The collection of <typeparamref name="T"/> members.</param>
    /// <param name="predicate">The predicate to apply on each member of <paramref name="collectionToJoin"/> before the concatenation.</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="collectionToJoin"/> or the <paramref name="predicate"/> is null.</exception>
    /// <returns>
    /// A string that consist of the members of <paramref name="collectionToJoin"/> delimited by the <paramref name="separator"/> character,
    /// or <see cref="string.Empty"/> if the <paramref name="collectionToJoin"/> is empty.
    /// </returns>
    public static string Join<T>(
    char separator,
    IEnumerable<T> collectionToJoin,
    Func<T, string> predicate)
    {
        return string.Join(separator, collectionToJoin.Select(predicate));
    }
}
