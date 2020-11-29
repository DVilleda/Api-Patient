using ServicePatient.Models;
using ServicePatient.Models.DAOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicePatient.Controllers
{
    public class DocteurController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Docteur> Get()
        {
            return DocteurProvider.GetAllDocteur();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            Docteur docteur = DocteurProvider.getDocteur(Int32.Parse(id));
            if (docteur != null)
            {
                return this.Ok(docteur);
            }
            else 
            {
                return this.NotFound();
            }
        }

        // POST api/<controller>
        public int Post(Docteur docteur)
        {
            return DocteurProvider.AddDocteur(docteur);
        }

        // PUT api/<controller>/5
        public void Put(Docteur docteur)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
        }
    }
}