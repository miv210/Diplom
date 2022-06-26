using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Page = System.Windows.Controls.Page;

namespace Diplom.ViewModels
{
    class ZayvkiPageViewModel : BaseViewModel, Excel.Page
    {
        public List<Bid> ZayvkiList { get; set; } //свойство для биндинга datagrid.ItemsSource
        List<Bid> list;
        public string SerchZayvitel { get; set; } // свойство для биндинга textbox.Text

        private Command serchZayvitelCommand;
        public Command SerchZayvitelCommand //команда для кнопки поиска
        {
            get
            {
                return serchZayvitelCommand ?? (serchZayvitelCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    DataGrid dgZayvki = pg.FindName("DataGridZayvki") as DataGrid;
                    dgZayvki.ItemsSource = FiltersZayvki("serch"); // загрузка отфилтрованного списка     
                }));
            }
        }
        public Command CreateReportCommand { get; set; }
        public Command RedactBidCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public ZayvkiPageViewModel()
        {
            list = null;
            ZayvkiList = DiplomContext.GetContext().Bids
            .Include(u => u.IdClientaNavigation).Include(d => d.IdOperatoraNavigation).Include(u => u.StatusB).Include(d => d.TypeB).ToList();
            DiplomContext.CloseContext();
            CreateReportCommand = new Command(CreateReport);
            RedactBidCommand = new Command(RedactBid);
            RefreshCommand = new Command(Refresh);
        }
        

        private List<Bid> FiltersZayvki(string arg) //отфилтрованный список
        {
            

            switch (arg)
            {
                case "serch":
                    if (SerchZayvitel != null)
                        list = ZayvkiList.Where(c => c.IdClientaNavigation.IdPassportaNavigation.Surname.Contains(SerchZayvitel)).ToList();
                    else
                        MessageBox.Show("Введите фамилию");
                    break;
                case "bttWork":
                    list = ZayvkiList.Where(c => c.StatusBid == 2).ToList();
                    break;
                case "bttNew":
                    list = ZayvkiList.Where(c => c.StatusBid == 1).ToList();
                    break;
                case "bttComplet":
                    list = ZayvkiList.Where(c => c.StatusBid == 4).ToList();
                    break;
            }
            return list;
        }
        private async void CreateReport(object obj)
        {
            System.Windows.Controls.Page pg = obj as System.Windows.Controls.Page;

            DataGrid dgZayvki = pg.FindName("DataGridZayvki") as DataGrid;
            if (list != null)
            {
                dgZayvki.ItemsSource = list;

                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                TextBlock name =  dgZayvki.Columns[4].GetCellContent(dgZayvki.Items[0]) as TextBlock;
                sheet1.Name = $"Отчет";
                for (int j = 0; j < dgZayvki.Columns.Count; j++)
                {
                    Excel.Range myRange = (Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = false;
                    sheet1.Columns[j + 1].ColumnWidth = 15;
                    myRange.Value2 = dgZayvki.Columns[j].Header;
                }

                for (int i = 0; i < dgZayvki.Columns.Count; i++)
                { //www.yazilimkodlama.com
                    for (int j = 0; j < dgZayvki.Items.Count; j++)
                    {
                        dgZayvki.UpdateLayout();
                        dgZayvki.ScrollIntoView(dgZayvki.Items[j]);

                        
                        

                        TextBlock ? b = dgZayvki.Columns[i].GetCellContent(dgZayvki.Items[j]) as TextBlock;

                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        if (i == 0)
                        {

                            myRange.Value2 = list[j].Id;
                        }
                        if (i == 1)
                        {
                            if(list[j].IdClientaNavigation.Id != 0)
                            {
                                string adCl = $"{list[j].IdClientaNavigation.Address} \n" +
                                $"{list[j].IdClientaNavigation.IdPassportaNavigation.Surname} " +
                                $"{list[j].IdClientaNavigation.IdPassportaNavigation.Name}" + $" {list[j].IdClientaNavigation.IdPassportaNavigation.Patronymic}";
                                myRange.Value2 = adCl;
                            }
                            
                        }

                        if (b != null)
                        {
                            try
                            {

                                myRange.Value2 = b.Text;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите статус для формирования отчета.");
            }

        }
        private async void RedactBid(object obj)
        {
            Bid bd = obj as Bid;
            RedactBidWindow rbw= new RedactBidWindow(bd);
            rbw.ShowDialog();
        }
        private async void Refresh(object obj)
        {
            Page pg = obj as Page;
            DataGrid dg = pg.FindName("DataGridZayvki") as DataGrid;
            DiplomContext.GetContext().ChangeTracker.Entries().ToList().ForEach(e => e.ReloadAsync());
            ZayvkiList = DiplomContext.GetContext().Bids
                .Include(u => u.IdClientaNavigation)
                .Include(d => d.IdOperatoraNavigation)
                .Include(u => u.StatusB).Include(d => d.TypeB).ToList();
            DiplomContext.CloseContext();
            dg.ItemsSource = ZayvkiList;
            
        }
        private Command selectNewZayvkiCommand;
        public Command SelectNewZayvkiCommand
        {
            get
            {
                return selectNewZayvkiCommand ?? (selectNewZayvkiCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    DataGrid dgZayvki = pg.FindName("DataGridZayvki") as DataGrid;
                    dgZayvki.ItemsSource = FiltersZayvki("bttNew");
                }));
            }
        }

        private Command selectWorkZayvkiCommand;
        public Command SelectWorkZayvkiCommand
        {
            get
            {
                return selectWorkZayvkiCommand ?? (selectWorkZayvkiCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    DataGrid dgZayvki = pg.FindName("DataGridZayvki") as DataGrid;
                    dgZayvki.ItemsSource = FiltersZayvki("bttWork");
                }));
            }
        }

        private Command selectComletedZayvkiCommand;
        public Command SelectComletedZayvkiCommand
        {
            get
            {
                return selectComletedZayvkiCommand ?? (selectComletedZayvkiCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    DataGrid dgZayvki = pg.FindName("DataGridZayvki") as DataGrid;
                    dgZayvki.ItemsSource = FiltersZayvki("bttComplet");
                }));
            }
        }
       
        private Command selectedAllZayvkiCommand;
        public Command SelectedAllZayvkiCommand
        {
            get
            {
                return selectedAllZayvkiCommand ?? (selectedAllZayvkiCommand = new Command(obj =>
                {
                    Page pg = obj as Page;

                    DataGrid dgZayvki = pg.FindName("DataGridZayvki") as DataGrid;
                    dgZayvki.ItemsSource = ZayvkiList;
                }));
            }
        }

        public HeaderFooter LeftHeader => throw new NotImplementedException();

        public HeaderFooter CenterHeader => throw new NotImplementedException();

        public HeaderFooter RightHeader => throw new NotImplementedException();

        public HeaderFooter LeftFooter => throw new NotImplementedException();

        public HeaderFooter CenterFooter => throw new NotImplementedException();

        public HeaderFooter RightFooter => throw new NotImplementedException();
    }
}
