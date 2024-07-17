using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CartML
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
