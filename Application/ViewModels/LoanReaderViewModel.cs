using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class LoanReaderViewModel
    {
        public BookViewModel Book { get; set; }
        public IEnumerable<ReaderViewModel> Readers { get; set; } = Enumerable.Empty<ReaderViewModel>();
    }
}
