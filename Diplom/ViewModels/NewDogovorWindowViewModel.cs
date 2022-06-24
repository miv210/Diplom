using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Diplom.ViewModels
{
    public class NewDogovorWindowViewModel : BaseViewModel
    {
        public static int idClienta = 0;
        public string ContractAmount { get; set; }
        public string TypeContract { get; set; }
        public DateTime DateOfSigning { get; set; }
        public List<StatusContract> Status { get; set; }
        public byte[] Doc { get; set; } 
        public Client client { get; set; } 
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public Command SaveContractCommand { get; set; }
        public Command AddFileCommand { get; set; }
        private StatusContract selectedStatusContract;
        public StatusContract SelectedStatusContract
        {
            get { return selectedStatusContract; }
            set
            {
                if (selectedStatusContract != value)
                {
                    selectedStatusContract = value;
                }
            }
        }

        public NewDogovorWindowViewModel(Contract dogovor)
        {
            client = DiplomContext.GetContext().Clients.Include(u => u.IdPassportaNavigation).Where(c => c.Id == idClienta).FirstOrDefault();
            Status = DiplomContext.GetContext().StatusContracts.ToList();

            Surname = client.IdPassportaNavigation.Surname;
            Name = client.IdPassportaNavigation.Name;
            Patronymic = client.IdPassportaNavigation.Patronymic;

            if (dogovor != null)
            {
                SelectedStatusContract = dogovor.StatusNavigation;
                TypeContract = dogovor.TypeContractNavigation.NameType;
                ContractAmount = dogovor.ContractAmount;
            }
            AddFileCommand = new Command(AddFile);
            SaveContractCommand = new Command(SaveContract);
        }

        private async void AddFile(object obj)
        {
            Window wnd = obj as Window;
            TextBox tbFile = wnd.FindName("tbPathFile") as TextBox;

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.Filter = "Files(*.docx; *.doc; *.pdf)|*.docx; *.pdf|All files (*.*)|*.*";
            if (openFile.ShowDialog() == true)
            {

                try
                {
                    Doc = File.ReadAllBytes(openFile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            tbFile.Text = openFile.FileName;
        }

        private async void SaveContract(object obj)
        {
            Window wnd = obj as Window;
            TypeContract contractType = new TypeContract 
            {
                NameType = TypeContract
            };

            Contract contract = new Contract
            {
                IdClienta = idClienta,
                TypeContractNavigation = contractType,
                TypeContract = contractType.Id,
                ContractAmount = ContractAmount,
                DateOfSigning = DateOfSigning,
                Doc = Doc,
                Status = selectedStatusContract.Id
            };

            using (DiplomContext db = new DiplomContext())
            {
                db.Contracts.Add(contract);            
                try
                {
                    await db.SaveChangesAsync();
                    //client.NContract = contract.Id;
                    //await db.SaveChangesAsync();
                    MessageBox.Show("Информация сохранена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            wnd.Close();
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
