using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class TreatmentsController : Controller
    {
        private readonly ITreatments _treatments;
        private readonly IPatients _patients;
        public TreatmentsController(ITreatments treatments, IPatients patients)
        {
            _treatments = treatments;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.treatmentsModel = _treatments.GetTreatmentsById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TreatmentsModel treatmentsModel)
        {
            treatmentsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _treatments.UpdateTreatmentsInfo(treatmentsModel);
            return Redirect("~/Patients/View/" + pId + "/#treatments");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _treatments.DeleteTreatmentsById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#treatments");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

