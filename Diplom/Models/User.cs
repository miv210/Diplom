using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public partial class User
    {
        public User()
        {
            Zayvkis = new HashSet<Zayvki>();
        }

        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Zayvki> Zayvkis { get; set; }
    }
}
