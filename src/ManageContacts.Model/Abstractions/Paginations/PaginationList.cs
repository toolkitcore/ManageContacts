using Newtonsoft.Json;

namespace ManageContacts.Model.Abstractions.Paginations;

public class PaginationList<T>
{
    [JsonIgnore]
    public int IndexFrom { get; set; }

    [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
    public int PageIndex { get; set; }

    [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
    public int PageSize { get; set; }

    [JsonProperty("total_count", NullValueHandling = NullValueHandling.Ignore)]
    public int TotalCount { get; set; }

    [JsonProperty("total_pages", NullValueHandling = NullValueHandling.Ignore)]
    public int TotalPages { get; set; }

    [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<T> Items { get; set; }

    [JsonProperty("has_prev_page", NullValueHandling = NullValueHandling.Ignore)]
    public bool HasPreviousPage { get; set; }

    [JsonProperty("has_next_page", NullValueHandling = NullValueHandling.Ignore)]
    public bool HasNextPage { get; set; }
}