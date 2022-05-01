using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Entities
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime? RegisterDate { get; set; }
        public IList<Address> Addresses { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
