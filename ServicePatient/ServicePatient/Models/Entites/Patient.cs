using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicePatient.Models
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string numAssMaladie { get; set; }
        [DataMember]
        public string nom { get; set; }
        [DataMember]
        public string prenom { get; set; }
        [DataMember]
        public DateTime date_naissance { get; set; }
        [DataMember]
        public string sexe { get; set; }
        [DataMember]
        public string allergies { get; set; }
        [DataMember]
        public string adresse { get; set; }
        [DataMember]
        public int num_tel { get; set; }
        [DataMember]
        public bool assurance { get; set; }
    }
}