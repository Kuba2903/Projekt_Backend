using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UniversityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getUniversities")]

        public IActionResult GetUniversities()
        {
            var uni = _context.Universities.Select(x => new { x.Id, x.CountryId });

            return Ok(uni);
        }
    }
}
