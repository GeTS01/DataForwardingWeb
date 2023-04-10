using DataForwardingWeb.DTO.Model;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using DTO.Response;
using Microsoft.EntityFrameworkCore;
using Services.Helper;

namespace Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SignInDTO> Auth(AuthModel request)
        {

            var user = await _userRepository
                .GetAll()
                .Where(x => x.Login == request.Login && x.Password == request.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("NOT ");

            var identity = TokenHelper.GetIdentity(user);
            var token = TokenHelper.GetSecurityToken(identity, false);

            return new SignInDTO(user, token);
        }
    }
}
