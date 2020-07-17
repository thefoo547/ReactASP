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
    }
}
