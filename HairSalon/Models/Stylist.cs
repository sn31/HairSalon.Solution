using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Stylist(string newName, int newId = 0)
        {
            Name = newName;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `stylists` (`name`) VALUES (@NewName);";
            MySqlParameter food = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewName", this.Name);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `stylists`;";
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);

                Stylist newStylist = new Stylist(Name, Id);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `stylists`;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"UPDATE `stylists` SET name = @NewName WHERE id = @thisId;";

           
            cmd.Parameters.AddWithValue("@NewName", newName);
        
            cmd.Parameters.AddWithValue("@thisId", this.Id);
            this.Name = newName;
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static int FindId(string StylistName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"SELECT id FROM `stylists` WHERE Name = @NewName;";

            cmd.Parameters.AddWithValue("@NewName", StylistName);
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            rdr.Read();
            int newId = rdr.GetInt32(0);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newId;
        }
        
    }
}