namespace WebShop.Sql.Models
{
    public class CartProductItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductsAmmount { get; set; }

        public Guid CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
