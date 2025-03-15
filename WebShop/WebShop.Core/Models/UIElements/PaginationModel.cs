using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models.UIElements
{
    public class PaginationModel
    {
        public int CurrentPageNumber { get; set; }

        public int AmountPerPage { get; set; } = 18;

        public int TotalAmount { get; set; }
    }
}
