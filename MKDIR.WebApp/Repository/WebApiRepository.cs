using MKDIR.Domain;
using MKDIR.WebApp.Entity;
using MKDIR.WebApp.Interfaces.Repository;
using System.Text;
using System.Text.Json;

namespace MKDIR.WebApp.Repository
{
    public class WebApiRepository : IWebApiRepository
    {
        private readonly HttpClient httpClient;

        public WebApiRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private JsonSerializerOptions OpcionesPorDefectoJSON => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        //public async Task<ServiceResponse<T>> Get<T>(string url)
        //{
        //    var respuestaHTTP = await httpClient.GetAsync(url);

        //    if (respuestaHTTP.IsSuccessStatusCode)
        //    {
        //        var respuesta = await DeserializarRespuesta<T>(respuestaHTTP, OpcionesPorDefectoJSON);
        //        return new ServiceResponse<T>(respuesta, error: false, respuestaHTTP);
        //    }
        //    else
        //    {
        //        return new ServiceResponse<T>(default, error: true, respuestaHTTP);
        //    }
        //}               

        //public async Task<ServiceResponse<object>> Post<T>(string url, T enviar)
        //{
        //    var enviarJSON = JsonSerializer.Serialize(enviar);
        //    var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
        //    var responseHttp = await httpClient.PostAsync(url, enviarContent);
        //    return new ServiceResponse<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);

        //}

        public async Task<InternalResponse> Post<T, TResponse>(string url, T enviar)
        {
            HttpResponseMessage responseHttp = new HttpResponseMessage();
            InternalResponse internalResponse = new InternalResponse();

            try
            {            
                var enviarJSON = JsonSerializer.Serialize(enviar);
                var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
                responseHttp = await httpClient.PostAsync(url, enviarContent);

                var respuestaString = await responseHttp.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<InternalResponse>(respuestaString, OpcionesPorDefectoJSON);
            }
            catch(Exception ex)
            {
                return new InternalResponse()
                {
                    success = false,
                    operationDate = System.DateTime.Now,
                    hasErrors = true,
                    errors = new ErrorResult() { ErrorCode=0, Message= ex.Message, Exception = ex},
                    dataCountToPaging = 0
                };
            }

        }

        //public async Task<ServiceResponse<object>> Put<T>(string url, T enviar)
        //{
        //    var enviarJSON = JsonSerializer.Serialize(enviar);
        //    var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
        //    var responseHttp = await httpClient.PutAsync(url, enviarContent);
        //    return new ServiceResponse<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        //}

        //public async Task<ServiceResponse<object>> Delete(string url)
        //{
        //    var responseHTTP = await httpClient.DeleteAsync(url);
        //    return new ServiceResponse<object>(null,
        //        !responseHTTP.IsSuccessStatusCode, responseHTTP);
        //}

        //private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse,
        //    JsonSerializerOptions jsonSerializerOptions)
        //{
        //    var respuestaString = await httpResponse.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions);
        //}
    }
}
