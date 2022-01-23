using Application.Interface;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;

        public ReaderController(IReaderService readerService)
        {
            
            _readerService = readerService;
        }

        public IActionResult Index()
        {
            var model = _readerService.GetReaders();
            return View(model);
        }

        public IActionResult AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return View();
            }

            var readerViewModel = _readerService.GetReaderById(id);
            return View(readerViewModel);
        }

        [HttpPost]
        public IActionResult AddOrEdit(ReaderViewModel readerViewModel)
        {
            if (readerViewModel.Id == Guid.Empty)
            {
                _readerService.AddReader(readerViewModel);
            }
            else
            {
                _readerService.EditReader(readerViewModel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult LoanBook(Guid id)
        {
            var model = _readerService.GetReaderWithLoanedBooks(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult LoanBook(Guid readerId, Guid bookId)
        {
            
            _readerService.LoanBook(readerId, bookId);
            var model = _readerService.GetReaderWithLoanedBooks(readerId);

            return View(model);
        }

        [HttpPost]
        public IActionResult ReturnBook(Guid readerId, Guid bookId)
        {
            _readerService.ReturnBook(readerId, bookId);

            return RedirectToAction("LoanBook", new { id = readerId });
        }

        public IActionResult SearchByFullName(string searchTerm)
        {
            var model = _readerService.FullTextSearch(searchTerm);

            return Json(model);
        }
    }
}
