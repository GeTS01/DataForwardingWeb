using DataForwardingWeb.DTO.Model;
using DTO.Response;

namespace Service
{
    public interface IAuthService
    {
        Task<SignInDTO> Auth(AuthModel request);
    }
}
