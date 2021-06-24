using Newtonsoft.Json;
using OdataServiceClient.BruSAP.Konsole.V1;
using System;
using System.IO;
using System.Net;

namespace OdataService.BruSAP.Konsole.V1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string odataServiceUrl = "https://sap-d.brunata.local/sap/opu/odata4/sap/za_serviceordertwa_o4/srvd_a2x/sap/zapi_serviceordertwa/0001/TWA_Auftrag?sap-client=100&$format=json&$top=5";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(odataServiceUrl);
                //webRequest.Credentials = new NetworkCredential("user", "pwd");
                webRequest.Headers.Add("Authorization: Basic ***");
                WebResponse webResponse = webRequest.GetResponse();
                using (var sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    string content = sr.ReadToEnd();
                    var resultType = new { @odata_context = "", @odata_metadataEtag = "", value = new TWA_Auftrag[] { } };
                    TWA_Auftrag[] tWA_Auftrags = JsonConvert.DeserializeAnonymousType(content, resultType).value;
                    foreach (var twa in tWA_Auftrags)
                    {
                        Console.WriteLine($"TWA_Auftrag: ID = {twa.Id}");
                    }
                }

            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
