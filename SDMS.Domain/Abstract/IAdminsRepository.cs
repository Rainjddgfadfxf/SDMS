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
        IEnumerable<Staff> Staffs { get; }
        //修改学生
        int EditStudent(Student student);
        //删除学生
        int DeleteStudent(string Id);
        //修改工作人员
        int EditStaff(Staff staff);
        //删除工作人员
        int DeleteStaff(string Id);
        //工作人员查询 根据Id
        Staff SearchStaff(string Id);
        //工作人员查询 根据name
        IEnumerable<Staff> SearchStaffs(string name);


    }
}
