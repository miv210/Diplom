using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        private Command loadClientPageCommand;
        public Command LoadClientPageCommand
        {
            get
            {
                return loadClientPageCommand ?? (loadClientPageCommand = new Command(obj =>
                {
                    MainFrame.Navigate(new ClientPage());
                }));
            }
        }

        private Command loadZayvkiPageCommand;
        public Command LoadZayvkiPageCommand
        {
            get
            {
                return loadZayvkiPageCommand ?? (loadZayvkiPageCommand = new Command(obj =>
                {
                    MainFrame.Navigate(new ZayvkiPage());
                }));
            }
        }
        private Command minimaizeWindowCommand;
        public Command MinimaizeWindowCommand
        {
            get
            {
                return minimaizeWindowCommand ?? (minimaizeWindowCommand = new Command(obj => 
                { 
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                }));
            }
            set { minimaizeWindowCommand = value; }
        }

        private Command windowStateCommand;
        public Command WindowStateCommand
        {
            get
            {
                return windowStateCommand ?? (windowStateCommand = new Command(obj =>
                {
                    if(Application.Current.MainWindow.WindowState != WindowState.Maximized)
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Normal;
                    }
                }));
            }
            set { windowStateCommand = value; }
        }

        private Command closeWindowCommand;
        public Command CloseWindowCommand
        {
            get
            {
                return closeWindowCommand ?? (closeWindowCommand = new Command(obj =>
                {
                    Application.Current.Shutdown();
                }));
            }
            set { closeWindowCommand = value; }
        }

    }
}
