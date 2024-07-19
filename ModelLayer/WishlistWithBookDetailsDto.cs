using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class WishlistWithBookDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public string BookImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
