using MvvmHelpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class LoginViewModel : BaseViewModel
    {  
        public string Login { get; set; }
        public string Password { private get; set; }        
        public Command LoginCommand { get; set;}
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            
        }
        private async void OnLoginClicked(object obj)
        { 
            Window wnd = obj as Window;
            Operator authUs = null;

            using (DiplomContext db = new DiplomContext())
            {
                authUs = await db.Operators.Where(b => b.Login == Login.Trim() && b.Password == Password.Trim()).FirstOrDefaultAsync();
            }

            if (authUs != null)
            {
                MainWindowViewModel.idOperatora = authUs.Id;
                Application.Current.MainWindow.Show();
                wnd.Hide();
            }
            else
                MessageBox.Show("«Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
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
