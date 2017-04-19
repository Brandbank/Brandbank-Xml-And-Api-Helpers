namespace Brandbank.Api.NotifyClient
{
    public interface INotifyClient
    {
        void Notify(string subject, string body);
    }
}