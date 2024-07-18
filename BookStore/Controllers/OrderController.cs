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
    [Authorize(Roles = "User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL _orderBL;
        private ResponseML responseML;

        public OrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
            responseML = new ResponseML();
        }

        [HttpPost("placeOrder")]
        public async Task<ActionResult> PlacedOrder(OrderML model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("Id").Value);
                var result = await _orderBL.PlacedOrder(model, userId);

                responseML.Success = true;
                responseML.Message = "Order Placed Successfully";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch(OrderException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpGet("getAllOrders")]
        public async Task<ActionResult> GetAllPlacedOrderByUserId()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("Id").Value);
                var result = await _orderBL.GetAllPlacedOrderByUserId(userId);

                responseML.Success = true;
                responseML.Message = "All Orders Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(OrderException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML) ;
            }
        }
    }
}
