using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class ShellPageViewModel : BaseViewModel
    {
        public static Frame ShellFrame { get; set; }

        private Command loadClientPageCommand;
        public Command LoadClientPageCommand {
            get 
            {
                return loadClientPageCommand ?? (loadClientPageCommand = new Command(obj => 
                {
                    ShellPageViewModel.ShellFrame.Navigate(new ClientPage());
                }));
            } 
        }
    }
}
