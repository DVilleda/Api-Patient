using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicePatient.Models
{
    [DataContract]
    public class Notes
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Titre { get; set; }
        [DataMember]
        public string contenu { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int id_patient { get; set; }
        [DataMember]
        public int id_docteur { get; set; }
    }
}