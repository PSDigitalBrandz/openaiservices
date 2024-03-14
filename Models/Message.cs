namespace openaiservices.Models
{
    public class Message
    {
        public long TimeStamp { get; set; }
        public string? Body { get; set; }
        public bool IsRequest { get; set; }
        public Message(string body, bool isRequest)
        {
            TimeStamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            Body = body;
            IsRequest = isRequest;
        }
    }
}
