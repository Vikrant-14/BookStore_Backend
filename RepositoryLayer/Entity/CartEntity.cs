using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CartEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BookEntity")]
        public int BookId { get; set; }
        public BookEntity Book { get; set; }
        [ForeignKey("UserEntity")]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int Quantity { get; set; }
        public bool IsPurchased { get; set; } = false;
    }
}
