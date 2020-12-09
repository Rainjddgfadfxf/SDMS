using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDMS.Domain.Abstract;
using SDMS.Domain.Concrete.Model;

namespace SDMS.Domain.Concrete
{
    public class EFCommonRepository : ICommonRepository
    {
        private SDMSEntities db = new SDMSEntities();
        public IEnumerable<Repair> Repairs
        {
            get
            {
                return db.Repair;
            }
        }
        public IEnumerable<Student> Students
        {
            get
            {
                return db.Student;
            }
        }

        public IEnumerable<ChangeDorm> changeDorms
        {
            get
            {
                return db.ChangeDorm;
            }
        }

        public IEnumerable<ChangeDorm> SearchChangeDorm(Student student)
        {
            return db.ChangeDorm.Where(e => e.StudentId == student.Code);
        }

        public  DormDetail SearchDorm(string Id)
        {
            Dorm dorm = db.Dorm.Find(Id);
            IEnumerable<WaterAndElectricity> waterAndElectricity = db.WaterAndElectricity.Where(e => e.DormNum == dorm.DormNum);
            IEnumerable<Student> students = db.Student.Where(e => e.DormNum == dorm.DormNum);
            return new DormDetail
            {
                dorm = dorm,
                waterAndElectricity = waterAndElectricity,
                students = students
            };

        }

        public IEnumerable<Repair> SearchRepairs(string DormId)
        {
            return db.Repair.Where(e => e.DormNum == DormId);
        }

        public Student SearchStudent(string Id)
        {
            return db.Student.Find(Id);
        }

        public IEnumerable<Student> SearchStudents(string name)
        {
            return db.Student.Where(e => e.Name == name);
        }

        public IEnumerable<Dorm> SpareDorm()
        {
            return db.Dorm.Where(e => e.UsedNum ==e.AllowNum);
        }
    }
}
