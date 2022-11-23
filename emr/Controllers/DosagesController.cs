using emr.Models;
using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace emr.Controllers
{
    public class DosagesController : Controller
    {
        private readonly IDosages _dosages;
        private readonly IMedications _medications;
        public DosagesController(IDosages dosages, IMedications medications)
        {
            _dosages = dosages;
            _medications = medications;
        }
        public IActionResult Edit(int id)
        {
            var result = _medications.GetFormDosageAll();
            List<SelectListItem> forms = new();
            foreach (var item in result)
            {
                var data = new SelectListItem()
                {
                    Value = item.FormDesc,
                    Text = item.FormDesc
                };
                forms.Add(data);
            }
            ViewBag.forms = forms;
            var dosage = _dosages.GetDosageById(id);
            if (dosage != null)
            {
                return View(dosage);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(dosages dosages)
        {
            int? id = dosages.medication_id;
            if (ModelState.IsValid)
            {
                dosages.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                _dosages.UpdateDosagesInfo(dosages);
            }
            return Redirect("~/Medications/view/" + id + "/#dosages");

            // return RedirectToAction("View", "Medications", new { id });
        }

        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _dosages.DeleteDosagesById(id, userId);
            if (status != null)
            {
                return Redirect("~/Medications/view/" + status.medication_id + "/#dosages");

            }
            return RedirectToAction("View", "Medications", new { id });
        }
    }
}
