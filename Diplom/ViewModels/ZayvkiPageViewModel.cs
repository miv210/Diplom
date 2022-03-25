using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    class ZayvkiPageViewModel : BaseViewModel
    {
        public List<Zayvki> ZayvkiList { get; set; } = TestBdContext.GetContext().Zayvkis.Include(u => u.Client).ToList();
    }
}
