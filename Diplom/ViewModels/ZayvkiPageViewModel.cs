using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class ZayvkiPageViewModel : BaseViewModel
    {
        public List<Zayvki> ZayvkiList { get; set; } = TestBdContext.GetContext().Zayvkis
            .Include(u => u.Client).Include(d => d.User).ToList(); //свойство для биндинга datagrid.ItemsSource
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

        private List<Zayvki> FiltersZayvki(string arg) //отфилтрованный список
        {
            List<Zayvki> list = null;
            if (arg == "serch")
            {
                list = ZayvkiList.Where(c => c.Client.Fio.Contains(SerchZayvitel)).ToList();
            }
            if(arg == "bttWork")
            {
                list = ZayvkiList.Where(c => c.StatusZayvki == "В работе").ToList();
            }
            if(arg == "bttNew")
            {
                list = ZayvkiList.Where(c => c.StatusZayvki == "Новая").ToList();
            }
            if(arg == "bttComplet")
            {
                list = ZayvkiList.Where(c => c.StatusZayvki == "Выполнена").ToList();
            }
            return list;
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
    }
}
