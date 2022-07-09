using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
        [ApiController]
        [Route("api/public")]
        public class BookStoreController : Controller
        {
            public UserRepository _repository = new UserRepository();

            [HttpGet]
            [Route("GetUsers")]
            public IActionResult GetUser()
            {
                 return Ok(_repository.GetUsers());
            }

            [HttpPost]
            [Route("login")]
            public IActionResult Login(LoginModel model)
            {
                User user = _repository.Login(model);
                if (user == null)
                    return NotFound();

                UserModel userModel = new UserModel(user);
                return Ok(userModel);
            }

            [HttpPost]
            [Route("register")]
            public IActionResult Register(RegisterModel model)
            {
                User user = _repository.Register(model);
                UserModel userModel = new UserModel(user);
                return Ok(userModel);
            }
        }
}
