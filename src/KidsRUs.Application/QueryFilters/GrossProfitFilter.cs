namespace KidsRUs.Application.QueryFilters;

public class GrossProfitFilter
{
    /// <summary>
    /// Example: 2022-07-20T06:31:27.140Z
    /// </summary>
    public DateTime? MinDate { get; set; }
    
    /// <summary>
    /// Example: 2022-07-20T06:31:27.140Z
    /// </summary>
    public DateTime? MaxDate { get; set; }
}