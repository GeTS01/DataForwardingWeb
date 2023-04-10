using Domain;

namespace DTO.Response
{
    public class SignInDTO
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public SecurityTokenViewModel Token { get; set; }

        public SignInDTO(User user, SecurityTokenViewModel token)
        {
            
            Name = user.Name;
            Login = user.Login;
            Token = token;
        }
    }

    public class SecurityTokenViewModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public SecurityTokenViewModel(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }
    }
}
