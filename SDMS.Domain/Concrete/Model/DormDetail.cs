using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Concrete.Model
{
    public class DormDetail
    {
        public IEnumerable<Student> students { get; set; }
        public Dorm dorm { get; set; }
        public IEnumerable<WaterAndElectricity> waterAndElectricity { get; set; }
        public IEnumerable<DormHygiene> dormHygienes { get; set; }
    }
}
