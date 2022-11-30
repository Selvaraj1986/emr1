using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Support;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace emr.Services
{
    public class Medications : IMedications
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Medications(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// store the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveMedicationInfo(medications model)
        {
            int returnID = 0;
            try
            {
                var medications = new medications()
                {
                    created = DateTime.Now,
                    creator_id = model.creator_id,
                    modified = DateTime.Now,
                    modifier_id = model.modifier_id,
                    generic = model.generic,
                    brand = model.brand,
                    classification = model.classification,
                    active = true
                };
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
        /// update the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMedicationInfo(medications model)
        {
            int returnID = 0;
            try
            {
                var medications = new medications()
                {
                    id = model.id,
                    created = model.created,
                    creator_id = model.creator_id,
                    modified = DateTime.Now,
                    modifier_id = model.modifier_id,
                    generic = model.generic,
                    brand = model.brand,
                    classification = model.classification,
                    active = true
                };
                _dbContext.Update(medications);
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
        /// get medications info 
        /// </summary>
        /// <returns></returns>
        public object GetMedicationsAll()
        {
            var result = new object();
            try
            {
                result = (from m in _dbContext.medications
                          where m.active == true
                          select new MedicationsModel
                          {
                              id = m.id,
                              generic = m.generic.ToCleanString(),
                              brand = m.brand.ToCleanString(),
                              classification = m.classification.ToCleanString(),
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
        /// get medications info 
        /// </summary>
        /// <returns></returns>
        public MedicationsModel GetMedicationsById(int id)
        {
            var result = new MedicationsModel();
            try
            {
                result = (from m in _dbContext.medications
                          where m.id == id
                          select new MedicationsModel
                          {
                              id = m.id,
                              created = Helpers.ToDateTimeFormat(m.created),
                              creator_id = m.creator_id,
                              modified = Helpers.ToDateTimeFormat(m.modified),
                              modifier_id = m.modifier_id,
                              generic = m.generic,
                              brand = m.brand,
                              classification = m.classification,
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
        /// delete a medications info 
        /// </summary>
        /// <returns></returns>
        public bool DeleteMedicationInfoById(int id, int userId)
        {
            bool status = false;
            try
            {
                var model = (from m in _dbContext.medications
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return status;


        }
        /// <summary>
        /// get dosage forms info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<FormDosageModel> GetFormDosageAll()
        {
            var froms = new List<FormDosageModel>();
            try
            {
                var formDatas = System.IO.File.ReadAllText(@"./assets/form_dosage.json");
                froms = JsonSerializer.Deserialize<List<FormDosageModel>>(formDatas);
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }

            return froms;
        }
        /// <summary>
        /// store the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDosageInfo(dosages model)
        {
            int returnID = 0;
            try
            {
                var medications = new dosages()
                {
                    created = DateTime.Now,
                    creator_id = model.creator_id,
                    modified = DateTime.Now,
                    modifier_id = model.modifier_id,
                    name = model.name.ToCleanString(),
                    ingredient = model.ingredient.ToCleanString(),
                    form = model.form.ToCleanString(),
                    dosage = model.dosage.ToCleanString(),
                    medication_id = model.medication_id,
                    active = true
                };
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
        /// get dosage info 
        /// </summary>
        /// <returns></returns>
        public List<DosagesModel> GetDosageAll(int id)
        {
            var result = new List<DosagesModel>();
            try
            {
                result = (from d in _dbContext.dosages
                          where d.active == true && d.medication_id == id
                          select new DosagesModel
                          {
                              id = d.id,
                              created = d.created,
                              creator_id = d.creator_id,
                              modified = d.modified,
                              modifier_id = d.modifier_id,
                              medication_id = d.medication_id,
                              name = d.name.ToCleanString(),
                              ingredient = d.ingredient.ToCleanString(),
                              form = d.form.ToCleanString(),
                              dosage = d.dosage.ToCleanString()
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
