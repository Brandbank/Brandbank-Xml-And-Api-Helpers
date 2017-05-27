using Brandbank.Xml.Logging;

namespace Brandbank.Api.NotifyClient
{
    public class EmailClientLogger : INotifyClient
    {
        private readonly ILogger<INotifyClient> _logger;
        private readonly INotifyClient _emailClient;

        public EmailClientLogger(ILogger<INotifyClient> logger, INotifyClient emailClient)
        {
            _logger = logger;
            _emailClient = emailClient;
        }

        public void Notify(string subject, string body)
        {
            _logger.LogDebug($"Sending email with subject {subject}");
            _emailClient.Notify(subject, body);
            _logger.LogDebug($"Sent email with subject {subject}");
        }
    }
}
