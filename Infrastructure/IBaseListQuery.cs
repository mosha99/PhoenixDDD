namespace BuildingBlocks;

public interface IBaseListQuery<TEntity, TFilter> where TFilter : BaseFilter<TEntity>
{
    public TFilter? Filter { get; set; }
    public int SkipCount { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public OrderType OrderType { get; set; }
}