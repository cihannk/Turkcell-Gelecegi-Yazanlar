using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Response
{
    public class GetUsersResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime? RegisterDate { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
