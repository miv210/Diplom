using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class Contract
    {
        public Contract()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }
        public int? TypeContract { get; set; }
        public string? ContractAmount { get; set; }
        public DateTime? DateOfSigning { get; set; }
        public int? IdClienta { get; set; }
        public byte[]? Doc { get; set; }
        public int? Status { get; set; }

        public virtual Client? IdClientaNavigation { get; set; }
        public virtual StatusContract? StatusNavigation { get; set; }
        public virtual TypeContract? TypeContractNavigation { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
