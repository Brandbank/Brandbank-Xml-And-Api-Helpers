using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Validation.Models
{
    public class ValidationItemType : ItemType
    {
        public int Occurrences { get; set; }
        public int? MaxOccurrences { get; set; }

        public IEnumerable<string> GetErrors()
        {
            var nameTypeErrors = NameTypes.Select(nameType => $"{nameType.ToString()} is invalid for ItemType {ItemTypeId} ({ItemTypeDescription}) on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription})");
            var lookupTypeErrors = LookupTypes.Select(lookupType => $"{lookupType.ToString()} is invalid for ItemType {ItemTypeId} ({ItemTypeDescription}) on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription})");
            var tagTypeErrors = TagTypes?.Select(tagType => $"{tagType.ToString()} is an invalid TagType on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription})");
            var textConstraintErrors = TextConstraints?.Select(tc => $"The text \"{tc.NameType.Text}\" for ItemType {ItemTypeId} ({ItemTypeDescription}) on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription})  is not in the correct format for {tc.NameType.ToString()} accepted format must be {tc.RegExErrorMessage}");
            return nameTypeErrors.Concat(lookupTypeErrors)
                                 .Concat(tagTypeErrors)
                                 .Concat(textConstraintErrors);
        }
    }

    public class ValidationItemTypeComparer : IEqualityComparer<ValidationItemType>
    {
        public bool Equals(ValidationItemType x, ValidationItemType y)
        {
            return x.ItemTypeId == y.ItemTypeId && x.ItemBaseTypeId == y.ItemBaseTypeId;
        }

        public int GetHashCode(ValidationItemType obj)
        {
            return (obj.ItemTypeId + obj.ItemBaseTypeId).GetHashCode();
        }
    }
}
