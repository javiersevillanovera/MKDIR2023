using System.Net;

namespace MKDIR.WebApp.Interfaces.Repository
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(bool success, string mensajeError, T? response )
        {

            Success = success;
            MensajeError = mensajeError;
            Response = response;
        }

        public bool Success { get; set; }        
        public string? MensajeError { get; set; }
        public T? Response { get; set; }
        //public HttpResponseMessage HttpResponseMessage { get; set; }

        //public async Task<string?> ObtenerMensajeError()
        //{
        //    if (!Error)
        //    {
        //        return null;
        //    }

        //    var codigoEstatus = HttpResponseMessage.StatusCode;

        //    if (codigoEstatus == HttpStatusCode.NotFound)
        //    {
        //        return "Recurso no encontrado";
        //    }
        //    else if (codigoEstatus == HttpStatusCode.BadRequest)
        //    {
        //        return await HttpResponseMessage.Content.ReadAsStringAsync();
        //    }
        //    else if (codigoEstatus == HttpStatusCode.Unauthorized)
        //    {
        //        return "Tienes que loguearte para hacer esto";
        //    }
        //    else if (codigoEstatus == HttpStatusCode.Forbidden)
        //    {
        //        return "No tienes permisos para hacer esto";
        //    }
        //    else
        //    {
        //        return "Ha ocurrido un error inesperado";
        //    }
        //}
    }
}
