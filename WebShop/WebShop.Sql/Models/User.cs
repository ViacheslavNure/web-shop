using Microsoft.AspNetCore.Identity;

namespace WebShop.Sql.Models
{
    public class User : IdentityUser
    {
        public Guid CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
