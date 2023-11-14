using System.Runtime.InteropServices;

namespace HospitalMVC.Utils;

public class QueryParam
{
    public string SearchTerm { get; set; } = string.Empty;

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public SortOrder SortOrder { get; set; } = SortOrder.None;

    public SortOrder DescSortOrder { get; set; } = SortOrder.DescDescending;

    public SortOrder CostSortOrder { get; set; } = SortOrder.CostDescending;

    public QueryParam Copy([Optional] string? searchTerm, [Optional] int? pageIndex, [Optional] int? pageSize, [Optional] SortOrder? sortOrder) => new()
    {
        SearchTerm = searchTerm ?? SearchTerm,
        PageIndex = pageSize != null ? 1 : pageIndex ?? PageIndex,
        PageSize = pageSize ?? PageSize,
        SortOrder = sortOrder ?? SortOrder,
        DescSortOrder = sortOrder?.ToggleOr(DescSortOrder) ?? DescSortOrder,
        CostSortOrder = sortOrder?.ToggleOr(CostSortOrder) ?? CostSortOrder,
    };
}
