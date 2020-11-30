using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ServicePatient.Models.DAOS
{
    public class ReferenceProvider
    {
        public static string cnxString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=clinique";

        public static List<References> GetAllReferences() 
        {
            List<References> liste = new List<References>();
            References reference;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from reference";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                reference = new References()
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    Nom_Doc = dr["Nom_Docteur"].ToString(),
                    adresse_ref = dr["Lieu_Reference"].ToString(),
                    raison_ref = dr["Raison"].ToString(),
                    date_creation = DateTime.Parse(dr["Date_Reference"].ToString())
                };
                liste.Add(reference);
            }
            cnx.Close();
            return liste;
        }
        public static List<References> GetAllReferencesByPatient(int id)
        {
            List<References> liste = new List<References>();
            References reference;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM reference WHERE id_patient=@id";
            DbParameter param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(param);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                reference = new References()
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    Nom_Doc = dr["Nom_Docteur"].ToString(),
                    adresse_ref = dr["Lieu_Reference"].ToString(),
                    raison_ref = dr["Raison"].ToString(),
                    date_creation = DateTime.Parse(dr["Date_Reference"].ToString())
                };
                liste.Add(reference);
            }
            cnx.Close();
            return liste;
        }
        public static List<References> GetAllReferencesByDocteur(int id)
        {
            List<References> liste = new List<References>();
            References reference;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from reference Where id_docteur=@id";
            DbParameter param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = id
            };
            cmd.Parameters.Add(param);
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                reference = new References()
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    Nom_Doc = dr["Nom_Docteur"].ToString(),
                    adresse_ref = dr["Lieu_Reference"].ToString(),
                    raison_ref = dr["Raison"].ToString(),
                    date_creation = DateTime.Parse(dr["Date_Reference"].ToString())
                };
                liste.Add(reference);
            }
            cnx.Close();
            return liste;
        }
        public static bool AddReference(References reference)      
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Insert INTO reference(id_patient,id_docteur,Nom_Docteur,Lieu_Reference,Raison,Date_Reference)" +
                "Values(@id_patient,@id_doc,@nom_doc,@lieu,@raison,@date)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id_patient",
                DbType = System.Data.DbType.Int32,
                Value = reference.id_patient
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "id_doc",
                DbType = System.Data.DbType.Int32,
                Value = reference.id_docteur
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "nom_doc",
                DbType = System.Data.DbType.String,
                Value = reference.Nom_Doc
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "lieu",
                DbType = System.Data.DbType.String,
                Value = reference.adresse_ref
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "raison",
                DbType = System.Data.DbType.String,
                Value = reference.raison_ref
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "date",
                DbType = System.Data.DbType.Date,
                Value = reference.date_creation.Date
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        public bool UpdateReference(References reference) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE reference Set Lieu_Reference=@lieu,Raison=@raison WHERE ID=@id ";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = reference.id
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "lieu",
                DbType = System.Data.DbType.String,
                Value = reference.adresse_ref
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "raison",
                DbType = System.Data.DbType.String,
                Value = reference.raison_ref
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }
        public static bool DeleteReference(int id) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Delete from reference WHERE ID=@id";
            DbParameter param = new MySqlParameter
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
    }
}