using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProductCase.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static IQueryable<T> AppendWhereIf<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, bool condition)
        {
            return condition ? query.Where(expression) : query;
        }
    }   
}
