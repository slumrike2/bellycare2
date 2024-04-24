namespace BellyCare.Models
{
    public class Patient : User
    {
        public bool IsFullRegistered { get; set; }
        public string SelectedCulturalGroup { get; set; }
        public int PregnanciesCount { get; set; }
        public int NaturalBirthsCount { get; set; }
        public int CesareanBirthsCount { get; set; }
        public DateTime? LastMenstruationDate { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string Parish { get; set; }
        public string MainStreet { get; set; }
        public string SecondaryStreet { get; set; }
        public string AdressReference { get; set; }
        public bool HasInsurance { get; set; }
        public string InsuranceName { get; set; }
    }
}