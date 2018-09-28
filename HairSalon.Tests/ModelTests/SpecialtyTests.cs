using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using HairSalon;

namespace StylistBox.Tests
{
    [TestClass]
    public class SpecialtyTest : IDisposable
    {
        public SpecialtyTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=skye_nguyen_test;";
        }
        public void Dispose()
        {
            
            Stylist.ClearAll();
            Specialty.ClearAll();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"ALTER TABLE stylists_clients AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"ALTER TABLE stylists_specialties AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"ALTER TABLE stylists AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"ALTER TABLE specialties AUTO_INCREMENT = 1;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        [TestMethod]
        public void GetAll_DBStartFromEmpty()
        {
            //Arrange
            int expectedCount = 0;

            //Act
            List <Specialty> allSpecialties = Specialty.GetAll();
            int count = allSpecialties.Count;

            //Assert
            Assert.AreEqual(expectedCount, count);
        }
        [TestMethod]
        public void Create_AddNewSpecialty()
        {
            //Arrange
            Specialty newSpecialty = new Specialty("Color");
            newSpecialty.Create();
            List <Specialty> expectedSpecialties = new List <Specialty> {newSpecialty};

            //Act
            List <Specialty> Specialties = Specialty.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedSpecialties, Specialties);
        }
        [TestMethod]
        public void GetAll_ReturnEverySpecialties_List()
        {
            //Arrange
            Specialty newSpecialty1 = new Specialty("Cut");
            newSpecialty1.Create();
            Specialty newSpecialty2 = new Specialty("Perm");
            newSpecialty2.Create();
            List <Specialty> expectedSpecialties = new List <Specialty> {newSpecialty1, newSpecialty2};
            
            //Act
            List <Specialty> Specialties = Specialty.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedSpecialties, Specialties);
        }
        [TestMethod]
        public void Find_FindExactSpecialty_Specialty()
        {
            //Arrange
            Specialty newSpecialty1 = new Specialty("Cut");
            newSpecialty1.Create();
            Specialty newSpecialty2 = new Specialty("Color");
            newSpecialty2.Create();

            //Act
            Specialty foundSpecialty = Specialty.Find(newSpecialty1.Id);

            //Assert
            Assert.AreEqual(newSpecialty1, foundSpecialty);
        }
        [TestMethod]
        public void Update_UpdateNewName()
        {
            //Arrange
            Specialty newSpecialty = new Specialty("Trim");
            newSpecialty.Create();
            string newName = "Color";

            //Act
            newSpecialty.Update(newName);

            //Assert
            Assert.AreEqual(newName, newSpecialty.Name);
        }
        [TestMethod]
        public void Delete_DeleteSpecialtyProperly()
        {
            //Arrange
            Specialty newSpecialty1 = new Specialty("Color");
            newSpecialty1.Create();
            Specialty newSpecialty2 = new Specialty("Cut");
            newSpecialty2.Create();
            List <Specialty> expectedSpecialties = new List <Specialty> {newSpecialty2};

            //Act
            Specialty.Delete(newSpecialty1.Id);
            List <Specialty> Specialties = Specialty.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedSpecialties, Specialties);
        }
        [TestMethod]
        public void DeleteAll_DeleteAllSpecialties()
        {
            //Arrange
            Specialty newSpecialty1 = new Specialty("Color");
            newSpecialty1.Create();
            Specialty newSpecialty2 = new Specialty("Cut");
            newSpecialty2.Create();
            List <Specialty> expectedSpecialties = new List <Specialty> {};

            //Act
            Specialty.ClearAll();
            List <Specialty> Specialties = Specialty.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedSpecialties, Specialties);
        }
        [TestMethod]
        public void AddStylist_AddStylistExactly()
        {   
            //Arrange
            Specialty newSpecialty = new Specialty("Color");
            newSpecialty.Create();
            Stylist newStylist = new Stylist("Jane");
            newStylist.Create();
            List <Stylist> expectedStylists = new List <Stylist> {newStylist};

            //Act
            newSpecialty.AddStylist(newStylist);
            List <Stylist> Stylists = newSpecialty.GetStylists();

            //Assert
            CollectionAssert.AreEqual(expectedStylists, Stylists);
        }
        [TestMethod]
        public void GetStylists_GetAllStylists_List()
        {
            //Arrange
            Specialty newSpecialty = new Specialty("Stuff");
            newSpecialty.Create();
            Stylist newStylist = new Stylist("Baxx");
            newStylist.Create();
            List <Stylist> expectedStylists = new List <Stylist> {newStylist};
            newSpecialty.AddStylist(newStylist);

            //Act
            List <Stylist> Stylists = newSpecialty.GetStylists();

            //AssertAssert
            CollectionAssert.AreEqual(expectedStylists, Stylists);
        }
    }
}