using System.ComponentModel.DataAnnotations;
using WebShop.Core.Models.Order;

namespace WebShop.Core.Models.User
{
    public class UserProfileViewModel
    {
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Display(Name = "Ім'я")]
        public string UserName { get; set; }

        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Кількість замовлень")]
        public int OrdersCount { get; set; }

        [Display(Name = "Загальна сума замовлень")]
        public decimal TotalOrdersPrice { get; set; }

        public List<UserOrderViewModel> RecentOrders { get; set; }
    }

    public class UserOrderViewModel
    {
        public long OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
} 