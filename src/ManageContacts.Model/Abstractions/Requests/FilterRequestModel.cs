using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.Model.Abstractions.Requests;

[BindProperties]
public abstract class FilterRequestModel
{
    [BindProperty(Name = "page_index")]
    public int PageIndex { get; set; } = 1;

    [BindProperty(Name = "page_size")]
    public int PageSize { get; set; } = 10;
    
    [BindProperty(Name = "search_string")]
    public string? SearchString { get; set; }
}