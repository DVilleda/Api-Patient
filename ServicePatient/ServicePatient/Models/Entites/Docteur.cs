using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicePatient.Models
{
    [DataContract]
    public class Docteur
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string nom { get; set; }
        [DataMember]
        public string prenom { get; set; }
        [DataMember]
        public int num_employe { get; set; }
        [DataMember]
        public string specialite { get; set; }
        [DataMember]
        public string apikey { get; set; }
    }
}