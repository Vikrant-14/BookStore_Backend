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
    [Authorize(Roles = "Admin")]
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
        
        public async Task<ActionResult> AddBookAsync(BookML model)
        {
            try
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                //var aa = User.FindFirst("Id").Value;

                var result = await _bookBL.AddBookAsync(model, adminId);

                responseML.Success = true;
                responseML.Message = $"Book ID : {result.Id} Added Successfully";
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

        [HttpPut("updateBook/{id}")]
        public async Task<ActionResult> UpdateBookAsync(int id, BookML model)
        {
            try
            {
                var adminId = Convert.ToInt32(User.FindFirst("Id").Value); 

                var result = await _bookBL.UpdateBookAsync(id, model, adminId);

                responseML.Success = true;
                responseML.Message = $"Book ID : {result.Id} Updated Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (BookException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(400, responseML);
            }
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<ActionResult> DeleteBookAsync(int id)
        {
            try
            {
                var result = await _bookBL.DeleteBookAsync(id);

                responseML.Success = true;
                responseML.Message = $"Book ID : {result.Id} Deleted Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (BookException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("getBook/{bookId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetBookByIdAsyncAsync(int bookId)
        {
            try
            {
                var result = await _bookBL.GetBookByIdAsync(bookId);

                responseML.Success = true;
                responseML.Message = $"Book ID : {result.Id} Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (BookException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("getAllBooks")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllBookAsync()
        {
            try
            {
                var result = await _bookBL.GetAllBookAsync();

                responseML.Success = true;
                responseML.Message = $"Books Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (BookException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }
    }
}
