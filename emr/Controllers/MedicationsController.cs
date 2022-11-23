using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace emr.Controllers
{
    //[Route("Medications")]
    public class MedicationsController : Controller
    {
        private readonly IMedications _medications;

        public MedicationsController(IMedications medications)
        {
            _medications = medications;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _medications.GetMedicationsAll();
            return Json(result);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MedicationsSaveModel model)
        {
            if (ModelState.IsValid)
            {
                var medications = new medications()
                {
                    brand = model.brand,
                    generic = model.generic,
                    classification = model.classification,
                };
                medications.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                medications.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                _medications.SaveMedicationInfo(medications);
                return RedirectToAction("Index", "Medications");
            }

            return View();

        }
        // [Route("{id?}")]
        public IActionResult View(int id)
        {
            var result = _medications.GetFormDosageAll();
            List<SelectListItem> forms = new();
            foreach (var item in result)
            {
                var data = new SelectListItem()
                {
                    Value = item.FormID,
                    Text = item.FormDesc
                };
                forms.Add(data);
            }
            ViewBag.forms = forms;
            MedicationsModel med = _medications.GetMedicationsById(id);
            med.dosageModel = _medications.GetDosageAll(id);
            if (med != null)
            {
                return View(med);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            MedicationsModel med = _medications.GetMedicationsById(id);
            if (med != null)
            {
                return View(med);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(MedicationsModel model)
        {
            //if (ModelState.IsValid)
            //{
            var medications = new medications()
            {
                brand = model.brand,
                generic = model.generic,
                classification = model.classification
            };
            medications.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _medications.UpdateMedicationInfo(medications);
            // }
            return RedirectToAction("Index", "Medications");
        }

        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            bool status = _medications.DeleteMedicationInfoById(id, userId);
            if (status)
            {
                return RedirectToAction("Index", "Medications");
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetFormsAll()
        {
            var result = _medications.GetFormDosageAll();
            List<SelectListItem> forms = new();
            foreach (var item in result)
            {
                var data = new SelectListItem()
                {
                    Value = item.FormID,
                    Text = item.FormDesc
                };
                forms.Add(data);
            }
            return Json(forms);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDosage(dosages dosages, int id)
        {
            dosages.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            dosages.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            dosages.medication_id = id;
            var fromResult = _medications.GetFormDosageAll();
            var selectedForm = (from d in fromResult where d.FormID == dosages.form select d).FirstOrDefault();
            if (selectedForm != null)
            {
                dosages.form = selectedForm.FormDesc;
            }
            _medications.SaveDosageInfo(dosages);
            return Redirect("~/Medications/view/" + id + "/#dosages");

        }
    }
}
