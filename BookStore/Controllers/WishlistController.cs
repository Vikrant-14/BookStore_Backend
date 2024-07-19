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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL _wishlistBL;
        private readonly ResponseML responseML;

        public WishlistController(IWishlistBL wishlistBL)
        {
            _wishlistBL = wishlistBL;
            responseML = new ResponseML();
        }

        [HttpPost("addItemToWishlist")]
        public async Task<ActionResult> AddToWishlistAsync(WishlistML model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result = await _wishlistBL.AddToWishlistAsync(model, userId);

                responseML.Success = true;
                responseML.Message = $"Item added to wishlist!!!";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch(WishlistException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("removeItemFromWishlist")]
        public async Task<ActionResult> RemoveFromWishlistasync(WishlistML model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result = await _wishlistBL.RemoveFromWishlistasync(model, userId);

                responseML.Success = true;
                responseML.Message = $"Item removed to wishlist!!!";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(WishlistException ex)
            {
                responseML.Success=false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("getAllWishListItems")]
        public async Task<ActionResult> GetAllItemFromWishlistByUserIdAsync()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("Id").Value);

                var result = await _wishlistBL.GetAllItemFromWishlistByUserIdAsync(userId);

                responseML.Success = true;
                responseML.Message = $"All Item Fetched from wishlist successfully!!!";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (WishlistException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

    }
}
