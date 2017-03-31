using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Helpers;
using Brandbank.Xml.Validation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Tests
{
    [TestClass]
    public class ValidationExtensionTests
    {
        #region SetUp
        //Name - Name
        //Lookup - Value
        public ProductValidationData GetTestProductValidationDataForBaseType(string baseId)
        {
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeId = baseId,
                ItemTypeId = "1"
            };
            var itemType2 = new ValidationItemType()
            {
                ItemBaseTypeId = baseId,
                ItemTypeId = "2"
            };
            var itemType3 = new ValidationItemType()
            {
                ItemBaseTypeId = "3",
                ItemTypeId = "3"
            };
            var itemTypes = new List<ValidationItemType>();
            itemTypes.Add(itemType);
            itemTypes.Add(itemType2);
            itemTypes.Add(itemType3);

            var nameType1 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseId,
                ItemTypeId = "1",
                ItemNameTypeId = "1"
            };
            var nameType2 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseId,
                ItemTypeId = "2",
                ItemNameTypeId = "2"
            };
            var nameType3 = new ValidationItemNameType()
            {
                ItemBaseTypeId = "2",
                ItemTypeId = "3",
                ItemNameTypeId = "3"
            };
            var nameTypes = new List<ValidationItemNameType>();
            nameTypes.Add(nameType1);
            nameTypes.Add(nameType2);
            nameTypes.Add(nameType3);

            var lookupType1 = new ValidationItemLookupType()
            {
                ItemBaseTypeId = baseId,
                ItemTypeId = "1",
                ItemLookupTypeId = "1"
            };
            var lookupType2 = new ValidationItemLookupType()
            {
                ItemBaseTypeId = baseId,
                ItemTypeId = "1",
                ItemLookupTypeId = "2"
            };
            var lookupType3 = new ValidationItemLookupType()
            {
                ItemBaseTypeId = "2",
                ItemTypeId = "3",
                ItemLookupTypeId = "3"
            };
            var lookupTypes = new List<ValidationItemLookupType>();
            lookupTypes.Add(lookupType1);
            lookupTypes.Add(lookupType2);
            lookupTypes.Add(lookupType3);
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = nameTypes,
                ItemLookupTypes = lookupTypes
            };

            return productValidationData;
        }
        public ProductValidationData GetTestProductValidationDataForStatements()
        {
            var baseTypeId = "1";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "1",
                ItemTypeDescription = "Third Party Logos",
                ItemBaseTypeDescription = "Statement"
            };

            var nameType1 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "1",
                ItemNameTypeId = "1"
            };

            var nameType2 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "1",
                ItemNameTypeId = "2"
            };
            var itemTypes = new List<ValidationItemType> { itemType };
            var nameTypes = new List<ValidationItemNameType>() { nameType1, nameType2 };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemLookupTypes = new List<ValidationItemLookupType>(),
                ItemNameTypes = nameTypes,
            };

            return productValidationData;
        }
        public ProductValidationData GetTestProductValidationDataForNameLookups()
        {
            var baseTypeId = "2";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "4",
                ItemBaseTypeDescription = "NameLookup",
                ItemTypeDescription = "Allergy Advice DB"
            };

            var nameType1 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "4",
                ItemNameTypeId = "103"
            };

            var lookupType1 = new ValidationItemLookupType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "4",
                ItemLookupTypeId = "1"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var nameTypes = new List<ValidationItemNameType> { nameType1 };
            var lookupTypes = new List<ValidationItemLookupType> { lookupType1 };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = nameTypes,
                ItemLookupTypes = lookupTypes
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForNameTexts()
        {
            var baseTypeId = "3";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Name Text",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "6"
            };

            var nameType1 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "6",
                ItemNameTypeId = "146"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var nameTypes = new List<ValidationItemNameType> { nameType1 };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = nameTypes,
                ItemLookupTypes = new List<ValidationItemLookupType>()
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForMemos()
        {
            var baseTypeId = "4";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Memo",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "3"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = new List<ValidationItemNameType>(),
                ItemLookupTypes = new List<ValidationItemLookupType>()
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForLongTexts()
        {
            var baseTypeId = "5";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "LongText",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "12"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = new List<ValidationItemNameType>(),
                ItemLookupTypes = new List<ValidationItemLookupType>()
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForNameTextLookups()
        {
            var baseTypeId = "6";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Name Text Lookups",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "1"
            };

            var itemTypes = new List<ValidationItemType> { itemType };

            var nameType1 = new ValidationItemNameType()
            {
                ItemNameTypeId = "1",
                ItemTypeId = "1",
                ItemBaseTypeId = baseTypeId
            };

            var lookupType1 = new ValidationItemLookupType()
            {
                ItemLookupTypeId = "1",
                ItemTypeId = "1",
                ItemBaseTypeId = baseTypeId
            };

            var nameTypes = new List<ValidationItemNameType> { nameType1 };
            var lookupTypes = new List<ValidationItemLookupType> { lookupType1 };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = nameTypes,
                ItemLookupTypes = lookupTypes
            };

            return productValidationData;

        }

        public ProductValidationData GetTestProductValidationDataForTexturalNutrition()
        {
            var baseTypeId = "7";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Textural Nutrition",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "85"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                TagTypes = tagTypes,
                ItemTypes = itemTypes,
                ItemNameTypes = new List<ValidationItemNameType>(),
                ItemLookupTypes = new List<ValidationItemLookupType>()
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForCalculatedNutrition()
        {
            var baseTypeId = "8";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Calculate Nutrition",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "86"
            };

            var nameType1 = new ValidationItemNameType()
            {
                ItemBaseTypeId = baseTypeId,
                ItemNameTypeId = "1182",
                ItemTypeId = "86"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var nameTypes = new List<ValidationItemNameType> { nameType1 };
            var tagTypes = new List<string> { "2" };
            var productValidationData = new ProductValidationData()
            {
                ItemTypes = itemTypes,
                ItemNameTypes = nameTypes,
                TagTypes = tagTypes,
                ItemLookupTypes = new List<ValidationItemLookupType>()
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForTaggedMemos()
        {
            var baseTypeId = "9";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Tagged Memos",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "176"
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var tagTypes = new List<string> { "2" };

            var productValidationData = new ProductValidationData()
            {
                ItemTypes = itemTypes,
                ItemNameTypes = new List<ValidationItemNameType>(),
                ItemLookupTypes = new List<ValidationItemLookupType>(),
                TagTypes = tagTypes
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForTaggedLongTexts()
        {
            var baseTypeId = "10";
            var itemType = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Tagged Long Texts",
                ItemBaseTypeId = baseTypeId,
                ItemTypeId = "177",
            };

            var itemTypes = new List<ValidationItemType> { itemType };
            var tagTypes = new List<string> { "1" };

            var productValidationData = new ProductValidationData()
            {
                ItemTypes = itemTypes,
                ItemNameTypes = new List<ValidationItemNameType>(),
                ItemLookupTypes = new List<ValidationItemLookupType>(),
                TagTypes = tagTypes
            };

            return productValidationData;
        }

        public ProductValidationData GetTestProductValidationDataForDuplicates()
        {
            var stateBaseTypeId = "1";
            var itemTypeState = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Statements",
                ItemBaseTypeId = stateBaseTypeId,
                ItemTypeDescription = "Third Party Logos",
                ItemTypeId = "1",
                MaxOccurrences = 1
            };

            var itemTypeNameLookup = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Name Lookups",
                ItemBaseTypeId = "2",
                ItemTypeDescription = "Allergy Advice",
                ItemTypeId = "4",
                MaxOccurrences = 1
            };

            var tagTypes = new List<string> { "2" };

            var itemTypes = new List<ValidationItemType> { itemTypeState, itemTypeNameLookup };

            var productionData = new ProductValidationData()
            {
                ItemTypes = itemTypes,
                ItemLookupTypes = new List<ValidationItemLookupType>(),
                ItemNameTypes = new List<ValidationItemNameType>(),
                TagTypes = tagTypes
            };

            return productionData;
        }

        public ProductValidationData GetTestProductValidationDataForDuplicateLookups()
        {
            var data = GetTestProductValidationDataForNameTextLookups();
            data.ItemNameTypes.Add(new ValidationItemNameType()
            {
                ItemBaseTypeId = "6",
                ItemTypeDescription = "Cooking Guidelines",
                ItemTypeId = "1",
                ItemNameTypeDescription = "Oven cook",
                ItemNameTypeId = "145"
            });

            data.ItemLookupTypes.Add(new ValidationItemLookupType()
            {
                ItemBaseTypeId = "6",
                ItemTypeDescription = "Cooking Guidelines",
                ItemTypeId = "1",
                ItemLookupTypeDescription = "From Ambient",
                ItemLookupTypeId = "10772"
            });

            return data;
        }

        public ProductValidationData GetTestProductValidationDataForTextConstraints()
        {
            var nameTextBaseTypeId = "3";
            var itemTypeNameText = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Name Texts",
                ItemBaseTypeId = nameTextBaseTypeId,
                ItemTypeDescription = "Nappy Size",
                ItemTypeId = "6",
            };

            var nameText1 = new ValidationItemNameType()
            {
                ItemBaseTypeId = nameTextBaseTypeId,
                ItemTypeId = "6",
                ItemNameTypeId = "146",
                ItemNameTypeDescription = "Nappy Size (age)",
                ItemTypeDescription = "Nappy Size",
                RegExErrorMessage = "Must be a range of ages in months (mths) or years (yrs) seperated by a hyphen (-). Eg 12mths - 18mths or newborn - 3mths",
                RegEx = @"^(([1-9][0-9]?(yrs|mths|kg))|newborn)(\-[1-9][0-9]?(yrs|mths|kg)|(\splus))$"
            };

            var nameText2 = new ValidationItemNameType()
            {
                ItemBaseTypeId = nameTextBaseTypeId,
                ItemTypeId = "6",
                ItemNameTypeId = "147",
                ItemNameTypeDescription = "Nappy Size (weight)",
                ItemTypeDescription = "Nappy Size",
                RegExErrorMessage = "Must be a range of weights in kilos (kg) seperated by a hyphen (-). Eg 6kg-12kg",
                RegEx = @"^([<>]\s?([1-9][0-9]?|[0-9]{1,}\.[0-9]{1,})\s?kg)|^([1-9][0-9]?|[0-9]{1,}\.[0-9]{1,})\s?kg(\+{1}|\s?\-\s?([1-9][0-9]?|[0-9]{1,}\.[0-9]{1,})\s?kg)$"
            };

            var nameTextLookupBaseTypeId = "6";
            var itemTypeNameTextLookup = new ValidationItemType()
            {
                ItemBaseTypeDescription = "Name Text Lookup",
                ItemBaseTypeId = nameTextLookupBaseTypeId,
                ItemTypeDescription = "Cooking Guidelines",
                ItemTypeId = "7"
            };

            var nameText3 = new ValidationItemNameType()
            {
                ItemBaseTypeId = nameTextLookupBaseTypeId,
                ItemTypeId = "7",
                ItemNameTypeId = "207",
                ItemNameTypeDescription = "Cooking Instructions",
                ItemTypeDescription = "Cooking Guidelines",
                RegExErrorMessage = "Must be a numeric value",
                RegEx = @"^[0-9]+(\.[0-9]+)?$",
            };

            var nameText4 = new ValidationItemNameType()
            {
                ItemBaseTypeId = nameTextLookupBaseTypeId,
                ItemTypeId = "7",
                ItemNameTypeId = "145",
                ItemNameTypeDescription = "Oven cook",
                ItemTypeDescription = "Cooking Guidelines",
                RegExErrorMessage = "Must be a numeric value",
                RegEx = @"^[0-9]+(\.[0-9]+)?$",
            };

            var itemTypes = new List<ValidationItemType> { itemTypeNameText, itemTypeNameTextLookup };
            var tagTypes = new List<string> { "2" };

            var productData = new ProductValidationData()
            {
                ItemTypes = itemTypes,
                ItemLookupTypes = new List<ValidationItemLookupType>(),
                ItemNameTypes = new List<ValidationItemNameType> { nameText1, nameText2, nameText3, nameText4 },
                TagTypes = tagTypes
            };

            return productData;
        }

        public MessageType GetMessageTypeForTesting()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg - test.xml");
            var input = sr.ReadToEnd();
            return input.ConvertToModel();
        }

        public MessageType GetMessageTypeForTestingNoItemTypeGroups()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg - No ItemTypeGroups.xml");
            var input = sr.ReadToEnd();
            return input.ConvertToModel();
        }

        public MessageType GetMessageTypeDuplicateItemTypes()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg - Duplicate ItemTypes.xml");
            var input = sr.ReadToEnd();
            return input.ConvertToModel();
        }

        public MessageType GetMessageTypeDuplicateImageTypes()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg - DuplicateImageTypes.xml");
            var input = sr.ReadToEnd();
            return input.ConvertToModel();
        }

        public MessageType GetMessageTypeTextConstraints()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg - TextConstraints.xml");
            var input = sr.ReadToEnd();
            return input.ConvertToModel();
        }

        #endregion SetUp

        [TestMethod]
        public void ShouldConvertValidXMLStringToXML()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg.xml");
            var input = sr.ReadToEnd();
            var result = input.ConvertToModel();
            Assert.AreEqual("282862b6-81b7-400d-8208-dceeb612eda5", result.Id);
            Assert.AreEqual("Grocery", result.Product.ElementAt(0).Data.ElementAt(0).Categorisations.ElementAt(0).Level.ElementAt(0).Value);
        }

        [TestMethod]
        public void ShouldGetNameTextLookupsForLanguageType()
        {
            var sr = new StreamReader("Files/BrandbankXML_v5a_eg.xml");
            var input = sr.ReadToEnd();
            var messageType = input.ConvertToModel();

            var langType = messageType.Product.First().Data.First();

            var result = messageType.GetNameTextLookupsForLanguage(langType);

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual("331", result.FirstOrDefault().LookupTypes.FirstOrDefault().Id);
            Assert.AreEqual("207", result.FirstOrDefault().NameTypes.FirstOrDefault().Id);
        }

        [TestMethod]
        public void ShouldGetNameAndLookupsForItemType()
        {
            var baseId = "6";

            var productValidationData = GetTestProductValidationDataForBaseType(baseId);

            var result = productValidationData.GetItemTypes();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result.First().NameTypes.Count());
            Assert.AreEqual(2, result.First().LookupTypes.Count());

        }



        [TestMethod]
        public void ShouldGetInvalidStatements()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForStatements();

            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "1").Count());
            Assert.AreEqual(3, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.Count());
            Assert.AreEqual("Canada Flag", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.First().Value);
        }

        [TestMethod]
        public void ShouldGetInvalidNameLookups()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForNameLookups();
            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "2").Count());
            Assert.AreEqual(4, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.Count());
            Assert.AreEqual(2, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().LookupTypes.Count());
            Assert.AreEqual("Celeriac", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.First().Value);
            Assert.AreEqual("May Contain", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().LookupTypes.First().Value);
        }

        [TestMethod]
        public void ShouldGetInvalidNameTexts()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForNameTexts();
            var products = messageType.GetProductsForValidation();
            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "3").Count());
            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.Count());
            Assert.AreEqual("Nappy Size (weight)", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.First().Value);
        }

        [TestMethod]
        public void ShouldGetInvalidMemos()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForMemos();
            var products = messageType.GetProductsForValidation();
            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(2, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "4").Count());
        }

        [TestMethod]
        public void ShouldGetInvalidLongTexts()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForLongTexts();
            var products = messageType.GetProductsForValidation();
            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(2, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "5").Count());
        }

        [TestMethod]
        public void ShouldGetInvalidItemNameTextLookups()
        {
            var messageType = GetMessageTypeForTesting();

            var productValidationData = GetTestProductValidationDataForNameTextLookups();

            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "6").Count());
            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().LookupTypes.Count());
            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.Count());
            Assert.AreEqual("Oven cook", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.First().Value);
            Assert.AreEqual("From Ambient", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().LookupTypes.First().Value);
        }

        [TestMethod]
        public void ShouldGetInvalidTexturalNutritions()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForTexturalNutrition();
            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "7").Count());
        }

        [TestMethod]
        public void ShouldGetCalculatedNutritionsForLanguage()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForCalculatedNutrition();
            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);

            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "8").Count());
            Assert.AreEqual(4, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.Count());
            Assert.AreEqual("Energy (kcal)", result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().NameTypes.First().Value);
        }

        [TestMethod]
        public void ShouldGetTaggedMemosForLanguage()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForTaggedMemos();
            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);
            Assert.AreEqual(2, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "9").Count());
            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().TagTypes.Count());
        }

        [TestMethod]
        public void ShouldGetTaggedLongTextsForLanguage()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForTaggedLongTexts();
            var products = messageType.GetProductsForValidation();

            var result = productValidationData.GetInvalidData(products);
            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidItemTypes.Where(it => it.ItemBaseTypeId == "10").Count());
            Assert.AreEqual(1, result.First().InvalidLanguageData.First().InvalidNameAndLookUps.First().TagTypes.Count());
        }

        [TestMethod]
        public void ShouldGetErrorsForStatements()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForStatements();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            Assert.AreEqual("Product: 5039023015901,1651886 has the following errors:", errors.First());
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 1 ("));
            Assert.AreEqual(4, errorsForBaseType.Count());

            Assert.AreEqual("20 (Canada Flag) is invalid for ItemType 1 (Third Party Logos) on BaseType 1 (Statement)", errorsForBaseType.ElementAt(1));
        }

        [TestMethod]
        public void ShouldGetErrorsForNameLookups()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForNameLookups();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 2"));
            Assert.AreEqual("ItemType 555 (Additives) is invalid on BaseType 2 (NameLookup)", errorsForBaseType.First());
            Assert.AreEqual("104 (Celeriac) is invalid for ItemType 4 (Allergy Advice) on BaseType 2 (NameLookup)", errorsForBaseType.ElementAt(1));
        }

        [TestMethod]
        public void ShouldGetErrorsForNameTexts()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForNameTexts();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 3"));
            Assert.AreEqual("ItemType 8 (Nut Statement) is invalid on BaseType 3 (Name Text)", errorsForBaseType.First());
            Assert.AreEqual("147 (Nappy Size (weight)) is invalid for ItemType 6 (Nappy Size) on BaseType 3 (Name Text)", errorsForBaseType.ElementAt(1));
        }

        [TestMethod]
        public void ShouldGetErrorsForMemos()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForMemos();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 4"));
            Assert.AreEqual("ItemType 15 (Manufacturers Address) is invalid on BaseType 4 (Memo)", errorsForBaseType.First());
        }

        [TestMethod]
        public void ShouldGetErrorsForLongTexts()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForLongTexts();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 5"));
            Assert.AreEqual("ItemType 13 (Additives Other Text) is invalid on BaseType 5 (LongText)", errorsForBaseType.First());
        }

        [TestMethod]
        public void ShouldGetErrorsForNameTextLookups()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForNameTextLookups();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 6"));
            Assert.AreEqual("ItemType 24 (Lower Age Limit) is invalid on BaseType 6 (Name Text Lookups)", errorsForBaseType.First());
            Assert.AreEqual("145 (Oven cook) is invalid for ItemType 1 (Cooking Guidelines) on BaseType 6 (Name Text Lookups)", errorsForBaseType.ElementAt(1));
        }

        [TestMethod]
        public void ShouldGetErrorsForTexturalNutritions()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForTexturalNutrition();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 7"));
            Assert.AreEqual("ItemType 86 (All Attributes) is invalid on BaseType 7 (Textural Nutrition)", errorsForBaseType.First());
        }

        [TestMethod]
        public void ShouldGetErrorsForCalculateNutritions()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForCalculatedNutrition();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 8"));
            Assert.AreEqual("ItemType 87 (Calculated Nutrition) is invalid on BaseType 8 (Calculate Nutrition)", errorsForBaseType.First());
            Assert.AreEqual("1183 (Energy (kcal)) is invalid for ItemType 86 (Calculated Nutrition) on BaseType 8 (Calculate Nutrition)", errorsForBaseType.ElementAt(1));
        }

        [TestMethod]
        public void ShouldGetErrorsForTaggedMemos()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForTaggedMemos();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 9"));
            Assert.AreEqual("ItemType 177 (Taggable Information) is invalid on BaseType 9 (Tagged Memos)", errorsForBaseType.First());
            Assert.AreEqual("99 (Allergen Highlighting Method) is an invalid TagType on BaseType 9 (Tagged Memos)", errorsForBaseType.ElementAt(2));
        }

        [TestMethod]
        public void ShouldGetTaggedLongTexts()
        {
            var messageType = GetMessageTypeForTesting();
            var productValidationData = GetTestProductValidationDataForTaggedLongTexts();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForBaseType = errors.Where(e => e.Contains("BaseType 10"));
            Assert.AreEqual("ItemType 178 (Taggable Ingredients) is invalid on BaseType 10 (Tagged Long Texts)", errorsForBaseType.First());
            Assert.AreEqual("9 (Allergen As On Pack) is an invalid TagType on BaseType 10 (Tagged Long Texts)", errorsForBaseType.ElementAt(1));
        }

        [TestMethod]
        public void ShouldHandleXmlWithNotItemTypeGroups()
        {
            var messageType = GetMessageTypeForTestingNoItemTypeGroups();
            var productValidationData = GetTestProductValidationDataForStatements();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void DetectItemTypesWithInvalidMaxOccurances()
        {
            var messageType = GetMessageTypeDuplicateItemTypes();
            var productValidationData = GetTestProductValidationDataForDuplicates();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var occuranceErrors = errors.Where(e => e.Contains("occurances"));
            Assert.AreEqual("ItemType 1 (Third Party Logos) appears 2 times on BaseType 1 (Statements) but the max occurances is 1", occuranceErrors.First());
            Assert.AreEqual("ItemType 4 (Allergy Advice) appears 2 times on BaseType 2 (Name Lookups) but the max occurances is 1", occuranceErrors.ElementAt(1));
        }

        [TestMethod]
        public void ShouldDetectDuplicatedImageTypes()
        {
            var messageType = GetMessageTypeDuplicateImageTypes();
            var productValidationData = GetTestProductValidationDataForDuplicates();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var imageShotErrors = errors.Where(e => e.Contains("Image Shot"));
            Assert.AreEqual("Image Shot Type 1 has been duplicated", imageShotErrors.First());
        }

        [TestMethod]
        public void ShouldValidateTextAgainstConstraints()
        {
            var messageType = GetMessageTypeTextConstraints();
            var productValidationData = GetTestProductValidationDataForTextConstraints();
            var errors = messageType.GetAllInvalidDataInMessage(productValidationData);
            var errorsForText = errors.Where(e => e.Contains("Text"));
            Assert.AreEqual("The text \"abc\" for ItemType 6 (Nappy Size) on BaseType 3 (Name Texts)  is not in the correct format for 147 (Nappy Size (weight)) accepted format must be Must be a range of weights in kilos (kg) seperated by a hyphen (-). Eg 6kg-12kg", errorsForText.First());
            Assert.AreEqual("The text \"Information explaining how the product should be cooked in the Oven from Ambient\r\nExample - Pre-heat the oven.\r\n200°C / 400°F / Gas 6 12-14 mins.\" for ItemType 7 (Cooking Guidelines) on BaseType 6 (Name Text Lookup)  is not in the correct format for 145 (Oven cook) accepted format must be Must be a numeric value", errorsForText.ElementAt(3));
        }
    }
}
