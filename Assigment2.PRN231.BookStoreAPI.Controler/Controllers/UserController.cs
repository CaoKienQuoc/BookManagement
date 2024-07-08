using Assigment2.PRN231.BookStoreAPI.Repositories.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PRN231_Group.Assigment.API.Repo.Interface;

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _unitOfWork.UserRepository.Get(x => x.EmailAddress == loginModel.Email && x.PasswordHash == loginModel.Password).FirstOrDefault();
            if (user == null)
                return Unauthorized("Invalid email or password");

            // Optionally, you can return additional user information if needed
            return Ok(user);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Since there's no session or token to invalidate, simply return a success message
            return Ok("Logout successful");
        }
    }

}

