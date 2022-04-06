using Microsoft.EntityFrameworkCore;
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
        public string VisibilityGrid { get; set; } = "Collapsed";

        private Command selectRowCommand;
        public Command SelectRowCommand 
        {
            get
            {
                return selectRowCommand ?? (selectRowCommand = new Command(obj =>
                {
                    
                    Page pg = obj as Page;

                    ListView listViewDogovor = pg.FindName("ListDogovor") as ListView;
                    int idClienta = GetIdClient(pg);
                    listViewDogovor.ItemsSource = DogovorS.Where(u => u.IdClient == idClienta).ToList();
                    
                }));
            }
        }

        private Command newClientOpenCommand;
        public Command NewClientOpenCommand
        {
            get
            {
                return newClientOpenCommand ?? (newClientOpenCommand = new Command(obj =>
                {
                    Page pg = obj as Page;
                    ListView listViewClient = pg.FindName("ListClin") as ListView;
                    NewClientWindow newClientWindow = new NewClientWindow();
                    OpenDialogWindow(newClientWindow, pg);
                    Clients = TestBdContext.GetContext().Clients.ToList();
                    listViewClient.ItemsSource = Clients;

                }));
            }
        }

        private Command newDogovorOpenCommand;
        public Command NewDogovorOpenCommand
        {
            get
            {
                return newDogovorOpenCommand ?? (newDogovorOpenCommand = new Command(obj =>
                {
                    Page pg = obj as Page;
                    ListView listViewDogovor = pg.FindName("ListDogovor") as ListView;
                    NewDogovorWindow newDogovorWindow = new NewDogovorWindow ();
                    OpenDialogWindow(newDogovorWindow, pg);
                    DogovorS = TestBdContext.GetContext().Dogovors.ToList();
                    listViewDogovor.ItemsSource = DogovorS.Where(u => u.IdClient == GetIdClient(pg)).ToList(); 
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

                    ListView listViewDogovor = pg.FindName("ListDogovor") as ListView; ;
                    ListView listViewClient = pg.FindName("ListClin") as ListView;
           
                    List<Dogovor> dogForRemoving = listViewDogovor.SelectedItems.Cast<Dogovor>().ToList();

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
                                    DogovorS = db.Dogovors.ToList(); ;
                                    listViewDogovor.ItemsSource = DogovorS.Where(u => u.IdClient == GetIdClient(pg)).ToList(); 
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
        private Command deleteClientCommand;
        public Command DeleteClientCommand
        {
            get
            {
                return deleteClientCommand ?? (deleteClientCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    ListView listViewClient = pg.FindName("ListClin") as ListView;
                    List<Client> clientForRemoving = listViewClient.SelectedItems.Cast<Client>().ToList();

                    if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов,", "Вгимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            using (TestBdContext db = new TestBdContext())
                            {
                                db.Clients.RemoveRange(clientForRemoving);
                                db.SaveChanges();
                                MessageBox.Show("Данные удаленны");

                                listViewClient.ItemsSource = db.Clients.ToList();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }));
            }
        }
        private void OpenDialogWindow(Window wn, object dataContext)
        {
            wn.Owner = Application.Current.MainWindow;
            wn.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wn.ShowDialog();
            wn.DataContext = dataContext;
        }

        private int GetIdClient(Page pg)
        {
            ListView listViewClient = pg.FindName("ListClin") as ListView;
            int idClienta = (listViewClient.SelectedItem as Client).Id;
            return idClienta;
        }
    }
}
