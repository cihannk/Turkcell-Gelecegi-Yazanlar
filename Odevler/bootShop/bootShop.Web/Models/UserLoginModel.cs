using System.ComponentModel.DataAnnotations;

namespace bootShop.Web.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Username boş bırakılamaz"), ]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username boş bırakılamaz"),]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
