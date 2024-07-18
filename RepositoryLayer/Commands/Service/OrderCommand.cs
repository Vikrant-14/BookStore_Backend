using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Service
{
    public class OrderCommand : IOrderCommand
    {
        private readonly IOrderRL _orderRL;

        public OrderCommand(IOrderRL orderRL)
        {
            _orderRL = orderRL;
        }

        public async Task<string> PlacedOrder(OrderML model, int userId)
        {
            try
            {
                return await _orderRL.PlacedOrder(model, userId);
            }
            catch(OrderException)
            {
                throw;
            }
        }
    }
}
