﻿using App.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.DapperConn.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class CoursesController : BreveControllerBase
    {
        // GET: api/Courses

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            return await Mediator.Send(new QueryAll.CoursesList());
        }

        // GET: api/Courses/5

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(Guid id)
        {
            var course = await Mediator.Send(new QueryId.CourseById { Id = id });

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]

        public async Task<ActionResult<Unit>> PutCourse(Guid id, EditCourse.Update course)
        {
            course.CourseId = id;

            return await Mediator.Send(course);
        }

        // POST: api/Courses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]

        public async Task<ActionResult<Unit>> PostCourse(NewCourse.Create newcourse)
        {
            return await Mediator.Send(newcourse);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCourse(Guid id)
        {
            return await Mediator.Send(new RemoveCourse.Delete() { CourseId = id });
        }

        private bool CourseExists(int id)
        {
            //return _context.Courses.Any(e => e.CourseId == id);

            throw new NotImplementedException();
        }

        [HttpPost("Report")]
        public async Task<ActionResult<PaginationModel>> Report(PaginateCourses.Execute data)
        {
            return await Mediator.Send(data);
        }
    }
}
