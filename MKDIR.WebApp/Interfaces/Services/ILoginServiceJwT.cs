namespace MKDIR.WebApp.Interfaces.Services
{
    public interface ILoginServiceJwT
    {
        Task Login(string token);
        Task Logout();
    }
}
