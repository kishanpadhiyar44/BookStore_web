using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System.Linq;
using System.Net;
using System;

namespace BookStore.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new Repository.UserRepository();

         [HttpGet]
         [Route("GetUsers")]
         public IActionResult GetUser()
         {

             return Ok(_repository.GetUsers());
         }

         [HttpPost]
         [Route("Login")]
         public IActionResult Login(LoginModel model)
         {
             User user = _repository.Login(model);
             if(user == null)
             {
                 return NotFound();
             }
             else
             {
                 return Ok(user);
             }
         }

         [HttpPost]
         [Route("RegisterUser")]
         public IActionResult Register(RegisterModel model)
         {
            User user = _repository.Register(model);
             if(user == null)
             {
                 return BadRequest();
             }
             else
             {
                 return Ok(user);
             }
         }
    
        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            ListResponse<User> response = _repository.GetUsers(pageIndex, pageSize, keyword);
            ListResponse<UserModel> users = new ListResponse<UserModel>()
            {
                Results = response.Results.Select(u => new UserModel(u)),
                TotalRecords = response.TotalRecords,
            };

            return Ok(users);

        }
       
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            User user = _repository.GetUser(id);
            if (user == null)
                return NotFound();

            UserModel userModel = new UserModel(user);
            return Ok(userModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(UserModel model)
        {
            if(model != null)
            {
                return BadRequest();
            }

            User user = new User()
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Roleid = model.Roleid,
            };

            user = _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            bool isDeleted = _repository.DeleteUser(id);
            return Ok(isDeleted);
        }

        [HttpGet]
        [Route("roles")]
        public IActionResult GetRoles(RoleModel role)
        {
            try
            {
                ListResponse<Role> roles = _repository.GetRoles(role);
                ListResponse<RoleModel> RoleList = new ListResponse<RoleModel>()
                {
                    Results = roles.Results.Select(x => new RoleModel(role)).ToList(),
                    TotalRecords = roles.TotalRecords
                };
                return Ok(RoleList) ;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
