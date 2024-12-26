using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EntityFrameworkRepository : IRepository<Student>
    {
        private readonly Context _context;

        public EntityFrameworkRepository()
        {
            _context = new Context();
        }

        public void Create(Student item)
        {
            _context.Set<Student>().Add(item);
            _context.SaveChanges();
        }

        public void Update(int _studentFromId, Student _studentTo)
        {
            Student studentTo = _studentTo;

            Student existingObj = _context.Students.Find(_studentFromId);
            existingObj.Name = studentTo.Name;
            existingObj.Speciality = studentTo.Speciality;
            existingObj.Group = studentTo.Group;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Students.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Set<Student>();
        }

        public Student GetById(int id)
        {
            Student existingObj = _context.Students.Find(id);
            return existingObj;
        }
    }
}
