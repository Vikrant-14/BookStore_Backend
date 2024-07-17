using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public Task<CartEntity> AddToCartAsync(CartML model, int userId);
        public Task<CartEntity> UpdateCartQuantityAsync(CartML model, int userId);
        public Task<CartEntity> RemoveItemFromCartAsync(int userId, int bookId);
        public Task<List<CartEntity>> GetItemListFromCartAsync(int userId);
    }
}
