using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class LanguageTypeReaderExtensionsTests
    {
        private LanguageType _languageType;
        public LanguageTypeReaderExtensionsTests()
        {
            _languageType = new LanguageType("Product Description", "en-gb");
        }

        [Fact]
        public void ShouldAssertThatStatmentExists()
        {
            var items = new List<int>
            {
                1,2,3
            };
            _languageType.AddStatement(1, items);
            _languageType.AddStatement(2, items);
            Assert.True(_languageType.StatementExists("1", "2"));
            Assert.False(_languageType.StatementExists("1", "5"));
            Assert.False(_languageType.StatementExists(null, null));
            Assert.False(_languageType.StatementExists("6", null));
        }

        [Fact]
        public void ShouldGetStatementValue()
        {
            var items = new List<int>
            {
                1,2,3
            };
            _languageType.AddStatement(1, items);
            _languageType.ItemTypeGroup.FirstOrDefault().Statements.FirstOrDefault().Statement.FirstOrDefault().Value = "Statement Value";

            Assert.Equal(_languageType.GetStatementValue("1", "1"), "Statement Value");
            Assert.Equal(_languageType.GetStatementValue("1", "1000"), string.Empty);
            Assert.Equal(_languageType.GetStatementValue("1", null), string.Empty);
            Assert.Equal(_languageType.GetStatementValue(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetLongTextItem()
        {
            _languageType.AddLongText(1, new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
            });
            Assert.Equal(_languageType.GetLongTextItems("1", ", "), "Item 1, Item 2, Item 3");
        }

        [Fact]
        public void ShouldGetMemoTextWhenNoneExist()
        {
            Assert.Equal(_languageType.GetMemo("1"), string.Empty);
            Assert.Equal(_languageType.GetMemo("2"), string.Empty);
        }

        [Fact]
        public void ShouldGetMemoText()
        {
            _languageType.AddMemo(1, "Memo Value");
            Assert.Equal(_languageType.GetMemo("1"), "Memo Value");
            Assert.Equal(_languageType.GetMemo("2"), string.Empty);
        }

        [Fact]
        public void ShouldGetNameText()
        {
            Assert.Equal(_languageType.GetNameTextText("1", "1"), string.Empty);

            var items = new Dictionary<int, string>
            {
                { 1, "Name Text 1" },
                { 2, "Name Text 2" },
                { 3, "Name Text 3" }
            };
            _languageType.AddNameText(1, items);

            Assert.Equal(_languageType.GetNameTextText("1", "1"), "Name Text 1");
            Assert.Equal(_languageType.GetNameTextText("2", "2"), string.Empty);
        }

        [Fact]
        public void ShouldGetNameLookup()
        {
            var items = new Dictionary<int, int>
            {
                { 10, 1 },
                { 20, 2 },
                { 30, 3 },
            };
            _languageType.AddNameLookup(1, items);
            _languageType.ItemTypeGroup.FirstOrDefault().NameLookups.FirstOrDefault().NameValue.FirstOrDefault().Value.Id = "Value Id";
            _languageType.ItemTypeGroup.FirstOrDefault().NameLookups.FirstOrDefault().NameValue.FirstOrDefault().Value.Code = "Code";

            Assert.Equal(_languageType.GetNameLookupNameValue("1", "10"), "Name Value");
            Assert.Equal(_languageType.GetNameLookupValueValue("1", "10"), "Value Value");
            Assert.Equal(_languageType.GetNameLookupValueId("1", "10"), "Value Id");
            Assert.Equal(_languageType.GetNameLookupValueCode("1", "10"), "Code");
        }

        [Fact]
        public void ShouldGetNameLookupText()
        {
            var items = new Dictionary<int, KeyValuePair<int, string>>
            {
                { 10, new KeyValuePair<int, string>(100, "item 1") },
                { 20, new KeyValuePair<int, string>(200, "item 2") },
            };
            _languageType.AddNameTextLookup(1, items);

            Assert.Equal(_languageType.GetNameLookupTextNameValue("1", "10"), "Value");
            Assert.Equal(_languageType.GetNameLookupTextValueId("1", "10"), "100");
            Assert.Equal(_languageType.GetNameLookupTextValueCode("1", "10"), string.Empty);
            Assert.Equal(_languageType.GetNameLookupTextValueValue("1", "10"), "Value");
            Assert.Equal(_languageType.GetNameLookupTextText("1", "10"), "item 1");
        }

        [Fact]
        public void ShouldGetNameLookupTextWhenNoneExist()
        {
            Assert.Equal(_languageType.GetNameLookupTextNameValue("1", "10"), string.Empty);
            Assert.Equal(_languageType.GetNameLookupTextValueId("1", "10"), string.Empty);
            Assert.Equal(_languageType.GetNameLookupTextValueCode("1", "10"), string.Empty);
            Assert.Equal(_languageType.GetNameLookupTextValueValue("1", "10"), string.Empty);
            Assert.Equal(_languageType.GetNameLookupTextText("1", "10"), string.Empty);
            Assert.Equal(_languageType.GetNameLookupTextText(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetLanguageDescription()
        {
            Assert.Equal(_languageType.GetDescription(), "Product Description");
        }

        [Fact]
        public void ShouldGetStrucutredNutritionHeading()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionHeading("256", "1"), "Value group definition 1");
            Assert.Equal(_languageType.GetStructuredNutritionHeading("256", "2"), "Value group definition 2");
            Assert.Equal(_languageType.GetStructuredNutritionHeading("256", "3"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionHeading(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetStrucutredNutritionTotal()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionTotal("256", "1"), 10.00000m);
            Assert.Equal(_languageType.GetStructuredNutritionTotal("256", "2"), 20.00000m);
            Assert.Equal(_languageType.GetStructuredNutritionTotal("256", "3"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionTotal(null, null), 0);
        }

        [Fact]
        public void ShouldGetStrucutredNutritionUnitName()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionUnitName("256", "1"), "Unit name 1");
            Assert.Equal(_languageType.GetStructuredNutritionUnitName("256", "2"), "Unit name 1");
            Assert.Equal(_languageType.GetStructuredNutritionUnitName("256", "3"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionUnitName(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetStrucutredNutritionUnitAbbriviation()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionUnitAbbriviation("256", "1"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionUnitAbbriviation("256", "2"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionUnitAbbriviation("256", "3"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionUnitAbbriviation(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetStrucutredNutritionReferenceIntakeHeaderCol1()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol1("256", "1"), "% Daily Value");
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol1("256", "2"), "% Daily Value");
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol1("256", "3"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol1(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetStrucutredNutritionReferenceIntakeHeaderCol2()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol2("256", "1"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol2("256", "2"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol2("256", "3"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeHeaderCol2(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetStructuredNutritionValue()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionValue("256", "1", "1"), 20.00000m);
            Assert.Equal(_languageType.GetStructuredNutritionValue("256", "2", "1"), 30.00000m);
            Assert.Equal(_languageType.GetStructuredNutritionValue("256", "1", "2"), 40.00000m);
            Assert.Equal(_languageType.GetStructuredNutritionValue("256", "2", "2"), 50.00000m);
            Assert.Equal(_languageType.GetStructuredNutritionValue("256", "3", "1"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionValue(null, null, null), 0);
        }

        [Fact]
        public void ShouldGetStructuredNutritionValueUnitAbbreviation()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionValueUnitAbbreviation("256", "1"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionValueUnitAbbreviation("256", "2"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionValueUnitAbbreviation("256", "3"), string.Empty);
            Assert.Equal(_languageType.GetStructuredNutritionValueUnitAbbreviation(null, null), string.Empty);
        }

        [Fact]
        public void ShouldGetStructuredNutritionReferenceIntakeValueCol1()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol1("256", "1", "1"), 5.00m);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol1("256", "2", "1"), 6.00m);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol1("256", "1", "2"), 10.00m);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol1("256", "2", "2"), 20.00m);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol1("256", "3", "1"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol1(null, null, null), 0);
        }

        [Fact]
        public void ShouldGetStructuredNutritionReferenceIntakeValueCol2()
        {
            _languageType.AddStructuredNutrition(256, GenerateStructuredNutrition());
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol2("256", "1", "1"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol2("256", "2", "1"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol2("256", "1", "2"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol2("256", "2", "2"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol2("256", "3", "1"), 0);
            Assert.Equal(_languageType.GetStructuredNutritionReferenceIntakeValueCol2(null, null, null), 0);
        }

        private static StructuredNutrition GenerateStructuredNutrition()
        {
            var valueGroupDefinitions = new List<ValueGroupDefinition>
            {
                new ValueGroupDefinition
                {
                    Id = 1,
                    AmountHeader = "Amount Header 1",
                    AmountTotal = new KeyValuePair<int, decimal>(1, 10.00000m),
                    ReferenceIntakeHeaders = new List<string>
                    {
                        "% Daily Value"
                    }
                },
                new ValueGroupDefinition
                {
                    Id = 2,
                    AmountHeader = "Amount Header 2",
                    AmountTotal = new KeyValuePair<int, decimal>(1, 20.00000m),
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
                            Value = 20.00000m,
                            ReferenceIntakeValue = new List<decimal>
                            {
                                5.00m
                            }
                        },
                        new ValueGroup
                        {
                            Id = 2,
                            Value = 30.00000m,
                            ReferenceIntakeValue = new List<decimal>
                            {
                                6.00m
                            }

                        }
                    }
                },
                new StructuredNutrient
                {
                    Id = 2,
                    UnitId = 2,
                    ValueGroups = new List<ValueGroup>
                    {
                        new ValueGroup
                        {
                            Id = 1,
                            Value = 40.00000m,
                            ReferenceIntakeValue = new List<decimal>
                            {
                                10.00m
                            }
                        },
                        new ValueGroup
                        {
                            Id = 2,
                            Value = 50.00000m,
                            ReferenceIntakeValue = new List<decimal>
                            {
                                20.00m
                            }

                        }
                    }
                }
            };

            return new StructuredNutrition
            {
                ValueGroupDefinitions = valueGroupDefinitions,
                StructuredNutrients = structuredNutrients
            };
        }
    }
}
