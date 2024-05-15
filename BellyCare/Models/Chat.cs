namespace BellyCare.Models
{
    public class Chat
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public int UnreadMessagesByDoctor { get; set; }
        public int UnreadMessagesByPatient { get; set; }

        public Dictionary<string, ChatMessage> Messages { get; set; }
    }
}