using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class StatusContract
    {
        public StatusContract()
        {
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
