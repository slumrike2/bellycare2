using Firebase.Database;

namespace BellyCare.Models
{
    public class Doctor : User
    {
        public string Code { get; set; }
        public string Speciality { get; set; }
    }

    public class FirebaseDoctor
    {
        public string Id { get; set; }
        public Doctor Doctor { get; set; }
    }
}