using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO_s
{
    public class UniversityRankingYearDTO
    {
        public int? UniversityId { get; set; }

        public int? RankingCriteriaId { get; set; }

        public int? Year { get; set; }

        public int? Score { get; set; }
    }
}
