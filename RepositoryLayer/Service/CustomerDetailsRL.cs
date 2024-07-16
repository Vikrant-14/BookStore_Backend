using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CustomerDetailsRL
    {
        private readonly ApplicationDBContext _context;

        public CustomerDetailsRL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CustomerDetailsEntity> AddCustomerAddressAsync(CustomerDetailML model, int userId)
        {
            try
            {
                CustomerDetailsEntity customerDetailsEntity = new CustomerDetailsEntity()
                {
                    AddressType = model.AddressType,
                    City = model.City,
                    FullAddress = model.FullAddress,
                    State = model.State,
                    UserId = userId,
                };

                await _context.CustomerDetails.AddAsync(customerDetailsEntity);
                await _context.SaveChangesAsync();

                return customerDetailsEntity;
            }
            catch(CustomerDetailException ex)
            {
                throw new CustomerDetailException("Error occured while adding customers details");
            }
        }
    }
}
