using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Models.Message
{
    public partial class MessageType
    {
        public MessageType() { }

        public MessageType(Guid guid, DateTime dateTime)
        {
            Id = guid.ToString();
            DateTime = dateTime;
            DataVersion = "2.00.00";
        }
    }

    public partial class ProductType
    {
        public ProductType() { }

        public ProductType(DateTime dateTime)
        {
            UpdateType = UpdateTypeType.AddOrUpdate;
            VersionDateTime = dateTime;
        }
    }

    public partial class IdentityType
    {
        public IdentityType() { }

        public IdentityType(string gtin, string subscriberCode, IEnumerable<string> targetMarkets)
        {
            TargetMarkets = targetMarkets.Select(tm => new TargetMarketType
            {
                Code = tm
            }).ToArray();
            ProductCodes = new List<ProductCodeType>
            {
                new ProductCodeType
                {
                    Value = gtin,
                    Scheme = "GTIN"
                }
            }.ToArray();
            Subscription = new SubscriptionType
            {
                Code = subscriberCode,
                Id = "1"
            };
        }
    }

    public partial class AssetsType
    {
        public AssetsType()
        {
            Image = new List<ImageType>().ToArray();
        }
    }

    public partial class ImageType
    {
        public ImageType() { }

        public ImageType(int shotTypeId, string imageUrl, int height, int width)
        {
            ShotTypeId = shotTypeId;
            ShotType = "Shot Type Name";
            MimeType = "image/jpeg";
            Specification = new ImageSpecificationType
            {
                RequestedDimensions = new DimensionsType
                {
                    Height = height,
                    Width = width,
                    Units = DimensionsUnitsType.Pixels
                }
            };
            Url = new ExpiringUrlType
            {
                Value = imageUrl
            };
        }
    }

    public partial class DocumentType
    {
        public DocumentType() { }

        public DocumentType(string documentUrl)
        {
            Url = new ExpiringUrlType
            {
                Value = documentUrl
            };
        }
    }

    public partial class LanguageType
    {
        public LanguageType() { }

        public LanguageType(string description, string languageCode)
        {
            Description = description;
            Code = languageCode;
            GroupingSetName = "No Grouping";
            ItemTypeGroup = new List<ItemTypeGroupType>
            {
                new ItemTypeGroupType
                {
                    Id = 0,
                    Name = "All Attributes"
                }
            }.ToArray();
        }
    }
}