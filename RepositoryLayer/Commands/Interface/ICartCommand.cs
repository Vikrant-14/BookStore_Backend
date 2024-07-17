using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Interface
{
    public interface ICartCommand
    {
        public Task<CartEntity> AddToCartAsync(CartML model, int userId);
        public Task<CartEntity> UpdateCartQuantityAsync(CartML model, int userId);
        public Task<CartEntity> RemoveItemFromCartAsync(int userId, int bookId);
    }
}
