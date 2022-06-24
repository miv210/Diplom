using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diplom.ViewModels
{
    public class GeneralPageViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string TelethonNumber { get; set; }
        public string Address { get; set; }
        public int PersonalAccount { get; set; }
        public static Client client = null;
        public Command SaveNewClientCommand { get; set; }
        public Command BackCommand { get; set; }
        public GeneralPageViewModel()
        {
            
            SaveNewClientCommand = new Command(SaveNewClient);
            BackCommand = new Command(Back);
        }
        private async void SaveNewClient(object obj)
        {
            Window wnd = obj as Window;

            if (client == null)
            {
                client = new Client
                {
                    Login = Login,
                    Password = Password,
                    TelethonNumber = TelethonNumber,
                    Address = Address,
                    PersonalAccount = PersonalAccount,
                    IdPassportaNavigation = PassportPageViewModel.passport,
                    IdPassporta = PassportPageViewModel.passport.Id
                };
            }
            using (DiplomContext db = new DiplomContext())
            {
                db.Clients.Add(client);
                try
                {
                    await db.SaveChangesAsync();
                    MessageBox.Show("Информация сохранена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            Application.Current.Windows[4].Close();
        }
        private async void Back(object obj)
        {
            NewClientWindowViewModel.MainFrame.GoBack();
        }
    }
}
