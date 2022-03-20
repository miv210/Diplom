using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Diplom.View
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            //TestBdContext db = new TestBdContext();
            //DgClient.ItemsSource = db.Clients.ToList();
        }

        private void DgClient_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            using (TestBdContext db = new TestBdContext())
            {
                var clients = db.Clients.Join(
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
                DgClientDog.ItemsSource = clients.ToList();
            }
        }

        private void bttAddDog_Click(object sender, RoutedEventArgs e)
        {
            ShellPageViewModel.ShellFrame.Navigate(new NewDogovorPage());
        }
    }
}
