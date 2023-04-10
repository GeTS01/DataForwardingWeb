using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using Service;

namespace DataForwardingWeb.Service.Implementation
{
    public class UserService : IAccountService<User>
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public TokenData<User> authenticate(string username, string password)
        {
            return null;
        }
    }
}
