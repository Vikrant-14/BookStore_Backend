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
    public class CustomerDetailsBL : ICustomerDetailsBL
    {
        private readonly ICustomerDetailsCommand _customerDetailsCommand;
        private readonly ICustomerDetailsQuery _customerDetailsQuery;

        public CustomerDetailsBL(ICustomerDetailsCommand customerDetailsCommand, ICustomerDetailsQuery customerDetailsQuery)
        {
            _customerDetailsCommand = customerDetailsCommand;
            _customerDetailsQuery = customerDetailsQuery;
        }

        public async Task<CustomerDetailsEntity> AddCustomerAddressAsync(CustomerDetailML model, int userId)
        {
            try
            {
                return await _customerDetailsCommand.AddCustomerAddressAsync(model, userId);
            }
            catch (CustomerDetailException)
            {
                throw;
            }
        }

        public async Task<CustomerDetailsEntity> UpdateCustomerDetailsAsync(CustomerDetailML model, int userId)
        {
            try
            {
                return await _customerDetailsCommand.UpdateCustomerDetailsAsync(model, userId);
            }
            catch (CustomerDetailException)
            {
                throw;
            }
        }

        public async Task<List<CustomerDetailsEntity>> GetCustomerDetailsByIdAsync(int userId)
        {
            try
            {
                return await _customerDetailsQuery.GetCustomerDetailsByIdAsync(userId);
            }
            catch(CustomerDetailException)
            {
                throw;
            }
        }
    }
}
