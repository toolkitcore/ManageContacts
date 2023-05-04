using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ManageContacts.Model.Abstractions.Audits;

public abstract class FilterRequestAuditModel
{
    [BindProperty(Name = "page_index")]
    public int PageIndex { get; set; } = 1;

    [BindProperty(Name = "page_size")]
    public int PageSize { get; set; } = 10;
    
    [BindProperty(Name = "search_string")]
    public string? SearchString { get; set; } = String.Empty;
}