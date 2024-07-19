﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public string Role { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<BookEntity> Books { get; set; }
        [JsonIgnore]
        public ICollection<CustomerDetailsEntity> customerDetails { get; set; }
        [JsonIgnore]
        public ICollection<CartEntity> Carts { get; set; }
        [JsonIgnore]
        public ICollection<OrderEntity> Order { get; set; }
        [JsonIgnore]
        public ICollection<WishlistEntity> Wishlist { get; set; }
    }
}
