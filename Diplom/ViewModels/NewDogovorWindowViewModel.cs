using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Diplom.ViewModels
{
    class NewDogovorWindowViewModel : BaseViewModel
    {
        public string TypeDog { get; set; }
        public int ClientS { get; set; } 
        public string Status { get; set; }
        public string Date { get; set; }
        public int Summ { get; set; }

        private Command saveDogovorCommand;
        public Command SaveDogovorCommand
        {
            get
            {
                return saveDogovorCommand ?? (saveDogovorCommand = new Command(obj =>
                {
                    //ClientPageViewModel clpg = new ClientPageViewModel();
                    //int idclienta = clpg.GetIdClient();

                    Window wnd = obj as Window;

                    Dogovor dogovor = new Dogovor 
                    {
                        TypeDogovora = TypeDog, 
                        IdClient = ClientS, 
                        SumDogovora = Summ,
                        DataPodpisanie = Date
                    }; 

                    using (TestBdContext db = new TestBdContext())
                    {
                        db.Dogovors.Add(dogovor);
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Информация сохранена!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                       
                    }
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

        private Command addFilewCommand;
        public Command AddFilewCommand
        {
            get
            {
                return addFilewCommand ?? (addFilewCommand = new Command(obj =>
                {
                    Window wnd = obj as Window;
                    TextBox tbFile = wnd.FindName("tbPathFile") as TextBox;

                    OpenFileDialog openFile = new OpenFileDialog();
                    openFile.CheckFileExists = true;
                    openFile.CheckPathExists = true;
                    openFile.Filter = "Files(*.docx; *.doc; *.pdf)|*.docx; *.pdf|All files (*.*)|*.*";
                    if (openFile.ShowDialog() == true)
                    { 
                        Dogovor fileDogovor = new Dogovor();

                    }

                    tbFile.Text = openFile.FileName;
                }));
            }
            set { addFilewCommand = value; }
        }
    }
}
