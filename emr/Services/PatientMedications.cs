using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class PatientMedications : IPatientMedications
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public PatientMedications(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get PatientMedications by id info 
        /// </summary>
        /// <returns></returns>
        public PatientMedicationsModel GetPatientMedicationsById(int id)
        {
            var result = new PatientMedicationsModel();
            try
            {
                result = (from d in _dbContext.patient_medications
                          where d.id == id
                          select new PatientMedicationsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              medication = d.medication,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              dose = d.dose,
                              frequency=d.frequency,
                              taken_today= (d.taken_today == null ? false : d.taken_today),
                              brought_with= (d.brought_with == null ? false : d.brought_with),
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
        /// update the PatientMedications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePatientMedicationsInfo(PatientMedicationsModel model)
        {
            int returnID = 0;
            try
            {
                var medication = new patient_medications();
                medication = _dbContext.patient_medications.Where(id => id.id == model.id).FirstOrDefault();
                if (medication != null)
                {
                    medication.record_date = model.record_date;
                    medication.medication = model.medication;
                    medication.dose = model.dose;
                    medication.frequency = model.frequency;
                    medication.taken_today= model.taken_today;
                    medication.brought_with= model.brought_with;
                    medication.modifier_id = model.modifier_id;
                    medication.modified = DateTime.Now;
                }
                _dbContext.Update(medication);
                _dbContext.SaveChanges();
                returnID = medication.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a PatientMedications info 
        /// </summary>
        /// <returns></returns>
        public PatientMedicationsModel DeletePatientMedicationsById(int id, int userId)
        {
            var status = new PatientMedicationsModel();
            try
            {
                var model = (from m in _dbContext.patient_medications
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                status.patient_id = model.patient_id;
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return status;


        }
    }
}

