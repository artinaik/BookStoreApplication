using BookStoreManager.Interface;
using BookStoreModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserManager userManager;
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        
        [HttpPost]
        [Route("Register")]
        public IActionResult Registration([FromBody]UserRegistration user)
        {
            try
            {
                var result = userManager.Register(user);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", Response = user });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Unuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }          
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel login)
        {
            try
            {
                string token = userManager.Login(login);
                if (token!=null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull",Token=token});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Incorrect Email and Password" });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
