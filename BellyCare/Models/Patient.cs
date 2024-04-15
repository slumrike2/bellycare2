namespace BellyCare.Models
{
    public class Patient : User
    {
        public bool? HasInsurance { get; set; }
    }
}