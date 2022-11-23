﻿using emr.Models;
using emr.Models.Model;
using emr.Services;
using LtiLibrary.NetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Security.Principal;

namespace emr.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatients _patients;
        private readonly IProviders _providers;
        public PatientsController(IPatients patients, IProviders providers)
        {
            _patients = patients;
            _providers = providers;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult togglelock(int id)
        {
            _patients.ToggleLock(id);
            return RedirectToAction("Index", "Patients");
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            int courseId = Convert.ToInt32(HttpContext.Session.GetString("courseId"));
            var result = _patients.GetpatientsAll(courseId);
            return Json(result);
        }

        public IActionResult Add()
        {
            int courseId = Convert.ToInt32(HttpContext.Session.GetString("courseId"));
            var providers = _providers.GetProviders(courseId);
            List<SelectListItem> provider = new();
            foreach (var item in providers)
            {
                var data = new SelectListItem()
                {
                    Value = item.id.ToString(),
                    Text = item.name
                };
                provider.Add(data);
            }
            ViewBag.provider = provider;
            var gender = _patients.GetGender();
            List<SelectListItem> sex = new();
            foreach (var item in gender)
            {
                var data = new SelectListItem()
                {
                    Value = item.genderId,
                    Text = item.genderName
                };
                sex.Add(data);
            }
            ViewBag.sex = sex;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(PatientsModel model)
        {
            var patients = new patients()
            {
                created = DateTime.Now,
                modified = DateTime.Now,
                medical_number = model.medical_number,
                first_name = model.first_name,
                last_name = model.last_name,
                gender = model.gender,
                dob = model.dob,
                provider_id = model.provider_id,
                description = model.description,
                active = true,
            };
            patients.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            patients.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            patients.course_id = Convert.ToInt32(HttpContext.Session.GetString("courseId"));

            _patients.SavepatientsInfo(patients);
            return RedirectToAction("Index", "Patients");
        }
        public IActionResult View(int id)
        {
            // var diagnosis = new List<AdmittingDiagnosesModel>();
            PatientsModel model = _patients.GetpatientsById(id);
            model.diagnosesModelAll = _patients.GetDiagnosisAll(id);
            model.codeStatusesModelAll = _patients.GetCodeStatusAll(id);
            model.precautionsModelAll = _patients.GetPrecautionsAll(id);
            model.allergyModelAll = _patients.GetAllergyAll(id);
            model.patientMedicationsModel = new PatientMedicationsModel();
            model.patientMedicationsModel.record_date = DateTime.Now;
            model.patientMedicationsModel.brought_with = false;
            model.patientMedicationsModel.taken_today = false;
            model.patientMedicationsModelAll = _patients.GetPatientMedicationsAll(id);
            model.roomsModelAll = _patients.GetRoomsAll(id);
            model.heightsModelAll = _patients.GetHeightAll(id);
            model.weightsModelAll = _patients.GetWeightsAll(id);
            model.dailyActivitiesModelAll = _patients.GetDailyActivitiesAll(id);
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            int courseId = Convert.ToInt32(HttpContext.Session.GetString("courseId"));
            var providers = _providers.GetProviders(courseId);
            List<SelectListItem> provider = new();
            foreach (var item in providers)
            {
                var data = new SelectListItem()
                {
                    Value = item.id.ToString(),
                    Text = item.name
                };
                provider.Add(data);
            }
            ViewBag.provider = provider;
            var gender = _patients.GetGender();
            List<SelectListItem> sex = new();
            foreach (var item in gender)
            {
                var data = new SelectListItem()
                {
                    Value = item.genderId,
                    Text = item.genderName
                };
                sex.Add(data);
            }
            ViewBag.sex = sex;

            var model = new PatientsModel();
            model = _patients.GetpatientsById(id);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PatientsModel model)
        {
            model.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.UpdatepatientsInfo(model);
            return RedirectToAction("Index", "Patients");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.DeletePatientsById(id, userId);

            return RedirectToAction("Index", "Patients");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDiagnoses(AdmittingDiagnosesModel admittingDiagnosesModel, int id)
        {
            admittingDiagnosesModel.patient_id = id;
            admittingDiagnosesModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            admittingDiagnosesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveDiangnosesInfo(admittingDiagnosesModel);
            return Redirect("~/Patients/view/" + id + "/#diagnoses");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCodeStatus(CodeStatusesModel codeStatusesModel, int id)
        {
            codeStatusesModel.patient_id = id;
            codeStatusesModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            codeStatusesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveCodeStatusInfo(codeStatusesModel);
            return Redirect("~/Patients/view/" + id + "/#code_statuses");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPrecaution(PrecautionsModel precautionsModel, int id)
        {
            precautionsModel.patient_id = id;
            precautionsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            precautionsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SavePrecautionsInfo(precautionsModel);
            return Redirect("~/Patients/view/" + id + "/#precautions");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAllergy(AllergyModel allergyModel, int id)
        {
            allergyModel.patient_id = id;
            allergyModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            allergyModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveAllergyInfo(allergyModel);
            return Redirect("~/Patients/view/" + id + "/#allergies");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatientMedications(PatientMedicationsModel patientMedicationsModel, int id)
        {
            patientMedicationsModel.patient_id = id;
            patientMedicationsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            patientMedicationsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SavePatientMedicationsInfo(patientMedicationsModel);
            return Redirect("~/Patients/view/" + id + "/#medications");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRooms(RoomsModel roomsModel, int id)
        {
            roomsModel.patient_id = id;
            roomsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            roomsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveRoomsInfo(roomsModel);
            return Redirect("~/Patients/view/" + id + "/#rooms");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddHights(HeightsModel heightsModel, int id)
        {
            heightsModel.patient_id = id;
            heightsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            heightsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveHeightInfo(heightsModel);
            return Redirect("~/Patients/view/" + id + "/#heights");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWeights(WeightsModel weightsModel, int id)
        {
            weightsModel.patient_id = id;
            weightsModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            weightsModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveWeightsInfo(weightsModel);
            return Redirect("~/Patients/view/" + id + "/#weights");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddActivities(DailyActivitiesModel dailyActivitiesModel, int id)
        {
            dailyActivitiesModel.patient_id = id;
            dailyActivitiesModel.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            dailyActivitiesModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _patients.SaveDailyActivitiesInfo(dailyActivitiesModel);
            return Redirect("~/Patients/view/" + id + "/#daily_activities");
        }
    }
}
