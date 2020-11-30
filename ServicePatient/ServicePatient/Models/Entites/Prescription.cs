using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicePatient.Models
{
    [DataContract]
    public class Prescription
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string prescription { get; set; }
        [DataMember]
        public string medicament { get; set; }
        [DataMember]
        public string quantite { get; set; }
        [DataMember]
        public string instructions { get; set; }
        [DataMember]
        public DateTime date_prescription { get; set; }
        [DataMember]
        public int id_patient { get; set; }
        [DataMember]
        public int id_docteur { get; set; }
    }
}