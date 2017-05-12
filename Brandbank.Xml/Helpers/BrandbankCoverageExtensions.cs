using Brandbank.Xml.Models.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Helpers
{
    public static class BrandbankCoverageExtensions
    {
        public static ReportType ConvertToCoverageReport(this IEnumerable<IBrandbankCoverage> products)
        {
            return new ReportType
            {
                Item = products.GetItemTypes().ToArray(),
                Message = new MessageType
                {
                    DateTime = DateTime.Now,
                    DateTimeSpecified = true,
                    Domain = "TES"
                }
            };
        }

        private static IEnumerable<ItemType> GetItemTypes(this IEnumerable<IBrandbankCoverage> products)
        {
            foreach (var product in products)
            {
                yield return new ItemType
                {
                    RetailerID = product.RetailerId,
                    GTINs = product.Gtins.GetGTINs().ToArray(),
                    Description = product.Description,
                    OwnLabel = product.OwnLabel,
                    Categories = product.Categories.GetCategories().ToArray()
                };
            }
        }

        private static IEnumerable<GTINType> GetGTINs(this IEnumerable<KeyValuePair<string, string>> gtins)
        {
            foreach (var gtin in gtins)
            {
                yield return new GTINType
                {
                    Value = gtin.Key.PadLeft(14, '0'),
                    Suppliers = new []
                    {
                        new SupplierType
                        {
                            SupplierName = gtin.Value
                        }
                    }
                };
            }
        }

        private static IEnumerable<CategoryType> GetCategories(this IEnumerable<KeyValuePair<string, string>> categories)
        {
            return new List<CategoryType>
            {
                new CategoryType
                {
                    Group = categories.GetCategoryItems().ToArray()
                }
            };
        }

        private static IEnumerable<CategoryLevelType> GetCategoryItems(this IEnumerable<KeyValuePair<string, string>> categories)
        {
            var count = 1;
            foreach (var categrory in categories)
            {
                yield return new CategoryLevelType
                {
                    Value = categrory.Value,
                    Code = categrory.Key,
                    Level = count++.ToString()
                };
            }
        }
    }
}
