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
        int id { get; set; }
        [DataMember]
        string nom { get; set; }
        [DataMember]
        string prenom { get; set; }
        [DataMember]
        int num_employe { get; set; }
        [DataMember]
        string specialite { get; set; }
    }
}