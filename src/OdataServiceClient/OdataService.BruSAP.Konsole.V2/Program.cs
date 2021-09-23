using Com.Sap.Gateway.Srvd_a2x.Zapi_serviceordertwa.V0001;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdataService.BruSAP.Konsole.V2
{
    class Program
    {
        static void Main(string[] args)
        {
            CallIssue();
            CallTwa_Auftrage();
            Console.ReadLine();
        }

        static void CallIssue()
        {
            string odataServiceUrl = "https://sap-d.brunata.local/sap/opu/odata4/sap/zapi_s4crm_sr_zral/srvd_a2x/sap/zui_s4crm_sr_zral/0001/";
            var context = new Com.Sap.Gateway.Srvd_a2x.Zui_s4crm_sr_zral.V0001.Container(new Uri(odataServiceUrl));

            context.SendingRequest2 += (sender, e) => SendBaseAuthCredsOnTheRequest(sender, e);

            //get all TWA_Auftrags
            //var issue = context.Issue.First();

            var issueList = context.Issue.AddQueryOption("sap-client", "100").Execute().ToList();
            Console.WriteLine("Twa_AuftragList");
            foreach (var item in issueList)
            {
                Console.WriteLine($"Twa_Auftrag[{item.ServiceIssue}]: {item.ServiceDefectCodeText}");
            }
        }
        
        static void CallTwa_Auftrage()
        {
            string odataServiceUrl = "https://sap-d.brunata.local/sap/opu/odata4/sap/za_serviceordertwa_o4/srvd_a2x/sap/zapi_serviceordertwa/0001";
            var context = new Container(new Uri(odataServiceUrl));

            context.SendingRequest2 += (sender, e) => SendBaseAuthCredsOnTheRequest(sender, e);

            //get all TWA_Auftrags
            //TWA_AuftragTypeSingle twa_Auftrag = context.TWA_Auftrag.ByKey("6000000020");
            var twa_AuftragCount = context.TWA_Auftrag.AddQueryOption("sap-client", "100").Count();

            List<TWA_EntnahmestellenType> twa_EntnamestelleList = context.TWA_Entnahmestellen.AddQueryOption("sap-client", "100").GetAllPages().ToList();

            TWA_AuftragType twa_Auftrag1 = context.TWA_Auftrag.AddQueryOption("sap-client", "100").First();
            IEnumerable<TWA_AuftragType> twa_AuftragList = context.TWA_Auftrag.AddQueryOption("sap-client", "100").Execute();
            Console.WriteLine("Twa_AuftragList");
            int i = 1;
            foreach (var item in twa_AuftragList)
            {
                Console.WriteLine($"{i}: Twa_Auftrag[{item.Id}]: {item.WwaStrasseUndHausnummer}");
                i++;
            }
        }

        private static void SendBaseAuthCredsOnTheRequest(object sender, SendingRequest2EventArgs e)
        {
            var creds = "ABDALLAH.A" + ":" + "****";
            var bcreds = Encoding.ASCII.GetBytes(creds);
            var base64Creds = Convert.ToBase64String(bcreds);
            //this is where you pass the creds.
            e.RequestMessage.SetHeader("Authorization", "Basic " + base64Creds);
        }

        //Every time a OData request is build it adds an Authorization Header with the acesstoken 
        private static void OnBuildingRequest(object sender, BuildingRequestEventArgs e, string token)
        {
            e.Headers.Add("Authorization", "Basic " + token);
        }
    }
}
