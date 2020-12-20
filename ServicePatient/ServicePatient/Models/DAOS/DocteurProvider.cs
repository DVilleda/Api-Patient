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

        //Obtenir tout les Docteurs
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
        //Obtenir Docteur par ID
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
        //Obtenir Docteur par ID mais sans numero employe
        public static Docteur GetDocteurSecured(int id) 
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
                    specialite = dr["Specialisation"].ToString()
                };
                return docteur;
            }
            cnx.Close();
            return null;
        }
        //Obtenir Docteur par nom et prenom
        public static List<Docteur> getDocteurParNom(string nom,string prenom) 
        {
            List<Docteur> liste = new List<Docteur>();
            Docteur docteur;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM docteur WHERE Nom=@nom AND Prenom=@prenom";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "nom",
                DbType = System.Data.DbType.String,
                Value = nom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "prenom",
                DbType = System.Data.DbType.String,
                Value = prenom
            };
            cmd.Parameters.Add(param);
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
        //Obtenir Docteur par nom et prenom mais sans num employe
        public static List<Docteur> getDocteurParNomSecured(string nom, string prenom)
        {
            List<Docteur> liste = new List<Docteur>();
            Docteur docteur;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM docteur WHERE Nom=@nom AND Prenom=@prenom";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "nom",
                DbType = System.Data.DbType.String,
                Value = nom
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "prenom",
                DbType = System.Data.DbType.String,
                Value = prenom
            };
            cmd.Parameters.Add(param);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                docteur = new Docteur
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    nom = dr["Nom"].ToString(),
                    prenom = dr["Prenom"].ToString(),
                    specialite = dr["Specialisation"].ToString()
                };
                liste.Add(docteur);
            }
            cnx.Close();
            return liste;
        }
        //Methode qui va generer la cle API selon le ID du docteur
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
        //Ajouter un docteur dans la BD
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
        //Modifier un Docteur
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
        //Obtenir une liste de patients assignés à nous selon notre id
        public static List<Patient> ObtenirPatient(int id) 
        {
            List<Patient> liste = new List<Patient>();
            List<int> listeIndexPatient = new List<int>();
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

        #region Patient_Docteur
        //Ajouter un patient à note charge
        public static bool AjouterPatient(int IDPatient, int IDDocteur) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "INSERT INTO patient_docteur(id_patient,id_docteur) VALUES(@idPatient,@idDocteur) ";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "idPatient",
                DbType = System.Data.DbType.Int32,
                Value = IDPatient
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "idDocteur",
                DbType = System.Data.DbType.Int32,
                Value = IDDocteur
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        //Enlever un patient de notre charge
        public static bool EnleverPatient(int IDPatient, int IDDocteur) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "DELETE FROM patient_docteur WHERE id_patient=@idPatient AND id_docteur=@idDocteur ";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "idPatient",
                DbType = System.Data.DbType.Int32,
                Value = IDPatient
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "idDocteur",
                DbType = System.Data.DbType.Int32,
                Value = IDDocteur
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        //Trouver les patients qui n'ont pas de Docteur
        public static List<Patient> GetPatientsSansDocteur() 
        {
            List<Patient> listePatientSansDocteur = PatientProvider.GetAll();
            List<int> listeIndexPatient = new List<int>();
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select id_patient FROM patient_docteur";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int IDPatient = Int32.Parse(dr["id_patient"].ToString());
                listeIndexPatient.Add(IDPatient);
            }
            for (int i = 0; i < listeIndexPatient.Count; i++)
            {
                for (int j=0; j<listePatientSansDocteur.Count;j++) 
                {
                    if (listeIndexPatient[i] == listePatientSansDocteur[j].id) 
                    {
                        Debug.WriteLine(listePatientSansDocteur.Count);
                        listePatientSansDocteur.RemoveAt(j);
                    }
                }
            }
            cnx.Close();
            return listePatientSansDocteur;
        }
        //Assigner un Patient à un autre Docteur
        public static bool ChangerPatientDocteur(int IDPatient, int IDDocteur) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE patient_docteur SET id_docteur=@idDocteur WHERE id_patient=@idPatient ";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "idPatient",
                DbType = System.Data.DbType.Int32,
                Value = IDPatient
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "idDocteur",
                DbType = System.Data.DbType.Int32,
                Value = IDDocteur
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        #endregion
    }
}