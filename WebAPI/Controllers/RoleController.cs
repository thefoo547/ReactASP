using App.Secure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class RoleController : BreveControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Create(NewRole.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Unit>> Delete(DeleteRole.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<IdentityRole>>> QueryAll()
        {
            return await Mediator.Send(new QueryRoles.Execute());
        }

        [HttpPost("adduserrole")]
        public async Task<ActionResult<Unit>> AddRoleUser(UserRoleAdd.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("removeuserrole")]
        public async Task<ActionResult<Unit>> RemoveRoleUser(UserRoleRemove.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("userrolelist")]
        public async Task<ActionResult<List<string>>> UserRoleList(ListUserRole.Execute data)
        {
            return await Mediator.Send(data);
        }
    }
}
