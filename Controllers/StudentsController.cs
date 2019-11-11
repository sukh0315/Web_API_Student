using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Student.BusinessLayer;
using Web_API_Student.Models;

namespace Web_API_Student.Controllers
{
    //Student API controller.
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Web_API_StudentDBContext _context;

        public StudentsController(Web_API_StudentDBContext context)
        {
            _context = context;
        }

        // GET: api/Students
        //Gets all the students using a linq query
        [HttpGet]
        public IEnumerable<Student> GetStudent()
        {
            return (from student in _context.Student select student).ToList();
        }

        // GET: api/Students/5
        //Get student details using a linq query
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = (from students in _context.Student
                           where students.Id == id
                           select students).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        //Update student
        [HttpPut("{id}")]
        public IActionResult PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        //Adds a new student to the database.
        [HttpPost]
        public IActionResult PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Student.Add(student);
             _context.SaveChanges();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        //Removes the student uses a linq query to fetch a student.
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = (from students in _context.Student
                           where students.Id == id
                           select students).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
             _context.SaveChanges();

            return Ok(student);
        }

        //Uses a lamda query to check the existance of a given student with id.
        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}