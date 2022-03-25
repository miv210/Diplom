using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Diplom.ViewModels;

namespace Diplom.View
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public static DataGrid ClientGrid;
        public static DataGrid DogovorGrid;
        public ClientPage()
        {
            InitializeComponent();
            //DgClient = ClientGrid;
            //DgClientDog = DogovorGrid;
        }

        private void DgClient_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            using (TestBdContext db = new TestBdContext())
            {
                var dogovors = db.Clients.Join(
                    db.Dogovors,
                    u => u.Id,
                    c => c.IdClient, (u, c) => new
                    {
                        NDog = c.Id,
                        Type = c.TypeDogovora,
                        Sost = c.Sostoynie,
                        Dat = c.DataPodpisanie,
                        Summ = c.SumDogovora
                    });
                //var clients = new Client();
                //var dogovors = db.Dogovors.Where(c => c.IdClient == clients.Id);
                DgClientDog.ItemsSource = dogovors.ToList();

            }
        }

        private void DeleteDog_Click(object sender, RoutedEventArgs e)
        {
            var dogForRemoving = DgClientDog.SelectedItems.Cast<Dogovor>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {dogForRemoving.Count()} элементов,", "Вгимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TestBdContext.GetContext().Dogovors.RemoveRange(dogForRemoving);
                    TestBdContext.GetContext().SaveChanges();
                    MessageBox.Show("Данные удаленны");

                    DgClientDog.ItemsSource = TestBdContext.GetContext().Dogovors.ToList();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
