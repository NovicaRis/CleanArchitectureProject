using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Display(Name = "In Stock")]
        public int InStock { get; set; }
    }
}
