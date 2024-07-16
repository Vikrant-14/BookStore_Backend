using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ICustomerDetailsBL _customerDetailsBL;
        private readonly ResponseML responseML;

        public CustomerDetailsController(ICustomerDetailsBL customerDetailsBL)
        {
            _customerDetailsBL = customerDetailsBL;
            responseML = new ResponseML();
        }

        [HttpPost("addAddress")]
        public async Task<ActionResult> AddCustomerAddressAsync(CustomerDetailML model)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result  = await _customerDetailsBL.AddCustomerAddressAsync(model, userId);

                responseML.Success = true;
                responseML.Message = $"Customer Details ID : {userId} Added Successfully";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch(CustomerDetailException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpPut("updateDetails")]
        public async Task<ActionResult> UpdateCustomerDetailsAsync(CustomerDetailML model)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result = await _customerDetailsBL.UpdateCustomerDetailsAsync(model, userId);

                responseML.Success = true;
                responseML.Message = $"Customer Details ID : {userId} Updated Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (CustomerDetailException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(400, responseML);
            }
        }

        [HttpGet("getCustomerDetails/{userId}")]
        public async Task<ActionResult> GetCustomerDetailsByIdAsync(int userId)
        {
            try
            {
                var result = await _customerDetailsBL.GetCustomerDetailsByIdAsync(userId); 
                
                responseML.Success = true;
                responseML.Message = $"Customer ID : {userId} details fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(CustomerDetailException ex)
            {
                responseML.Success= false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }
    }
}
