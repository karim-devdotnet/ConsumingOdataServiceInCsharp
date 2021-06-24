using System;

namespace OdataServiceClient.SAP.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string metadataUrl = "https://sap-d.brunata.local/sap/opu/odata4/sap/za_serviceordertwa_o4/srvd_a2x/sap/zapi_serviceordertwa/0001/$metadata#TWA_Auftrag?sap-client=100";
            string metadataFile = @"file:///C:\Users\abdallah\source\repos\GitHub\ConsumingOdataServiceInCsharp\src\OdataServiceClient\OdataServiceClient.SAP.Konsole\TWA_Auftrag_Metadata.xml";
            var context = new CodeGenerationContext(new Uri(metadataFile, UriKind.Absolute), "Test");
        }
    }
}
