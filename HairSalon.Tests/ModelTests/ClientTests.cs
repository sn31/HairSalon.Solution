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
            Client.ClearAll();
            Stylist.ClearAll();
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
            Client firstClient = new Client("Jose","111111");
            Client secondClient = new Client("Jose", "111111");

            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void Create_ClientSavesToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Lila","111111");
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
            Client testClient = new Client("Josh","11111231");
            testClient.Create();

            //Act
            Client savedClient = Client.GetAll()[0];

            int result = savedClient.Id;
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

        // [TestMethod]
        // public void AddCategory_AddsCategoryToClient_CategoryList()
        // {
        //     Client testClient = new Client("Cow Cake", "Some steps", 4);
        //     testClient.Save();

        //     Category testCategory = new Category("Junk Food");
        //     testCategory.Save();

        //     testClient.AddCategory(testCategory);
        //     List<Category> result = testClient.GetCategories();
        //     List<Category> testList = new List<Category> { testCategory };

        //     CollectionAssert.AreEqual(testList, result);
        // }
        // [TestMethod]
        // public void GetCategories_ReturnAllClientCategories_CategoriesList()
        // {
        //     Client testClient = new Client("Cow Cake", "Some steps", 4);
        //     testClient.Save();

        //     Category testCategory1 = new Category("Junk Food");
        //     testCategory1.Save();
        //     Category testCategory2 = new Category("Health Food");
        //     testCategory2.Save();

        //     testClient.AddCategory(testCategory1);
        //     testClient.AddCategory(testCategory2);
        //     List<Category> result = testClient.GetCategories();
        //     List<Category> testList = new List<Category> { testCategory1, testCategory2 };

        //     CollectionAssert.AreEqual(testList, result);
        // }
        // [TestMethod]
        // public void Delete_DeletesClientAssociationsFromDatabase_ClientList()
        // {
        //     Category testCategory = new Category("Breakfast");
        //     testCategory.Save();

        //     Client testClient = new Client("cookies", "foo bar", 1);
        //     testClient.Save();
        //     testClient.AddCategory(testCategory);
        //     testClient.Delete();

        //     List<Client> resultCategoryClients = testCategory.GetClients();
        //     List<Client> testCategoryClients = new List<Client> { };

        //     //Assert
        //     CollectionAssert.AreEqual(testCategoryClients, resultCategoryClients);
        // }
        // [TestMethod]
        // public void Update_UpdateClientWithNewInfo_Client()
        // {
        //     Client testClient = new Client("cookies", "foo bar", 1);
        //     testClient.Save();
        //     testClient.Update("Milk", "foo bar", 2);

        //     string result = testClient.Name;

        //     Assert.AreEqual("Milk", result);

        // }
        // [TestMethod]
        // public void AddIngredient_AddIngredientCorrectly()
        // {
        //     //Arrange
        //     Client testClient = new Client("cookies", "foo bar", 1);
        //     testClient.Save();
        //     Ingredient newIngredient1 = new Ingredient("Egg");
        //     newIngredient1.Save();
        //     List <Ingredient> expectedIngredients = new List <Ingredient> {newIngredient1};

        //     //Act
        //     testClient.AddIngredient(newIngredient1);
        //     List <Ingredient> ingredients = testClient.GetIngredients();

        //     //Assert
        //     CollectionAssert.AreEqual(expectedIngredients, ingredients);
        // }
        // [TestMethod]
        // public void GetIngredients_GetAllIngredientsCorrectly()
        // {
        //     //Arrange
        //     Client testClient = new Client("cookies", "foo bar", 1);
        //     testClient.Save();
        //     Ingredient newIngredient1 = new Ingredient("Egg");
        //     newIngredient1.Save();
        //     Ingredient newIngredient2 = new Ingredient("Milk");
        //     newIngredient2.Save();
        //     List <Ingredient> expectedIngredients = new List <Ingredient> {newIngredient1, newIngredient2};
        //     testClient.AddIngredient(newIngredient1);
        //     testClient.AddIngredient(newIngredient2);

        //     //Act
        //     List <Ingredient> ingredients = testClient.GetIngredients();

        //     //Assert
        //     CollectionAssert.AreEqual(expectedIngredients, ingredients);
        // }

    }
}