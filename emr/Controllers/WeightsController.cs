using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class WeightsController : Controller
    {
        private readonly IWeights _weights;
        private readonly IPatients _patients;
        public WeightsController(IWeights weights, IPatients patients)
        {
            _weights = weights;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.weightsModel = _weights.GetWeightsById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(WeightsModel weightsModel)
        {
            weightsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _weights.UpdateWeightsInfo(weightsModel);
            return Redirect("~/Patients/View/" + pId + "/#weights");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _weights.DeleteWeightsById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#weights");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

