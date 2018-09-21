using System;
using MySql.Data.MySqlClient;
using HairSalon;
using static HairSalon.Startup;

namespace HairSalon.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}