//using DomainObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ViewModel
{
    public class Student : ViewModelBase
    {

        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string speciality;
        public string Speciality
        {
            get { return speciality; }
            set
            {
                speciality = value;
                OnPropertyChanged("Speciality");
            }
        }

        private string group;
        public string Group
        {
            get { return group; }
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Student student)
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
            int hashCode = -652161762;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Speciality);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Group);
            return hashCode;
        }
    }
}

