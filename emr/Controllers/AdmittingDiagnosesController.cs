using emr.Models;
using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace emr.Controllers
{
    public class AdmittingDiagnosesController : Controller
    {
        private readonly IAdmittingDiagnoses _diagnoses;
        private readonly IPatients _patients;
        public AdmittingDiagnosesController(IAdmittingDiagnoses diagnoses, IPatients patients)
        {
            _diagnoses = diagnoses;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.admittingDiagnosesModel = _diagnoses.GetDiagnosisById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AdmittingDiagnosesModel admittingDiagnosesModel)
        {
            admittingDiagnosesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _diagnoses.UpdateDiagnosisInfo(admittingDiagnosesModel);
            return Redirect("~/Patients/View/" + pId + "/#diagnoses");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _diagnoses.DeleteDiagnosisById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#diagnoses");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}
