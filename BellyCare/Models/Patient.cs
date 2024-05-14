using Firebase.Database;

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
        public int CurrentWeek => (DateTime.Now - (LastMenstruationDate ?? DateTime.Now)).Days / 7;
        public string Province { get; set; }
        public string Canton { get; set; }
        public string Parish { get; set; }
        public string MainStreet { get; set; }
        public string SecondaryStreet { get; set; }
        public string AdressReference { get; set; }
        public bool HasInsurance { get; set; }
        public string InsuranceName { get; set; }
        public string DoctorCode { get; set; }
        public double CurrentIMC { get; set; }

        public Dictionary<string, TrackEntry> TrackEntries { get; set; }
    }

    public class FirebasePatient
    {
        public string Id { get; set; }
        public Patient Patient { get; set; }
    }

}