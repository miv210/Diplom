using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Dogovor
    {
        public int Id { get; set; }
        public string? TypeDogovora { get; set; }
        public int? SumDogovora { get; set; }
        public string? Sostoynie { get; set; }
        public string? DataPodpisanie { get; set; }
        public int? IdClient { get; set; }

        public virtual Client? IdClientNavigation { get; set; }
    }
}
