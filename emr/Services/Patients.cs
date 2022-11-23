﻿using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;
using System.Text.Json;
using MoreLinq;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using LtiLibrary.NetCore.Lis.v2;
using System.Diagnostics;

namespace emr.Services
{
    public class Patients : IPatients
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Patients(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// store the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SavepatientsInfo(patients patients)
        {
            int returnID = 0;
            try
            {
                _dbContext.Add(patients);
                _dbContext.SaveChanges();
                returnID = patients.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// update the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatepatientsInfo(PatientsModel model)
        {
            int returnID = 0;
            try
            {
                var patients = new patients();
                patients = _dbContext.patients.Where(id => id.id == model.id).FirstOrDefault();
                if (patients != null)
                {
                    patients.medical_number = model.medical_number;
                    patients.first_name = model.first_name;
                    patients.last_name = model.last_name;
                    patients.gender = model.gender;
                    patients.dob = model.dob;
                    patients.provider_id = model.provider_id;
                    patients.description = model.description;
                    patients.modified = DateTime.Now;
                    patients.modifier_id = model.modifier_id;
                }
                _dbContext.Update(patients);
                _dbContext.SaveChanges();
                returnID = patients.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }

        /// <summary>
        /// get medications info 
        /// </summary>
        /// <returns></returns>
        public object GetpatientsAll(int courseId)
        {
            var result = new List<PatientsModel>();
            try
            {
                result = (from p in _dbContext.patients
                          join pr in _dbContext.providers on p.provider_id equals pr.id into pp
                          from pr in pp
                          where p.course_id == courseId && p.active == true
                          orderby p.last_name ascending
                          select new PatientsModel
                          {
                              id = p.id,
                              medical_number = p.medical_number,
                              first_name = p.first_name,
                              last_name = p.last_name,
                              gender = p.gender,
                              dob = p.dob,
                              provider_id = p.provider_id,
                              provider = pr.name,
                              locks = p.locks,
                              description = p.description
                          }).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// get dosage forms info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<GenderModel> GetGender()
        {
            var froms = new List<GenderModel>();
            try
            {
                var formDatas = System.IO.File.ReadAllText(@"./assets/gender.json");
                froms = JsonSerializer.Deserialize<List<GenderModel>>(formDatas);
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }

            return froms;
        }
        /// <summary>
        /// save patients
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void PatientsInsertDefault(int courseId)
        {
            try
            {
                var data = new List<PatientsModel>();
                var formDatas = System.IO.File.ReadAllText(@"./assets/PatientsInsertDefault.json");
                data = JsonSerializer.Deserialize<List<PatientsModel>>(formDatas);
                foreach (var item in data)
                {
                    int providerId = 0;
                    var pId = (from p in _dbContext.providers where p.name == item.provider select p).FirstOrDefault();
                    if (pId != null)
                    {
                        providerId = pId.id;
                    }
                    patients patients = new patients()
                    {
                        created = DateTime.Now,
                        modified = DateTime.Now,
                        creator_id = 1,
                        modifier_id = 1,
                        medical_number = item.medical_number,
                        first_name = item.first_name,
                        last_name = item.last_name,
                        gender = item.gender,
                        dob = item.dob,
                        provider_id = providerId,
                        course_id = courseId,
                        active = true
                    };
                    _dbContext.Add(patients);
                    _dbContext.SaveChanges();
                }
                this.GetOptionsInfo(courseId);


            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }
        }
        /// <summary>
        /// get options info 
        /// </summary>
        /// <returns></returns>
        public void GetOptionsInfo(int courseId)
        {
            var options = new List<options>();

            Random gen = new Random();
            var myDate = DateTime.Now;
            var begin = myDate.AddYears(-80);
            var end = myDate.AddDays(-1);
            DateTime start = new DateTime(begin.Year, 1, 1);
            int range = (end - start).Days;
            options = (from o in _dbContext.options select o).ToList();
            var providers = (from p in _dbContext.providers where p.course_id == courseId && p.active == true && p.name.Contains("M.D.") select p).ToList();
            var lastNames = (from o in options where o.group_name == "last_name" select new { name = o.name }).ToList();
            var firstNames = (from o in options where o.group_name.StartsWith("first_name") select new { group_name = o.group_name, name = o.name, description = o.description }).ToList();
            lastNames.Shuffle();
            firstNames.Shuffle();
            providers.Shuffle();
            int l_count = lastNames.Count();
            int f_count = firstNames.Count();
            int p_count = providers.Count();

            int i = 0;
            int count = Convert.ToInt16(_iconfiguration["Patient.init_count"]);
            while (i < count)
            {
                var fname = firstNames[i % f_count];
                var lname = lastNames[i % l_count];
                var dob = start.AddDays(gen.Next(range)).ToString("yyyy-MM-dd");
                var gender = "M";
                if (fname.group_name == "first_name_f")
                {
                    gender = "F";
                }
                string random = string.Join("", Guid.NewGuid().ToString().Take(8).Select(o => o));
                var medicalRecord = random.ToUpper();
                var pId = 0;
                if (providers != null)
                {
                    pId = providers[i++ % p_count].id;
                }

                patients patients = new patients()
                {
                    created = DateTime.Now,
                    modified = DateTime.Now,
                    creator_id = 1,
                    modifier_id = 1,
                    medical_number = medicalRecord,
                    first_name = fname.name,
                    last_name = lname.name,
                    gender = gender,
                    dob = dob,
                    provider_id = pId,
                    course_id = courseId,
                    active = true
                };
                _dbContext.Add(patients);
                i++;
            }
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// update the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void ToggleLock(int id)
        {
            try
            {
                var patients = new patients();
                patients = (from p in _dbContext.patients where p.id == id select p).FirstOrDefault();
                if (patients != null)
                {
                    patients.locks = (patients.locks == false) ? true : false;
                }
                // _dbContext.Update(patients);
                // _dbContext.patients.Where(x => x.id== id).up(x => new patients { locks = false });
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }

        }
        /// <summary>
        /// get medications by id
        /// </summary>
        /// <returns></returns>
        public PatientsModel GetpatientsById(int id)
        {
            var result = new PatientsModel();
            try
            {
                result = (from p in _dbContext.patients
                          join pr in _dbContext.providers on p.provider_id equals pr.id into pp
                          from pr in pp
                          where p.id == id && p.active == true
                          orderby p.last_name ascending
                          select new PatientsModel
                          {
                              id = p.id,
                              created = p.created,
                              modified = p.modified,
                              creator_id = p.creator_id,
                              modifier_id = p.modifier_id,
                              medical_number = p.medical_number,
                              first_name = p.first_name,
                              last_name = p.last_name,
                              gender = p.gender,
                              dob = p.dob,
                              provider_id = p.provider_id,
                              provider = pr.name,
                              locks = p.locks,
                              description = p.description
                          }).FirstOrDefault();

                var peopleCreated = (from p in _dbContext.people where p.id == result.creator_id select p).FirstOrDefault();
                var peopleModified = (from p in _dbContext.people where p.id == result.modifier_id select p).FirstOrDefault();
                if (peopleCreated != null)
                {
                    result.creator = peopleCreated.username;
                }
                if (peopleModified != null)
                {
                    result.modifier = peopleModified.username;

                }
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// delete a Patient info 
        /// </summary>
        /// <returns></returns>
        public int DeletePatientsById(int id, int userId)
        {
            try
            {
                var model = (from m in _dbContext.patients
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return id;
        }
        /// <summary>
        /// store the diagnoses info 
        /// </summary>
        /// <param name="diagnoses"></param>
        /// <returns></returns>
        public int SaveDiangnosesInfo(AdmittingDiagnosesModel model)
        {
            int returnID = 0;
            try
            {
                var diagnosis = new admitting_diagnoses();
                diagnosis.patient_id = model.patient_id;
                diagnosis.creator_id = model.creator_id;
                diagnosis.created = DateTime.Now;
                diagnosis.modifier_id = model.modifier_id;
                diagnosis.modified = DateTime.Now;
                diagnosis.record_date = model.record_date;
                diagnosis.diagnosis = model.diagnosis;
                diagnosis.active = true;
                _dbContext.Add(diagnosis);
                _dbContext.SaveChanges();
                returnID = diagnosis.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Diagnosis info 
        /// </summary>
        /// <returns></returns>
        public List<AdmittingDiagnosesModel> GetDiagnosisAll(int id)
        {
            var result = new List<AdmittingDiagnosesModel>();
            try
            {
                result = (from d in _dbContext.admitting_diagnoses
                          where d.active == true && d.patient_id == id
                          select new AdmittingDiagnosesModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              diagnosis = d.diagnosis,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified

                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// store the code status info 
        /// </summary>
        /// <param name="diagnoses"></param>
        /// <returns></returns>
        public int SaveCodeStatusInfo(CodeStatusesModel model)
        {
            int returnID = 0;
            try
            {
                var code = new code_statuses();
                code.patient_id = model.patient_id;
                code.creator_id = model.creator_id;
                code.created = DateTime.Now;
                code.modifier_id = model.modifier_id;
                code.modified = DateTime.Now;
                code.record_date = model.record_date;
                code.code_status = model.code_status;
                code.notes = model.notes;
                code.active = true;
                _dbContext.Add(code);
                _dbContext.SaveChanges();
                returnID = code.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get CodeStatu info 
        /// </summary>
        /// <returns></returns>
        public List<CodeStatusesModel> GetCodeStatusAll(int id)
        {
            var result = new List<CodeStatusesModel>();
            try
            {
                result = (from d in _dbContext.code_statuses
                          where d.active == true && d.patient_id == id
                          select new CodeStatusesModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              code_status = d.code_status,
                              notes = d.notes,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the Precautions info 
        /// </summary>
        /// <param name="diagnoses"></param>
        /// <returns></returns>
        public int SavePrecautionsInfo(PrecautionsModel model)
        {
            int returnID = 0;
            try
            {
                var code = new precautions();
                code.patient_id = model.patient_id;
                code.creator_id = model.creator_id;
                code.created = DateTime.Now;
                code.modifier_id = model.modifier_id;
                code.modified = DateTime.Now;
                code.record_date = model.record_date;
                code.status = model.status;
                code.notes = model.notes;
                code.active = true;
                _dbContext.Add(code);
                _dbContext.SaveChanges();
                returnID = code.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Precautions info 
        /// </summary>
        /// <returns></returns>
        public List<PrecautionsModel> GetPrecautionsAll(int id)
        {
            var result = new List<PrecautionsModel>();
            try
            {
                result = (from d in _dbContext.precautions
                          where d.active == true && d.patient_id == id
                          select new PrecautionsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              status = d.status,
                              notes = d.notes,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the Allergy info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveAllergyInfo(AllergyModel model)
        {
            int returnID = 0;
            try
            {
                var code = new allergies();
                code.patient_id = model.patient_id;
                code.creator_id = model.creator_id;
                code.created = DateTime.Now;
                code.modifier_id = model.modifier_id;
                code.modified = DateTime.Now;
                code.record_date = model.record_date;
                code.allergy_type = model.allergy_type;
                code.allergy = model.allergy;
                code.active = true;
                _dbContext.Add(code);
                _dbContext.SaveChanges();
                returnID = code.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Allergy info 
        /// </summary>
        /// <returns></returns>
        public List<AllergyModel> GetAllergyAll(int id)
        {
            var result = new List<AllergyModel>();
            try
            {
                result = (from d in _dbContext.allergies
                          where d.active == true && d.patient_id == id
                          select new AllergyModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              allergy_type = d.allergy_type,
                              allergy = d.allergy,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the PatientMedications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SavePatientMedicationsInfo(PatientMedicationsModel model)
        {
            int returnID = 0;
            try
            {
                var medications = new patient_medications();
                medications.patient_id = model.patient_id;
                medications.creator_id = model.creator_id;
                medications.created = DateTime.Now;
                medications.modifier_id = model.modifier_id;
                medications.modified = DateTime.Now;
                medications.record_date = model.record_date;
                medications.medication = model.medication;
                medications.dose = model.dose;
                medications.frequency = model.frequency;
                medications.taken_today = model.taken_today;
                medications.brought_with = model.brought_with;
                medications.active = true;
                _dbContext.Add(medications);
                _dbContext.SaveChanges();
                returnID = medications.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get PatientMedications info 
        /// </summary>
        /// <returns></returns>
        public List<PatientMedicationsModel> GetPatientMedicationsAll(int id)
        {
            var result = new List<PatientMedicationsModel>();
            try
            {
                result = (from d in _dbContext.patient_medications
                          where d.active == true && d.patient_id == id
                          select new PatientMedicationsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              medication = d.medication,
                              dose = d.dose,
                              frequency = d.frequency,
                              taken_today = (d.taken_today == null ? false : d.taken_today),
                              brought_with = (d.brought_with == null ? false : d.brought_with),
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the Rooms info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveRoomsInfo(RoomsModel model)
        {
            int returnID = 0;
            try
            {
                var medications = new rooms();
                medications.patient_id = model.patient_id;
                medications.creator_id = model.creator_id;
                medications.created = DateTime.Now;
                medications.modifier_id = model.modifier_id;
                medications.modified = DateTime.Now;
                medications.record_date = model.record_date;
                medications.room = model.room;
                medications.notes = model.notes;
                medications.active = true;
                _dbContext.Add(medications);
                _dbContext.SaveChanges();
                returnID = medications.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Rooms info 
        /// </summary>
        /// <returns></returns>
        public List<RoomsModel> GetRoomsAll(int id)
        {
            var result = new List<RoomsModel>();
            try
            {
                result = (from d in _dbContext.rooms
                          where d.active == true && d.patient_id == id
                          select new RoomsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              room = d.room,
                              notes = d.notes,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the Height info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveHeightInfo(HeightsModel model)
        {
            int returnID = 0;
            try
            {
                var medications = new heights();
                medications.patient_id = model.patient_id;
                medications.creator_id = model.creator_id;
                medications.created = DateTime.Now;
                medications.modifier_id = model.modifier_id;
                medications.modified = DateTime.Now;
                medications.record_date = model.record_date;
                medications.height = model.height;
                medications.height_unit = model.height_unit;
                medications.active = true;
                _dbContext.Add(medications);
                _dbContext.SaveChanges();
                returnID = medications.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Height info 
        /// </summary>
        /// <returns></returns>
        public List<HeightsModel> GetHeightAll(int id)
        {
            var result = new List<HeightsModel>();
            try
            {
                result = (from d in _dbContext.heights
                          where d.active == true && d.patient_id == id
                          select new HeightsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              height = d.height,
                              height_unit = d.height_unit,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the Weights info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveWeightsInfo(WeightsModel model)
        {
            int returnID = 0;
            try
            {
                var weights = new weights();
                weights.patient_id = model.patient_id;
                weights.creator_id = model.creator_id;
                weights.created = DateTime.Now;
                weights.modifier_id = model.modifier_id;
                weights.modified = DateTime.Now;
                weights.record_date = model.record_date;
                weights.weight = model.weight;
                weights.weight_unit = model.weight_unit;
                weights.active = true;
                _dbContext.Add(weights);
                _dbContext.SaveChanges();
                returnID = weights.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Weights info 
        /// </summary>
        /// <returns></returns>
        public List<WeightsModel> GetWeightsAll(int id)
        {
            var result = new List<WeightsModel>();
            try
            {
                result = (from d in _dbContext.weights
                          where d.active == true && d.patient_id == id
                          select new WeightsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              weight = d.weight,
                              weight_unit = d.weight_unit,
                              created = d.created,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// store the DailyActivities info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDailyActivitiesInfo(DailyActivitiesModel model)
        {
            int returnID = 0;
            try
            {
                var activity = new daily_activities();
                activity.record_date = model.record_date;
                activity.patient_id = model.patient_id;
                activity.creator_id = model.creator_id;
                activity.created = DateTime.Now;
                activity.modifier_id = model.modifier_id;
                activity.modified = DateTime.Now;
              
                activity.bathing = model.bathing;
                activity.dressing = model.dressing;
                activity.grooming = model.grooming;
                activity.oral_care = model.oral_care;
                activity.toileting = model.toileting;
                activity.transferring = model.transferring;
                activity.walking = model.walking;
                activity.eating = model.eating;
                activity.notes = model.notes;
                activity.active = true;
                _dbContext.Add(activity);
                _dbContext.SaveChanges();
                returnID = activity.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get DailyActivities info 
        /// </summary>
        /// <returns></returns>
        public List<DailyActivitiesModel> GetDailyActivitiesAll(int id)
        {
            var result = new List<DailyActivitiesModel>();
            try
            {
                result = (from d in _dbContext.daily_activities
                          where d.active == true && d.patient_id == id
                          select new DailyActivitiesModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              bathing = d.bathing,
                              dressing = d.dressing,
                              grooming = d.grooming,
                              oral_care = d.oral_care,
                              toileting = d.toileting,
                              transferring = d.transferring,
                              walking = d.walking,
                              eating = d.eating,
                              notes = d.notes,
                              creator = (from p in _dbContext.people where p.id == d.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.created,
                              modifier = (from p in _dbContext.people where p.id == d.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + d.modified
                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
    }
}
