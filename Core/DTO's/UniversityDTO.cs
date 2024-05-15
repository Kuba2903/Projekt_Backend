using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO_s
{
    public class UniversityDTO
    {
        public int Id { get; set; }

        public int? CountryId { get; set; }

        public string? UniversityName { get; set; }
        public List<ScoreDto> Scores { get; set; }
    }
}
