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
        int id { get; set; }
        [DataMember]
        string nom { get; set; }
        [DataMember]
        string prenom { get; set; }
        [DataMember]
        int age { get; set; }
        [DataMember]
        string sexe { get; set; }
        [DataMember]
        string allergies { get; set; }
        [DataMember]
        string adresse { get; set; }
        [DataMember]
        int num_tel { get; set; }
        [DataMember]
        bool assurance { get; set; }
    }
}