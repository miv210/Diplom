using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Bid
    {
        public int Id { get; set; }
        public DateTime? DateBid { get; set; }
        public int? TypeBid { get; set; }
        public int? StatusBid { get; set; }
        public string? Description { get; set; }
        public int? IdClienta { get; set; }
        public int? IdOperatora { get; set; }

        public virtual Client? IdClientaNavigation { get; set; }
        public virtual Operator? IdOperatoraNavigation { get; set; }
        public virtual StatusBid? StatusB { get; set; }
        public virtual TypeBid? TypeB { get; set; }
    }
}
