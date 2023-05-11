using ManageContacts.Shared.Helper;

namespace ManageContacts.Model.Abstractions.Requests;

public static class FilterRequestExtensions
{
    public static string GetSortType(this FilterRequestModel filterRequest)
        => ConstantHelper.GetSortKey(filterRequest.SortType, filterRequest.SortField);
}