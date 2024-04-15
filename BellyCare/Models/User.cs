using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.Models
{
    public class User
    {
        public string Names { get; set; }
        public string Lastnames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Birthdate { get; set; }
        public string SelectedCulturalGroup { get; set; }
        public string IdentificationNumber { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string MainStreet { get; set; }
        public string SecondaryStreet { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFullRegistered { get; set; }
    }
}
