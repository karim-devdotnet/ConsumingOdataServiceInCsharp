using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Simple.OData.Client;

namespace OdataServiceClient.BruSAP.Konsole.V2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var oDataClientSettings = new ODataClientSettings(new Uri("https://sap-d.brunata.local/sap/opu/odata4/sap/za_serviceordertwa_o4/srvd_a2x/sap/zapi_serviceordertwa/0001/"));
            oDataClientSettings.BeforeRequest += delegate (HttpRequestMessage message)
            {
                message.Headers.Add("Authorization", "Basic QUJEQUxMQUguQTpLYXJpbWFiZDE5ODQ/");
            };
            var client = new ODataClient(oDataClientSettings);
            var auftraege = await client
     .FindEntriesAsync("TWA_Entnahmestellen?$filter=id eq '6000000000'");
            foreach (var auftrag in auftraege)
            {
                Console.WriteLine(auftrag["wwaStrasseUndHausnummer"]);
            }
            Console.ReadLine();
        }
    }
}
