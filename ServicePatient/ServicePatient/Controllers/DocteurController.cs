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

        [Route("api/Docteur/{docteurID}/prescriptions")]
        [HttpGet]
        public IEnumerable<Prescription> GetPrescriptions(int docteurID)
        {
            return PrescriptionProvider.GetAllPrescriptionsByDoctor(docteurID);
        }

        [Route("api/Docteur/{docteurID}/references")]
        [HttpGet]
        public IEnumerable<References> GetReferences(int docteurID)
        {
            return ReferenceProvider.GetAllReferencesByDocteur(docteurID);
        }

        [Route("api/Docteur/{docteurID}/Notes")]
        [HttpGet]
        public IEnumerable<Notes> GetNotes(int docteurID)
        {
            return NotesProvider.GetNotesByDocteur(docteurID);
        }

        // POST api/<controller>
        public string Post(Docteur docteur)
        {
            return DocteurProvider.AddDocteur(docteur);
        }

        // PUT api/<controller>/5
        public bool Put(Docteur docteur)
        {
            return DocteurProvider.ModifierDocteur(docteur);
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
        }

        [Route("api/Docteur/PostNotes/")]
        [HttpPost]
        public bool Post(Notes note)
        {
            return NotesProvider.AddNote(note);
        }

        [Route("api/Docteur/PostReference/")]
        [HttpPost]
        public bool Post(References references)
        {
            return ReferenceProvider.AddReference(references);
        }

        [Route("api/Docteur/PostPrescription/")]
        [HttpPost]
        public bool Post(Prescription prescription)
        {
            return PrescriptionProvider.AddPrescription(prescription);
        }
    }
}