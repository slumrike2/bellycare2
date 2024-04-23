using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.Models
{
    public class User
    {
        public string Names { get; set; }
        public string Lastnames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
