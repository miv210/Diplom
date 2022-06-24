using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Passport
    {
        public Passport()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public int Series { get; set; }
        public int Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CodeDepartment { get; set; } = null!;
        public string WhoIssued { get; set; } = null!;
        public string RegistrationAddress { get; set; } = null!;

        public virtual ICollection<Client> Clients { get; set; }
    }
}
