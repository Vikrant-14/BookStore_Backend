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
    public class UserController : ControllerBase
    {
        public IUserBL _userBL { get; }
        public ResponseML responseML;

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
            responseML = new ResponseML();
        }


        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUserAsync(UserML model)
        {
            try
            {
                var result = await _userBL.RegisterUserAsync(model,"User");

                responseML.Success = true;
                responseML.Message = "User register successfully";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch(UserException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(400, responseML);
            }
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdminAsync(UserML model)
        {
            try
            {
                var result = await _userBL.RegisterUserAsync(model, "Admin");

                responseML.Success = true;
                responseML.Message = "User register successfully";
                responseML.Data = result;

                return StatusCode(201, responseML);
            }
            catch (UserException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(400, responseML);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginML model)
        {
            try
            {
                var result = await _userBL.LoginAsync(model);

                responseML.Success = true;
                responseML.Message = "Login successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(UserException ex) 
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(400, responseML);
            }
        }

        [HttpGet("getUserById/{id}")]
        [Authorize(Roles ="User, Admin")]
        public async Task<ActionResult> GetUserbyId(int id)
        {
            try
            {
                var result = await _userBL.GetUserbyId(id);

                responseML.Success = true;
                responseML.Message = $"User ID : {id} fetched successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (UserException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }
    }
}
