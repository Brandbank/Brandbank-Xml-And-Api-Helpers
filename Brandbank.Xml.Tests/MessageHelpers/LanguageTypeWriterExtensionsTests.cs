using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class LanguageTypeWriterExtensionsTests
    {
        private LanguageType _languageType;
        public LanguageTypeWriterExtensionsTests()
        {
            _languageType = new LanguageType("Product Description", "en-gb");
        }

        [Fact]
        public void ShouldAddCategory()
        {
            _languageType.AddCategory("1", "Brandbank");
            _languageType.AddCategory("2", "Brandbank");
            _languageType.AddCategory("3", "Brandbank");
            Assert.Equal(_languageType.GetCategoryCodes("BRANDBANK"), "010203");
        }

        [Fact]
        public void ShouldAddLongTextItem()
        {
            var items = new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
            };
            var items2 = new List<string>
            {
                "Item 1",
                "",
                "Item 3",
            };
            _languageType.AddLongText(1, items);
            _languageType.AddLongText(2, items);
            _languageType.AddLongText(3, "Single Item String");
            _languageType.AddLongText(4, items2);
            _languageType.AddLongText(5, "    ");

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().LongTextItems[0].Id, "1");
            Assert.Equal(_languageType.GetLongTextItems("1").Count(), 3);
            Assert.Equal(_languageType.GetLongTextItems("1", ", "), "Item 1, Item 2, Item 3");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().LongTextItems[1].Id, "2");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().LongTextItems[1].Text.Length, 3);
            Assert.Equal(_languageType.GetLongTextItems("3").First(), "Single Item String");
            Assert.Equal(_languageType.GetLongTextItems("4", ", "), "Item 1, Item 3");
            Assert.Equal(_languageType.GetLongTextItems("5", ", "), string.Empty);

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().LongTextItems.Length, 4);
        }

        [Fact]
        public void ShouldAddMemoType()
        {
            _languageType.AddMemo(1, "Memo Value");
            _languageType.AddMemo(2, null);
            _languageType.AddMemo(3, "          ");

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Memo[0].Id, "1");
            Assert.Equal(_languageType.GetMemo("1"), "Memo Value");
            Assert.Equal(_languageType.GetMemo("2"), string.Empty);
            Assert.Equal(_languageType.GetMemo("3"), string.Empty);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Memo.Length, 1);
        }

        [Fact]
        public void ShouldAddNameLookup()
        {
            var items = new Dictionary<int, int>
            {
                { 10, 1 },
                { 20, 2 },
                { 30, 3 },
            };
            _languageType.AddNameLookup(1, items);
            _languageType.AddNameLookup(2, items);

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups[0].Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups[0].NameValue.Length, 3);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups[0].NameValue[0].Name.Id, "10");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups[0].NameValue[0].Value.Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups[1].Id, "2");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups[1].NameValue.Length, 3);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameLookups.Length, 2);
        }

        [Fact]
        public void ShouldAddStatement()
        {
            var items = new List<int>
            {
                1,2,3
            };
            _languageType.AddStatement(1, items);
            _languageType.AddStatement(2, items);

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Statements[0].Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Statements[0].Statement.Length, 3);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Statements[0].Statement[0].Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Statements[1].Id, "2");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Statements[1].Statement.Length, 3);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().Statements.Length, 2);
        }

        [Fact]
        public void ShouldAddNameText()
        {
            var items = new Dictionary<int, string>
            {
                { 1, "10" },
                { 2, "20" },
                { 3, "30" }
            };
            var items2 = new Dictionary<int, string>
            {
                { 1, "10" },
                { 2, "" },
                { 3, "30" }
            };
            _languageType.AddNameText(1, items);
            _languageType.AddNameText(2, items2);
            _languageType.AddNameText(3, new Dictionary<int, string>());

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems[0].Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems[0].NameText.Length, 3);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems[0].NameText[0].Name.Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems[0].NameText[0].Text.Value, "10");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems[1].Id, "2");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems[1].NameText.Length, 2);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextItems.Length, 2);
        }

        [Fact]
        public void ShouldAddNameTextLookups()
        {
            var items = new Dictionary<int, KeyValuePair<int, string>>
            {
                { 1, new KeyValuePair<int, string>(10, "text 1") },
                { 2, new KeyValuePair<int, string>(20, "text 2") },
                { 3, new KeyValuePair<int, string>(30, "text 3") }
            };
            var item2 = new Dictionary<int, KeyValuePair<int, string>>
            {
                { 1, new KeyValuePair<int, string>(10, "text 1") },
                { 2, new KeyValuePair<int, string>(20, "") },
                { 3, new KeyValuePair<int, string>(30, "text 3") }
            };
            _languageType.AddNameTextLookup(1, items);
            _languageType.AddNameTextLookup(2, item2);
            _languageType.AddNameTextLookup(3, new Dictionary<int, KeyValuePair<int, string>>());

            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[0].Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[0].NameValueText.Length, 3);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[0].NameValueText[0].Name.Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[0].NameValueText[0].Value.Id, "10");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[0].NameValueText[0].Text.Value, "text 1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[1].Id, "2");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[1].NameValueText.Length, 2);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[1].NameValueText[1].Name.Id, "3");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[1].NameValueText[1].Value.Id, "30");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups[1].NameValueText[1].Text.Value, "text 3");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups.Length, 2);
        }

        [Fact]
        public void ShouldAddTextualNutrition()
        {
            var nutritionitems = new List<string>
            {
                "Per 100g|per Portion**|%* Per Portion**",
                "Energy|1114kJ/270kcal|167kJ/40kcal|2%",
                "Fat|27g|4.1g|6%",
                "of which saturates|2.7g|0.4g|2%",
                "Carbohydrates|6.2g|0.9g|<1%",
                "of which sugars|2.3g|<0.5g|<1%",
                "Protein|0.7g|<0.5g|<1%",
                "Salt|1.8g|0.27g|5%",
                "*% of Reference Intake of an average adult (8400kJ/2000kcal)|",
                "**1 Portion = 15g (pack contains Approx. 28 portions)|"
            };

            _languageType.AddTextualNutrition(85, nutritionitems);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().TextualNutrition.FirstOrDefault().Nutrient.Count(), 9);
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().TextualNutrition.FirstOrDefault().Headings.Count(), 3);
        }

        [Fact]
        public void ShouldAddStructuredNutrition()
        {
            var valueGroupDefinitions = new List<ValueGroupDefinition>
            {
                new ValueGroupDefinition
                {
                    Id = 1,
                    AmountHeader = "Amount Header",
                    AmountTotal = new KeyValuePair<int, decimal>(1, 16.00000m),
                    ReferenceIntakeHeaders = new List<string>
                    {
                        "% Daily Value"
                    }
                }
            };

            var structuredNutrients = new List<StructuredNutrient>
            {
                new StructuredNutrient
                {
                    Id = 1,
                    UnitId = 1,
                    ValueGroups = new List<ValueGroup>
                    {
                        new ValueGroup
                        {
                            Id = 1,
                            Value = 16.00000m,
                            ReferenceIntakeValue = new List<decimal>
                            {
                                0.00m
                            }
                            
                        }
                    }
                }
            };

            _languageType.AddStructuredNutrition(256, new StructuredNutrition
            {
                ValueGroupDefinitions = valueGroupDefinitions,
                StructuredNutrients = structuredNutrients
            });
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().StructuredNutrition.FirstOrDefault().Id, "256");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().StructuredNutrition.FirstOrDefault().ValueGroupDefinitions.FirstOrDefault().Id, "1");
            Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().StructuredNutrition.FirstOrDefault().ValueGroupDefinitions.FirstOrDefault().AmountHeader, "Amount Header");
            //Assert.Equal(_languageType.ItemTypeGroup.FirstOrDefault().TextualNutrition.FirstOrDefault().Headings.Count(), 3);


        }

        

    }
}
