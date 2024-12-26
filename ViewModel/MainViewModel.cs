using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using Ninject;
using ModelConfig;
using System.Windows.Controls;
using Shared;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IModel _model;
        private bool _isEditing = true;

        public RelayCommand AddNewStudentCommand { get; set; }
        public RelayCommand DeleteStudentCommand { get; set; }
        public RelayCommand SaveChangesCommand { get; }

        public ObservableCollection<Student> Students { get; }


        public MainViewModel()
        {
            Students = new ObservableCollection<Student>();

            IKernel ninjectKernel;

            ninjectKernel = new StandardKernel(new ModelConfigModule());
            _model = ninjectKernel.Get<IModel>();

            SaveChangesCommand = new RelayCommand(SaveChanges);
            AddNewStudentCommand = new RelayCommand(PrepareToAddStudent);
            DeleteStudentCommand = new RelayCommand(DeleteStudent);

            UpdateStudentsCollection();
        }

        private void UpdateStudentsCollection()
        {
            Students.Clear();
            List<string[]> strings = _model.GetAllIdNameSpecGroup();
            foreach (string[] str in strings)
            {
                Students.Add(new Student()
                {
                    Id = Convert.ToInt32(str[0]),
                    Name = str[1],
                    Group = str[3],
                    Speciality = str[2]
                });
            }
        }

        private Student _currentStudent;
        public Student CurrentStudent
        {
            get => _currentStudent;
            set
            {
                _currentStudent = value;
                if (value != null)
                {
                    StudentInEditWindow = new Student()
                    {
                        Id = Convert.ToInt32(_currentStudent.Id),
                        Name = _currentStudent.Name,
                        Speciality = _currentStudent.Speciality,
                        Group = _currentStudent.Group
                    };
                }

                OnPropertyChanged("CurrentStudent");
            }
        }

        private Student _studentInEditWindow;
        public Student StudentInEditWindow
        {
            get => _studentInEditWindow;
            set
            {
                _studentInEditWindow = value;
                OnPropertyChanged("StudentInEditWindow");
            }
        }


        private void SaveChanges()
        {
            if (_studentInEditWindow != null && _currentStudent != null)
            {
                if (_isEditing)
                {
                    EditStudent();
                    _isEditing = true;
                }
                else
                {
                    AddStudent();
                    _isEditing = true;
                }

                UpdateStudentsCollection();
            }
        }

        private void EditStudent()
        {
            StudentEventArgs argsFrom = new StudentEventArgs()
            {
                Name = CurrentStudent.Name,
                Speciality = CurrentStudent.Speciality,
                Group = CurrentStudent.Group
            };
            StudentEventArgs argsTo = new StudentEventArgs()
            {
                Name = StudentInEditWindow.Name,
                Speciality = StudentInEditWindow.Speciality,
                Group = StudentInEditWindow.Group
            };

            try
            {
                _model.EditStudent(argsFrom, argsTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CurrentStudent = new Student()
            {
                Id = CurrentStudent.Id,
                Name = StudentInEditWindow.Name,
                Group = StudentInEditWindow.Group,
                Speciality = StudentInEditWindow.Speciality
            };
        }

        private void PrepareToAddStudent()
        {
            _isEditing = false;
            CurrentStudent = new Student();
        }

        private void AddStudent()
        {
            StudentEventArgs args = new StudentEventArgs()
            {
                Name = StudentInEditWindow.Name,
                Speciality = StudentInEditWindow.Speciality,
                Group = StudentInEditWindow.Group
            };

            try
            {
                _model.AddStudent(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteStudent()
        {
            var result = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _model.DeleteStudent(CurrentStudent.Id);
                UpdateStudentsCollection();
            }
        }
    }
}
