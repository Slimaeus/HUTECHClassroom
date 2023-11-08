using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Domain.Enums;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Common.Extensions;

public static class MultipleResultQueryExtensions
{
    public static IMultipleResultQuery<TEntity> SortEntityQuery<TEntity>(
        this IMultipleResultQuery<TEntity> query,
        SortingOrder sortingOrder,
        Expression<Func<TEntity, object?>> keySelector)
        where TEntity : class
        => sortingOrder switch
        {
            SortingOrder.Ascending => (IMultipleResultQuery<TEntity>)query.OrderBy(keySelector),
            SortingOrder.Descending => (IMultipleResultQuery<TEntity>)query.OrderByDescending(keySelector),
            _ => query
        };
}
