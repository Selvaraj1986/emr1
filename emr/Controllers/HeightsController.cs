using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class HeightsController : Controller
    {
        private readonly IHeight _height;
        private readonly IPatients _patients;
        public HeightsController(IHeight height, IPatients patients)
        {
            _height = height;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.heightsModel = _height.GetHeightById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(HeightsModel heightsModel)
        {
            heightsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _height.UpdateHeightInfo(heightsModel);
            return Redirect("~/Patients/View/" + pId + "/#heights");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _height.DeleteHeightById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#heights");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

