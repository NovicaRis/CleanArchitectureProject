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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public BookViewModel AddBook(BookViewModel bookRequest)
        {
            var book = _mapper.Map<Book>(bookRequest);
            var addedBook = _bookRepository.Add(book);
            return _mapper.Map<BookViewModel>(addedBook);
        }

        public void EditBook(BookViewModel bookRequest)
        {
            var book = _mapper.Map<Book>(bookRequest);
            _bookRepository.Update(book);
        }

        public List<BookViewModel> FullTextSearch(string searchTerm)
        {
            var booksVm = new List<BookViewModel>();

            var books = string.IsNullOrEmpty(searchTerm) ? _bookRepository.GetAll() : _bookRepository.FullTextSearch(searchTerm);

            if (books != null && books.Any())
            {
                booksVm = _mapper.Map<List<BookViewModel>>(books);
            }

            return booksVm;
        }

        public BookViewModel GetBookById(Guid id)
        {
            var book = _bookRepository.GetById(id);

            var bookVm = _mapper.Map<BookViewModel>(book);

            return bookVm;
        }

        public List<BookViewModel> GetBooks()
        {
            var books = _bookRepository.GetAll();
            var booksVm = _mapper.Map<List<BookViewModel>>(books);

            return booksVm;
        }

        public LoanReaderViewModel GetBookWithReaders(Guid id)
        {
            var book = _bookRepository.GetBookWithReaders(id);

            return new LoanReaderViewModel()
            {
                Book = _mapper.Map<BookViewModel>(book),
                Readers = _mapper.Map<IEnumerable<ReaderViewModel>>(book.Readers)
            };
        }
    }
}
