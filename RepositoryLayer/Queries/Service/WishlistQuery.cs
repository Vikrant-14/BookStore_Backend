using ModelLayer;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Service
{
    public class WishlistQuery  : IWishlistQuery
    {
        private readonly IWishlistRL _wishlistRL;

        public WishlistQuery(IWishlistRL wishlistRL)
        {
            _wishlistRL = wishlistRL;
        }

        public async Task<List<WishlistWithBookDetailsDto>> GetAllItemFromWishlistByUserIdAsync(int userId)
        {
            try
            {
                return await _wishlistRL.GetAllItemFromWishlistByUserIdAsync(userId);   
            }
            catch(WishlistException)
            {
                throw;
            }
        }
    }
}
