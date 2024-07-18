using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        [ForeignKey("CustomerDetails")]
        public int CustomerDetailsId { get; set; }
        public CustomerDetailsEntity CustomerDetails { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public CartEntity Cart { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
