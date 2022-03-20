using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        
        

        
       
    }
}
