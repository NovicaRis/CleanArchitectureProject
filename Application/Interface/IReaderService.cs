using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IReaderService
    {
        List<ReaderViewModel> GetReaders();

        ReaderViewModel GetReaderById(Guid id);

        ReaderViewModel AddReader(ReaderViewModel readerRequest);

        void EditReader(ReaderViewModel readerRequest);

        LoanBookViewModel GetReaderWithLoanedBooks(Guid id);

        void LoanBook(Guid readerId, Guid bookId);

        void ReturnBook(Guid readerId, Guid bookId);

        List<ReaderViewModel> FullTextSearch(string searchTerm);
    }
}
