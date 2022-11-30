using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;

namespace emr.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotes _notes;
        private readonly IPatients _patients;
        public NotesController(INotes notes, IPatients patients)
        {
            _notes = notes;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.notesModel = _notes.GetNotesById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(NotesModel notesModel)
        {
            notesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _notes.UpdateNotesInfo(notesModel);
            return Redirect("~/Patients/View/" + pId + "/#notes");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _notes.DeleteNotesById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#notes");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

