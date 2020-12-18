using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ServicePatient.Models.DAOS
{
    public class DocteurProvider
    {
        public static string cnxString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=clinique";

        public static List<Docteur> GetAllDocteur() 
        {
            List<Docteur> liste = new List<Docteur>();
            Docteur docteur;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM docteur";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                docteur = new Docteur
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    num_employe = Int32.Parse(dr["Num_Employe"].ToString()),
                    specialite = dr["Specialisation"].ToString()
                };
                liste.Add(docteur);
            }
            cnx.Close();
            return liste;
        }
        public static Docteur getDocteur(int id) 
        {
            Docteur docteur;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM docteur WHERE ID=@id";
            DbParameter param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(param);
            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                docteur = new Docteur
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    num_employe = Int32.Parse(dr["Num_Employe"].ToString()),
                    specialite = dr["Specialisation"].ToString(),
                };
                return docteur;
            }
            cnx.Close();
            return null;
        }
        private static string RetournerAPIKey(Docteur docteur)
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM docteur WHERE Num_Employe=@num";
            DbParameter param = new MySqlParameter
            {
                ParameterName = "num",
                DbType = System.Data.DbType.Int32,
                Value = docteur.num_employe
            };
            cmd.Parameters.Add(param);
            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Docteur docteurKEY = new Docteur
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    num_employe = Int32.Parse(dr["Num_Employe"].ToString()),
                    specialite = dr["Specialisation"].ToString()
                };
                int idDoc = docteurKEY.id;
                return new JWTAuthentication().GenererToken(idDoc, "Docteur");
            }
            cnx.Close();
            return null;
        }

        public static string AddDocteur(Docteur docteur) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "INSERT INTO docteur(Nom,Prenom,Num_Employe,Specialisation) " +
                "Values(@nom,@prenom,@num_employe,@specialite)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "nom",
                DbType = System.Data.DbType.String,
                Value = docteur.nom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "prenom",
                DbType = System.Data.DbType.String,
                Value = docteur.prenom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "num_employe",
                DbType = System.Data.DbType.Int32,
                Value = docteur.num_employe
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "specialite",
                DbType = System.Data.DbType.String,
                Value = docteur.specialite
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            if (result) 
            {
                return RetournerAPIKey(docteur);
            }
            else { return null; }
        }

        public static bool ModifierDocteur(Docteur docteur)
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE docteur Set Specialisation=@spec Where ID=@id";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = docteur.id
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "spec",
                DbType = System.Data.DbType.String,
                Value = docteur.specialite
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }

        public static List<Patient> ObtenirPatient(int id) 
        {
            List<Patient> liste = new List<Patient>();
            List<int> listeIndexPatient = new List<int>();
            Patient docteur;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select id_patient FROM patient_docteur WHERE id_docteur=@id";
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
                int IDPatient = Int32.Parse(dr["id_patient"].ToString());
                listeIndexPatient.Add(IDPatient);
            }

            for (int i=0;i<listeIndexPatient.Count;i++) 
            {
                liste.Add(PatientProvider.GetPatient(listeIndexPatient[i]));
            }
            cnx.Close();
            return liste;
        }




        #region oldMethod
        private static bool AddKeyDatabase(Docteur docteur,string apikey) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE docteur Set APIKey=@apikey Where ID=@id";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = docteur.id
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "apikey",
                DbType = System.Data.DbType.String,
                Value = apikey
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
        #endregion
    }
}