using SDMS.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Abstract
{
    public interface IAdminsRepository
    {
        IEnumerable<Admin> Admins { get; }
        IEnumerable<Student> Students { get; }
        IEnumerable<Dorm> Dorms { get; }
        //修改学生
        int EditStudent(Student student);
        //删除学生
        int DeleteStudent(string Id);
        //换寝上报处理
        int DealChangeDorm(int Id, bool flag,string why);
        //维修上报处理
        int DealRepair(int Id, decimal money, string ReasonsForUncompletion = "无");
        int EditDorm(Dorm dorm);
        int DeleteDorm(String Id);
    }
}
