using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using View;
using ViewModel;

namespace View
{
    public class ViewManager : IViewManager
    {
        VMManager vMManager;
        private Dictionary<ViewModelBase, Window> VMsToWidnows;

        public ViewManager()
        {

            VMsToWidnows = new Dictionary<ViewModelBase, Window>();

            vMManager = new VMManager();
            //vMManager.ReadyToWorkHandler += Run;

            var VM = vMManager.CreateMainVM();
            VMsToWidnows[VM] = new MainWindow() { DataContext = VM };
            RunMainWindow();
        }

        private void RunMainWindow()
        {
            //var f = VMsToWidnows[vm];
            foreach (var VMB in VMsToWidnows.Keys)
            {
                if (VMB.GetType().Name.Equals("MainViewModel"))
                {
                    VMsToWidnows[VMB].Show();
                }
            }
        }

        private void Run(object o, EventArgs e)
        {
            var mainVM = (MainViewModel)o;

            var mainWindow = new MainWindow
            {
                DataContext = mainVM
            };
            mainWindow.SubscribeToCollectionChanged();

            mainWindow.Show();
        }
    }
}
