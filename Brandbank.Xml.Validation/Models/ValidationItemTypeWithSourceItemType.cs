using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class ValidationItemTypeWithSourceItemType
    {
        public ValidationItemType ValidationItemType { get; set; }
        public ItemType SourceItemType { get; set; }

        public IEnumerable<IdValue> GetInvalidNameTypes(IEnumerable<ItemType> validationItemTypes)
        {
            return ValidationItemType.NameTypes
                    .Except(validationItemTypes
                                            .Where(sourceNameTextLookup =>
                                            sourceNameTextLookup.ItemBaseTypeId == ValidationItemType.ItemBaseTypeId
                                            && sourceNameTextLookup.ItemTypeId == ValidationItemType.ItemTypeId
                                            )
                                            .SelectMany(sourceNameTextLookup => sourceNameTextLookup.NameTypes), new IdValueComparer()
                            );
        }

        public IEnumerable<IdValue> GetInvalidLookupTypes(IEnumerable<ItemType> validationItemTypes)
        {
            return ValidationItemType.LookupTypes
                    .Except(validationItemTypes
                                             .Where(sourceNameTextLookup => 
                                             sourceNameTextLookup.ItemBaseTypeId == ValidationItemType.ItemBaseTypeId 
                                             && sourceNameTextLookup.ItemTypeId == ValidationItemType.ItemTypeId)
                                             .SelectMany(databaseNameTextLookup => databaseNameTextLookup.LookupTypes), new IdValueComparer());
        }

        public IEnumerable<IdValue> GetInvalidTagTypes(IEnumerable<ItemType> validationItemTypes)
        {
            return ValidationItemType.TagTypes
                    .Except(validationItemTypes.SelectMany(sourceNameTextLookup => sourceNameTextLookup.TagTypes), new IdValueComparer());
        }

        public IEnumerable<TextConstraint> GetTextConstraints(ProductValidationData productValidationData)
        {
            return ValidationItemType.NameTypes
                                    .Select(nt => new {
                                        NameType = nt,
                                        SourceNameTexts = productValidationData.ItemNameTypes.Where(dbint =>
                                        dbint.ItemBaseTypeId == ValidationItemType.ItemBaseTypeId
                                        && dbint.ItemTypeId == ValidationItemType.ItemTypeId
                                        && dbint.ItemNameTypeId == nt.Id)
                                    })
                                    .Where(nts =>
                                        nts.SourceNameTexts.FirstOrDefault() != null
                                        && nts.SourceNameTexts.First().RegEx != null
                                        && !nts.SourceNameTexts.First().PassesRegex(nts.NameType.Text)
                                    )
                                    .Select(nts => new TextConstraint()
                                    {
                                        NameType = nts.NameType,
                                        RegExErrorMessage = nts.SourceNameTexts.First().RegExErrorMessage
                                    });
        }
    }
}
