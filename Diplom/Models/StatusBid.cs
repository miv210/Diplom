using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class StatusBid
    {
        public StatusBid()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string? NameStatusa { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
