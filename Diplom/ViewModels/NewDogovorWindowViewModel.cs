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
    }
}
