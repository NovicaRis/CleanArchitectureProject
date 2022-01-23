using Application.Interface;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;

        public BookController(IBookService bookService, IReaderService readerService)
        {
            _bookService = bookService;
            _readerService = readerService;
        }

        public IActionResult Index()
        {
            var bookListViewModel = _bookService.GetBooks();
            return View(bookListViewModel);
        }

        public IActionResult AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return View();
            }

            var editBookViewModel = _bookService.GetBookById(id);
            return View(editBookViewModel);
        }

        [HttpPost]
        public IActionResult AddOrEdit(BookViewModel bookViewModel)
        {
            if (bookViewModel.Id == Guid.Empty)
            {
                _bookService.AddBook(bookViewModel);
            }
            else
            {
                _bookService.EditBook(bookViewModel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult SearchByFullName(string searchTerm)
        {
            var model = _bookService.FullTextSearch(searchTerm);

            return Json(model);
        }

        public IActionResult Loan(Guid id)
        {
            var model = _bookService.GetBookWithReaders(id);

            return View("LoanReader", model);
        }

        [HttpPost]
        public IActionResult Loan(Guid readerId, Guid bookId)
        {
            _readerService.LoanBook(readerId, bookId);

            var model = _bookService.GetBookWithReaders(bookId);

            return View("LoanReader", model);
        }

        [HttpPost]
        public IActionResult Return(Guid readerId, Guid bookId)
        {
            _readerService.ReturnBook(readerId, bookId);
            return RedirectToAction("Loan", new { id = bookId });
        }
    }
}
