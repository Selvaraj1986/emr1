using emr.Models;
using emr.Models.Model;
using emr.Services;
using emr.Support;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class ObstetricsController : Controller
    {
        private readonly IObAdmission _obAdmission;
        private readonly IAdmission _admit;
        private readonly IPatients _patients;
        private readonly IAllergy _allergy;
        private readonly IPatientMedications _patientMedications;
        public ObstetricsController(IObAdmission obAdmission, IAdmission admit, IPatients patients, IAllergy allergy, IPatientMedications patientMedications)
        {
            _obAdmission = obAdmission;
            _patients = patients;
            _allergy = allergy;
            _admit = admit;
            _patientMedications = patientMedications;
        }
        public IActionResult Index()
        {
            int pId = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            var result = _admit.GetpatientsById(pId);
            result.obAdmissionsModelAll = _obAdmission.GetobAdmissionsAll(pId);
            return View(result);
        }

        // GET: ObAdmissionsController/Details/5
        public ActionResult View(int id)
        {
            HttpContext.Session.SetString("aId", id.ToString());
            int pId = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            PatientsModel model = _admit.GetpatientsById(pId);
            model.obAdmissionsModel = _obAdmission.ViewobAdmissionsById(id);

            return View(model);

        }
        public ActionResult Add()
        {
            int pId = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            var result = _admit.GetpatientsById(pId);
            return View(result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ObAdmissionsModel obAdmissionsModel)
        {
            obAdmissionsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            obAdmissionsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            obAdmissionsModel.patient_id = Convert.ToInt32(HttpContext.Session.GetString("pId"));
            _obAdmission.SaveobAdmissionInfo(obAdmissionsModel);
            return RedirectToAction("Index", "Obstetrics");
        }

        public ActionResult Edit(int id)
        {
            PatientsModel model = _admit.GetpatientsById(id);
            model.obAdmissionsModel = _obAdmission.GetobAdmissionsById(id);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ObAdmissionsModel obAdmissionsModel)
        {
            obAdmissionsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var aId = _obAdmission.UpdateobAdmissionInfo(obAdmissionsModel);
            return Redirect("~/Obstetrics/View/" + aId + "/#home");

        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _obAdmission.DeleteObAdmissionById(id, userId);
            return RedirectToAction("Index", "Obstetrics");
        }
    }
}
