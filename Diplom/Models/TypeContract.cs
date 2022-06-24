using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class TypeContract
    {
        public TypeContract()
        {
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string? NameType { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
