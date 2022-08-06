using MdcstecTools.Shared;
using System.Threading.Tasks;

namespace MdcstecTools.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Logout();
        Task<RegisterResponse> Register(RegisterRequest registerRequest);
    }
}