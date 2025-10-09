﻿using Proyecto_Aerolinea.Web.Core.Pagination;

namespace Proyecto_Aerolinea.Web.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> PaginateAsync<T>(this IQueryable<T> queryable, PaginationRequest request)
        {
            return queryable.Skip((request.Page - 1) * request.RecordsPerPage)
                            .Take(request.RecordsPerPage);
        }
    }
}