using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Proyecto_Aerolinea.Web.Core;

namespace Proyecto_Aerolinea.Web.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public static ObjectResult ControllerBasicValidation<T>(Response<T> response, ModelStateDictionary? modelstate = null, int? statuscode=null)
        {
            if (modelstate is not null && !modelstate.IsValid)
            {
                List<string> errors = modelstate.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();

                return new ObjectResult(Response<T>.Failure("Debe ajustar los errores de validacion", errors))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (statuscode is not null && statuscode >= 100 && statuscode <=599)
            {
                return new ObjectResult(response)
                {
                    StatusCode= statuscode
                };
            }

            if (response.Succeed)
            {
                return new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

        }
    }
}
