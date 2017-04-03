using Brandbank.Xml.Validation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Validation.Helpers
{
    public static class LanguageExtensions
    {
        public static IEnumerable<InvalidItemType> GetInvalidItemTypes(this Language language, IEnumerable<ItemType> validationItemTypes)
        {
            return language.ItemTypes
                                .Where(messageTypeNameTextLookup => 
                                validationItemTypes.FirstOrDefault(itemType => 
                                    ((itemType.ItemBaseTypeId == messageTypeNameTextLookup.ItemBaseTypeId) 
                                    && (itemType.ItemTypeId == messageTypeNameTextLookup.ItemTypeId))) == null)
                                .Select(messageTypeNameTextLookup => new InvalidItemType
                                {
                                    ItemBaseTypeId = messageTypeNameTextLookup.ItemBaseTypeId,
                                    ItemBaseTypeDescription = validationItemTypes.FirstOrDefault(dntl => dntl.ItemBaseTypeId == messageTypeNameTextLookup.ItemBaseTypeId)?.ItemBaseTypeDescription,
                                    ItemTypeDescription = messageTypeNameTextLookup.ItemTypeDescription,
                                    ItemTypeId = messageTypeNameTextLookup.ItemTypeId
                                });
        }

        public static IEnumerable<ValidationItemType> GetInvalidNameAndLookups(this Language language, IEnumerable<ItemType> validationItemTypes, ProductValidationData productValidationData)
        {

            return language.ItemTypes
                             .Where(messageTypeNameTextLookup => 
                                validationItemTypes.FirstOrDefault(sourceItemType => 
                                    sourceItemType.ItemBaseTypeId == messageTypeNameTextLookup.ItemBaseTypeId 
                                    && sourceItemType.ItemTypeId == messageTypeNameTextLookup.ItemTypeId) != null)
                             .Select(messageTypeNameTextLookup => new ValidationItemTypeWithSourceItemType
                             {
                                 ValidationItemType = messageTypeNameTextLookup,
                                 SourceItemType = productValidationData.ItemTypes.FirstOrDefault(it => it.ItemTypeId == messageTypeNameTextLookup.ItemTypeId)
                             })
                             .Select(valItemWithSourceItemType => new ValidationItemType
                             {
                                 ItemTypeId = valItemWithSourceItemType.ValidationItemType.ItemTypeId,
                                 ItemTypeDescription = valItemWithSourceItemType.ValidationItemType.ItemTypeDescription,
                                 ItemBaseTypeDescription = valItemWithSourceItemType.SourceItemType.ItemBaseTypeDescription,
                                 ItemBaseTypeId = valItemWithSourceItemType.SourceItemType.ItemBaseTypeId,
                                 NameTypes = valItemWithSourceItemType.GetInvalidNameTypes(validationItemTypes),
                                 LookupTypes = valItemWithSourceItemType.GetInvalidLookupTypes(validationItemTypes),
                                 TagTypes = valItemWithSourceItemType.GetInvalidTagTypes(validationItemTypes),
                                 TextConstraints = valItemWithSourceItemType.GetTextConstraints(productValidationData)

                             });
        }

        public static IEnumerable<InvalidItemTypeOccurrences> GetInvalidItemTypeOccurances(this Language language, IEnumerable<ValidationItemType> validationItemTypes)
        {
            return language.ItemTypes
                                .Distinct(new ValidationItemTypeComparer())
                                .Select(it => new
                                {
                                    ItemType = it,
                                    SourceItemType = validationItemTypes.FirstOrDefault(dbit => dbit.ItemTypeId == it.ItemTypeId),
                                })
                                .Where(itc => 
                                    itc.SourceItemType != null 
                                    && itc.SourceItemType.MaxOccurrences.HasValue 
                                    && (itc.ItemType.Occurrences > itc.SourceItemType.MaxOccurrences))
                                .Select(itc => new InvalidItemTypeOccurrences()
                                {
                                    ItemTypeId = itc.ItemType.ItemTypeId,
                                    ItemBaseTypeId = itc.ItemType.ItemBaseTypeId,
                                    ItemTypeDescription = itc.ItemType.ItemTypeDescription,
                                    ItemBaseTypeDescription = itc.SourceItemType.ItemBaseTypeDescription,
                                    MaxOccurrances = itc.SourceItemType.MaxOccurrences.Value,
                                    Occurrances = itc.ItemType.Occurrences,
                                });
        }
    }
}
