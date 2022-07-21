namespace ChatApp.Shared.Entities
{
    public class Message
    {
        public string? SenderId { get; set; }
        public string? UserName { get; set; }
        public string? Subject { get; set; }
        public string? DateTime { get; set; }
        public List<string>? ReceiverIds { get; set; }
        public bool Mine { get; set; }
        public string CSS => Mine ? "sent" : "received";
    }
}
