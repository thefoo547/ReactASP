using App.Instructors;
using Microsoft.AspNetCore.Mvc;
using Persistence.DapperConn.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class InstructorController : BreveControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> GetAll()
        {
            return await Mediator.Send(new QueryAll.Execute());
        }
    }
}
