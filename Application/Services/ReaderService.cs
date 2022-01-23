using Application.Interface;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ReaderService(IReaderRepository readerRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _readerRepository = readerRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public ReaderViewModel AddReader(ReaderViewModel readerRequest)
        {
            var reader = _mapper.Map<Reader>(readerRequest);
            var addedReader = _readerRepository.Add(reader);
            return _mapper.Map<ReaderViewModel>(addedReader);
        }

        public void EditReader(ReaderViewModel readerRequest)
        {
            var reader = _mapper.Map<Reader>(readerRequest);
            _readerRepository.Update(reader);
        }

        public List<ReaderViewModel> FullTextSearch(string searchTerm)
        {
            var readersVm = new List<ReaderViewModel>();

            var readers = string.IsNullOrEmpty(searchTerm) ? _readerRepository.GetAll() : _readerRepository.FullTextSearch(searchTerm);

            if (readers != null && readers.Any())
            {
                readersVm = _mapper.Map<List<ReaderViewModel>>(readers);
            }

            return readersVm;
        }

        public ReaderViewModel GetReaderById(Guid id)
        {
            var reader = _readerRepository.GetById(id);
            return _mapper.Map<ReaderViewModel>(reader); 
        }

        public List<ReaderViewModel> GetReaders()
        {
            var readers = _readerRepository.GetAll();
            return _mapper.Map<List<ReaderViewModel>>(readers);
        }

        public LoanBookViewModel GetReaderWithLoanedBooks(Guid id)
        {
            var reader = _readerRepository.GetReaderWithLoanedBooks(id);

            var readerVm = _mapper.Map<ReaderViewModel>(reader);
            var booksVm = _mapper.Map<IEnumerable<BookViewModel>>(reader.Books);

            var loanedBookViewModel = new LoanBookViewModel
            {
                Reader = readerVm,
                Books = booksVm,
            };

            return loanedBookViewModel;
        }

        public void LoanBook(Guid readerId, Guid bookId)
        {
            var book = _bookRepository.GetById(bookId);

            if (book.HasStock)
            {
                throw new Exception($"There is no stock of this book {book.Name}");
            }

            var reader = _readerRepository.GetReaderWithLoanedBooks(readerId);

            if (!reader.Books.Any())
            {
                reader.Books = new List<Book>();
            }

            var alreadyLoanedBook = reader.Books.FirstOrDefault(b => b.Id == bookId);
            if (alreadyLoanedBook != null)
            {
                throw new Exception($"This reader has already loaned this book");
            }

            reader.LoanBook(book);

            _bookRepository.Update(book);
        }

        public void ReturnBook(Guid readerId, Guid bookId)
        {
            var book = _bookRepository.GetById(bookId);

            var reader = _readerRepository.GetReaderWithLoanedBooks(readerId);

            reader.ReturnBook(book);

            _bookRepository.Update(book);
        }
    }
}
