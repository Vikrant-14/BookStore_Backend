using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class BookML
    {
        [Required]
        public string BookName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Author { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string BookImage { get; set; } = string.Empty;
    }
}
