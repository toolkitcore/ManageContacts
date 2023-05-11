namespace ManageContacts.Shared.Helper;

public static class ConstantHelper
{
    public static string GetSortKey(string sortType, string sortField)
        => string.IsNullOrEmpty(sortField) ? "default" : $"{sortField.ToLower()}_{sortType.ToLower()}";
}