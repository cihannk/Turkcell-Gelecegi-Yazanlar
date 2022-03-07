using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public int CartId { get; set; }
        public Cart Cart { get; set; }
        public DateTime SignedUpDate { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string Address { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Order> Orders { get; set; }
    }
}
