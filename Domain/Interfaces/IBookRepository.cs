using Domain.Interfaces.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        IEnumerable<Book> FullTextSearch(string searchTerm);

        Book GetBookWithReaders(Guid id);
    }
}
