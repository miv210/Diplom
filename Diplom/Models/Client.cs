using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Client
    {
        public Client()
        {
            Dogovors = new HashSet<Dogovor>();
            Zayvkis = new HashSet<Zayvki>();
        }

        public int Id { get; set; }
        public int? NDogovora { get; set; }
        public string? Fio { get; set; }
        public string? Telethon { get; set; }
        public string? Adres { get; set; }
        public int? LicevoiChet { get; set; }
        public int? Seriy { get; set; }
        public int? Nomer { get; set; }
        public DateTime? DataVidachi { get; set; }
        public DateTime? DataRojdeniy { get; set; }
        public string? KodPodrazdeleniy { get; set; }
        public string? KemVidan { get; set; }
        public string? AdresRegistraci { get; set; }

        public virtual ICollection<Dogovor> Dogovors { get; set; }
        public virtual ICollection<Zayvki> Zayvkis { get; set; }
    }
}
