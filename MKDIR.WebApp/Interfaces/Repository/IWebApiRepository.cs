using MKDIR.WebApp.Entity;

namespace MKDIR.WebApp.Interfaces.Repository
{
    public interface IWebApiRepository
    {
        //Task<ServiceResponse<object>> Delete(string url);
        //Task<ServiceResponse<T>> Get<T>(string url);
        //Task<ServiceResponse<object>> Post<T>(string url, T enviar);
        Task<InternalResponse> Post<T, TResponse>(string url, T enviar);
        //Task<ServiceResponse<object>> Put<T>(string url, T enviar);
    }
}
