using AutoMapper;
using Core.DTO_s;
using Core.Models;

namespace API
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Country,CountryDTO>();
            CreateMap<RankingCriterion,RankingCriterionDTO>();
            CreateMap<RankingSystem, RankingSystemDTO>();
            CreateMap<University, UniversityDTO>();
            CreateMap<UniversityRankingYear, UniversityRankingYearDTO>();
            CreateMap<UniversityYear, UniversityYearDTO>();


            CreateMap<CountryDTO,Country>();
            CreateMap<RankingCriterionDTO,RankingCriterion>();
            CreateMap<RankingSystemDTO,RankingSystem>();
            CreateMap<UniversityDTO,University>();
            CreateMap<UniversityRankingYearDTO,UniversityRankingYear>();
            CreateMap<UniversityYearDTO,UniversityYear>();
        }
    }
}
