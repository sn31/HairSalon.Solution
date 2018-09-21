using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<Stylist> StylistList = Stylist.GetAll();
            return View(StylistList);
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
            List<Stylist> StylistList = Stylist.GetAll();

            int flag = 0;
            foreach (Stylist Stylist in StylistList)
            {
                if (Stylist.Name == Request.Form["newStylist"])
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                Stylist newStylist = new Stylist(Request.Form["newStylist"]);
                newStylist.Create();
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            Stylist selectedStylist = Stylist.Find(id);
            List<Client> allClients = Client.Find(id);
            model.Add("stylist", selectedStylist);
            model.Add("client", allClients);
            Console.WriteLine(allClients.Count);
            return View(model);
        }
        [HttpGet("/stylists/{id}/clients/new")]
        [HttpPost("/stylists/{id}/clients/new")]
        public ActionResult CreateForm(int id)
        {
            if (Request.Method == "POST")
            {
                Client newClient = new Client(Request.Form["newClient"],id, Request.Form["newPhone"]);
                newClient.Create();
                return RedirectToAction("Details");
            }
            else{
                return View(id);
            }
        }

    }
}