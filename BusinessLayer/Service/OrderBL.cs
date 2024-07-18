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
    public class OrderBL : IOrderBL
    {
        private readonly IOrderCommand _orderCommand;
        private readonly IOrderQuery _orderQuery;

        public OrderBL(IOrderCommand orderCommand, IOrderQuery orderQuery)
        {
            _orderCommand = orderCommand;
            _orderQuery = orderQuery;
        }

        public async Task<string> PlacedOrder(OrderML model, int userId)
        {
            try
            {
                return await _orderCommand.PlacedOrder(model, userId);
            }
            catch(OrderException)
            {
                throw;
            }
        }

        public async Task<List<OrderEntity>> GetAllPlacedOrderByUserId(int userId)
        {
            try
            {
                return await _orderQuery.GetAllPlacedOrderByUserId(userId);
            }
            catch (OrderException)
            {
                throw;
            }
        }
    }
}
