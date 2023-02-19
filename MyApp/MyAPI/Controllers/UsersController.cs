using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MyAPI.MyJWT.Contracts;
using MyModels;
using MyPersistence.Contracts;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWT;
        private readonly IUserServiceRepository _serviceRepository;

        public UsersController(IJWTManagerRepository jWT, IUserServiceRepository serviceRepository)
        {
            this._jWT = jWT;
            this._serviceRepository = serviceRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(User user)
        {
            Token? token = _jWT.Authenticate(user);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };

            return users;
        }
    }
}
