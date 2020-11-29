using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicePatient.Models
{
    [DataContract]
    public class References
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Nom_Doc { get; set; }
        [DataMember]
        public string adresse_ref { get; set; }
        [DataMember]
        public string raison_ref { get; set; }
        [DataMember]
        public DateTime date_creation { get; set; }
        [DataMember]
        public int id_patient { get; set; }
        [DataMember]
        public int id_docteur { get; set; }
    }
}