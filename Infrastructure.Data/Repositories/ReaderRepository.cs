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
    public class ReaderRepository : BaseRepository<Reader>, IReaderRepository
    {
        private readonly LibraryDbContext _dbContext;

        public ReaderRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Reader> FullTextSearch(string searchTerm)
        {
            return _dbContext.Readers.AsEnumerable().Where(r => r.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || r.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || r.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public Reader GetReaderWithLoanedBooks(Guid id)
        {
            var reader = _dbContext.Readers.Include(x => x.Books).FirstOrDefault(reader => reader.Id == id);

            return reader;
        }
    }
}
