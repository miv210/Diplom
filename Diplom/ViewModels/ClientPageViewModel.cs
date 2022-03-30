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
        public List<Dogovor> DogovorS { get; set; } = TestBdContext.GetContext().Dogovors.Include(u => u.IdClientNavigation).ToList();
        public string VisibilityGrid { get; set; } = "Collapsed";

        private Client selectedClient;
        public Client SelectedClient 
        {
            get
            {
                return selectedClient;
            }
            set { selectedClient = value; }
        }

        private SelectedCellsChangedEventHandler selectedCellsChanged;
        public SelectedCellsChangedEventHandler SelectedCellsChanged
        {
            get
            {
                VisibilityGrid = "Visible";
                return selectedCellsChanged;
            }

            set { selectedCellsChanged = value; }

        }


        private Command selectRowCommand;
        public Command SelectRowCommand 
        {
            get
            {
                return selectRowCommand ?? (selectRowCommand = new Command(obj =>
                {
                    
                    Page pg = obj as Page;

                    ListView listViewDogovor = pg.FindName("ListDogovor") as ListView;
                    ListView listViewClient = pg.FindName("ListClin") as ListView;
                    var idClienta = (listViewClient.SelectedItem as Client).Id;

                    listViewDogovor.ItemsSource = DogovorS.Where(u=> u.IdClient == idClienta).ToList();
                }));
            }
            set { selectRowCommand = value; }
            
        }

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

                    ListView listViewDogovor = pg.FindName("ListDogovor") as ListView; ;
                    ListView listViewClient = pg.FindName("ListClin") as ListView;
                    var idClienta = (listViewClient.SelectedItem as Client).Id;
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
                                    DogovorS = null;
                                    listViewDogovor.ItemsSource = db.Dogovors.Where(u => u.IdClient == idClienta).ToList();
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
        private void OpenDialogWindow(Window wn)
        {
            wn.Owner = Application.Current.MainWindow;
            wn.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wn.ShowDialog();
        }
    }
}
