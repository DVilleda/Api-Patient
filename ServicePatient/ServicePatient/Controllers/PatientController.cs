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
    public class PatientController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Patient> Get()
        {
            return PatientProvider.GetAll();
        }

        [Route("api/Patient/{patientID}/prescriptions/{token}")]
        [HttpGet]
        public IEnumerable<Prescription> GetPrescriptions(int patientID, string token)
        {
            if (token != PatientProvider.GetPatient(patientID).apikey) 
            {
                return null;
            }
            return PrescriptionProvider.GetAllPrescriptionsByPatient(patientID);
        }

        [Route("api/Patient/{patientID}/references/{token}")]
        [HttpGet]
        public IEnumerable<References> GetReferences(int patientID,string token)
        {
            if (token != PatientProvider.GetPatient(patientID).apikey)
            {
                return null;
            }
            return ReferenceProvider.GetAllReferencesByPatient(patientID);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id,string token)
        {
            if (token != PatientProvider.GetPatient(id).apikey) { return Unauthorized(); }
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
            if (token != PatientProvider.GetPatient(patient.id).apikey) { return false; }
            return PatientProvider.ModifierPatient(patient);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}