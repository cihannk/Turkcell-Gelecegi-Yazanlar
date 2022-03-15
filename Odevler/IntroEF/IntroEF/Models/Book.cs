using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class Book
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public double Price { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; } = new Author();
        public string Description { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Comment> Comments { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
