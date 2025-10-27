namespace Proyecto_Aerolinea.Web.Core.Pagination
{
    public class PaginationRequest
    {
        private int _pages = 1;
        private int _recordsPerPages = 15;
        private const int MAX_RECORDS_PER_PAGES = 50;
        public string? Filter { get; set; }

        public int Pages { get => _pages; set => _pages = value > 0 ? value : _pages; }
        public int RecordsPerPages { get => _recordsPerPages;
            set => _recordsPerPages = value <= MAX_RECORDS_PER_PAGES ? value : MAX_RECORDS_PER_PAGES; }
    }
}