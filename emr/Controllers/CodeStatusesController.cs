using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class CodeStatusesController : Controller
    {
        private readonly ICodeStatuses _codeStatuses;
        private readonly IPatients _patients;
        public CodeStatusesController(ICodeStatuses codeStatuses, IPatients patients)
        {
            _codeStatuses = codeStatuses;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.codeStatusesModel = _codeStatuses.GetCodeStatusesById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CodeStatusesModel codeStatusesModel)
        {
            codeStatusesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _codeStatuses.UpdateCodeStatusesInfo(codeStatusesModel);
            return Redirect("~/Patients/View/" + pId + "/#code_statuses");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _codeStatuses.DeleteCodeStatusesById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#code_statuses");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}
