using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ServicePatient.Models.DAOS
{
    public class NotesProvider
    {
        public static string cnxString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=clinique";

        public static List<Notes> GetAllNotes() 
        {
            List<Notes> liste = new List<Notes>();
            Notes note;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText="Select * from Notes";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                note = new Notes
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    Titre = dr["Titre"].ToString(),
                    contenu = dr["Contenu"].ToString(),
                    Date = DateTime.Parse(dr["Date_Note"].ToString())
                };
                liste.Add(note);
            }
            cnx.Close();
            return liste;
        }

        public static List<Notes> GetNotesByPatient(int id) 
        {
            List<Notes> liste = new List<Notes>();
            Notes note;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from Notes Where id_patient=@id";
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
                note = new Notes
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    Titre = dr["Titre"].ToString(),
                    contenu = dr["Contenu"].ToString(),
                    Date = DateTime.Parse(dr["Date_Note"].ToString())
                };
                liste.Add(note);
            }
            cnx.Close();
            return liste;
        }

        public static List<Notes> GetNotesByDocteur(int id)
        {
            List<Notes> liste = new List<Notes>();
            Notes note;
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "Select * from Notes Where id_docteur=@id";
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
                note = new Notes
                {
                    id = Int32.Parse(dr["ID"].ToString()),
                    id_patient = Int32.Parse(dr["id_patient"].ToString()),
                    id_docteur = Int32.Parse(dr["id_docteur"].ToString()),
                    Titre = dr["Titre"].ToString(),
                    contenu = dr["Contenu"].ToString(),
                    Date = DateTime.Parse(dr["Date_Note"].ToString())
                };
                liste.Add(note);
            }
            cnx.Close();
            return liste;
        }

        public static bool AddNote(Notes note)
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "INSERT INTO notes(id_patient,id_docteur,Titre,Contenu,Date_Note) " +
                "VALUES(@id_patient,@id_doc,@titre,@contenu,@date)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "id_patient",
                DbType = System.Data.DbType.Int32,
                Value = note.id_patient
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "id_doc",
                DbType = System.Data.DbType.Int32,
                Value = note.id_docteur
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "titre",
                DbType = System.Data.DbType.String,
                Value = note.Titre
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "contenu",
                DbType = System.Data.DbType.String,
                Value = note.contenu
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "date",
                DbType = System.Data.DbType.Date,
                Value = note.Date.Date
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }

        public static bool UpdateNote(Notes note) 
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "UPDATE notes SET Titre=@titre,Contenu=@contenu WHERE ID=@id";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "titre",
                DbType = System.Data.DbType.String,
                Value = note.Titre
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "contenu",
                DbType = System.Data.DbType.String,
                Value = note.contenu
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "id",
                DbType = System.Data.DbType.Int32,
                Value = note.id
            };
            cmd.Parameters.Add(param);
            bool result = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return result;
        }

        public static bool DeleteNote(int id)
        {
            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = cnxString;
            cnx.Open();
            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "DELETE FROM notes WHERE ID=@id";
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