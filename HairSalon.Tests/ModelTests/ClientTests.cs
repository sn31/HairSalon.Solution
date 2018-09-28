using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static HairSalon.Startup;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {

        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=skye_nguyen_test;";
        }

        public void Dispose()
        {
            Stylist.ClearAll();
            Client.ClearAll();

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
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            int result = Client.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_TrueForSameClientName_Client()
        {
            Client firstClient = new Client("Jose", "111111");
            Client secondClient = new Client("Jose", "111111");

            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void Create_ClientCreatesToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Lila", "111111");
            testClient.Create();

            //Act
            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client> { testClient };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Create_AssignsIdToObject_id()
        {
            //Arrange
            Client testClient = new Client("Josh", "11111231");
            testClient.Create();
            Console.WriteLine(testClient.Id);
            //Act
            Client CreatedClient = Client.GetAll() [0];

            int result = CreatedClient.Id;
            int testId = testClient.Id;

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsClientInDatabase_Client()
        {
            Client testClient = new Client("Jello", "12893128");
            testClient.Create();

            Client result = Client.Find(testClient.Id);

            Assert.AreEqual(testClient, result);
        }

        [TestMethod]
        public void AddStylist_AddsStylistToClient_StylistList()
        {
            Client testClient = new Client("Skye", "001283913");
            testClient.Create();

            Stylist testStylist = new Stylist("Bob");
            testStylist.Create();

            testClient.AddStylist(testStylist);
            List<Stylist> result = testClient.GetStylists();
            List<Stylist> testList = new List<Stylist> { testStylist };

            CollectionAssert.AreEqual(testList, result);
        }
        [TestMethod]
        public void GetStylists_ReturnAllClientStylists_StylistsList()
        {
            Client testClient = new Client("Huy", "71273319");
            testClient.Create();

            Stylist testStylist1 = new Stylist("Guy 1");
            testStylist1.Create();
            Stylist testStylist2 = new Stylist("Guy2");
            testStylist2.Create();

            testClient.AddStylist(testStylist1);
            testClient.AddStylist(testStylist2);
            List<Stylist> result = testClient.GetStylists();
            List<Stylist> testList = new List<Stylist> { testStylist1, testStylist2 };

            CollectionAssert.AreEqual(testList, result);
        }
        [TestMethod]
        public void Delete_DeletesClientAssociationsFromDatabase_ClientList()
        {
            Stylist testStylist = new Stylist("Bob");
            testStylist.Create();

            Client testClient = new Client("Cow", "172378");
            testClient.Create();
            testClient.AddStylist(testStylist);
            Client.Delete(testClient.Id);

            List<Client> resultStylistClients = testStylist.GetClients();
            List<Client> testStylistClients = new List<Client> { };

            //Assert
            CollectionAssert.AreEqual(testStylistClients, resultStylistClients);
        }
        [TestMethod]
        public void Update_UpdateClientWithNewInfo_Client()
        {
            Client testClient = new Client("hye","163712321");
            testClient.Create();
            testClient.Update("Milk", "7128123");

            string result = testClient.Name;

            Assert.AreEqual("Milk", result);

        }
        [TestMethod]
        public void AddStylists_AddStylistsCorrectly()
        {
            //Arrange
            Client testClient = new Client("foo","12983712");
            testClient.Create();
            Stylist newStylist = new Stylist("Bazzzzy");
            newStylist.Create();
            List <Stylist> expectedStylists = new List <Stylist> {newStylist};
            
            //Act
            testClient.AddStylist(newStylist);
            List <Stylist> Stylists = testClient.GetStylists();

            //Assert
            CollectionAssert.AreEqual(expectedStylists, Stylists);
        }
        [TestMethod]
        public void GetStylists_GetAllStylistsCorrectly()
        {
            //Arrange
            Client testClient = new Client("bobz","10927312");
            testClient.Create();
            Stylist newStylists1 = new Stylist("Buz");
            newStylists1.Create();
            Stylist newStylists2 = new Stylist("Biz");
            newStylists2.Create();
            List <Stylist> expectedStylists = new List <Stylist> {newStylists1, newStylists2};
            testClient.AddStylist(newStylists1);
            testClient.AddStylist(newStylists2);

            //Act
            List <Stylist> Stylists = testClient.GetStylists();

            //Assert
            CollectionAssert.AreEqual(expectedStylists, Stylists);
        }

    }
}