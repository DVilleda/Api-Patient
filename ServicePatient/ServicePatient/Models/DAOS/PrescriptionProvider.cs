using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ServicePatient.Models.DAOS
{
    public class PrescriptionProvider
    {
        public static string cnxString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=clinique";

        public static List<Prescription> GetAllPrescriptions() 
        {
            List<Prescription> liste = new List<Prescription>();
            Prescription prescription;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from prescriptions";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                prescription = new Prescription
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    prescription = dr["Prescription"].ToString(),
                    medicament = dr["Medicament"].ToString(),
                    quantite = dr["Quantite"].ToString(),
                    instructions = dr["Instructions"].ToString(),
                    date_prescription = DateTime.Parse(dr["Date_Prescription"].ToString())
                };
                liste.Add(prescription);
            }
            cnx.Close();
            return liste;
        }

        public static List<Prescription> GetAllPrescriptionsByPatient(int id)
        {
            List<Prescription> liste = new List<Prescription>();
            Prescription prescription;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from prescriptions where id_patient=@id";
            DbParameter parameter = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(parameter);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                prescription = new Prescription
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    prescription = dr["Prescription"].ToString(),
                    medicament = dr["Medicament"].ToString(),
                    quantite = dr["Quantite"].ToString(),
                    instructions = dr["Instructions"].ToString(),
                    date_prescription = DateTime.Parse(dr["Date_Prescription"].ToString())
                };
                liste.Add(prescription);
            }
            cnx.Close();
            return liste;
        }

        public static List<Prescription> GetAllPrescriptionsByDoctor(int id)
        {
            List<Prescription> liste = new List<Prescription>();
            Prescription prescription;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from prescriptions where id_docteur=@id";
            DbParameter parameter = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(parameter);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                prescription = new Prescription
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    prescription = dr["Prescription"].ToString(),
                    medicament = dr["Medicament"].ToString(),
                    quantite = dr["Quantite"].ToString(),
                    instructions = dr["Instructions"].ToString(),
                    date_prescription = DateTime.Parse(dr["Date_Prescription"].ToString())
                };
                liste.Add(prescription);
            }
            cnx.Close();
            return liste;
        }
        public static bool AddPrescription(Prescription prescription) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Insert INTO prescriptions(id_patient,id_docteur,Prescription,Medicament,Quantite,Instructions,Date_Prescription)" +
                "Values(@id_patient,@id_doc,@prescription,@medecine,@quantite,@instructions,@date)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id_patient",
                DbType = System.Data.DbType.Int32,
                Value = prescription.id_patient
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "id_doc",
                DbType = System.Data.DbType.Int32,
                Value = prescription.id_docteur
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "prescription",
                DbType = System.Data.DbType.String,
                Value = prescription.prescription
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "medecine",
                DbType = System.Data.DbType.String,
                Value = prescription.medicament
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "quantite",
                DbType = System.Data.DbType.String,
                Value = prescription.quantite
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "instructions",
                DbType = System.Data.DbType.String,
                Value = prescription.instructions
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "date",
                DbType = System.Data.DbType.Date,
                Value = prescription.date_prescription.Date
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        public static bool UpdatePrescription(Prescription prescription) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE prescriptions Set Quantite=@quantite,Instructions=@instructions Where ID=@id";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = prescription.id
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "quantite",
                DbType = System.Data.DbType.String,
                Value = prescription.quantite
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "instructions",
                DbType = System.Data.DbType.String,
                Value = prescription.instructions
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }

        public static bool DeletePrescription(int id) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "DELETE FROM prescription WHERE ID=@id";
            DbParameter param = new MySqlParameter 
            {
                ParameterName ="id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
    }
}