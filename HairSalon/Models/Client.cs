using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone {get;set;}

        public Client(string newName, string newPhone,int newId = 0)
        {
            Name = newName;
            Phone = newPhone;

            Id = newId;
        }
        public Client(string newName,int newId = 0)
        {
            Name = newName;
          
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO `clients` (`name`,`phone`) VALUES (@NewName,@NewPhone);";
            
            cmd.Parameters.AddWithValue("@NewName", this.Name);
           
            cmd.Parameters.AddWithValue("@NewPhone", this.Phone);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `clients`;";
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                string Phone = rdr.GetString(3);
              
                Client newClient = new Client(Name,Phone);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `clients`;";

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
            cmd.CommandText = @"UPDATE `clients` SET name = @NewName WHERE id = @thisId;";

           
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

        public static Client Find(int searchId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT*FROM clients WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", searchId);

            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            int Id = rdr.GetInt32(0);
            string Name = rdr.GetString(1);
            string Phone = rdr.GetString(2);
            Client foundClient = new Client(Name, Phone, Id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundClient;
        }
        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO stylists_clients (`stylist_id`, `client_id`) VALUES (@StylistId, @ClientId);";
            cmd.Parameters.AddWithValue("@StylistId", newStylist.Id);
            cmd.Parameters.AddWithValue("@RecipeId", this.Id);
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
            cmd.CommandText = @"SELECT stylists.* FROM clients
            JOIN stylists_clients ON (clients.id = stylists_clients.client_id)
            JOIN stylists ON (stylists_clients.stylist_id = stylists.id)
            WHERE clients.id = @thisId;";
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