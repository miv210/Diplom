using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        User authUs = null;
        public string Login { get; set; }
        public string Password { private get; set; }

        private Command loginCommand;
        public Command LoginComand {
            get
            {
                return loginCommand?? (loginCommand = new Command(obj =>
                {
                    using (TestBdContext db = new TestBdContext())
                    {
                        authUs = db.Users.Where(b => b.Login == Login && b.Password == Password).FirstOrDefault();
                        //authWorker = db.Admins.Where(b => b.AdminLogin == login && b.AdminPassword == pass).FirstOrDefault();
                    }

                    if (authUs != null)
                    {
                        MainWindowViewModel.MainFrame.Navigate(new ShellPage());
                    }
                    else
                        MessageBox.Show("«Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                }));
            }
            
        }
    }
}
