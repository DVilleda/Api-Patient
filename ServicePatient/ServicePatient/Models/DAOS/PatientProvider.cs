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
        //Obtenir tout les patients
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
                    date_naissance = DateTime.Parse(dr["Date_Naissance"].ToString()),
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
        //Obtenir un patient selon le nom
        public static List<Patient> GetPatientByNom(string nom_famille,string prenom) 
        {
            List<Patient> liste = new List<Patient>();
            Patient patient;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from patient WHERE Nom=@nom_famille AND Prenom=@prenom";
            DbParameter parameter; 
            parameter = new MySqlParameter
            {
                ParameterName = "nom_famille",
                DbType = System.Data.DbType.String,
                Value = nom_famille
            };
            cmd.Parameters.Add(parameter);
            parameter = new MySqlParameter
            {
                ParameterName = "prenom",
                DbType = System.Data.DbType.String,
                Value = prenom
            };
            cmd.Parameters.Add(parameter);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                patient = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    date_naissance = DateTime.Parse(dr["Date_Naissance"].ToString()),
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
        //Obtenir un patient selon le numéro assurance maladie
        public static List<Patient> GetPatientByNumAssMal(string NumAss)
        {
            List<Patient> liste = new List<Patient>();
            Patient patient;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from patient WHERE Num_AssMal=@num_assMal";
            DbParameter parameter = new MySqlParameter
            {
                ParameterName = "num_assMal",
                DbType = System.Data.DbType.String,
                Value = NumAss
            };
            cmd.Parameters.Add(parameter);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                patient = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    date_naissance = DateTime.Parse(dr["Date_Naissance"].ToString()),
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
        //Obtenir un Patient par ID
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
            cmd.Parameters.Add(parameter);
            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                patient = new Patient
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    numAssMaladie = dr["Num_AssMal"].ToString(),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    date_naissance = DateTime.Parse(dr["Date_Naissance"].ToString()),
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
        //Méthode en charge de créer une clé API selon un patient créé
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
                    date_naissance = DateTime.Parse(dr["Date_Naissance"].ToString()),
                    sexe = dr["Sexe"].ToString(),
                    allergies = dr["Allergies"].ToString(),
                    adresse = dr["Adresse"].ToString(),
                    num_tel = Int32.Parse(dr["Num_Tel"].ToString()),
                    assurance = Convert.ToBoolean(dr["Assurance"].ToString())
                };
                int idPatient = patientKEY.id;
                return new JWTAuthentication().GenererToken(idPatient, "Patient");
            }
            cnx.Close();
            return null;
        }
        //Ajouter un Patient
        public static string AddPatient(Patient patient) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "INSERT INTO patient(Num_AssMal,Nom,Prenom,Date_Naissance,Sexe,Allergies,Adresse,Num_Tel,Assurance) " +
                "Values(@numAssMal,@nom,@prenom,@date,@sexe,@allergies,@adresse,@numero,@assurance)";
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
                ParameterName = "date",
                DbType = System.Data.DbType.Date,
                Value = patient.date_naissance
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
        //Modifier un Patient
        public static bool ModifierPatient(Patient patient)
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE patient SET Nom=@nom,Prenom=@prenom,Date_Naissance=@date,Sexe=@sexe," +
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
                ParameterName = "date",
                DbType = System.Data.DbType.Date,
                Value = patient.date_naissance
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
        public static bool DeletePatient(int id) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "DELETE from patient WHERE ID=@id";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        //Obtenir nos Docteurs
        public static List<Docteur> ObtenirDocteurs(int id) 
        {
            List<Docteur> liste = new List<Docteur>();
            List<int> listeIndexDocteur = new List<int>();
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select id_docteur FROM patient_docteur WHERE id_patient=@id";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(param);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int IDPatient = Int32.Parse(dr["id_docteur"].ToString());
                listeIndexDocteur.Add(IDPatient);
            }

            for (int i = 0; i < listeIndexDocteur.Count; i++)
            {
                liste.Add(DocteurProvider.GetDocteurSecured(listeIndexDocteur[i]));
            }
            cnx.Close();
            return liste;
        }
    }
}