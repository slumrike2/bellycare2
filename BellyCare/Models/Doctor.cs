namespace BellyCare.Models
{
    public class Doctor : User
    {
        public string Code { get; set; }
        public string Speciality { get; set; }
    }
}