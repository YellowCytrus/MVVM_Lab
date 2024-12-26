using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class VMManager : IVMManager
    {
        MainViewModel mainViewModel;
        public EventHandler<EventArgs> ReadyToWorkHandler;

        public VMManager()
        {
        }

        public ViewModelBase CreateMainVM()
        {
            mainViewModel = new MainViewModel();
            //mainViewModel.ReadyToWorkHandler += CallReadyToWork;
            return mainViewModel;
        }

        private void CallReadyToWork(object o, EventArgs args)
        {
            ReadyToWorkHandler.Invoke(this, new EventArgs());
        }
    }
}
