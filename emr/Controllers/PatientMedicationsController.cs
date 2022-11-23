using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class PatientMedicationsController : Controller
    {
        private readonly IPatientMedications _patientMedications;
        private readonly IPatients _patients;
        public PatientMedicationsController(IPatientMedications patientMedications, IPatients patients)
        {
            _patientMedications = patientMedications;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.patientMedicationsModel = _patientMedications.GetPatientMedicationsById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PatientMedicationsModel patientMedicationsModel)
        {
            patientMedicationsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _patientMedications.UpdatePatientMedicationsInfo(patientMedicationsModel);
            return Redirect("~/Patients/View/" + pId + "/#medications");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _patientMedications.DeletePatientMedicationsById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#medications");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

