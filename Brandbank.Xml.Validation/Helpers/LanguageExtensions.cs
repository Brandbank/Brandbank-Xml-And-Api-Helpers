using Brandbank.Xml.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Helpers
{
    public static class LanguageExtensions
    {
        public static IEnumerable<InvalidItemType> GetInvalidItemTypes(this Language language, IEnumerable<ItemType> validationItemTypes)
        {
            return language.ItemTypes
                              .Where(messageTypeNameTextLookup => validationItemTypes.FirstOrDefault(itemType => ((itemType.ItemBaseTypeId == messageTypeNameTextLookup.ItemBaseTypeId) && (itemType.ItemTypeId == messageTypeNameTextLookup.ItemTypeId))) == null)
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
                             .Where(messageTypeNameTextLookup => validationItemTypes.FirstOrDefault(databaseItemType => databaseItemType.ItemBaseTypeId == messageTypeNameTextLookup.ItemBaseTypeId && databaseItemType.ItemTypeId == messageTypeNameTextLookup.ItemTypeId) != null)
                             .Select(messageTypeNameTextLookup => new
                             {
                                 messageTypeNameTextLookup,
                                 itemType = productValidationData.ItemTypes.FirstOrDefault(it => it.ItemTypeId == messageTypeNameTextLookup.ItemTypeId)
                             }
                             )
                             .Select(mtntl => new ValidationItemType
                             {
                                 ItemTypeId = mtntl.messageTypeNameTextLookup.ItemTypeId,
                                 ItemTypeDescription = mtntl.messageTypeNameTextLookup.ItemTypeDescription,
                                 ItemBaseTypeDescription = mtntl.itemType.ItemBaseTypeDescription,
                                 ItemBaseTypeId = mtntl.itemType.ItemBaseTypeId,
                                 NameTypes = mtntl.messageTypeNameTextLookup.NameTypes.Except(validationItemTypes
                                                                                            .Where(databaseNameTextLookup => databaseNameTextLookup.ItemBaseTypeId == mtntl.messageTypeNameTextLookup.ItemBaseTypeId && databaseNameTextLookup.ItemTypeId == mtntl.messageTypeNameTextLookup.ItemTypeId)
                                                                                            .SelectMany(databaseNameTextLookup => databaseNameTextLookup.NameTypes), new IdValueComparer()),
                                 LookupTypes = mtntl.messageTypeNameTextLookup.LookupTypes.Except(validationItemTypes
                                                                                                .Where(databaseNameTextLookup => databaseNameTextLookup.ItemBaseTypeId == mtntl.messageTypeNameTextLookup.ItemBaseTypeId && databaseNameTextLookup.ItemTypeId == mtntl.messageTypeNameTextLookup.ItemTypeId)
                                                                                                .SelectMany(databaseNameTextLookup => databaseNameTextLookup.LookupTypes), new IdValueComparer()),
                                 TagTypes = mtntl.messageTypeNameTextLookup.TagTypes.Except(validationItemTypes.SelectMany(databaseNameTextLookup => databaseNameTextLookup.TagTypes), new IdValueComparer()),
                                 TextConstraints = mtntl.messageTypeNameTextLookup.NameTypes
                                                                                    .Select(nt => new { NameType = nt, DatabaseNameTexts = productValidationData.ItemNameTypes.Where(dbint => dbint.ItemBaseTypeId == mtntl.messageTypeNameTextLookup.ItemBaseTypeId && dbint.ItemTypeId == mtntl.messageTypeNameTextLookup.ItemTypeId && dbint.ItemNameTypeId == nt.Id) })
                                                                                    .Where(nts => nts.DatabaseNameTexts.FirstOrDefault() != null && nts.DatabaseNameTexts.First().RegEx != null && !nts.DatabaseNameTexts.First().PassesRegex(nts.NameType.Text))
                                                                                    .Select(nts => new TextConstraint()
                                                                                    {
                                                                                        NameType = nts.NameType,
                                                                                        RegExErrorMessage = nts.DatabaseNameTexts.First().RegExErrorMessage
                                                                                    })

                             });
        }

        public static IEnumerable<InvalidItemTypeOccurrences> GetInvalidItemTypeOccurances(this Language language, IEnumerable<ValidationItemType> validationItemTypes)
        {
            return language.ItemTypes
                                .Distinct(new ValidationItemTypeComparer())
                                .Select(it => new
                                {
                                    ItemType = it,
                                    DbItemType = validationItemTypes.FirstOrDefault(dbit => dbit.ItemTypeId == it.ItemTypeId),
                                }
                                        )
                                .Where(itc => itc.DbItemType != null && itc.DbItemType.MaxOccurrences.HasValue && (itc.ItemType.Occurrences > itc.DbItemType.MaxOccurrences))
                                .Select(itc => new InvalidItemTypeOccurrences()
                                {
                                    ItemTypeId = itc.ItemType.ItemTypeId,
                                    ItemBaseTypeId = itc.ItemType.ItemBaseTypeId,
                                    ItemTypeDescription = itc.ItemType.ItemTypeDescription,
                                    ItemBaseTypeDescription = itc.DbItemType.ItemBaseTypeDescription,
                                    MaxOccurrances = itc.DbItemType.MaxOccurrences.Value,
                                    Occurrances = itc.ItemType.Occurrences,
                                });
        }
    }
}
