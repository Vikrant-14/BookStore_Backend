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
    public class OrderQuery : IOrderQuery
    {
        private readonly IOrderRL _orderRL;

        public OrderQuery(IOrderRL orderRL)
        {
            _orderRL = orderRL;
        }

        public async Task<List<OrderEntity>> GetAllPlacedOrderByUserId(int userId)
        {
            try
            {
                return await _orderRL.GetAllPlacedOrderByUserId(userId);
            }
            catch (OrderException)
            {
                throw;
            }
        }
    }
}
