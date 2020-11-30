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
    public class PatientController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Patient> Get()
        {
            return PatientProvider.GetAll();
        }

        [Route("api/Patient/{patientID}/prescriptions")]
        [HttpGet]
        public IEnumerable<Prescription> GetPrescriptions(int patientID)
        {
            return PrescriptionProvider.GetAllPrescriptionsByPatient(patientID);
        }

        [Route("api/Patient/{patientID}/references")]
        [HttpGet]
        public IEnumerable<References> GetReferences(int patientID)
        {
            return ReferenceProvider.GetAllReferencesByPatient(patientID);
        }

        [Route("api/Patient/{patientID}/Notes")]
        [HttpGet]
        public IEnumerable<Notes> GetNotes(int patientID)
        {
            return NotesProvider.GetNotesByPatient(patientID);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
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
        public bool Put(Patient patient)
        {
            return PatientProvider.ModifierPatient(patient);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}