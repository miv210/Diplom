using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diplom.ViewModels
{
    public class RedactBidWindowViewModal
    {
        public List<StatusBid> StatusBids { get; set; }
        public StatusBid SelectedStatusBid { get; set; }
        public Bid RedactBid { get; set; }
        public Command SaveCommand { get; set; }
        public RedactBidWindowViewModal(Bid bid)
        {
            RedactBid = bid;
            if (RedactBid != null)
            {
                if(RedactBid != null)
                    SelectedStatusBid = RedactBid.StatusB;
            }
            StatusBids = DiplomContext.GetContext().StatusBids.ToList();
            SaveCommand = new Command(Save);
        }
        private async void Save(object obj)
        {
            using (DiplomContext db = new DiplomContext())
            {
                try
                {
                    var ld = db.Bids.Where(g => g.Id == RedactBid.Id).FirstOrDefault();
                    ld.IdOperatora = MainWindowViewModel.idOperatora;
                    ld.StatusBid = SelectedStatusBid.Id;
                    await db.SaveChangesAsync();
                    MessageBox.Show("Отредактированная информация сохранена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
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
