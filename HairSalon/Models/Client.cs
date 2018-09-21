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
        public int stylist_id {get;set;}
        public string Phone {get;set;}

        public Client(string newName,int newStylist_id, string newPhone,int newId = 0)
        {
            Name = newName;
            Phone = newPhone;
            stylist_id = newStylist_id;
            Id = newId;
        }
        public Client(string newName,int newStylist_id,int newId = 0)
        {
            Name = newName;
            stylist_id = newStylist_id;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO `clients` (`name`,`stylist_id`,`phone`) VALUES (@NewName,@NewStylistId,@NewPhone);";
            
            cmd.Parameters.AddWithValue("@NewName", this.Name);
            cmd.Parameters.AddWithValue("@NewPhone", this.Phone);
            cmd.Parameters.AddWithValue("@NewStylistId", this.stylist_id);

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
            cmd.CommandText = @"SELECT * FROM `Clients`;";
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                int stylist_id = rdr.GetInt32(2);
                string Phone = rdr.GetString(3);
              
                Client newClient = new Client(Name,stylist_id,Phone);
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
            cmd.CommandText = @"DELETE FROM `Clients`;";

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

        public static List<Client> Find(int id)
        {
            List<Client> allClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `clients` WHERE stylist_id = @SelectedId;";

            cmd.Parameters.AddWithValue("@SelectedId", id);
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            int newId = 0;
            string newName ="";
            int newStylist_id = 0;
            string newPhone ="";
   
            while(rdr.Read())
            {
                newId= rdr.GetInt32(0);
                newName= rdr.GetString(1);
                newStylist_id= rdr.GetInt32(2);
                newPhone = rdr.GetString(3);

            }
            
            Client selectedClient = new Client(newName,newStylist_id,newPhone,newId);
            allClients.Add(selectedClient);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        
    }
}