using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OdataServiceClient.SAP.Konsole
{
    public class CodeGenerationContext
    {
        private readonly XElement edmx;
        private readonly string namespacePrefix;

        public CodeGenerationContext(Uri metadataUri, string namespacePrefix)
       : this(GetEdmxStringFromMetadataPath(metadataUri), namespacePrefix)
        {
        }

       

        public CodeGenerationContext(string edmx, string namespacePrefix)
        {
            this.edmx = XElement.Parse(edmx);
            this.namespacePrefix = namespacePrefix;
        }


        private static string GetEdmxStringFromMetadataPath(Uri metadataUri)
        {
            string content = null;
            using (StreamReader streamReader = new StreamReader(GetEdmxStreamFromUri(metadataUri)))
            {
                content = streamReader.ReadToEnd();
            }

            return content;
        }
        private static Stream GetEdmxStreamFromUri(Uri metadataUri)
        {
            Debug.Assert(metadataUri != null, "metadataUri != null");
            Stream metadataStream = null;
            if (metadataUri.Scheme == "file")
            {
                metadataStream = new FileStream(Uri.UnescapeDataString(metadataUri.AbsolutePath), FileMode.Open, FileAccess.Read);
            }
            else if (metadataUri.Scheme == "http" || metadataUri.Scheme == "https")
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(metadataUri);
                    webRequest.Credentials = new NetworkCredential("ABDALLAH.A","++++");
                    WebResponse webResponse = webRequest.GetResponse();
                    metadataStream = webResponse.GetResponseStream();
                }
                catch (WebException e)
                {
                    if (e.Response is HttpWebResponse webResponse && webResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new WebException("Failed to access the metadata document. The OData service requires authentication for accessing it. Please download the metadata, store it into a local file, and set the value of “MetadataDocumentUri” in the .odata.config file to the file path. After that, run custom tool again to generate the OData Client code.");
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Only file, http, https schemes are supported for paths to metadata source locations.");
            }

            return metadataStream;
        }
    }
}
