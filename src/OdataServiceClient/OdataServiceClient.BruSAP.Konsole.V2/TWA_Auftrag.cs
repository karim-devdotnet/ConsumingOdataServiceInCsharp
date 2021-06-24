using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataServiceClient.BruSAP.Konsole.V2
{
    public class TWA_Auftrag
    {
        public string Id { get; set; }
        public string WwaId { get; set; }
        public string WwaStrasseUndHausnummer { get; set; }
        public string WwaPlz { get; set; }
        public string WwaOrt { get; set; }
        public int Untersuchungspflicht { get; set; }
        public string BemerkungTourenplanung { get; set; }
        public string GebaeudeNutzung { get; set; }
        public string Objektnummer { get; set; }
        public string BeprobenBis { get; set; }
        public int Status { get; set; }
        public string Nutzungsarten { get; set; }
        public int Auftragsart { get; set; }
        public int StufeNachuntersuchung { get; set; }
    }
}
