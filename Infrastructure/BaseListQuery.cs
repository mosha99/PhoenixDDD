using System.Text.Json.Serialization;

namespace BuildingBlocks;

public abstract class BaseListQuery<TEntity> : IBaseListQuery<TEntity, BaseFilter<TEntity>>
{
    [JsonIgnore]
    public BaseFilter<TEntity>? Filter { get; set; }
    public int SkipCount { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
    public OrderType OrderType { get; set; }
}
public abstract class BaseListQuery<TEntity, TFilter> : BaseListQuery<TEntity> where TFilter : BaseFilter<TEntity>
{
    [JsonPropertyName("Filter")]
    public TFilter? _filter { get => Filter as TFilter; set => Filter = value; }
}