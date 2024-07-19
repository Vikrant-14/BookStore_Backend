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
    public class WishlistCommand : IWishlistCommand
    {
        private readonly IWishlistRL _wishlistRL;

        public WishlistCommand(IWishlistRL wishlistRL)
        {
            _wishlistRL = wishlistRL;
        }


        public async Task<WishlistEntity> AddToWishlistAsync(WishlistML model, int userId)
        {
            try
            {
                return await _wishlistRL.AddToWishlistAsync(model, userId);
            }
            catch (WishlistException)
            {
                throw;
            }
        }

        public async Task<WishlistEntity> RemoveFromWishlistasync(WishlistML model, int userId)
        {
            try
            {
                return await _wishlistRL.RemoveFromWishlistasync(model, userId);
            }
            catch(WishlistException)
            {
                throw;
            }
        }
    }
}
