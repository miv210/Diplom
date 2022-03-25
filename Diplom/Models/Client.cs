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
        public int? PasportId { get; set; }
        public int? LicevoiChet { get; set; }

        public virtual Pasport? Pasport { get; set; }
        public virtual ICollection<Dogovor> Dogovors { get; set; }
        public virtual ICollection<Zayvki> Zayvkis { get; set; }
    }
}
