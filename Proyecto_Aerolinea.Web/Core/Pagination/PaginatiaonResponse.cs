/*using Proyecto_Aerolinea.Web.Core.Pagination.Abstraction;

namespace Proyecto_Aerolinea.Web.Core.Pagination
{
    public class PaginatiaonResponse : IPagination
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
            }
        }

    }
}*/
