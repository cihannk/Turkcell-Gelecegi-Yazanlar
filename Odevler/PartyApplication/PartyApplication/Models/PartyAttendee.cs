using System.ComponentModel.DataAnnotations;

namespace PartyApplication.Models
{
    public class PartyAttendee
    {
        [Required(ErrorMessage = "İsim boş olamaz")]
        [MinLength(2, ErrorMessage = "En az iki karakter olmalı.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yaş boş olamaz")]
        public int Age { get; set; }
        [Required(ErrorMessage ="Gelecek misin belirtmen gerek")]
        public bool DoesUserCome { get; set; }
    }
}
