using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static HairSalon.Startup;

namespace HairSalon.Tests {
    [TestClass]
    public class ClientTests : IDisposable {

        public ClientTests () {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=skye_nguyen_test;";
        }

        public void Dispose () {
            Client.ClearAll ();
        }

        [TestMethod]
        public void GetAll_ClientsEmptyAtFirst_0 () {
            int result = Client.GetAll ().Count;
            Assert.AreEqual (0, result);
        }
        [TestMethod]
        public void Create_ClientAddedCorrectly_True () {
            Client newClient = new Client ("Jose",1,"123456789");
            newClient.Create ();

            Client test = Client.GetAll () [0];
            Assert.AreEqual (newClient.Name, test.Name);
        }
        
        [TestMethod]
        public void ClearAll_DeleteAllCusines_Int ()
        {
            Client newClient1 = new Client ("Lilly",1,"123456789");
            newClient1.Create ();
            Client newClient2 = new Client ("Doug",2,"123456789");
            newClient2.Create ();
            Client.ClearAll();
            Client newClient5 = new Client ("Rose",1,"123456789");
            newClient5.Create ();            
            int result = Client.GetAll().Count;
            Assert.AreEqual( result, 1);
        }

        [TestMethod]
        public void Update_ChangeClientNameCorrectly_True()
        {
            Client newClient = new Client("Rose",3,"123456789");
            
            newClient.Create();
            
            newClient.Update("Lilly");

            Client result = Client.GetAll()[0];

            Assert.AreEqual("Lilly",newClient.Name);
        }
    
    }
}