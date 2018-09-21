using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static HairSalon.Startup;

namespace HairSalon.Tests {
    [TestClass]
    public class StylistTests : IDisposable {

        public StylistTests () {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=skye_nguyen_test;";
        }

        public void Dispose () {
            Stylist.ClearAll ();
        }

        [TestMethod]
        public void GetAll_StylistsEmptyAtFirst_0 () {
            int result = Stylist.GetAll ().Count;
            Assert.AreEqual (0, result);
        }

        [TestMethod]
        public void Create_StylistAddedCorrectly_True () {
            Stylist newStylist = new Stylist ("Jose");
            newStylist.Create ();

            Stylist test = Stylist.GetAll () [0];
            Assert.AreEqual (newStylist.Name, test.Name);
        }
        
        [TestMethod]
        public void ClearAll_DeleteAllCusines_Int ()
        {
            Stylist newStylist1 = new Stylist ("Lilly");
            newStylist1.Create ();
            Stylist newStylist2 = new Stylist ("Doug");
            newStylist2.Create ();
            Stylist.ClearAll();
            Stylist newStylist5 = new Stylist ("Rose");
            newStylist5.Create ();            
            int result = Stylist.GetAll().Count;
            Assert.AreEqual( result, 1);
        }

        [TestMethod]
        public void Update_ChangeStylistNameCorrectly_True()
        {
            Stylist newStylist = new Stylist("Rose");
            
            newStylist.Create();
            Console.WriteLine(newStylist.Id);
            newStylist.Update("Lilly");

            Stylist result = Stylist.GetAll()[0];

            Assert.AreEqual("Lilly",newStylist.Name);
        }
    }
}