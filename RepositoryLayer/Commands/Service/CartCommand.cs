using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Service
{
    public class CartCommand : ICartCommand
    {
        private readonly ICartRL _cartRL;

        public CartCommand(ICartRL cartRL)
        {
            _cartRL = cartRL;
        }

        public async Task<CartEntity> AddToCartAsync(CartML model, int userId)
        {
            try
            {
                return await _cartRL.AddToCartAsync(model, userId);
            }
            catch (CartException)
            {
                throw;
            }
        }

        public async Task<CartEntity> UpdateCartQuantityAsync(CartML model, int userId)
        {
            try
            {
                return await _cartRL.UpdateCartQuantityAsync(model, userId);
            }
            catch (CartException)
            {
                throw;
            }
        }

        public async Task<CartEntity> RemoveItemFromCartAsync(int userId, int bookId)
        {
            try
            {
                return await _cartRL.RemoveItemFromCartAsync(userId, bookId);
            }
            catch(CartException)
            {
                throw;
            }
        }
    }
}
