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

        // [HttpPost("/stylists")]
        // public ActionResult Create()
        // {
        //     List<Stylist> StylistList = Stylist.GetAll();

        //     int flag = 0;
        //     foreach (Stylist Stylist in StylistList)
        //     {
        //         if (Stylist.Name == Request.Form["newStylist"])
        //         {
        //             flag++;
        //         }
        //     }
        //     if (flag == 0)
        //     {
        //         Stylist newStylist = new Stylist(Request.Form["newStylist"]);
        //         newStylist.Create();
        //     }
        //     return RedirectToAction("Index","Clients");
        // }

    }
}