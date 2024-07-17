using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {
        private readonly ICartCommand _cartCommand;
        private readonly ICartQuery _cartQuery;
        public CartBL(ICartCommand cartCommand, ICartQuery cartQuery)
        {
            _cartCommand = cartCommand;
            _cartQuery = cartQuery;
        }

        public async Task<CartEntity> AddToCartAsync(CartML model,int userId)
        {
            try
            {
                return await _cartCommand.AddToCartAsync(model, userId);
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
                return await _cartCommand.UpdateCartQuantityAsync(model, userId);
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
                return await _cartCommand.RemoveItemFromCartAsync(userId, bookId);
            }
            catch(CartException)
            { 
                throw;
            }
        }

        public async Task<List<CartEntity>> GetItemListFromCartAsync(int userId)
        {
            try
            {
                return await _cartQuery.GetItemListFromCartAsync(userId);
            }
            catch (CartException)
            {
                throw;
            }
        }

    }
}
