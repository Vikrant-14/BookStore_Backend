using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public Task<string> PlacedOrder(OrderML model, int userId);
        public Task<List<OrderEntity>> GetAllPlacedOrderByUserId(int userId);
    }
}
