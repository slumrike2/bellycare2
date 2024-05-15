namespace BellyCare.Models
{
    public class ChatMessage
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }



        public bool IsMine { get; set; }
        public bool IsNotMine => !IsMine;
    }
}