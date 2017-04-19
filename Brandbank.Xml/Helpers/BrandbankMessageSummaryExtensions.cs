using Brandbank.Xml.Models.Feedback;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Helpers
{
    public static class BrandbankMessageSummaryExtensions
    {
        public static ReportType CreateFeedback(this IEnumerable<IBrandbankMessageSummary> brandbankMessageSummaries)
        {
            var items = Enumerable.Empty<ItemType>().ToList();
            foreach (var brandbankMessageSummary in brandbankMessageSummaries)
            {
                items.AddRange(brandbankMessageSummary.ProductDetails.Select(pt => new ItemType
                {
                    Comment = pt.Gtin,
                    BrandbankID = pt.Pvid.ToString(),
                    Description = pt.ProductDescription,
                    RetailerID = pt.ExternalId,
                    MessageGUID = brandbankMessageSummary.MessageGuid.ToString(),
                    Status = Models.Feedback.StatusType.Matched,
                    StatusSpecified = true
                }));
            }
            return new ReportType
            {
                Message = new Models.Feedback.MessageType
                {
                    DateTime = DateTime.Now,
                    DateTimeSpecified = true
                },
                Item = items.ToArray()
            }; ;
        }
    }
}
