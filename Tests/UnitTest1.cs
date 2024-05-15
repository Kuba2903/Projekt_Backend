using Core.DTO_s;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text.Json;

namespace Tests
{
    public class UniversitiesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UniversitiesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        private void SeedDatabase(AppDbContext context)
        {

            context.Countries.RemoveRange(context.Countries);
            context.Universities.RemoveRange(context.Universities);
            context.RankingCriteria.RemoveRange(context.RankingCriteria);
            context.UniversityRankingYears.RemoveRange(context.UniversityRankingYears);


            var country = new Country { Id = 1, CountryName = "TestCountry" };
            context.Countries.Add(country);


            var university = new University { Id = 1, CountryId = 1, UniversityName = "Test University" };
            context.Universities.Add(university);

  
            var rankingCriteria1 = new RankingCriterion { Id = 1, CriteriaName = "Criteria 1" };
            var rankingCriteria2 = new RankingCriterion { Id = 2, CriteriaName = "Criteria 2" };
            context.RankingCriteria.AddRange(rankingCriteria1, rankingCriteria2);


            var universityRanking1 = new UniversityRankingYear
            {
                UniversityId = 1,
                RankingCriteriaId = 1,
                Year = 2020,
                Score = 85
            };
            var universityRanking2 = new UniversityRankingYear
            {
                UniversityId = 1,
                RankingCriteriaId = 2,
                Year = 2020,
                Score = 90
            };
            context.UniversityRankingYears.AddRange(universityRanking1, universityRanking2);

            context.SaveChanges();
        }

        [Fact]
        public async Task GetUniversities_ReturnsOkResult_WithUniversities()
        {

            using var context = GetInMemoryDbContext();
            SeedDatabase(context);

            var countryName = "TestCountry";
            var url = $"/api/getUniversities/{countryName}?pageNumber=1&pageSize=5";

            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<UniversityDTO>>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test University", result[0].UniversityName);


        }
    }
}