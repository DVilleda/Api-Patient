using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ServicePatient.Models.DAOS
{
    public class PatientProvider
    {
        public static string cnxString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=clinique";

        public static List<Patient> GetAll() 
        {
            List<Patient> liste = new List<Patient>();
            Patient patient;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from patient";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                patient = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    age = Int32.Parse(dr["Age"].ToString()),
                    sexe = dr["Sexe"].ToString(),
                    allergies = dr["Allergies"].ToString(),
                    adresse = dr["Adresse"].ToString(),
                    num_tel = Int32.Parse(dr["Num_Tel"].ToString()),
                    assurance = Convert.ToBoolean(dr["Assurance"].ToString())
                };
                liste.Add(patient);
            }
            cnx.Close();
            return liste;
        }

        public static List<Patient> GetPatientByNom(string nom_famille) 
        {
            List<Patient> liste = new List<Patient>();
            Patient patient;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from patient WHERE Nom=@nom_famille";
            DbParameter parameter = new MySqlParameter
            {
                ParameterName = "nom_famille",
                DbType = System.Data.DbType.String,
                Value = nom_famille
            };
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                patient = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    age = Int32.Parse(dr["Age"].ToString()),
                    sexe = dr["Sexe"].ToString(),
                    allergies = dr["Allergies"].ToString(),
                    adresse = dr["Adresse"].ToString(),
                    num_tel = Int32.Parse(dr["Num_Tel"].ToString()),
                    assurance = Convert.ToBoolean(dr["Assurance"].ToString())
                };
                liste.Add(patient);
            }
            cnx.Close();
            return liste;
        }

        public static Patient GetPatient(int id)
        {
            Patient patient;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from patient WHERE ID=@id";
            DbParameter parameter = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.String,
                Value = id
            };
            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                patient = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    age = Int32.Parse(dr["Age"].ToString()),
                    sexe = dr["Sexe"].ToString(),
                    allergies = dr["Allergies"].ToString(),
                    adresse = dr["Adresse"].ToString(),
                    num_tel = Int32.Parse(dr["Num_Tel"].ToString()),
                    assurance = Convert.ToBoolean(dr["Assurance"].ToString())
                };
                return patient;
            }
            cnx.Close();
            return null;
        }
        public static string RetournerAPIKey(Patient patient)
        {
            Patient patientKEY;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from patient WHERE Num_AssMal=@numAssMal";
            DbParameter parameter = new MySqlParameter
            {
                ParameterName = "numAssMal",
                DbType = System.Data.DbType.String,
                Value = patient.numAssMaladie
            };
            cmd.Parameters.Add(parameter);
            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                patientKEY = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    age = Int32.Parse(dr["Age"].ToString()),
                    sexe = dr["Sexe"].ToString(),
                    allergies = dr["Allergies"].ToString(),
                    adresse = dr["Adresse"].ToString(),
                    num_tel = Int32.Parse(dr["Num_Tel"].ToString()),
                    assurance = Convert.ToBoolean(dr["Assurance"].ToString())
                };
                string idPatient = patientKEY.id.ToString() + ".Patient";
                return EncodeToBase64(idPatient);
            }
            cnx.Close();
            return null;
        }
        public static string AddPatient(Patient patient) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "INSERT INTO patient(Num_AssMal,Nom,Prenom,Age,Sexe,Allergies,Adresse,Num_Tel,Assurance) " +
                "Values(@numAssMal,@nom,@prenom,@age,@sexe,@allergies,@adresse,@numero,@assurance)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "numAssMal",
                DbType = System.Data.DbType.String,
                Value = patient.numAssMaladie
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "nom",
                DbType = System.Data.DbType.String,
                Value = patient.nom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "prenom",
                DbType = System.Data.DbType.String,
                Value = patient.prenom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "age",
                DbType = System.Data.DbType.Int32,
                Value = patient.age
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "sexe",
                DbType = System.Data.DbType.String,
                Value = patient.sexe
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "allergies",
                DbType = System.Data.DbType.String,
                Value = patient.allergies
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "adresse",
                DbType = System.Data.DbType.String,
                Value = patient.adresse
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "numero",
                DbType = System.Data.DbType.Int32,
                Value = patient.num_tel
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "assurance",
                DbType = System.Data.DbType.Boolean,
                Value = patient.assurance
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            if (result)
            {
                return RetournerAPIKey(patient);
            }
            else { return null; }
        }
        public static bool ModifierPatient(Patient patient)
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE patient SET Nom=@nom,Prenom=@prenom,Age=@age,Sexe=@sexe," +
                "Allergies=@allergies,Adresse=@adresse,Num_Tel=@numero,Assurance=@assurance WHERE Num_AssMal=@numAssMal";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "numAssMal",
                DbType = System.Data.DbType.String,
                Value = patient.numAssMaladie
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "nom",
                DbType = System.Data.DbType.String,
                Value = patient.nom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "prenom",
                DbType = System.Data.DbType.String,
                Value = patient.prenom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "age",
                DbType = System.Data.DbType.Int32,
                Value = patient.age
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "sexe",
                DbType = System.Data.DbType.String,
                Value = patient.sexe
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "allergies",
                DbType = System.Data.DbType.String,
                Value = patient.allergies
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "adresse",
                DbType = System.Data.DbType.String,
                Value = patient.adresse
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "numero",
                DbType = System.Data.DbType.Int32,
                Value = patient.num_tel
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "assurance",
                DbType = System.Data.DbType.Boolean,
                Value = patient.assurance
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }

        private static string EncodeToBase64(string id)
        {
            var TextBytes = System.Text.Encoding.UTF8.GetBytes(id);
            return System.Convert.ToBase64String(TextBytes);
        }
    }
}