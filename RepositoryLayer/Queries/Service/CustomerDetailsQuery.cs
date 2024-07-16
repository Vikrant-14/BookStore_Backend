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
    public class CustomerDetailsQuery : ICustomerDetailsQuery
    {
        private readonly ICustomerDetailsRL _customerDetailsRL;

        public CustomerDetailsQuery(ICustomerDetailsRL customerDetailsRL)
        {
            _customerDetailsRL = customerDetailsRL;
        }

        public async Task<List<CustomerDetailsEntity>> GetCustomerDetailsByIdAsync(int userId)
        {
            try
            {
                return await _customerDetailsRL.GetCustomerDetailsByIdAsync(userId);
            }
            catch (CustomerDetailException)
            {
                throw;
            }
        }
    }
}
