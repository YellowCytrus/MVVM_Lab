using DataAccessLayer;
using DomainObjects;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Ninject.Infrastructure.Language;

namespace Blogic
{
    delegate void randHandler();


    public class Logic : IModel
    {
        public IRepository<Student> Repository { get; set; }

        public event Action<IEnumerable<StudentEventArgs>> DataChanged;

        public Logic(IRepository<Student> repository)
        {
            Repository = repository;
            
            //AddStudent(new string[] { "", "таня лейка", "программист", "Ramestein" });
            //AddStudent(new string[] { "", "маша чашка", "программист", "ДДТ" });
            //AddStudent(new string[] { "", "галя пилорама", "дизайнер", "Дайте танк(!)" });
            //AddStudent(new string[] { "", "коля пароплан", "программист", "Крови" });
            //AddStudent(new string[] { "", "владик водопадик", "программист", "Кино" });
            //AddStudent(new string[] { "", "миша SSD", "тестировщик", "Twenty One Pilots" });
            //AddStudent(new string[] { "", "саня HDMI", "дизайнер", "Вокалоиды" });
        }

        public void CallDataChanged()
        {
            DataChanged?.Invoke(GetData());
        }


        public void AddStudent(StudentEventArgs arg)
        {
            Student student = new Student()
            {
                Id = arg.Id,
                Name = arg.Name,
                Group = arg.Group,
                Speciality = arg.Speciality
            };

            if (!GetAllStudents().Contains(student))
            {
                Repository.Create(student);
            }
            else
            {
                throw new Exception("object already contains");
            }
            DataChanged?.Invoke(GetData());
        }


        public bool EditStudent(StudentEventArgs from, StudentEventArgs to)
        {
            Student studentFrom = new Student()
            {
                Id = from.Id,
                Name = from.Name,
                Speciality = from.Speciality,
                Group = from.Group,
            };

            int studentFromId = Find(studentFrom);

            Student studentTo = new Student()
            {
                Id = to.Id,
                Name = to.Name,
                Speciality = to.Speciality,
                Group = to.Group,
            };

            if (!GetAllStudents().Contains(studentTo))
            {
                Repository.Update(studentFromId, studentTo);
                DataChanged?.Invoke(GetData());
            }
            else
            {
                return false;
            }
            return true;
        }


        public void DeleteStudent(int Id)
        {
            try
            {
                Repository.Delete(Id);
                DataChanged?.Invoke(GetData());
            }
            catch
            {
                throw new Exception("can not delete student");
            }
        }

        public string[] GetById(int Id)
        {
            Student student = Repository.GetById(Id);
            string[] IdNameSpecGroup = new string[]
            {
                student.Id.ToString(),
                student.Name,
                student.Speciality,
                student.Group
            };
            return IdNameSpecGroup;
        }


        public List<string[]> GetAllIdNameSpecGroup()
        {
            List<Student> students = GetAllStudents();
            List<string[]> IdNameSpecGroups = new List<string[]>();

            for (int i = 0; i < students.Count; i++)
            {
                IdNameSpecGroups.Add(Convert(students[i]));
            }
            return IdNameSpecGroups;
        }


        private Student Convert(string[] IdNameSpecGroup)
        {
            Student student = new Student()
            {
                // Id = AutoNum
                Name = IdNameSpecGroup[1],
                Speciality = IdNameSpecGroup[2],
                Group = IdNameSpecGroup[3]
            };
            return student;
        }


        private string[] Convert(Student student)
        {
            string[] IdNameSpecGroup = new string[]
            {
                student.Id.ToString(),
                student.Name,
                student.Speciality,
                student.Group
            };
            return IdNameSpecGroup;
        }


        private int Find(Student student)
        {
            List<Student> students = GetAllStudents();
            foreach (Student studentInDB in students)
            {
                if (student.Name == studentInDB.Name &&
                    student.Speciality == studentInDB.Speciality &&
                    student.Group == studentInDB.Group)
                {
                    return System.Convert.ToInt32(studentInDB.Id);
                }
            }
            throw new Exception("not found");
        }


        private List<Student> GetAllStudents()
        {
            List<Student> students = Repository.GetAll().ToList();
            return students;
        }

        private IEnumerable<StudentEventArgs> GetData()
        {
            List<StudentEventArgs> studentEventArgs = new List<StudentEventArgs>();

            List<Student> students = Repository.GetAll().ToList();
            foreach (Student student in students)
            {
                StudentEventArgs arg = new StudentEventArgs()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Speciality = student.Speciality,
                    Group = student.Group
                };
                studentEventArgs.Add(arg);
            }

            return studentEventArgs;
        }
    }
}
