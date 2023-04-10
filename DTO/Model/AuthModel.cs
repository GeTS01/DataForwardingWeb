using Domain;
using DTO;
using System.ComponentModel.DataAnnotations;

namespace DataForwardingWeb.DTO.Model
{
    public class AuthModel : IModel<User>
    {
        [Required(ErrorMessage = "Укажите логин")]
        [MinLength(8, ErrorMessage = "Минимальная длинна логина 8")]
        [MaxLength(11, ErrorMessage = "Максимальная длинна логина 11")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(8, ErrorMessage = "Минимальная длинна пароля 8")]
        [MaxLength(11, ErrorMessage = "Максимальная длинна пароля 11")]
        public string Password { get; set; }

        public AuthModel(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        User IModel<User>.toEntity()
        {
            throw new NotImplementedException();
        }

        User IModel<User>.toEntity(long id)
        {
            throw new NotImplementedException();
        }
    }
}
