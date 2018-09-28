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

        [HttpGet("/stylists/{stylistId}")]
        public ActionResult Details(int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            Stylist selectedStylist = Stylist.Find(stylistId);

            List<Client> allClients = selectedStylist.GetClients();
            Console.WriteLine(stylistId);
            Console.WriteLine(allClients.Count);
            model.Add("stylist", selectedStylist);
            model.Add("client", allClients);
            
            return View(model);
        }
        [HttpGet("/stylists/{stylistId}/clients/new")]
        [HttpPost("/stylists/{stylistId}/clients/new")]
        public ActionResult CreateForm(int stylistId)
        {
            if (Request.Method == "POST")
            {
                Client newClient = new Client(Request.Form["newClient"], Request.Form["newPhone"]);
                newClient.Create();
                Stylist selectedStylist = Stylist.Find(stylistId);
                
                selectedStylist.AddClient(newClient);
                return RedirectToAction("Details");
            }
            else{
                return View(stylistId);
            }
        }
        [HttpGet("/stylists/{stylistId}/specialties/new")]
        [HttpPost("/stylists/{stylistId}/specialties/new")]
        public ActionResult CreateSpecialty(int stylistId)
        {
            if (Request.Method == "POST")
            {
                Specialty newSpecialty= new Specialty(Request.Form["newSpecialty"]);
                newSpecialty.Create();
                Stylist selectedStylist = Stylist.Find(stylistId);
                newSpecialty.AddStylist(selectedStylist);
                selectedStylist.AddSpecialty(newSpecialty);
                return RedirectToAction("Details");
            }
            else{
                return View(stylistId);
            }
        }
    
        [HttpPost("/stylists/delete/{stylistId}")]
        public ActionResult Delete(int stylistId)
        {
            Stylist.Delete(stylistId);
            return RedirectToAction("Index");
        }
        [HttpPost("/stylists/deleteall")]
        public ActionResult DeleteAll()
        {
            Stylist.ClearAll();
            return RedirectToAction("Index");
        }
        [HttpPost("/stylists/{stylistId}/clients/remove/{clientId}")]
        public ActionResult RemoveClient(int stylistId, int clientId)
        {
            Stylist selectedStylist = Stylist.Find(stylistId);
            selectedStylist.RemoveClient(clientId);
            return RedirectToAction("Details",selectedStylist);
        }
    }
}