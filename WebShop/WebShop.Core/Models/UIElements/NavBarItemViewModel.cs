namespace WebShop.Core.Models.UIElements
{
    public class NavBarItemViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RedirectToController { get; set; }

        public string RedirectToAction { get; set; }
    }
}
