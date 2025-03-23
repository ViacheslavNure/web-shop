namespace WebShop.Sql.Models
{
    public class AmountOfProducts
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductsAmmount { get; set; }

        public Guid? CartId { get; set; }

        public virtual Cart? Cart { get; set; }

        public Guid? OrderId { get; set; }

        public virtual Order? Order { get; set; }
    }
}
