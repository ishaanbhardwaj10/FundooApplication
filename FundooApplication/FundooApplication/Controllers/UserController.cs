using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost("UserRegistration")]
        //[Route("UserRegistration")]   //name for the particular method in request url
        public IActionResult UserRegistration(UserPostModel userPostModel)
        {
            try
            {
                var response = this.userBusiness.UserRegistration(userPostModel);
                if (response != null) 
                {
                    return this.Ok(new {success = true, message = "registration successful", data = response});
                }
                else
                {
                    return this.BadRequest(new {success = false, message = "registration failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UserLogin")]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var response = this.userBusiness.UserLogin(userLoginModel);
                if(response != null)
                {
                    return this.Ok(new { success = true, message = "login successful", token = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "login failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var response = this.userBusiness.ForgotPassword(email);
            if(response != null)
            {
                return this.Ok(new { success = true, message = "forgot password request recieved", data = response });
            }
            else
            {
                return this.BadRequest(new { success = true, message = "operation failed"});
            }
        }


        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetUserPassword(string password, string confirmPassword)
        {
            try
            {
                string email = this.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value;
                var response = this.userBusiness.ResetUserPassword(email, password, confirmPassword);
                if (response)
                {
                    return this.Ok(new { success = true, message = "password reset successful", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "password reset failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
