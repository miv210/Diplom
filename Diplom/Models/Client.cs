using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Client
    {
        public Client()
        {
            Bids = new HashSet<Bid>();
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? NContract { get; set; }
        public string? TelethonNumber { get; set; }
        public string? Address { get; set; }
        public int? PersonalAccount { get; set; }
        public int? IdPassporta { get; set; }

        public virtual Passport? IdPassportaNavigation { get; set; }
        public virtual Contract? NContractNavigation { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
