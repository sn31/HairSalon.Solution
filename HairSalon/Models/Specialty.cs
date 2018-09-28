using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Specialty(string newName, int newId = 0)
        {
            Name = newName;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `specialties` (`name`) VALUES (@NewName);";
            MySqlParameter food = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewName", this.Name);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `specialties`;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);

                Specialty newSpecialty= new Specialty(Name, Id);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `specialties`;";

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

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `specialties` SET name = @NewName WHERE id = @thisId;";

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

        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name FROM `specialties` WHERE id = @NewId;";

            cmd.Parameters.AddWithValue("@NewId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            rdr.Read();

            string newName = rdr.GetString(0);

            Specialty foundSpecialty = new Specialty(newName, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundSpecialty;
        }
        public static void Delete(int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_specialties WHERE specialty_id = @thisId;
            DELETE FROM specialties WHERE id = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", specialtyId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO stylists_specialties (`stylist_id`, `specialty_id`) VALUES (@StylistId, @SpecialtyId);";
            cmd.Parameters.AddWithValue("@StylistId", newStylist.Id);
            cmd.Parameters.AddWithValue("@SpecialtyId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Stylist> GetStylists()
        {
            List<Stylist> allStylists = new List<Stylist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT stylists.* FROM specialties
            JOIN stylists_specialties ON (specialties.id = stylists_clients.specialty_id)
            JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
            WHERE specialties.id = @thisId;";
            cmd.Parameters.AddWithValue("@thisId",this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                Stylist newStylist = new Stylist(Name, Id);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn!=null)
            {
                conn.Dispose();
            }
        return allStylists;
        }
    }
}