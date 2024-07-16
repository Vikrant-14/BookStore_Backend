using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CustomerDetailsRL : ICustomerDetailsRL
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

        public async Task<CustomerDetailsEntity> UpdateCustomerDetailsAsync(CustomerDetailML model, int userId)
        {
            var customer = _context.CustomerDetails.FirstOrDefault(customer => customer.UserId == userId && customer.AddressType == model.AddressType);

            if (customer == null)
            {
                throw new CustomerDetailException($"Customer details not found");
            }

            customer.AddressType = model.AddressType;
            customer.FullAddress = model.FullAddress;
            customer.City = model.City;
            customer.State = model.State;

            _context.CustomerDetails.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<List<CustomerDetailsEntity>> GetCustomerDetailsByIdAsync(int userId)
        {
            var customerDetails = await _context.CustomerDetails.Where(customer => customer.UserId == userId).ToListAsync();

            if (customerDetails == null)
            {
                throw new CustomerDetailException($"Customer details not found");
            }

            return customerDetails;
        }
    }
}
