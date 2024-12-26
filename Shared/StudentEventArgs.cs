using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class StudentEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string Group { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is StudentEventArgs student)
            {
                bool equals = Name == student.Name &&
                              Speciality == student.Speciality &&
                              Group == student.Group;
                return equals;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 26082846;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Speciality);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Group);
            return hashCode;
        }
    }
}
