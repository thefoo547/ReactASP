using App.Secure;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebAPI.Controllers

{
    [AllowAnonymous]
    public class UserController : BreveControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login.LoginRequest request) => await Mediator.Send(request);

        [HttpPost("signup")]
        public async Task<ActionResult<UserData>> Signup(Register.Signup request) => await Mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<UserData>> GetUser()
        {
            return await Mediator.Send(new ActualUser.Execute());
        }
    }
}
