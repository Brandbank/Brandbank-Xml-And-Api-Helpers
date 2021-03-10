# Xml-And-Api-Helpers
GetUnsent Basic Usage .Net Framework
-----------

The following C# code demonstrates how to retrive XML content from Brandbank GetUnsent API using .Net Framework.  
You will need to intall the Brandbank.API NuGet package.

```csharp
using Brandbank.Api.Clients;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System;

namespace BrandbankXMLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var getUnsentClient = new GetUnsentClient(
                new Guid("XXXXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"), 
                new Brandbank.Api.ExtractData.DataExtractSoapClient()
                );

            var xml = getUnsentClient.GetUnsentProductData();

            var message = xml.ConvertTo<MessageType>();

            var messageSummary = getUnsentClient.AcknowledgeMessage(new BrandbankMessageSummary(message));
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
