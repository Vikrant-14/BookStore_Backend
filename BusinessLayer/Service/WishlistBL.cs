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
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistCommand _wishlistCommand;
        private readonly IWishlistQuery _wishlistQuery;
        public WishlistBL(IWishlistCommand wishlistCommand, IWishlistQuery wishlistQuery)
        {
            _wishlistCommand = wishlistCommand;
            _wishlistQuery = wishlistQuery;
        }

        public async Task<WishlistEntity> AddToWishlistAsync(WishlistML model, int userId)
        {
            try
            {
                return await _wishlistCommand.AddToWishlistAsync(model, userId);
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
                return await _wishlistCommand.RemoveFromWishlistasync(model, userId);
            }
            catch (WishlistException)
            {
                throw;
            }
        }

        public async Task<List<WishlistWithBookDetailsDto>> GetAllItemFromWishlistByUserIdAsync(int userId)
        {
            try
            {
                return await _wishlistQuery.GetAllItemFromWishlistByUserIdAsync(userId);
            }
            catch (WishlistException)
            {
                throw;
            }
        }
    }
}
