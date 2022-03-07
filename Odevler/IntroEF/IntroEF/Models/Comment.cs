using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime PostDate { get; set; }
        public string UserComment { get; set; }
    }
}
