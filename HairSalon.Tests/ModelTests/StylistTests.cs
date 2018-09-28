using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static HairSalon.Startup;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {

        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=skye_nguyen_test;";
        }

        public void Dispose()
        {
            Stylist.ClearAll();
            Client.ClearAll();
            Specialty.ClearAll();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"ALTER TABLE stylists_clients AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"ALTER TABLE stylists AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"ALTER TABLE clients AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        [TestMethod]
        public void GetAll_StylistsEmptyAtFirst_0()
        {
            int result = Stylist.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Create_StylistAddedCorrectly_True()
        {
            Stylist newStylist = new Stylist("Jose");
            newStylist.Create();

            Stylist test = Stylist.GetAll() [0];
            Assert.AreEqual(newStylist.Name, test.Name);
        }

        [TestMethod]
        public void ClearAll_DeleteAllCusines_Int()
        {
            Stylist newStylist1 = new Stylist("Lilly");
            newStylist1.Create();
            Stylist newStylist2 = new Stylist("Doug");
            newStylist2.Create();
            Stylist.ClearAll();
            Stylist newStylist5 = new Stylist("Rose");
            newStylist5.Create();
            int result = Stylist.GetAll().Count;
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void Update_ChangeStylistNameCorrectly_True()
        {
            Stylist newStylist = new Stylist("Rose");

            newStylist.Create();
            Console.WriteLine(newStylist.Id);
            newStylist.Update("Lilly");

            Stylist result = Stylist.GetAll() [0];

            Assert.AreEqual("Lilly", newStylist.Name);
        }

        [TestMethod]
        public void AddClient_AddAClientProperly()
        {
            //Arrange
            Stylist newStylist = new Stylist("Foo");
            newStylist.Create();
            Client newClient = new Client("Skye","172361");
            newClient.Create();
            List<Client> expectedClients = new List<Client> { newClient };
           
            //Act
            newStylist.AddClient(newClient);
            List<Client> allClients = newStylist.GetClients();

            //Assert
            Assert.AreEqual(expectedClients.Count, allClients.Count);
        }

        [TestMethod]
        public void GetClients_ReturnAllClients_List()
        {
            //Arrange
            Stylist newStylist1 = new Stylist("Baz");
            newStylist1.Create();
            Client newClient1 = new Client("Pug","182973");
            newClient1.Create();
            Client newClient2 = new Client("Pugna","1829738192");
            newClient2.Create();
            newStylist1.AddClient(newClient1);
            newStylist1.AddClient(newClient2);
            List<string> expectedClients = new List<string> { newClient1.Name, newClient2.Name };

            //Act
            List<Client> allClients = newStylist1.GetClients();
            List<String> allClientsName = new List<String> {};
            foreach(Client client in allClients)
            {
                allClientsName.Add(client.Name);
            }

            //Assert
            CollectionAssert.AreEqual(expectedClients, allClientsName);
        }
        [TestMethod]
        public void RemoveClient_CorrectlyRemoveClientFromStylist()
        {
            Client newClient = new Client("Skye","18923182");
            newClient.Create();
            List<Client> test = new List<Client>{};
            Stylist newStylist = new Stylist("Baz");
            newStylist.Create();
            newStylist.AddClient(newClient);
            newStylist.RemoveClient(newClient.Id);
            List<Client> allClients = newStylist.GetClients();
            CollectionAssert.Equals(test,allClients);
        }
        [TestMethod]
        public void RemoveSpecialty_CorrectlyRemoveSpecialtyFromStylist()
        {
            Specialty newSpecialty = new Specialty("Color");
            newSpecialty.Create();
            List<Specialty> test = new List<Specialty>{};
            Stylist newStylist = new Stylist("Baz");
            newStylist.Create();
            newStylist.AddSpecialty(newSpecialty);
            newStylist.RemoveSpecialty(newSpecialty.Id);
            List<Specialty> allspecialtys = newStylist.GetSpecialties();
            CollectionAssert.Equals(test,allspecialtys);
        }
    }
}