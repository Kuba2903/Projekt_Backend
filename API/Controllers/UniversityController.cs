using AutoMapper;
using Core.DTO_s;
using Core.Models;
using Core.Services;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversity _service;
        private readonly IMapper mapper;
        private readonly AppDbContext _context;
        public UniversityController(IUniversity service, IMapper mapper, AppDbContext context)
        {
            _service = service;
            this.mapper = mapper;
            _context = context;

        }

        [HttpGet]
        [Route("getUniversities/{countryName}")]
        public IActionResult GetUniversities([FromRoute] string countryName, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("pageSize or pageNumber cannot be less than 1");

            var dto = new CountryDTO { CountryName = countryName };

            var country2 = _context.Countries.SingleOrDefault(x => x.CountryName == dto.CountryName);
            var countryDb = _service.GetAll<Country>(pageSize, pageNumber);
            var uni = _service.GetAll<University>(pageSize, pageNumber);
            var uniRanking = _service.GetAll<UniversityRankingYear>(pageSize, pageNumber);
            var rankingCriterion = _service.GetAll<RankingCriterion>(pageSize, pageNumber);

            if (countryDb != null && country2 != null)
            {
                var query =
                            from university in uni
                            join university_ranking in uniRanking on university.Id equals university_ranking.UniversityId
                            join criterion in rankingCriterion on university_ranking.RankingCriteriaId equals criterion.Id
                            where university.CountryId == country2.Id
                            select new
                            {
                                uniId = university.Id,
                                uniName = university.UniversityName,
                                year = university_ranking.Year,
                                score = university_ranking.Score,
                                criteria = criterion.CriteriaName
                            };

                var paginatedQuery = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                var result = paginatedQuery
                    .GroupBy(x => new { x.uniId, x.uniName })
                    .Select(g => new
                    {
                        universityId = g.Key.uniId,
                        universityName = g.Key.uniName,
                        scores = g.Select(x => new
                        {
                            year = x.year,
                            score = x.score,
                            criteriaName = x.criteria
                        }).ToList()
                    })
                    .ToList();

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

            
        }


        [HttpPost]
        [Route("/api/universities/{id}/scores")]
        public IActionResult AddResult([FromRoute] int id, UniversityRankingYearDTO dto)
        {

            var university = _context.Universities.FirstOrDefault(x => x.Id == id);

            if(university != null)
            {
                UniversityRankingYear entity = new UniversityRankingYear()
                {
                    UniversityId = university.Id,
                    RankingCriteriaId = dto.RankingCriteriaId,
                    Score = dto.Score,
                    Year = dto.Year,
                };

                _context.UniversityRankingYears.Add(entity);
                _context.SaveChanges();

                return Created();
            }
            else
            {
                return BadRequest();
            }
        }

    }
 }

