namespace Brandbank.Xml.Models.Message
{
    public class BrandbankMessageSummaryProduct : IBrandbankMessageSummaryProduct
    {
        public string ExternalId { get; set; }
        public string ProductDescription { get; set; }
        public int Pvid { get; set; }
        public string Gtin { get; set; }
        public string UpdateType { get; set; }
    }
}
