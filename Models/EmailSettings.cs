namespace API_FEB.Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }  // ✅ Add this property
        public string Password { get; set; }  // ✅ Add this property
        public bool EnableSsl { get; set; }
    }
}
