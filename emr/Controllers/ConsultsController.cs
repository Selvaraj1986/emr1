using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class ConsultsController : Controller
    {
        private readonly IConsults _consults;
        private readonly IPatients _patients;
        public ConsultsController(IConsults consults, IPatients patients)
        {
            _consults = consults;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.consultsModel = _consults.GetConsultsById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ConsultsModel consultsModel)
        {
            consultsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _consults.UpdateConsultsInfo(consultsModel);
            return Redirect("~/Patients/View/" + pId + "/#consults");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _consults.DeleteConsultsById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#consults");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

