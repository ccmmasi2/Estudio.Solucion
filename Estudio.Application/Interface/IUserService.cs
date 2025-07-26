namespace Estudio.Application.Interface
{
    public interface IUserService
    {
        string? Login(string username, string password);
        string GenerateToken(string username, string role);
    }
}
