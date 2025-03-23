using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models.Order
{
    public class CheckoutViewModel : IValidatableObject
    {
        [Display(Name = "Адреса доставки")]
        public string? DeliveryAddress { get; set; }

        [Display(Name = "Місто")]
        public string? City { get; set; }

        [Display(Name = "Поштовий індекс")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Виберіть спосіб доставки")]
        [Display(Name = "Спосіб доставки")]
        public DeliveryMethod DeliveryMethod { get; set; }

        [Required(ErrorMessage = "Введіть номер карти")]
        [Display(Name = "Номер карти")]
        [RegularExpression(@"^(\d{4}\s?){4}$", ErrorMessage = "Номер карти повинен містити 16 цифр")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Введіть термін дії карти")]
        [Display(Name = "Термін дії (MM/YY)")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Формат: MM/YY")]
        public string CardExpiry { get; set; }

        [Required(ErrorMessage = "Введіть CVV")]
        [Display(Name = "CVV")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV повинен містити 3 цифри")]
        public string CardCvv { get; set; }

        [Display(Name = "Коментарі до замовлення")]
        public string? OrderComments { get; set; }

        public decimal TotalPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DeliveryMethod != DeliveryMethod.SelfPickup)
            {
                if (string.IsNullOrWhiteSpace(DeliveryAddress))
                {
                    yield return new ValidationResult("Введіть адресу доставки", new[] { nameof(DeliveryAddress) });
                }

                if (string.IsNullOrWhiteSpace(City))
                {
                    yield return new ValidationResult("Введіть місто", new[] { nameof(City) });
                }

                if (string.IsNullOrWhiteSpace(PostalCode))
                {
                    yield return new ValidationResult("Введіть поштовий індекс", new[] { nameof(PostalCode) });
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(PostalCode, @"^\d{5}$"))
                {
                    yield return new ValidationResult("Поштовий індекс повинен містити 5 цифр", new[] { nameof(PostalCode) });
                }
            }
        }
    }
} 