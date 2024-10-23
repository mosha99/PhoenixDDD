using System.Linq.Expressions;
using BuildingBlocks;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public static class DbExtension
{
    public static async Task<ListResult<TAggregate>> ToListResultAsync<TAggregate>(this IQueryable<TAggregate> List, BaseListQuery<TAggregate> paging, CancellationToken cancellationToken)
    {
        var result = new ListResult<TAggregate>();

        List = List.SetPagination(paging, out Task<int> count);

        result.Count = await count;

        result.List = await List.ToListAsync(cancellationToken);

        return result;
    }
    public static async Task<ListResult<TAggregate>> ToListResultAsync<TAggregate>(this IQueryable<TAggregate> queryable, int skip, int size)
    {
        try
        {
            var result = new ListResult<TAggregate>
            {
                Count = await queryable.CountAsync()
            };

            queryable = queryable.Skip(skip).Take(size);

            result.List = await queryable.ToListAsync();

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static IQueryable<TAggregate> SetPagination<TAggregate, TFilter>(this IQueryable<TAggregate> queryable, IBaseListQuery<TAggregate, TFilter> pagingCondition, out Task<int> countQuery)
        where TFilter : BaseFilter<TAggregate>

    {
        try
        {
            queryable = SetFilter(queryable, pagingCondition);

            queryable = SetOrder(queryable, pagingCondition);

            countQuery = queryable.CountAsync();

            queryable = queryable.Skip(pagingCondition?.SkipCount ?? 0).Take(pagingCondition?.PageSize ?? 10);

            return queryable;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static IQueryable<TAggregate> SetOrder<TAggregate, TFilter>(IQueryable<TAggregate> queryable, IBaseListQuery<TAggregate, TFilter> pagingCondition)
        where TFilter : BaseFilter<TAggregate>

    {
        if (!string.IsNullOrWhiteSpace(pagingCondition?.OrderBy))
        {

            queryable = pagingCondition.OrderType == OrderType.Asc ?
                queryable.OrderByColumn(pagingCondition.OrderBy) :
                queryable.OrderByColumnDescending(pagingCondition.OrderBy);
        }

        return queryable;
    }

    private static IQueryable<TAggregate> SetFilter<TAggregate, TFilter>(IQueryable<TAggregate> queryable, IBaseListQuery<TAggregate, TFilter> pagingCondition)
        where TFilter : BaseFilter<TAggregate>

    {
        if (pagingCondition?.Filter is BaseFilter<TAggregate> filter)
            foreach (var expression in filter.GetAllExpression())
                queryable = queryable.Where(expression);

        return queryable;
    }
    private static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath)
    {
        return source.OrderByColumnUsing(columnPath, "OrderBy");
    }

    private static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath)
    {
        return source.OrderByColumnUsing(columnPath, "OrderByDescending");
    }

    private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath,
        string method)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var member = columnPath.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
        var keySelector = Expression.Lambda(member, parameter);
        var methodCall = Expression.Call(typeof(Queryable), method, [parameter.Type, member.Type],
            source.Expression, Expression.Quote(keySelector));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }
}