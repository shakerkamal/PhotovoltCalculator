using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(Message message)
        {
            using (MimeMessage email = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress("", _emailConfig.EmailFrom);
                email.From.Add(emailFrom);
                foreach (var toAddress in message.ToAddresses)
                {
                    MailboxAddress emailTo = new MailboxAddress("", toAddress);
                    email.To.Add(emailTo);
                }

                email.Subject = message.Subject;

                email.Body = new TextPart(TextFormat.Html) { Text = message.Body };

                if (message.Attachments != null && message.Attachments.Count > 0)
                {
                    var multipart = new Multipart("mixed")
                    {
                        new TextPart(TextFormat.Html) { Text = message.Body }
                    };

                    foreach (var attachment in message.Attachments)
                    {
                        var attachmentPart = new MimePart(attachment.ContentType)
                        {
                            Content = new MimeContent(attachment.OpenReadStream()),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = attachment.FileName
                        };

                        multipart.Add(attachmentPart);
                    }

                    email.Body = multipart;
                }
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailConfig.SmtpHost, _emailConfig.SmtpPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.SmtpUser, _emailConfig.SmtpPass);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}