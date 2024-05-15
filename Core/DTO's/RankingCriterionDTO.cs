using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO_s
{
    public class RankingCriterionDTO
    {
        public int Id { get; set; }

        public int? RankingSystemId { get; set; }

        public string? CriteriaName { get; set; }
    }
}
