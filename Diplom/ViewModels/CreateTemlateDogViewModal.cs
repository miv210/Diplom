using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public class CreateTemlateDogViewModal
    {
        public string TextDog { get; set; }

        public CreateTemlateDogViewModal(Contract dogovor)
        {
            TextDog = $"";
        }
    }
}
