using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class PrecautionsController : Controller
    {
        private readonly IPrecautions _precautions ;
        private readonly IPatients _patients;
        public PrecautionsController(IPrecautions precautions, IPatients patients)
        {
            _precautions = precautions;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.precautionsModel = _precautions.GetPrecautionsById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PrecautionsModel precautionsModel)
        {
            precautionsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _precautions.UpdatePrecautionsInfo(precautionsModel);
            return Redirect("~/Patients/View/" + pId + "/#precautions");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _precautions.DeletePrecautionsById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#precautions");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}
