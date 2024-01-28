using Microsoft.AspNetCore.Http;

namespace EmailService
{
    public class Message
    {
        public List<string> ToAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFileCollection Attachments { get; set; }
    }
}
