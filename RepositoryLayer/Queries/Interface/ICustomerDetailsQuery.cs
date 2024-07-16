using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Interface
{
    public interface ICustomerDetailsQuery
    {
        public Task<List<CustomerDetailsEntity>> GetCustomerDetailsByIdAsync(int userId);
    }
}
