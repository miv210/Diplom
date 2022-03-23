using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diplom.ViewModels
{
    class NewDogovorWindowViewModel
    {
        private Command saveDogovorCommand;
        public Command SaveDogovorCommand
        {
            get
            {
                return saveDogovorCommand ?? (saveDogovorCommand = new Command(obj =>
                {
                    Window wnd = obj as Window;
                    wnd.Close();
                }));
            }

        }
    }
}
