using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.CustomException;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL _bookBL;
        private readonly ResponseML responseML;

        public BookController(IBookBL bookBL)
        {
           _bookBL = bookBL;
            responseML = new ResponseML();
        }

        [HttpPost("addBook")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> AddBookAsync(BookML model)
        {
            try
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                //var aa = User.FindFirst("Id").Value;

                var result = await _bookBL.AddBookAsync(model, adminId);

                responseML.Success = true;
                responseML.Message = $"Book ID : {result.Id} added Successfully";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch (BookException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(200, responseML);
            }
        }
    }
}
