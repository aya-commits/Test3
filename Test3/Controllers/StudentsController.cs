using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test3.Common.Services;
using Test3.Domain.Models;

namespace Test3.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Post([FromBody]Student student)
        {
            var addedStudent = _studentService.AddStudent(student);
            return CreatedAtAction(nameof(Get), new { id = addedStudent.Id }, addedStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}