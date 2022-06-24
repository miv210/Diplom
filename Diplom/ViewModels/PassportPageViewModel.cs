using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diplom.ViewModels
{
    public class PassportPageViewModel : BaseViewModel
    {
        public string Surnmae { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int Series { get; set; }
        public int Number { get; set; }
        public string WhoIssued { get; set; }
        public string CodeDepartment { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string RegistrationAddress { get; set; }
        public static Passport passport = null;
        public Command NextCommand { get; set; }

        public PassportPageViewModel()
        {
            
            //if(passport != null)
            //{
            //    Surnmae = passport.Surname;
            //    Name = passport.Name;
            //    Patronymic = passport.Patronymic;
            //    Series = passport.Series;
            //    Number = passport.Number;
            //    WhoIssued = passport.WhoIssued;
            //    CodeDepartment = passport.CodeDepartment;
            //    DateOfIssue = passport.DateOfIssue;
            //    DateOfBirth = passport.DateOfBirth;
            //    RegistrationAddress = passport.RegistrationAddress;
            //}
            NextCommand = new Command(NextPage);
        }

        private async void NextPage()
        {
            if(passport == null)
            {
                passport = new Passport
                {
                    Surname = Surnmae,
                    Name = Name,
                    Patronymic = Patronymic,
                    Series = Series,
                    Number = Number,
                    WhoIssued = WhoIssued,
                    CodeDepartment = CodeDepartment,
                    DateOfIssue = DateOfIssue,
                    RegistrationAddress = RegistrationAddress,
                    DateOfBirth = DateOfBirth
                };
            }
            else
            {
                passport.Surname = Surnmae;
                passport.Name = Name;
                passport.Patronymic = Patronymic;
                passport.Series = Series;
                passport.Number = Number;
                passport.WhoIssued = WhoIssued;
                passport.CodeDepartment = CodeDepartment;
                passport.DateOfIssue = DateOfIssue;
                passport.RegistrationAddress = RegistrationAddress;
                passport.DateOfBirth = DateOfBirth;

            }

            NewClientWindowViewModel.MainFrame.Navigate(new GeneralPage());
        }
    }
}
