using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class SpecialtiesController : Controller
    {
        [HttpGet("/specialties")]
        public ActionResult Index()
        {
            List<Specialty> SpecialtyList = Specialty.GetAll();
            return View(SpecialtyList);
        }

        [HttpGet("/specialties/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/specialties/new")]
        public ActionResult Create()
        {
            List<Specialty> SpecialtyList = Specialty.GetAll();

            int flag = 0;
            foreach (Specialty Specialty in SpecialtyList)
            {
                if (Specialty.Name == Request.Form["newSpecialty"])
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                Specialty newSpecialty = new Specialty(Request.Form["newSpecialty"]);
                newSpecialty.Create();
            }
            return RedirectToAction("Index");
        }

        [HttpPost("/specialties/delete/{SpecialtyId}")]
        public ActionResult Delete(int SpecialtyId)
        {
            Specialty.Delete(SpecialtyId);
            return RedirectToAction("Index");
        }

        [HttpPost("/specialties/deleteall")]
        public ActionResult DeleteAll()
        {
            Specialty.ClearAll();
            return RedirectToAction("Index");
        }

        [HttpGet("/specialties/{specialtyId}/stylists/new")]
        [HttpPost("/specialties/{specialtyId}/stylists/new")]
        public ActionResult CreateStylist(int specialtyId)
        {
             if (Request.Method == "POST")
            {
            Stylist newStylist = new Stylist(Request.Form["newStylist"]);
            newStylist.Create();
            Specialty selectedSpecialty = Specialty.Find(specialtyId);

            selectedSpecialty.AddStylist(newStylist);
            return RedirectToAction("Index");
            }
            else{
                return View(specialtyId);
            }
        }
    }

}