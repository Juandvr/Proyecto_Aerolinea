using Proyecto_Aerolinea.Web.Core.Pagination.Abstraction;
using Proyecto_Aerolinea.Web.Core.Pagination;

namespace Proyecto_Aerolinea.Web.Core.Pagination
{
    public class PaginationResponse<T> : IPagination
    {
        public int CurrentPages { get ; set ; }
        public int TotalPages { get ; set ; }
        public int RecordsPerPages { get ; set ; }
        public int TotalCount { get ; set ; }
        public bool hasPrevious => CurrentPages > 1;
        public bool hasNext => CurrentPages < TotalPages;
        public int VisiblePages = 5;
        public string? Filter { get ; set ; }
        public List<int> Pages
        {
            get
            {
                List<int> pages = new List<int>();
                int half = VisiblePages / 2;
                int star = CurrentPages - half + 1 - (VisiblePages % 2);
                int end = CurrentPages + half;

                int vPages = VisiblePages;

                if (vPages > TotalPages)
                {
                    vPages = TotalPages;
                }

                if (star <= 0)
                {
                    star = 1;
                    end = vPages;
                }

                if (end > TotalPages)
                {
                    star = TotalPages - vPages + 1;
                    end = TotalPages;
                }

                int itPage = star;

                while (itPage <=end)
                {
                    pages.Add(itPage);
                    itPage++;
                }

                return pages;
            }
        }
        public PagedList<T> List { get; set; }
    }
}
