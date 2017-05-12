namespace Brandbank.Xml.Validation.Models
{
    public class InvalidItemType : ItemType
    {
        public string ProductDescription { get; set; }

        public string GetError()
        {
            return $"ItemType {ItemTypeId} ({ItemTypeDescription}) is invalid on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription})";
        }
    }
}
