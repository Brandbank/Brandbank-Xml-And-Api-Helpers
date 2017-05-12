using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Models;
using System.Collections.Generic;
using System.Linq;
using static Brandbank.Xml.Validation.Models.Enums;

namespace Brandbank.Xml.Validation.Helpers
{
    public static class MessageTypeExtensions
    {
        //Base Type 1
        public static IEnumerable<ValidationItemType> GetStatementsForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var statements = languageType.ItemTypeGroup
                                            .Where(i => i.Statements != null)
                                            .SelectMany(itemTypeGroup => itemTypeGroup.Statements
                                                .SelectMany(statement => statement.Statement
                                                .Select(lookup => new NameTextLookup
                                                {
                                                    BaseTypeId = ((int)BaseTypes.Statements).ToString(),
                                                    ItemTypeDescription = statement.Name,
                                                    ItemTypeId = statement.Id,
                                                    NameTypeId = lookup.Id,
                                                    NameTextValue = lookup.Value,
                                                    Occurrances = itemTypeGroup.Statements.Count(s => s.Id == statement.Id)
                                                })));

            return statements.GetValidationItemTypes();
        }

        //Base Type 2
        public static IEnumerable<ValidationItemType> GetNameLookupsForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var nameLookups = languageType.ItemTypeGroup
                                            .Where(i => i.NameLookups != null)
                                            .SelectMany(itemTypeGroup => itemTypeGroup.NameLookups
                                            .SelectMany(itemType => itemType.NameValue
                                            .Select(lookup => new NameTextLookup
                                            {
                                                BaseTypeId = ((int)BaseTypes.NameLookup).ToString(),
                                                ItemTypeDescription = itemType.Name,
                                                ItemTypeId = itemType.Id,
                                                NameTypeId = lookup.Name.Id,
                                                LookupId = lookup.Value.Id,
                                                NameTextValue = lookup.Name.Value,
                                                LookupTextValue = lookup.Value.Value,
                                                Occurrances = itemTypeGroup.NameLookups.Count(n => n.Id == itemType.Id)
                                            })));

            return nameLookups.GetValidationItemTypes();
        }

        //Base Type 3
        public static IEnumerable<ValidationItemType> GetNameTextsForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var nameTexts = languageType.ItemTypeGroup
                                            .Where(i => i.NameTextItems != null)
                                            .SelectMany(itemTypeGroup => itemTypeGroup.NameTextItems
                                            .SelectMany(itemType => itemType.NameText
                                            .Select(nameText => new NameTextLookup
                                            {
                                                BaseTypeId = ((int)BaseTypes.NameText).ToString(),
                                                ItemTypeDescription = itemType.Name,
                                                ItemTypeId = itemType.Id,
                                                NameTypeId = nameText.Name.Id,
                                                NameTextValue = nameText.Name.Value,
                                                NameTextText = nameText.Text.Value,
                                                Occurrances = itemTypeGroup.NameTextItems.Count(nt => nt.Id == itemType.Id)
                                            })));
            return nameTexts.GetValidationItemTypes();
        }

        //Base Type 4
        public static IEnumerable<ValidationItemType> GetMemosForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var memos = languageType.ItemTypeGroup
                                        .Where(i => i.Memo != null)
                                        .SelectMany(itemTypeGroup => itemTypeGroup.Memo
                                        .Select(memo => new NameTextLookup
                                        {
                                            BaseTypeId = ((int)BaseTypes.Memo).ToString(),
                                            ItemTypeDescription = memo.Name,
                                            ItemTypeId = memo.Id,
                                            Occurrances = itemTypeGroup.Memo.Count(m => m.Id == memo.Id)
                                        }));
            return memos.GetValidationItemTypes();
        }

        //Base Type 5
        public static IEnumerable<ValidationItemType> GetLongTextsForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var longTexts = languageType.ItemTypeGroup
                                            .Where(i => i.LongTextItems != null)
                                            .SelectMany(itemTypeGroup => itemTypeGroup.LongTextItems
                                            .Select(longText => new NameTextLookup
                                            {
                                                BaseTypeId = ((int)BaseTypes.LongText).ToString(),
                                                ItemTypeDescription = longText.Name,
                                                ItemTypeId = longText.Id,
                                                Occurrances = itemTypeGroup.LongTextItems.Count(l => l.Id == longText.Id)
                                            }));
            return longTexts.GetValidationItemTypes();
        }

        //Base Type 6
        public static IEnumerable<ValidationItemType> GetNameTextLookupsForLanguage(this MessageType messageType, LanguageType languageType)
        {

            var nameTextLookups = languageType.ItemTypeGroup
                                            .Where(i => i.NameTextLookups != null)
                                            .SelectMany(itemTypeGroup => itemTypeGroup.NameTextLookups
                                                .SelectMany(nameTextLookup => nameTextLookup.NameValueText
                                                    .Select(nameValueText => new NameTextLookup
                                                    {
                                                        BaseTypeId = ((int)BaseTypes.NameTextLookup).ToString(),
                                                        ItemTypeDescription = nameTextLookup.Name,
                                                        ItemTypeId = nameTextLookup.Id,
                                                        LookupId = nameValueText.Value.Id,
                                                        NameTypeId = nameValueText.Name.Id,
                                                        LookupTextValue = nameValueText.Value.Value,
                                                        NameTextValue = nameValueText.Name.Value,
                                                        NameTextText = nameValueText.Text.Value,
                                                        Occurrances = itemTypeGroup.NameTextLookups.Count(ntl => ntl.Id == nameTextLookup.Id)
                                                    })));

            return nameTextLookups.GetValidationItemTypes();
        }

        //Base Type 7
        public static IEnumerable<ValidationItemType> GetTexturalNutritionsForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var texturalNutritions = languageType.ItemTypeGroup
                                    .Where(i => i.TextualNutrition != null)
                                    .SelectMany(itemTypeGroup => itemTypeGroup.TextualNutrition
                                    .Select(nutrition => new NameTextLookup
                                    {
                                        BaseTypeId = ((int)BaseTypes.Nutrition).ToString(),
                                        ItemTypeDescription = itemTypeGroup.Name,
                                        ItemTypeId = nutrition.Id.ToString(),
                                        Occurrances = itemTypeGroup.TextualNutrition.Count(tn => tn.Id == nutrition.Id)
                                    }));

            return texturalNutritions.GetValidationItemTypes();
        }

        //Base Type 8
        public static IEnumerable<ValidationItemType> GetCalculatedNutritionsForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var calculatedNutritions = languageType.ItemTypeGroup
                                        .Where(i => i.NumericNutrition != null)
                                        .SelectMany(itemTypeGroup => itemTypeGroup.NumericNutrition
                                        .SelectMany(numericNutrition => numericNutrition.NutrientValues
                                        .Select(nutrientValue => new NameTextLookup
                                        {
                                            BaseTypeId = ((int)BaseTypes.CalculatedNutrition).ToString(),
                                            ItemTypeDescription = numericNutrition.Name,
                                            ItemTypeId = numericNutrition.Id.ToString(),
                                            NameTypeId = nutrientValue.Id,
                                            NameTextValue = nutrientValue.Name,
                                            Occurrances = itemTypeGroup.NumericNutrition.Count(nn => nn.Id == numericNutrition.Id)
                                        })));
            return calculatedNutritions.GetValidationItemTypes();
        }

        //Base Type 9
        public static IEnumerable<ValidationItemType> GetTaggedMemosForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var taggedMemos = languageType.ItemTypeGroup
                                .Where(i => i.TaggedMemo != null)
                                .SelectMany(itemTypeGroup => itemTypeGroup.TaggedMemo
                                .Select(taggedMemo => new NameTextLookup
                                {
                                    BaseTypeId = ((int)BaseTypes.TaggedMemo).ToString(),
                                    ItemTypeDescription = taggedMemo.Name,
                                    ItemTypeId = taggedMemo.Id,
                                    Tags = taggedMemo.Tags?.Select(tag => new IdValue
                                    {
                                        Id = tag.TagTypeId,
                                        Value = tag.TagType1
                                    }),
                                    Occurrances = itemTypeGroup.TaggedMemo.Count(tm => tm.Id == taggedMemo.Id)
                                }));

            return taggedMemos.GetValidationItemTypes();
        }

        //Base Type 10
        public static IEnumerable<ValidationItemType> GetTaggedLongTextForLanguage(this MessageType messageType, LanguageType languageType)
        {
            var taggedLongTexts = languageType.ItemTypeGroup
                                .Where(i => i.TaggedLongTextItems != null)
                                .SelectMany(itemTypeGroup => itemTypeGroup.TaggedLongTextItems
                                .Select(taggedLongText => new NameTextLookup
                                {
                                    BaseTypeId = ((int)BaseTypes.TaggedLongText).ToString(),
                                    ItemTypeDescription = taggedLongText.Name,
                                    ItemTypeId = taggedLongText.Id,
                                    Tags = taggedLongText.Tags?.Select(tag => new IdValue
                                    {
                                        Id = tag.TagTypeId,
                                        Value = tag.TagType1
                                    }),
                                    Occurrances = itemTypeGroup.TaggedLongTextItems.Count(tlt => tlt.Id == taggedLongText.Id)
                                }));

            return taggedLongTexts.GetValidationItemTypes();
        }
    }
}
