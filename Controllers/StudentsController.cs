using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bhandari.API.Controllers
{
    // https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET:https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStuents()
        {
            string[] students = new string[] { "Sandeep", "Laxmi", "Suresh", "Shankar" };

            return Ok(students);
        }
    }
}
