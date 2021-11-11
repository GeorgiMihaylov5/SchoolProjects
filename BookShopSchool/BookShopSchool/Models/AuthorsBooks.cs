using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSchool.Models
{
    public class AuthorsBooks
    {
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
