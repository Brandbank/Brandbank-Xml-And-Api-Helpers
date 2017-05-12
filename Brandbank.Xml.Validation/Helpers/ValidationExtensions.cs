using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Validation.Helpers
{
    public static class ValidationExtensions
    {
        public static IEnumerable<InvalidProductData> GetInvalidData(this ProductValidationData productValidationData, IEnumerable<ValidationProduct> messageProducts)
        {
            var sourceItemTypes = productValidationData.GetItemTypes();

            var invalidData = messageProducts.Select(product => new InvalidProductData
            {
                ProductCode = product.ProductCodes,
                DuplicateImageTypes = product.GetImageShotsWithDuplicates(),
                InvalidLanguageData = product.Languages?.Select(lang => new InvalidLanguageData
                {
                    ProductCode = product.ProductCodes,
                    LanguageCode = lang.Code,
                    InvalidItemTypes = lang.GetInvalidItemTypes(sourceItemTypes),
                    InvalidNameAndLookUps = lang.GetInvalidNameAndLookups(sourceItemTypes, productValidationData),
                    InvalidItemTypeOccurances = lang.GetInvalidItemTypeOccurances(sourceItemTypes)

                }) ?? Enumerable.Empty<InvalidLanguageData>()
            });

            return invalidData;
        }

        public static IEnumerable<string> GetAllInvalidDataInMessage(this MessageType model, ProductValidationData productValidationData)
        {
            var validationProducts = model.GetProductsForValidation();

            var errors = productValidationData.GetInvalidData(validationProducts)
                                               .SelectMany(ipd => ipd.GetErrors());

            return errors;
        }

        public static IEnumerable<ValidationItemType> GetItemTypes(this ProductValidationData productValidationData)
        {
            var itemTypes = productValidationData.ItemTypes
                    .Select(itemType => new ValidationItemType
                    {
                        ItemTypeId = itemType.ItemTypeId,
                        ItemBaseTypeDescription = itemType.ItemBaseTypeDescription,
                        ItemBaseTypeId = itemType.ItemBaseTypeId,
                        MaxOccurrences = itemType.MaxOccurrences,
                        NameTypes = productValidationData.ItemNameTypes
                            .Where(nameType => nameType.ItemTypeId == itemType.ItemTypeId)
                            .Select(nameType => new IdValue
                            {
                                Id = nameType.ItemNameTypeId,
                                Value = nameType.ItemNameTypeDescription
                            }),
                        LookupTypes = productValidationData.ItemLookupTypes
                            .Where(lookupType => lookupType.ItemTypeId == itemType.ItemTypeId)
                            .Select(lookupType => new IdValue
                            {
                                Id = lookupType.ItemLookupTypeId,
                                Value = lookupType.ItemLookupTypeDescription
                            }),
                        TagTypes = productValidationData.TagTypes?.Select(tagType => new IdValue
                        {
                            Id = tagType
                        })
                    });
            return itemTypes;
        }

        public static IEnumerable<ValidationProduct> GetProductsForValidation(this MessageType messageType)
        {
            return messageType.Product.Select(p => new ValidationProduct
            {
                ProductCodes = string.Join(",", p.Identity.ProductCodes.Select(pc => pc.Value)),
                Images = p.Assets?.Image?.Select(i => new Image
                {
                    ShotType = i.ShotType,
                    ShotTypeId = i.ShotTypeId
                }) ?? Enumerable.Empty<Image>(),
                Languages = p.Data?.Select(lang => new Language
                {
                    Code = lang.Code,
                    ItemTypes = lang.GetValidationItemTypesFromMessage(messageType)

                }) ?? Enumerable.Empty<Language>()
            });
        }

        public static IEnumerable<ValidationItemType> GetValidationItemTypesFromMessage(this LanguageType language, MessageType messageType)
        {
            if(language.ItemTypeGroup != null)
            {
                return messageType.GetStatementsForLanguage(language)
                                      .Concat(messageType.GetNameLookupsForLanguage(language))
                                      .Concat(messageType.GetNameTextsForLanguage(language))
                                      .Concat(messageType.GetMemosForLanguage(language))
                                      .Concat(messageType.GetLongTextsForLanguage(language))
                                      .Concat(messageType.GetNameTextLookupsForLanguage(language))
                                      .Concat(messageType.GetTexturalNutritionsForLanguage(language))
                                      .Concat(messageType.GetCalculatedNutritionsForLanguage(language))
                                      .Concat(messageType.GetTaggedMemosForLanguage(language))
                                      .Concat(messageType.GetTaggedLongTextForLanguage(language));
            }

            return Enumerable.Empty<ValidationItemType>();
        }


        public static IEnumerable<ValidationItemType> GetValidationItemTypes(this IEnumerable<NameTextLookup> nameTextLookups)
        {
            var itemTypes = nameTextLookups
                                    .GroupBy(nameTextLookup => nameTextLookup.ItemTypeId)
                                    .Select(nameTextLookup => new ValidationItemType
                                    {
                                        ItemTypeId = nameTextLookup.Key.ToString(),
                                        ItemBaseTypeId = nameTextLookup.First().BaseTypeId,
                                        ItemTypeDescription = nameTextLookup.First().ItemTypeDescription,
                                        NameTypes = nameTextLookup.Where(nameType => nameType.ItemTypeId == nameTextLookup.Key.ToString() && nameType.NameTypeId != null)
                                                                    .Select(nameType => new IdValue
                                                                    {
                                                                        Id = nameType.NameTypeId,
                                                                        Value = nameType.NameTextValue,
                                                                        Text = nameType.NameTextText
                                                                    }),
                                        LookupTypes = nameTextLookup.Where(nameType => nameType.ItemTypeId == nameTextLookup.Key.ToString() && nameType.LookupId != null)
                                                                        .Select(nameType => new IdValue
                                                                        {
                                                                            Id = nameType.LookupId,
                                                                            Value = nameType.LookupTextValue
                                                                        }),
                                        TagTypes = nameTextLookup.Where(nameType => nameType.ItemTypeId == nameTextLookup.Key.ToString() && nameType.Tags != null)
                                                                        .SelectMany(ntl => ntl.Tags),

                                        Occurrences = nameTextLookup.First().Occurrances
                                    });

            return itemTypes;
        }
    }
}
