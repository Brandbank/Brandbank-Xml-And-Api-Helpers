namespace Brandbank.Xml.Models.Message
{
    public interface IBrandbankMessageSummaryProduct
    {
        string ExternalId { get; set; }
        string ProductDescription { get; set; }
        int Pvid { get; set; }
        string Gtin { get; set; }
        string UpdateType { get; set; }
    }
}
