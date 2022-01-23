using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IBookService
    {
        List<BookViewModel> GetBooks();

        BookViewModel GetBookById(Guid id);

        BookViewModel AddBook(BookViewModel bookRequest);

        void EditBook(BookViewModel bookRequest);

        List<BookViewModel> FullTextSearch(string searchTerm);
        LoanReaderViewModel GetBookWithReaders(Guid id);

    }
}
