using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Abstract
{
    public interface IStaffRepository
    {
        //维修上报处理
        int DealRepair(int Id, decimal money, string ReasonsForUncompletion = "无");
        //换寝上报处理
        int DealChangeDorm(int Id, bool flag);
        
    }
}
