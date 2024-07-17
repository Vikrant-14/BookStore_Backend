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
    public class CartQuery : ICartQuery
    {
        private readonly ICartRL _cartRL;

        public CartQuery(ICartRL cartRL)
        {
            _cartRL = cartRL;
        }

        public async Task<List<CartEntity>> GetItemListFromCartAsync(int userId)
        {
            try
            {
                return await _cartRL.GetItemListFromCartAsync(userId);
            }
            catch (CartException)
            {
                throw;
            }
        }
    }
}
