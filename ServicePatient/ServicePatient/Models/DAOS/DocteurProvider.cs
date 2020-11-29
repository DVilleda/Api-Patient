using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
                    specialite = dr["Specialisation"].ToString()
                };
                return docteur;
            }
            cnx.Close();
            return null;
        }

        public static int AddDocteur(Docteur docteur) 
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
            int resultID = (int)cmd.ExecuteScalar();
            return resultID;
        }
    }
}