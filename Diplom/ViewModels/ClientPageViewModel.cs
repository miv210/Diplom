using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class ClientPageViewModel : BaseViewModel
    {


        private List<Client> clients = TestBdContext.GetContext().Clients.ToList(); 
        public List<Client> Clients 
        {
            get { return clients; }

            set { clients = value; } 
        }


        private Command newDogovorOpenCommand;
        public Command NewDogovorOpenCommand
        {
            get
            {
                return newDogovorOpenCommand ?? (newDogovorOpenCommand = new Command(obj =>
                {
                    NewDogovorWindow newDogovorWindow = new NewDogovorWindow ();
                    OpenDialogWindow(newDogovorWindow);
                }));
            }

        }
        private void OpenDialogWindow(Window wn)
        {
            wn.Owner = Application.Current.MainWindow;
            wn.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wn.ShowDialog();
        }



    }
}
