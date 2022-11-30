using emr.Models;
using emr.Models.Model;
using emr.Services;
using emr.Support;
using Microsoft.AspNetCore.Mvc;
namespace emr.Controllers
{
    public class AdmissionController : Controller
    {
        private readonly IAdmission _admission;
        private readonly IPatients _patients;
        private readonly IAllergy _allergy;
        private readonly IPatientMedications _patientMedications;
        public AdmissionController(IAdmission admission, IPatients patients, IAllergy allergy, IPatientMedications patientMedications)
        {
            _admission = admission;
            _patients = patients;
            _allergy = allergy;
            _patientMedications = patientMedications;
        }
        public IActionResult Index()
        {
            int pId = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            var result = _admission.GetpatientsById(pId);
            result.admissionsModelAll = _admission.GetAdmissionAll(pId);
            return View(result);
        }

        public IActionResult Add()
        {
            int pId = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            var result = _admission.GetpatientsById(pId);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AdmissionsModel admissionsModel)
        {
            admissionsModel.patient_id = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            admissionsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            admissionsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _admission.SaveAdmissionInfo(admissionsModel);
            return RedirectToAction("Index", "Admission");
        }
        public IActionResult View(int id)
        {
            HttpContext.Session.SetString("aId", id.ToString());
            int pId = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            PatientsModel model = _admission.GetpatientsById(pId);
            model.admissionsModel = _admission.ViewAdmissionById(id);
            model.allergyModelAll = _patients.GetAllergyAll(pId);
            model.patientMedicationsModel = new PatientMedicationsModel();
            model.patientMedicationsModel.record_date = Helpers.GetTodayDate();
            model.patientMedicationsModel.brought_with = false;
            model.patientMedicationsModel.taken_today = false;
            model.patientMedicationsModelAll = _patients.GetPatientMedicationsAll(pId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PatientsModel model = _admission.GetpatientsById(id);
            model.admissionsModel = _admission.GetAdmissionById(id);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AdmissionsModel admissionsModel)
        {
            admissionsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var aId = _admission.UpdateAdmissionInfo(admissionsModel);
            return Redirect("~/Admission/View/" + aId + "/#home");
        }

        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _admission.DeleteAdmissionById(id, userId);
            return RedirectToAction("Index", "Admission");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAllergy(AllergyModel allergyModel, int id)
        {
            int aId = Convert.ToInt32(HttpContext.Session.GetString("aId"));
            allergyModel.patient_id = id;
            allergyModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            allergyModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveAllergyInfo(allergyModel);
            return Redirect("~/Admission/view/" + aId + "/#allergies");
        }
        public IActionResult DeleteAllergy(int id)
        {
            int aId = Convert.ToInt32(HttpContext.Session.GetString("aId"));
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _allergy.DeleteAllergyById(id, userId);
            return Redirect("~/Admission/view/" + aId + "/#allergies");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatientMedications(PatientMedicationsModel patientMedicationsModel, int id)
        {
            int aId = Convert.ToInt32(HttpContext.Session.GetString("aId"));
            patientMedicationsModel.patient_id = id;
            patientMedicationsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            patientMedicationsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SavePatientMedicationsInfo(patientMedicationsModel);
            return Redirect("~/Admission/view/" + aId + "/#medications");
        }
        public IActionResult DeletePatientMedications(int id)
        {
            int aId = Convert.ToInt32(HttpContext.Session.GetString("aId"));
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _patientMedications.DeletePatientMedicationsById(id, userId);
            return Redirect("~/Admission/view/" + aId + "/#medications");
        }
    }
}
