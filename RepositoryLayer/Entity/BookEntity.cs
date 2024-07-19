using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public string BookImage { get; set; } = string.Empty;
        public DateTime createdAt { get; set; } = DateTime.Now;
        [ForeignKey("UserEntity")]
        [Column("Admin_ID")]
        public int UserEntityId { get; set; }
        public UserEntity User { get; set; }
        [JsonIgnore]
        public ICollection<CartEntity> Carts { get; set; }
        [JsonIgnore]
        public ICollection<WishlistEntity> Wishlist { get; set; }
    }
}
