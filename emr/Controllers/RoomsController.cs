using emr.Models.Model;
using emr.Models;
using emr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace emr.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRooms _rooms;
        private readonly IPatients _patients;
        public RoomsController(IRooms rooms, IPatients patients)
        {
            _rooms = rooms;
            _patients = patients;
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            model.roomsModel = _rooms.GetRoomsById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(RoomsModel roomsModel)
        {
            roomsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _rooms.UpdateRoomsInfo(roomsModel);
            return Redirect("~/Patients/View/" + pId + "/#rooms");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _rooms.DeleteRoomsById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#rooms");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

