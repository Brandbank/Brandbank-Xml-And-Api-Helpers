using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.MessageHelpers
{
    public static class LanguageTypeWriterExtensions
    {
        public static void AddCategories(this LanguageType languageType, string code, string scheme)
        {
            languageType.AddCategory(code.Substring(0, 2), scheme);
            languageType.AddCategory(code.Substring(2, 2), scheme);
            languageType.AddCategory(code.Substring(4, 2), scheme);
        }

        public static void AddCategory(this LanguageType languageType, string code, string scheme)
        {
            if (string.IsNullOrWhiteSpace(code))
                return;

            var levelType = new LevelType
            {
                Code = code.PadLeft(2, '0'),
                Value = string.Empty
            };

            if (languageType.Categorisations == null)
                languageType.Categorisations = new List<CategorisationType>().ToArray();

            var category = languageType.Categorisations.FirstOrDefault(c => c.Scheme.Equals(scheme, StringComparison.CurrentCultureIgnoreCase));
            if (category == null)
            {
                category = new CategorisationType { Scheme = scheme.ToUpperInvariant() };
                languageType.Categorisations = languageType.Categorisations.ExtendArray(category);
            }
            
            languageType.Categorisations.FirstOrNewIfNull(c => c.Scheme.Equals(scheme, StringComparison.CurrentCultureIgnoreCase)).Level = category.Level.ExtendArray(levelType);
        }

        public static void AddLongText(this LanguageType languageType, int id, string item)
        {
            languageType.AddLongText(id, new List<string> { item });
        }

        public static void AddLongText(this LanguageType languageType, int id, IEnumerable<string> items)
        {
            var cleanedItems = items
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => new TextType { Value = i });

            if (!cleanedItems.Any())
                return;

            var longTextItemsType = new LongTextItemsType
            {
                Id = id.ToString(),
                Name = "Item Name",
                Text = cleanedItems.ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().LongTextItems = languageType.ItemTypeGroup.FirstOrDefault().LongTextItems.ExtendArray(longTextItemsType, lt => lt.Id);
        }

        public static void RemoveLongText(this LanguageType languageType, int id)
        {
            languageType.ItemTypeGroup.FirstOrDefault().LongTextItems = languageType.ItemTypeGroup.FirstOrDefault().LongTextItems.Where(i => i.Id != id.ToString()).ToArray();
        }

        public static void AddMemo(this LanguageType languageType, int id, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            var memoType = new MemoType
            {
                Id = id.ToString(),
                Name = "Item Name",
                Value = value ?? string.Empty
            };
            languageType.ItemTypeGroup.FirstOrDefault().Memo = languageType.ItemTypeGroup.FirstOrDefault().Memo.ExtendArray(memoType, mt => mt.Id);
        }
        
        public static void AddNameLookup(this LanguageType languageType, int id, IEnumerable<KeyValuePair<int, int>> items)
        {
            if (!items.Any())
                return;

            var nameLookupsType = new NameLookupsType
            {
                Id = id.ToString(),
                Name = "Item Name",
                NameValue = items.Select(i => new NameValueType
                {
                    Name = new LookupType
                    {
                        Id = i.Key.ToString(),
                        Value = "Name Value"
                    },
                    Value = new LookupWithCodeType
                    {
                        Id = i.Value.ToString(),
                        Value = "Value Value",
                    }
                }).ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().NameLookups = languageType.ItemTypeGroup.FirstOrDefault().NameLookups.ExtendArray(nameLookupsType, nt => nt.Id);
        }

        public static void AddStatement(this LanguageType languageType, int id, IEnumerable<int> items)
        {
            if (!items.Any())
                return;
            
            var StatementsType = new StatementsType
            {
                Id = id.ToString(),
                Name = "Item Name",
                Statement = items.Distinct().Select(i => new LookupWithCodeType { Id = i.ToString() }).ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().Statements = languageType.ItemTypeGroup.FirstOrDefault().Statements.ExtendArray(StatementsType, s => s.Id);
            
        }

        public static void AddNameText(this LanguageType languageType, int id, IEnumerable<KeyValuePair<int, string>> items)
        {
            var cleanedItems = items
                .Where(i => !string.IsNullOrWhiteSpace(i.Value))
                .Select(i => new NameTextType
                {
                    Name = new LookupWithCodeType
                    {
                        Id = i.Key.ToString(),
                        Value = "Value"
                    },
                    Text = new TextType
                    {
                        Value = i.Value.ToString(),
                    }
                });

            if (!cleanedItems.Any())
                return;

            var nameTextItemsType = new NameTextItemsType
            {
                Id = id.ToString(),
                Name = "Item Name",
                NameText = cleanedItems.ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().NameTextItems = languageType.ItemTypeGroup.FirstOrDefault().NameTextItems.ExtendArray(nameTextItemsType, nt => nt.Id);
        }

        public static void AddNameTextLookup(this LanguageType languageType, int id, IEnumerable<KeyValuePair<int, KeyValuePair<int, string>>> items)
        {
            var cleanedItems = items
                .Where(i => !string.IsNullOrWhiteSpace(i.Value.Value))
                .Select(i => new NameValueTextType
                {
                    Name = new LookupType
                    {
                        Id = i.Key.ToString(),
                        Value = "Value"
                    },
                    Value = new LookupWithCodeType
                    {
                        Id = i.Value.Key.ToString(),
                        Value = "Value"
                    },
                    Text = new TextType
                    {
                        Value = i.Value.Value.ToString(),
                    }
                });

            if (!cleanedItems.Any())
                return;

            var nameTextLookupsType = new NameTextLookupsType
            {
                Id = id.ToString(),
                Name = "Item Name",
                NameValueText = cleanedItems.ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups = languageType.ItemTypeGroup.FirstOrDefault().NameTextLookups.ExtendArray(nameTextLookupsType, ntl => ntl.Id);
        }

        public static void AddTextualNutrition(this LanguageType languageType, int id, IEnumerable<string> items)
        {
            if (!items.Any())
                return;

            var textualNutritionType = new TextualNutritionType
            {
                Id = id,
                Name = "Textual Nutrition",
                Headings = items.First().Split('|'),
                Nutrient = items.Skip(1).Select(n => new NutrientType
                {
                    Name = new[] { n.Split('|').First() },
                    Values = n.Split('|').Skip(1).Select(v => v).ToArray()
                }).ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().TextualNutrition = languageType.ItemTypeGroup.FirstOrDefault().TextualNutrition.ExtendArray(textualNutritionType);
        }

        public static void AddStructuredNutrition(this LanguageType languageType, int id, StructuredNutrition structuredNutrition)
        {
            if (!structuredNutrition.ValueGroupDefinitions.Any() && !structuredNutrition.StructuredNutrients.Any())
                return;

            var valueGroupDefinitions = new List<ValueGroupDefinitionType>();
            foreach (var valueGroupDefinition in structuredNutrition.ValueGroupDefinitions)
            {
                valueGroupDefinitions.Add(new ValueGroupDefinitionType
                {
                    Id = valueGroupDefinition.Id.ToString(),
                    Name = $"Value group definition {valueGroupDefinition.Id}", 
                    AmountHeader = valueGroupDefinition.AmountHeader,
                    AmountTotal = new AmountTotalType
                    {
                        UnitName = $"Unit name {valueGroupDefinition.AmountTotal.Key}",
                        UnitId = valueGroupDefinition.AmountTotal.Key.ToString(),
                        Value = valueGroupDefinition.AmountTotal.Value,
                    },
                    ReferenceIntakeHeader = valueGroupDefinition.ReferenceIntakeHeaders.NewIfNull().ToArray()
                });
            }

            var structuredNutrientTypes = new List<StructuredNutrientType>();
            foreach (var structuredNutrient in structuredNutrition.StructuredNutrients)
            {
                if (!structuredNutrientTypes.Any(s => s.Id == structuredNutrient.Id.ToString()))
                    structuredNutrientTypes.Add(new StructuredNutrientType
                    {
                        Id = structuredNutrient.Id.ToString(),
                        Name = $"Structured Nutrient {structuredNutrient.Id}",
                        Unit = new StructuredNutritionUnitType
                        {
                            Id = structuredNutrient.UnitId.ToString(),
                            Name = $"Unit {structuredNutrient.UnitId}"
                        },
                        ValueGroup = structuredNutrient.ValueGroups.Select(vg => new ValueGroupType
                        {
                            Id = vg.Id.ToString(),
                            Name = $"Value Group {vg.Id}",
                            Amount = new AmountType
                            {
                                Value = vg.Value,
                                ValueSpecified = true,
                            },
                            ReferenceIntake = vg.ReferenceIntakeValue.NewIfNull().Select(riv => new ReferenceIntakeType
                            {
                                Value = riv,
                                ValueSpecified = true
                            }).ToArray()
                        }).ToArray()
                    });
            }

            var structuredNutrientType = new StructuredNutritionType
            {
                Id = id.ToString(),
                Name = "Structured Nutrition",
                ValueGroupDefinitions = valueGroupDefinitions.OrderBy(v => v.Id).ToArray(),
                Nutrients = structuredNutrientTypes.OrderBy(n => n.Id).ToArray()
            };
            languageType.ItemTypeGroup.FirstOrDefault().StructuredNutrition = languageType.ItemTypeGroup.FirstOrDefault().StructuredNutrition.ExtendArray(structuredNutrientType);
        }
    }
}
