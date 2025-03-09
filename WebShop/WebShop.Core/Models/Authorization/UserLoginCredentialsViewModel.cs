using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebShop.Core.Models.Authorization
{
    public class UserLoginCredentialsViewModel
    {
        [Required(ErrorMessage = "Заповніть ім'я користувача.")]
        [DisplayName("Ім'я користувача")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введіть пароль.")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
