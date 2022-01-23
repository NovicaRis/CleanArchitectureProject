using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Book : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public int InStock { get; set; }

        public ICollection<Reader> Readers { get; set; }

        public void ReduceStock()
        {
            --InStock;
        }

        public void AddStock()
        {
            ++InStock;
        }

        public bool HasStock
        {
            get
            {
                return InStock > 0;
            }
        }
    }
}
