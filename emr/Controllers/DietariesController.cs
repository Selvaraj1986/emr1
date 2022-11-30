using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class DietariesController : Controller
    {
        private readonly IDietaries _dietaries;
        private readonly IPatients _patients;
        public DietariesController(IDietaries dietaries, IPatients patients)
        {
            _dietaries = dietaries;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.dietariesModel = _dietaries.GetDietariesById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(DietariesModel dietariesModel)
        {
            dietariesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _dietaries.UpdateDietariesInfo(dietariesModel);
            return Redirect("~/Patients/View/" + pId + "/#dietaries");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _dietaries.DeleteDietariesById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#dietaries");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

