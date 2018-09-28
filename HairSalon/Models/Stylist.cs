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
        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = this.Id == newStylist.Id;
                bool nameEquality = this.Name == newStylist.Name;
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        public override string ToString()
        {
            return String.Format("{{ id={0}, name={1}}}", Id, Name);
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (`name`) VALUES (@NewName);";
            cmd.Parameters.AddWithValue("@NewName", this.Name);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
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

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

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

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `stylists_clients`;";
            cmd.ExecuteNonQuery();
             cmd.CommandText = @"DELETE FROM `stylists_specialties`;";
            cmd.ExecuteNonQuery();
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

            var cmd = conn.CreateCommand() as MySqlCommand;
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

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name FROM `stylists` WHERE id = @NewId;";

            cmd.Parameters.AddWithValue("@NewId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            rdr.Read();

            string newName = rdr.GetString(0);

            Stylist foundStylist = new Stylist(newName, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundStylist;
        }
        public static void Delete(int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_clients WHERE stylist_id = @thisId;
            DELETE FROM stylists WHERE id = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", stylistId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void AddClient(Client newClient)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO stylists_clients (stylist_id, client_id) VALUES (@StylistId, @ClientId);";
            cmd.Parameters.AddWithValue("@ClientId", newClient.Id);
            cmd.Parameters.AddWithValue("@StylistId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        
        public List<Client> GetClients()
        {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT clients.* FROM stylists
            JOIN stylists_clients ON (stylists.id = stylists_clients.stylist_id)
            JOIN clients ON (stylists_clients.client_id = clients.id)
            WHERE stylists.id = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                string Phone = rdr.GetString(2);
                Client newClient = new Client(Name, Id);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        public void AddSpecialty(Specialty newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO stylists_specialties (`stylist_id`, `specialty_id`) VALUES (@StylistId, @SpecialtyId);";
            cmd.Parameters.AddWithValue("@SpecialtyId", newSpecialty.Id);
            cmd.Parameters.AddWithValue("@StylistId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Specialty> GetSpecialties()
        {
            List<Specialty> allSpecialties = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT specialties.* FROM stylists
            JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
            JOIN specialties ON (stylists_specialties.specialty_id = specialties.id)
            WHERE stylists.id = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(Name, Id);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }
        public void RemoveClient(int clientId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_clients WHERE client_id = @clientId AND stylist_id = @stylistId;";
            cmd.Parameters.AddWithValue("@stylistId", this.Id);
            cmd.Parameters.AddWithValue("@clientId", clientId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void RemoveSpecialty(int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_specialties WHERE specialty_id = @specialtyId AND stylist_id = @stylistId;";
            cmd.Parameters.AddWithValue("@stylistId", this.Id);
            cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}