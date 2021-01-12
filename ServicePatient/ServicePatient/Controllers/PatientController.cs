using ServicePatient.Models;
using ServicePatient.Models.DAOS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ServicePatient.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PatientController : ApiController
    {
        #region PatientProfil
        /* Méthode enlevée temporairement
         * 
        // GET api/<controller>
        public IEnumerable<Patient> Get()
        {
            return PatientProvider.GetAll();
        }*/

        // GET api/<controller>/5
        public IHttpActionResult Get(string token)
        {
            //Pour si jamais besoin creer cle
            //Debug.WriteLine(new JWTAuthentication().GenererToken(1, "Patient"));
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            Debug.WriteLine(id);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            Patient patient = PatientProvider.GetPatient(id);
            if (patient != null)
            {
                return this.Ok(patient);
            }
            else
            {
                return this.NotFound();
            }
        }

        // POST api/<controller>
        public string Post(Patient patient)
        {
            return PatientProvider.AddPatient(patient);
        }

        // PUT api/<controller>/5
        public bool Put(Patient patient,string token)
        {
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return PatientProvider.ModifierPatient(patient);
        }

        // DELETE api/<controller>/5
        public bool Delete(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return PatientProvider.DeletePatient(id);
        }
        #endregion

        #region Patient_Docteur
        [Route("api/Patient/Docteurs/Nom")]
        [HttpGet]
        public IEnumerable<Docteur> GetDocteurParNom(string token,string nom,string prenom) 
        {
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return DocteurProvider.getDocteurParNomSecured(nom,prenom);
        }

        [Route("api/Patient/Docteurs")]
        [HttpGet]
        public IEnumerable<Docteur> GetDocteurDePatient(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return PatientProvider.ObtenirDocteurs(id);
        }

        #endregion
        #region Consulter NotesReferencesPrescriptions
        [Route("api/Patient/Prescriptions")]
        [HttpGet]
        public IEnumerable<Prescription> GetPrescriptions(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            Debug.WriteLine("Info id: " + id);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return PrescriptionProvider.GetAllPrescriptionsByPatient(id);
        }

        [Route("api/Patient/References")]
        [HttpGet]
        public IEnumerable<References> GetReferences(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return ReferenceProvider.GetAllReferencesByPatient(id);
        }

        [Route("api/Patient/Notes")]
        [HttpGet]
        public IEnumerable<Notes> GetNotes(string token)
        {
            int id = new JWTAuthentication().DécoderTokenPourId(token);
            if (new JWTAuthentication().DécoderTypeUtilisateur(token) != "Patient") { Unauthorized(); }
            return NotesProvider.GetNotesByPatient(id);
        }
        #endregion
    }
}