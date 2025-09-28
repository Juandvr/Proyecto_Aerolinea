namespace Proyecto_Aerolinea.Web.Core
{
    public class Response<T>
    {
        public bool Succed { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T? Result { get; set; }
    }
}
