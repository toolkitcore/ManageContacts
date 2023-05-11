namespace ManageContacts.Shared.Extensions;

public static class CollectionExtensions
{
    public static bool NotNullOrEmpty<T>(this IEnumerable<T> list)
        => list != null && list.Any();
    
    public static Func<IQueryable<T>, IOrderedQueryable<T>> GetSortType<T>(this Dictionary<string, Func<IQueryable<T>, IOrderedQueryable<T>>> sortFields, string sortKey)
    {
        if (!sortFields.NotNullOrEmpty() || !sortFields.ContainsKey(sortKey))
            return null;

        return sortFields[sortKey];
    }

}