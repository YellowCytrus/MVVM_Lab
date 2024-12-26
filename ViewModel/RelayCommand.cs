using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {
        #region Fields
        public event EventHandler CanExecuteChanged;

        private event Action _execute;
        private event Func<bool> _canExecute;
        #endregion

        #region Constructors
        public RelayCommand(Action Execute)
        {
            _execute = Execute;
        }

        public RelayCommand(Action Execute, Func<bool> CanExecute)
        {
            _execute = Execute;
            _canExecute = CanExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
        #endregion
    }
}
