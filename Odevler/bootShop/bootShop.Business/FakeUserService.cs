using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public class FakeUserService : IUserService
    {
        private List<User> users = new List<User> {
            new User { Id=1, Email="denem1@gmail.com", FullName="Deneme 1", Password="123", Username="deneme1", Role="admin"},
            new User { Id=2, Email="deneme2@gmail.com", FullName="Deneme 2", Password="123", Username="deneme2", Role="editor"},
            new User { Id=3, Email="deneme3@gmail.com", FullName="Deneme 3", Password="123", Username="deneme3", Role="client"}};
        public User ValidateUser(string username, string password)
        {
            return users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
