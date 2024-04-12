using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.Models
{
    public class User
    {
        public string Names { get; set; } = string.Empty;
        public string Lastnames { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string SelectedCulturalGroup { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Canton { get; set; } = string.Empty;
        public string MainStreet { get; set; } = string.Empty;
        public string SecondaryStreet { get; set; } = string.Empty;
        public bool HasInsurance { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
