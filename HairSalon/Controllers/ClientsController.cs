using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet("/clients")]
        public ActionResult Index()
        {
            List<Client> ClientList = Client.GetAll();
            return View(ClientList);
        }

        [HttpPost("/clients")]
        public ActionResult Create()
        {
            List<Client> ClientList = Client.GetAll();

            int flag = 0;
            foreach (Client Client in ClientList)
            {
                if (Client.Name == Request.Form["newClient"])
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                Client newClient = new Client(Request.Form["newClient"]);
                newClient.Create();
            }
            return RedirectToAction("Index");
        }

        [HttpPost("/clients/delete/{clientId}")]
        public ActionResult Delete(int clientId)
        {
            Client.Delete(clientId);
            return RedirectToAction("Index");
        }

        [HttpPost("/clients/deleteall")]
        public ActionResult DeleteAll()
        {
            Client.ClearAll();
            return RedirectToAction("Index");
        }
    }

}