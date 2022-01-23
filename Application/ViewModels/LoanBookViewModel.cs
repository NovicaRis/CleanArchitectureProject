using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class LoanBookViewModel
    {
        public ReaderViewModel Reader { get; set; }
        public IEnumerable<BookViewModel> Books { get; set; } = Enumerable.Empty<BookViewModel>();
    }
}
