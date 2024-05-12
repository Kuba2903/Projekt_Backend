using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO_s
{
    public class UniversityYearDTO
    {
        public int? UniversityId { get; set; }

        public int? Year { get; set; }

        public int? NumStudents { get; set; }

        public decimal? StudentStaffRatio { get; set; }

        public int? PctInternationalStudents { get; set; }

        public int? PctFemaleStudents { get; set; }
    }
}
