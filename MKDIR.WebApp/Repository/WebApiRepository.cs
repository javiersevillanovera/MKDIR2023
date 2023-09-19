using MKDIR.Domain;
using MKDIR.WebApp.Entity;
using MKDIR.WebApp.Interfaces.Repository;
using System.Net;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<ServiceResponse<Tdata>> Post<T, Tdata>(string url, T enviar)
        {
            HttpResponseMessage responseHttp = new HttpResponseMessage();
            InternalResponse internalResponse = new InternalResponse();
            List<ErrorResult> errorList = new List<ErrorResult>();
            Tdata? data;

            try
            {            
                var enviarJSON = JsonSerializer.Serialize(enviar);
                var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
                responseHttp = await httpClient.PostAsync(url, enviarContent);

                var responseString = await responseHttp.Content.ReadAsStringAsync();
                internalResponse = JsonSerializer.Deserialize<InternalResponse>(responseString, OpcionesPorDefectoJSON);
                

                if (responseHttp.StatusCode == HttpStatusCode.OK )
                {
                    if (internalResponse?.data is not null)
                    {
                        data = JsonSerializer.Deserialize<Tdata>(internalResponse.data.ToString(), OpcionesPorDefectoJSON);

                        return new ServiceResponse<Tdata>(internalResponse.success, string.Empty, data);
                    }
                    else
                        return new ServiceResponse<Tdata>(internalResponse.success, string.Empty, default);
                }
                else
                {
                    errorList = JsonSerializer.Deserialize<List<ErrorResult>>(internalResponse.errors.ToString(), OpcionesPorDefectoJSON);

                    return new ServiceResponse<Tdata>(internalResponse.success,
                                    errorList is not null ? (errorList.Count > 0 ? errorList[0].Message : null) : null, default);
                }                
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Tdata>(
                        false,
                        ex.Message,
                        default
                    );
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

        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse,
            JsonSerializerOptions jsonSerializerOptions)
        {
            var respuestaString = await httpResponse.Content.ReadAsStringAsync();
            T varr = JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions);
            return varr;
        }
    }
}
