namespace ManageContacts.Shared.Extensions;

public static class LinqExtensions
{
    public static bool HasDuplicated<T, TKey>(this IEnumerable<T> source, Func<T, TKey> groupBy)
        => source.GroupBy(groupBy).Any(g => g.Count() > 1);
}