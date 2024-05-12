using AutoMapper;
using Core.DTO_s;
using Core.Models;
using Core.Services;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversity _service;
        private readonly IMapper mapper;
        public UniversityController(IUniversity service, IMapper mapper)
        {
            _service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("getUniversities")]

        public IActionResult GetUniversities(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("pageSize or pageNumber cannot be less than 1");

            var uni = _service.GetAll<University>(pageSize,pageNumber);

            return Ok(uni.Select(mapper.Map<UniversityDTO>));
        }
    }
}
