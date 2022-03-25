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
        public List<Client> Clients { get; set; } = TestBdContext.GetContext().Clients.ToList();
        public List<Dogovor> DogovorS { get; set; } = TestBdContext.GetContext().Dogovors.ToList();

        private Command newClientOpenCommand;
        public Command NewClientOpenCommand
        {
            get
            {
                return newClientOpenCommand ?? (newClientOpenCommand = new Command(obj =>
                {
                    NewClientWindow newClientWindow = new NewClientWindow();
                    OpenDialogWindow(newClientWindow);
                }));
            }
        }

        //private Command loaddogovorcommand;
        //public Command Loaddogovorcommand
        //{
        //    get
        //    {
        //        return loaddogovorcommand ?? (loaddogovorcommand = new Command(obj =>
        //        {
        //            using (testbdcontext db = new testbdcontext())
        //            {
        //                var dogovors = db.dogovors.select(p => new
        //                {
        //                    id = p.id,

        //                });
        //                dgclientdog.itemssource = dogovors.tolist();

        //            }
        //        }));
        //    }
        //}
        private Command newDogovorOpenCommand;
        public Command NewDogovorOpenCommand
        {
            get
            {
                return newDogovorOpenCommand ?? (newDogovorOpenCommand = new Command(obj =>
                {
                    
                    NewDogovorWindow newDogovorWindow = new NewDogovorWindow ();
                    OpenDialogWindow(newDogovorWindow);
                    Page pg = obj as Page;
                    DataGrid sds = pg.FindName("DgClientDog") as DataGrid;
                    sds.Items.Refresh();
                }));
            }

        }

        private Command deleteDogovorCommand;
        public Command DeleteDogovorCommand
        {
            get
            {
                return deleteDogovorCommand ?? (deleteDogovorCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    DataGrid dataGr = pg.FindName("DgClientDog") as DataGrid;
                    var dogForRemoving = dataGr.SelectedItems.Cast<Dogovor>().ToList();

                    if (MessageBox.Show($"Вы точно хотите удалить следующие {dogForRemoving.Count()} элементов,", "Вгимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                using (TestBdContext db = new TestBdContext())
                                {
                                    db.Dogovors.RemoveRange(dogForRemoving);
                                    db.SaveChanges();
                                    MessageBox.Show("Данные удаленны");

                                    dataGr.ItemsSource = db.Dogovors.ToList();
                                }
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString());
                            }
                        }
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
