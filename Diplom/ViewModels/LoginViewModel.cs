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
        public bool DialogRes { get; set; }

        private Command loginCommand;
        public Command LoginComand {
            get
            {
                return loginCommand?? (loginCommand = new Command(obj =>
                {
                    Window wnd = obj as Window;

                    using (TestBdContext db = new TestBdContext())
                    {
                        authUs = db.Users.Where(b => b.Login == Login && b.Password == Password).FirstOrDefault();
                    }

                    if (authUs != null)
                    {
                        wnd.Close();
                    }
                    else
                        MessageBox.Show("«Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
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
                    if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
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
