using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Service
{
    public class CustomerDetailsCommand : ICustomerDetailsCommand
    {
        private readonly ICustomerDetailsRL _customerDetailsRL;

        public CustomerDetailsCommand(ICustomerDetailsRL customerDetailsRL)
        {
            _customerDetailsRL = customerDetailsRL;
        }

        public async Task<CustomerDetailsEntity> AddCustomerAddressAsync(CustomerDetailML model, int userId)
        {
            try
            {
                return await _customerDetailsRL.AddCustomerAddressAsync(model, userId);
            }
            catch(CustomerDetailException)
            {
                throw;
            }
        }

        public async Task<CustomerDetailsEntity> UpdateCustomerDetailsAsync(CustomerDetailML model, int userId)
        {
            try
            {
                return await _customerDetailsRL.UpdateCustomerDetailsAsync(model, userId);
            }
            catch (CustomerDetailException)
            {
                throw;
            }
        }
    }
}
