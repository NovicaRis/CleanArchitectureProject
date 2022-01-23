using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> FullTextSearch(string searchTerm)
        {
            return _dbContext.Books.AsEnumerable().Where(b => (b.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
           || b.AuthorName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
           || b.ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) && b.InStock > 0);
        }

        public Book GetBookWithReaders(Guid id)
        {
            var book = _dbContext.Books.Include(x => x.Readers).FirstOrDefault(book => book.Id == id);

            return book;
        }
    }
}
