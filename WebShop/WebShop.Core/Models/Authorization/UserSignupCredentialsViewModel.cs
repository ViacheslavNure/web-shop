using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models.Authorization
{
    public class UserSignupCredentialsViewModel
    {
        [Required(ErrorMessage = "Заповніть ім'я користувача.")]
        [DisplayName("Ім'я користувача")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Заповніть пароль.")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Пароль повинен бути не коротше 5 символів.")]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Підтвердіть пароль.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Паролі повинні бути однаковими.")]
        [DisplayName("Пароль")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Заповніть електронну пошту.")]
        [EmailAddress(ErrorMessage = "Електронна пошта повинна бути в правильному форматі.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заповніть номер телефону.")]
        [Phone(ErrorMessage = "Телефон повинен бути в правильному форматі.")]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }
    }
}
