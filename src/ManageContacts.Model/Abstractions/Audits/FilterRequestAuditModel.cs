using Newtonsoft.Json;

namespace ManageContacts.Model.Abstractions.Audits;

public abstract class FilterRequestAuditModel
{
    [JsonProperty(PropertyName = "page_index")]
    public int PageIndex { get; set; } = 1;

    [JsonProperty(PropertyName = "page_size")]
    public int PageSize { get; set; } = 10;
    
    [JsonProperty(PropertyName = "search_string")]
    public string? SearchString { get; set; } = String.Empty;
}