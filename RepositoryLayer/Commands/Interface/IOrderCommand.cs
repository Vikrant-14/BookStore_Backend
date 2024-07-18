using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Interface
{
    public interface IOrderCommand
    {
        public Task<string> PlacedOrder(OrderML model, int userId);
    }
}
