using App.Instructors;
using MediatR;
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
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewInstructor.Execute data)
        {
            return await Mediator.Send(data);
        } 
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(EditInstructor.Execute data, Guid id)
        {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(DeleteInstructor.Execute data, Guid id)
        {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }
        [HttpGet("{id}")]
        public async Task<InstructorModel> GetById(QueryId.Execute data, Guid id)
        {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }
    }
}
