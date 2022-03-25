using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Zayvki
    {
        public int Id { get; set; }
        public DateTime? DateZayvki { get; set; }
        public int? ClientId { get; set; }
        public string? TypeZayvki { get; set; }
        public int? UserId { get; set; }
        public string? StatusZayvki { get; set; }
        public string? Opisanie { get; set; }

        public virtual Client? Client { get; set; }
        public virtual User? User { get; set; }
    }
}
