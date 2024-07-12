using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.CustomException;

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
    }
}
