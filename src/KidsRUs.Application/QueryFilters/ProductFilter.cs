namespace KidsRUs.Application.QueryFilters;

public class ProductFilter : PaginationFilter
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Info { get; set; }  
    public string? CategoryName { get; set; }
    public string? TagName { get; set; }
}