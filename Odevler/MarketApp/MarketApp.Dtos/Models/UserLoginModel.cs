using System.ComponentModel.DataAnnotations;

namespace MarketApp.Dtos.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email boş bırakılamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage= "Şifre boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
