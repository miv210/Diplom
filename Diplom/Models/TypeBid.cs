using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class TypeBid
    {
        public TypeBid()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string? NameType { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
