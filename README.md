# Xml-And-Api-Helpers
GetUnsent Basic Usage
-----------

```csharp
using Brandbank.Api;
using Brandbank.Xml.ImageDownloader;
using Brandbank.Xml.ImageWriter;
using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System;

namespace BrandbankApiExample
{
    class Program
    {
        private static IBrandbankImageDownloader _imageDownloader;
        private static IImageWriter _imageWriter;
        
        static void Main(string[] args)
        {
            var directory = @"c:\Products";
            _imageWriter = new ImageWriter(directory);
            _imageDownloader = new ImageDownloader();

            var api = new BrandbankApi(new Guid("XXXXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"), directory);
            
            api.GetUnsent(ProcessMessage);
        }

        private static IBrandbankMessageSummary ProcessMessage(MessageType messageType)
        {
            foreach(var product in messageType.GetProducts())
            {
                var images = product.GetImages();
                foreach (var image in images)
                {
                    var imageStream = _imageDownloader.DownloadToStream(image.GetUrl());
                    _imageWriter.SaveToDisk
                    (
                        imageStream, 
                        $@"{product.Identity.GetPvid()}_{image.ShotTypeId}.jpg"
                    );
                }

                //Store Product
            }

            return new BrandbankMessageSummary(messageType);
        }
    }
}
```
To use the Brandbank Service you will need to add the following to the configuration section of the app config.
```xml
  <system.serviceModel>
    <bindings>
      <basicHttpBinding>
        <binding name="Data ExtractSoap" maxReceivedMessageSize="2147483647" maxBufferPoolSize="2147483647">
          <readerQuotas maxArrayLength="2147483647" maxStringContentLength="2147483647" />
          <security mode="Transport" />
        </binding>
        <binding name="Data ReportSoap">
          <security mode="Transport" />
        </binding>
      </basicHttpBinding>
    </bindings>
    <client>
      <endpoint address="https://api.brandbank.com/svc/feed/extractdata.asmx" binding="basicHttpBinding" bindingConfiguration="Data ExtractSoap" contract="ExtractData.DataExtractSoap" name="Data ExtractSoap" />
      <endpoint address="https://api.brandbank.com/svc/feed/reportdata.asmx" binding="basicHttpBinding" bindingConfiguration="Data ReportSoap" contract="ReportData.DataReportSoap" name="Data ReportSoap" />
    </client>
  </system.serviceModel>
```

To initialise the Brandbank API
```csharp
var api = new BrandbankApi(new Guid("XXXXXX-XXX-XXXX-XXXX-XXXXXXXXXX"), "C:\\Products");
```
The first argument is the API key for your feed. This can be found at the [Integration Management Portal](https://imp.brandbank.com).

Create a function to process the messages returned from the api.
```csharp
private static IBrandbankMessageSummary ProcessMessage(MessageType messageType)
```
Loop through the products found in the message. This is an extension method found in Brandbank.Xml.MessageHelpers
```csharp
foreach(var product in messageType.GetProducts())
```
Get all images found on the product. 
```csharp
var images = product.GetImages();
```
Download the image from its URL as a stream.
```csharp
var imageStream = _imageDownloader.DownloadToStream(image.GetUrl());
```
The stream could then be saved to disk using the image writer...
```csharp
_imageWriter.SaveToDisk(imageStream, $@"{product.Identity.GetPvid()}_{image.ShotTypeId}.jpg");
```
or uploaded to another web service.

Finally you will need to store the product somewhere, we recommend a NoSQL database.