using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class AllergyController : Controller
    {
        private readonly IAllergy _allergy;
        private readonly IPatients _patients;
        public AllergyController(IAllergy allergy, IPatients patients)
        {
            _allergy = allergy;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.allergyModel = _allergy.GetAllergyById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AllergyModel allergyModel)
        {
            allergyModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _allergy.UpdateAllergyInfo(allergyModel);
            return Redirect("~/Patients/View/" + pId + "/#allergies");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _allergy.DeleteAllergyById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#allergies");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}
