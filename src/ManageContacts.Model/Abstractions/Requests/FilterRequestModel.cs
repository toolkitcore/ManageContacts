using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.Model.Abstractions.Requests;

[BindProperties]
public abstract class FilterRequestModel
{
    [BindProperty(Name = "page")]
    public int PageIndex { get; set; } = 1;

    [BindProperty(Name = "limit")]
    public int PageSize { get; set; } = 10;
    
    [BindProperty(Name = "sort")]
    public string SortType { get; set; } = "asc";

    [BindProperty(Name = "field")]
    public string SortField { get; set; }
    
    [BindProperty(Name = "search_string")]
    public string? SearchString { get; set; }
}