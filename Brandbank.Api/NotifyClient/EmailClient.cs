using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Brandbank.Api.NotifyClient
{
    public class EmailClient : INotifyClient
    {
        private readonly string _host;
        private readonly IEnumerable<string> _to;
        private readonly string _from;

        public EmailClient(string host, string from, string to) : this (host, from, new[] { to })
        {
        }

        public EmailClient(string host, string from, IEnumerable<string> to)
        {
            _host = host;
            _from = from;
            _to = to;
        }

        public void Notify(string subject, string body)
        {
            using (var mail = new MailMessage())
            using (var client = new SmtpClient(_host, 25))
            {
                mail.From = new MailAddress(_from);
                mail.To.Add(string.Join(",", _to));
                mail.Body = body;
                mail.Subject = subject;
                mail.IsBodyHtml = false;
                mail.BodyEncoding = Encoding.UTF8;

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
            }
        }
    }
}
