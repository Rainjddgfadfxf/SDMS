using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDMS.Domain.Abstract;
namespace SDMS.Domain.Concrete
{
    public class EFAdminsRepository:IAdminsRepository
    {
        private SDMSEntities db = new SDMSEntities();
        public IEnumerable<Admin> Admins
        {
            get
            {
                return db.Admin;
            }
        }

        public IEnumerable<Student> Students
        {
            get
            {
                return db.Student;
            }
        }

        public IEnumerable<Staff> Staffs
        {
            get
            {
                return db.Staff;
            }
        }


        public int DeleteStaff(string Id)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Staff dbstaff = db.Staff.Find(Id);
                        if (dbstaff != null)
                        {
                            db.Staff.Remove(dbstaff);
                            db.SaveChanges();
                            return 1;//完成
                        }
                        return 0;//用户不存在
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }

        public int DeleteStudent(string Id)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Student dbstudent = db.Student.Find(Id);
                        if (dbstudent != null)
                        {
                            db.Student.Remove(dbstudent);
                            db.SaveChanges();
                            return 1;//完成
                        }
                        return 0;//用户不存在
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }

        public Staff SearchStaff(string Id)
        {
            Staff staff = db.Staff.Find(Id);
            if (staff != null) return staff;
            return null;
        }

        public IEnumerable<Staff> SearchStaffs(string name)
        {
            IEnumerable<Staff> staffs = db.Staff.Where(e => e.Name == name);
            if (staffs.Count() != 0) return staffs;
            return null;
        }

        public int EditStaff(Staff staff)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Staff dbstaff = db.Staff.Find(staff.Code);
                        if (dbstaff == null)
                        {
                            db.Staff.Add(staff);
                            db.SaveChanges();
                            return 1;//用户不存在 添加
                        }
                        else
                        {
                            dbstaff.Name = staff.Name;
                            dbstaff.Birth = staff.Birth;
                            dbstaff.Managenment = staff.Managenment;
                            dbstaff.Password = staff.Password;
                            dbstaff.Phone = staff.Phone;
                            dbstaff.Position = staff.Position;
                            dbstaff.Sex = staff.Sex;
                            return 0;//用户已存在 修改
                        }
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }
        public int DealChangeDorm(int Id, bool flag)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        ChangeDorm changeDorm = db.ChangeDorm.Find(Id);
                        if (changeDorm.AdminOpinion != null) return 0;//管理员已处理，无需处理
                        changeDorm.AdminOpinion = flag;
                        return 1;//处理完成  
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }
        public int EditStudent(Student student)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Student dbstudent = db.Student.Find(student.Code);
                        if (dbstudent == null)
                        {
                            db.Student.Add(student);
                            db.SaveChanges();
                            return 1;//用户不存在 新增
                        }
                        else
                        {
                            dbstudent.Class = student.Class;
                            dbstudent.DormNum = student.DormNum;
                            dbstudent.Name = student.Name;
                            dbstudent.Password = student.Password;
                            dbstudent.Phone = student.Phone;
                            dbstudent.Sex = student.Sex;
                        }
                        return 0;//用户已存在
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }
    }
}
