using BasicWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BasicWebApp.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext _studentDbContext;

        public StudentController(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        [HttpGet("GetStudentsList")]
        public async Task<IEnumerable<Student>> GetStudentsList()
        {
            try
            {
               return await _studentDbContext.Students.ToListAsync();
            }
            catch
            {
                return new List<Student>();
            }
        }

        [HttpPost("AddStudentsList")]
        public async Task<ActionResult<Student>> AddStudentsList(Student student)
        {
            _studentDbContext.Students.Add(student);
            _studentDbContext.SaveChanges();
            return student;

        }

        [HttpPut("UpdateStudent")]
        public async Task<ActionResult<Student>> UpdateStudent(Student student)
        {
            _studentDbContext.Entry(student).State= EntityState.Modified;
            _studentDbContext.SaveChanges();
            return student;

        }

        [HttpGet("GetStudent/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var s=await _studentDbContext.Students.FindAsync(id);
            if (s == null)
            {
                return NotFound();
            }
            return s;

        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var s=await _studentDbContext.Students.FindAsync(id);
            if (s != null)
            {
                _studentDbContext.Students.Remove(s);
                _studentDbContext.SaveChanges();
                return NoContent();
            }
            return NotFound();

        }
    }
}
