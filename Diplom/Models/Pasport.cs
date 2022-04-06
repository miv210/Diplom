using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Pasport
    {
        public int Id { get; set; }
        public int? Seriy { get; set; }
        public int? Nomer { get; set; }
        public string? DataVidachi { get; set; }
        public string? DataRojdeniy { get; set; }
        public string? KodPodrazdeleniy { get; set; }
        public string? KemVidan { get; set; }
        public string? MestoRojdeniy { get; set; }
        public string? AdresRegistarci { get; set; }
    }
}
