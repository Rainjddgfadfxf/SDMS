using SDMS.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Abstract
{
    public interface IStudentRepository
    {
        //维修上报
        int ReportForRepair(Repair repair);
        //换寝上报
        int ChangeDorm(string StudentId, string newDormNum, string why);

    }
}
