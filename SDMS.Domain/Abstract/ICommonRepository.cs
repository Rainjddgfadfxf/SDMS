using SDMS.Domain.Concrete;
using SDMS.Domain.Concrete.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Abstract
{
    public interface ICommonRepository
    {
        //寝室查询 //返回类型需要修改  自定义model类 需要学生list，水电，寝室
        DormDetail SearchDorm(string Id);
        //学生查询
        Student SearchStudent(string Id);
        //学生查询
        IEnumerable<Student> SearchStudents(String name);
        //空余寝室
        IEnumerable<Dorm> SpareDorm();
        //维修上报查询
        IEnumerable<Repair> SearchRepairs(string DormId);
        //换寝上报查询
        IEnumerable<ChangeDorm> SearchChangeDorm(Student student);
        //维修上报查询  管理员和工作人员用
        IEnumerable<Repair> Repairs { get; }
        //换寝上报查询  管理员和工作人员用
        IEnumerable<Student> Students { get; }
        //所有换寝上报
        IEnumerable<ChangeDorm> changeDorms { get; }
    }
}
