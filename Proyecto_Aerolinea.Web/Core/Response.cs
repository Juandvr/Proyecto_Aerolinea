namespace Proyecto_Aerolinea.Web.Core
{
    public class Response<T>
    {
        public bool Succeed { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T? Result { get; set; }

        public static Response<T> Failure(Exception ex, string message = "Ha ocurrido un error al generar al solicitud")
        {
            return new Response<T>
            {
                Succeed = false,
                Message = message,
                Errors = new List<string>
                {
                    ex.Message,
                }
            };
        }

        public static Response<T> Failure(string message, List<string> errors = null)
        {
            return new Response<T>
            {
                Succeed = false,
                Message = message,
                Errors = errors
            };
        }

        public static Response<T> Success(T result, string message = "Tarea realizada con éxito")
        {
            return new Response<T>
            {
                Succeed = true,
                Message = message,
                Result = result,
            };
        }

        public static Response<T> Success(string message = "Tarea realizada con éxito")
        {
            return new Response<T>
            {
                Succeed = true,
                Message = message,
            };
        }
    }
}
