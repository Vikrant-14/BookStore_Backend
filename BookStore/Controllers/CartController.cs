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
    public class CartController : ControllerBase
    {
        private readonly ICartBL _cartBL;
        private readonly ResponseML responseML;

        public CartController(ICartBL cartBL)
        {
            _cartBL = cartBL;
            responseML = new ResponseML();
        }

        [HttpPost("addToCart")]
        public async Task<ActionResult> AddToCartAsync(CartML model)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result = await  _cartBL.AddToCartAsync(model, userId);

                responseML.Success = true;
                responseML.Message = $"Book added to cart successfully";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch(CartException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(400, responseML);
            }
        }
        [HttpPut("updateQuantity")]
        public async Task<ActionResult> UpdateCartQuantityAsync(CartML model)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result = await _cartBL.UpdateCartQuantityAsync(model, userId);

                responseML.Success = true;
                responseML.Message = $"Book updated to cart successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (CartException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404,responseML);
            }
        }

        [HttpDelete("removeFromCart/{userId}")]
        public async Task<ActionResult> RemoveItemFromCartAsync(int userId, int bookId)
        {
            try
            {
                var result = await _cartBL.RemoveItemFromCartAsync(userId, bookId);

                responseML.Success = true;
                responseML.Message = $"Cart item : {bookId} remove successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(CartException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("getItemFromCart")]
        public async Task<ActionResult> GetItemListFromCartAsync()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("Id").Value);
                var result = await _cartBL.GetItemListFromCartAsync(userId);

                responseML.Success = true;
                responseML.Message = $"All Cart Items Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(CartException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }
    }
}
