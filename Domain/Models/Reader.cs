using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reader : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Book> Books { get; set; }

        public void LoanBook(Book book)
        {
            book.ReduceStock();
            Books.Add(book);
        }

        public void ReturnBook(Book book)
        {
            book.AddStock();
            Books.Remove(book);
        }
    }
}
