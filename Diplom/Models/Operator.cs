using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Operator
    {
        public Operator()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
