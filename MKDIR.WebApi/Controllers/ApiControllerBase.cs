using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKDIR.Domain;
using System.Net;

namespace MKDIR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {

        protected new IActionResult Response(
                                        object data = null,
                                        int dataCountToPaging = 0,
                                        HttpStatusCode statusCode = HttpStatusCode.OK,
                                        bool hasErrors = false,
                                        object errors = null)
        {
            var result = new
            {
                success = true,
                operationDate = DateTimeOffset.Now,
                hasErrors,
                errors,
                data,
                dataCountToPaging
            };
            return BaseResponse(statusCode, result);
        }

        protected IActionResult ResponseNotFound(ErrorResult error)
        {
            return ResponseNotFound(new List<ErrorResult>() { error });
        }
        protected IActionResult ResponseNotFound(IList<ErrorResult> errors)
        {
            return ResponseNotFound(null, errors);
        }
        protected IActionResult ResponseNotFound(object data = null, IList<ErrorResult> errors = null)
        {
            var result = new
            {
                success = false,
                operationDate = DateTimeOffset.Now,
                hasErrors = errors != null && errors.Count > 0,
                errors,
                data,
            };
            return BaseResponse(HttpStatusCode.NotFound, result);
        }

        private ObjectResult BaseResponse(HttpStatusCode statusCode, object result)
        {
            return StatusCode((int)statusCode, result);
        }

        protected IActionResult ResponseNotOk()
        {
            return ResponseNotOk(new List<ErrorResult>());
        }

        protected IActionResult ResponseNotOk(ErrorResult error)
        {
            return ResponseNotOk(new List<ErrorResult>() { error });
        }

        protected IActionResult ResponseNotOk(IList<ErrorResult> errors)
        {
            object data = null;
            var result = new
            {
                success = false,
                operationDate = DateTimeOffset.Now,
                hasErrors = true,
                errors = errors,
                data,
            };
            return BaseResponse(HttpStatusCode.BadRequest, result);
        }

        protected IActionResult ResponseNoContent()
        {
            object data = null;
            var result = new
            {
                success = true,
                operationDate = DateTimeOffset.Now,
                hasErrors = false,
                data,
            };
            return BaseResponse(HttpStatusCode.NoContent, result);
        }
    }
}
