﻿using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Interface
{
    public interface ICustomerDetailsCommand
    {
        public Task<CustomerDetailsEntity> AddCustomerAddressAsync(CustomerDetailML model, int userId);
        public Task<CustomerDetailsEntity> UpdateCustomerDetailsAsync(CustomerDetailML model, int userId);
    }
}
