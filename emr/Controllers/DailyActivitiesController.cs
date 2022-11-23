using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class DailyActivitiesController : Controller
    {
        private readonly IDailyActivities _dailyActivities;
        private readonly IPatients _patients;
        public DailyActivitiesController(IDailyActivities dailyActivities, IPatients patients)
        {
            _dailyActivities = dailyActivities;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.dailyActivitiesModel = _dailyActivities.GetDailyActivitiesById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(DailyActivitiesModel dailyActivitiesModel)
        {
            dailyActivitiesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _dailyActivities.UpdateDailyActivitiesInfo(dailyActivitiesModel);
            return Redirect("~/Patients/View/" + pId + "/#daily_activities");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _dailyActivities.DeleteDailyActivitiesById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#daily_activities");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

