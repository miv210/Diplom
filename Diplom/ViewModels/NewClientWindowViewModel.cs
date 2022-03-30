﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.ViewModels
{
    class NewClientWindowViewModel : BaseViewModel
    {
        public string FullName { get; set; } 
        public string Telethon { get; set; }
        public string Adres { get; set; }
        public int SeriyPasporta { get; set; }
        public int NomerPasporta { get; set; }
        public string KemVidanPasport { get; set; }
        public string KodPodrazdel { get; set; }
        public string DataVidachi { get; set; }
        public string DataRoj { get; set; }
        public string AdresReg { get; set; }
        public string MestoRoj { get; set; }  
        public int LicevoiChet { get; set; }


        private Command saveNewClientCommand;
        public  Command SaveNewClientCommand
        {
            get
            {
                return saveNewClientCommand ?? (saveNewClientCommand = new Command(obj =>
                {
                    Window wnd = obj as Window;
                    Pasport pasport;
                    Client client = new Client
                    {
                        Fio = FullName,
                        Telethon = Telethon,
                        Adres = Adres,
                        LicevoiChet = LicevoiChet,
                        Pasport = new Pasport
                        {
                            Seriy = SeriyPasporta,
                            Nomer = NomerPasporta,
                            KemVidan = KemVidanPasport,
                            KodPodrazdeleniy = KodPodrazdel,
                            DataVidachi = DataVidachi,
                            DataRojdeniy = DataRoj,
                            AdresRegistarci = AdresReg,
                            MestoRojdeniy = MestoRoj
                        }
                    };

                    TestBdContext.GetContext().Clients.Add(client);
                    try
                    {
                        TestBdContext.GetContext().SaveChanges();
                        MessageBox.Show("Информация сохранена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    //DataGrid dgCl = wnd.Owner.FindName("DgClient") as DataGrid;
                    //MainWindowViewModel.MainFrame.
                    wnd.Close();
                }));
            }
        }
    
        private Command closeWindowCommand;
        public Command CloseWindowCommand
        {
            get
            {
                return closeWindowCommand ?? (closeWindowCommand = new Command(obj =>
                {
                    Window wnd = obj as Window;
                    wnd.Close();
                }));
            }
            set { closeWindowCommand = value; }
        }
    }
}
