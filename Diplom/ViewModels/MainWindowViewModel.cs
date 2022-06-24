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
        public string Surnname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public static int idOperatora = 0;
        Operator oper { get; set; }
        public MainWindowViewModel()
        {
            
            if(idOperatora != 0)
            {
                DiplomContext.GetContext().Operators.Where(u => u.Id == idOperatora).FirstOrDefault();
                Surnname = oper.Surname;
            }
                
        }
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
