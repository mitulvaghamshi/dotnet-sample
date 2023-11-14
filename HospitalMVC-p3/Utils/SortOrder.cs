namespace HospitalMVC.Utils;

public enum SortOrder { DescDescending, Description, CostDescending, Cost, None };

public static class SortOrderEx
{
    public static SortOrder ToggleOr(this SortOrder order, SortOrder @default) => @default switch
    {
        SortOrder.DescDescending or SortOrder.Description => order switch
        {
            SortOrder.DescDescending => SortOrder.Description,
            SortOrder.Description => SortOrder.DescDescending,
            _ => @default,
        },
        SortOrder.CostDescending or SortOrder.Cost => order switch
        {
            SortOrder.CostDescending => SortOrder.Cost,
            SortOrder.Cost => SortOrder.CostDescending,
            _ => @default,
        },
        _ => @default,
    };
}
