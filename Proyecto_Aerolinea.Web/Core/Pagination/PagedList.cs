using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core.Extensions;

namespace Proyecto_Aerolinea.Web.Core.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPages { get; set; }
        public int TotalPages { get; set; }
        public int RecordsPerPages { get; set; }
        public int TotalCount { get; set; }
        public PagedList()
        {
        }
        public PagedList(List<T> items, int count, int pageNumber, int recordsPerPages)
        {
            TotalCount = count;
            RecordsPerPages = recordsPerPages;
            CurrentPages = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)recordsPerPages);
            AddRange(items);
        }
        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> queryable, PaginationRequest request)
        {
            int count = await queryable.CountAsync();
            List<T> list = await queryable.PaginateAsync<T>(request).ToListAsync();

            return new PagedList<T>(list, count, request.Pages, request.RecordsPerPages);
        }
    }
}