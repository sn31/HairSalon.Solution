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

        [HttpPost("/specialties")]
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
    }

}