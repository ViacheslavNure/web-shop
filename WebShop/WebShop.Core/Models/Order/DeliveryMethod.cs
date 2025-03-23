using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models.Order
{
    public enum DeliveryMethod
    {
        [Display(Name = "Нова Пошта")]
        NovaPoshta,
        [Display(Name = "Укрпошта")]
        UkrPoshta,
        [Display(Name = "Самовивіз")]
        SelfPickup
    }
}
