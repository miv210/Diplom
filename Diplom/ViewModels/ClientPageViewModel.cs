using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class ClientPageViewModel : BaseViewModel
    {
        public List<Client> Clients { get; set; } = DiplomContext.GetContext().Clients.Include(u=> u.IdPassportaNavigation).ToList();
        public List<Contract> DogovorS { get; set; } = DiplomContext.GetContext().Contracts.Include(u=> u.TypeContractNavigation).ToList();

        int idClienta = 0;
        public Command SelectRowCommand { get; set;}
        public Command NewClientOpenCommand { get; set; }
        public Command NewDogovorOpenCommand { get; set; }
        public Command OpenFileCommand { get; set; }
        public Command RedactClientWindowOpenCommand { get; set; }
        public ClientPageViewModel()
        {
            SelectRowCommand = new Command(SelectRow);
            NewClientOpenCommand = new Command(NewClientWindowOpen);
            NewDogovorOpenCommand = new Command(NewDogovorWindowOpen);
            RedactClientWindowOpenCommand = new Command(RedactClientWindowOpen);
            OpenFileCommand = new Command(OpenFile);
        }
        private async void SelectRow(object obj)
        {
            Page pg = obj as Page;

            ListView listViewDogovor = pg.FindName("ListDogovor") as ListView;
            idClienta = GetIdClient(pg);
            NewDogovorWindowViewModel.idClienta = idClienta;
            
            listViewDogovor.ItemsSource = DogovorS.Where(u => u.IdClienta == idClienta).ToList();
        }
        private async void NewClientWindowOpen(object obj)
        {
            Page pg = obj as Page;
            ListView listViewClient = pg.FindName("ListClin") as ListView;
            NewClientWindow newClientWindow = new NewClientWindow();
            
            OpenDialogWindow(newClientWindow);

            Clients = DiplomContext.GetContext().Clients.Include(u=> u.IdPassportaNavigation).ToList();
            listViewClient.ItemsSource = Clients;
        }
        private async void RedactClientWindowOpen(object obj)
        {
            Page pg = obj as Page;
            ListView listViewClient = pg.FindName("ListClin") as ListView;
            NewClientWindow newClientWindow = new NewClientWindow();
            OpenDialogWindow(newClientWindow);

            Clients = DiplomContext.GetContext().Clients.Include(u => u.IdPassportaNavigation).ToList();
            listViewClient.ItemsSource = Clients;
        }
        private async void NewDogovorWindowOpen(object obj)
        {
            Page pg = obj as Page;
            ListView listViewDogovor = pg.FindName("ListDogovor") as ListView;
            ListView listViewClient = pg.FindName("ListClin") as ListView;

            int id = GetIdClient(pg);
            if (id != 0)
            {
                NewDogovorWindow newDogovorWindow = new NewDogovorWindow(null);
             
                OpenDialogWindow(newDogovorWindow);
                


                listViewClient.ItemsSource = Clients;
                DogovorS = DiplomContext.GetContext().Contracts.ToList();
                listViewDogovor.ItemsSource = DogovorS.Where(u => u.IdClienta == id).ToList();
                

            }
            else
                MessageBox.Show("Выбирите клиента");
        }

        private async void OpenFile(object obj)
        {
            Contract btt = obj as Contract;
            
            
            DogovorS = DiplomContext.GetContext().Contracts.Include(u=> u.IdClientaNavigation).ToList();

            byte[] dataDoc = btt.Doc;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.CheckFileExists = true;
            //saveFileDialog.CheckPathExists = true;
            saveFileDialog.Filter = "Files(*.docx; *.doc; *.pdf)|*.docx; *.pdf|All files (*.*)|*.*";
            if (dataDoc != null)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, dataDoc);
                    MessageBox.Show("Файл сохранен");
                }
            }
            else
            {
                MessageBox.Show("Файл отсутствует");
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
                    ListView listViewDogovor = pg.FindName("ListDogovor") as ListView;
                    List<Contract> dogForRemoving = listViewDogovor.SelectedItems.Cast<Contract>().ToList();

                    if (MessageBox.Show($"Вы точно хотите удалить следующие {dogForRemoving.Count()} элементов,", "Вгимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                using (DiplomContext db = new DiplomContext())
                                {
                                    db.Contracts.RemoveRange(dogForRemoving);
                                    db.SaveChanges();
                                    MessageBox.Show("Данные удаленны");
                                    DogovorS = db.Contracts.ToList(); ;
                                    listViewDogovor.ItemsSource = DogovorS.Where(u => u.IdClienta == GetIdClient(pg)).ToList(); 
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
                    
                    if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов,", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            using (DiplomContext db = new DiplomContext())
                            {
                                db.Clients.RemoveRange(clientForRemoving);
                                db.SaveChanges();
                                MessageBox.Show("Данные удаленны");

                                listViewClient.ItemsSource = db.Clients.Include(u=> u.IdPassportaNavigation).ToList();
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

        public int GetIdClient(Page pg)
        {
            int idClienta = 0;
            ListView listViewClient = pg.FindName("ListClin") as ListView;
            if ((listViewClient.SelectedItem as Client) != null)
                idClienta = (listViewClient.SelectedItem as Client).Id;
            return idClienta;
        }
    }
}
