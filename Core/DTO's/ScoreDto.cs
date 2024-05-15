using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO_s
{
    public class ScoreDto
    {
        public int Year { get; set; }
        public int Score { get; set; }
        public string CriteriaName { get; set; }
        public int RankingCriteriaId { get; set; }
    }
}
