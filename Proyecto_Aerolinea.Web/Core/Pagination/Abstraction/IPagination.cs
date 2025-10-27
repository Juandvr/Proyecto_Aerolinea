namespace Proyecto_Aerolinea.Web.Core.Pagination.Abstraction
{
    public interface IPagination
    {
        public int CurrentPages { get; set; }
        public int TotalPages { get; set; }
        public int RecordsPerPages { get; set; }
        public int TotalCount { get; set; }
        public bool hasPrevious { get; }
        public bool hasNext { get; }
        public string? Filter { get; set; }
        public List<int> Pages { get; }
    }
}
