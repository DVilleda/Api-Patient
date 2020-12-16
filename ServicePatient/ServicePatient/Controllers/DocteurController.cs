using ServicePatient.Models;
using ServicePatient.Models.DAOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ServicePatient.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DocteurController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Docteur> Get()
        {
            return DocteurProvider.GetAllDocteur();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Docteur") { return Unauthorized(); }
            Docteur docteur = DocteurProvider.getDocteur(id);
            if (docteur != null)
            {
                return this.Ok(docteur);
            }
            else 
            {
                return this.NotFound();
            }
        }

        [Route("api/Docteur/prescriptions")]
        [HttpGet]
        public IEnumerable<Prescription> GetPrescriptions(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (token != DocteurProvider.getDocteur(id).apikey) { return null; }
            return PrescriptionProvider.GetAllPrescriptionsByDoctor(id);
        }

        [Route("api/Docteur/references")]
        [HttpGet]
        public IEnumerable<References> GetReferences(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (token != DocteurProvider.getDocteur(id).apikey) { return null; }
            return ReferenceProvider.GetAllReferencesByDocteur(id);
        }

        [Route("api/Docteur/Notes")]
        [HttpGet]
        public IEnumerable<Notes> GetNotes(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (token != DocteurProvider.getDocteur(id).apikey) { return null; }
            return NotesProvider.GetNotesByDocteur(id);
        }

        // POST api/<controller>
        public string Post(Docteur docteur)
        {
            return DocteurProvider.AddDocteur(docteur);
        }

        // PUT api/<controller>/5
        public bool Put(Docteur docteur,string token)
        {
            if (token != DocteurProvider.getDocteur(docteur.id).apikey) { return false; }
            return DocteurProvider.ModifierDocteur(docteur);
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {

        }

        [Route("api/Docteur/PostNotes/")]
        [HttpPost]
        public bool Post(Notes note,string token)
        {
            if (token != DocteurProvider.getDocteur(note.id_docteur).apikey) { return false; }
            return NotesProvider.AddNote(note);
        }

        [Route("api/Docteur/PostReference/")]
        [HttpPost]
        public bool Post(References references, string token)
        {
            if (token != DocteurProvider.getDocteur(references.id_docteur).apikey) { return false; }
            return ReferenceProvider.AddReference(references);
        }

        [Route("api/Docteur/PostPrescription/")]
        [HttpPost]
        public bool Post(Prescription prescription,string token)
        {
            if (token != DocteurProvider.getDocteur(prescription.id_docteur).apikey) { return false; }
            return PrescriptionProvider.AddPrescription(prescription);
        }

        [Route("api/Docteur/DeleteReference")]
        [HttpDelete]
        public bool Delete(int idRef,string token) 
        {
            int IDdoc = ReferenceProvider.GetReference(idRef).id_docteur;
            if (token != DocteurProvider.getDocteur(IDdoc).apikey) { return false; }
            return ReferenceProvider.DeleteReference(idRef);
        }
    }
}