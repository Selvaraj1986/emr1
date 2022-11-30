using emr.Models.DBContext;
using emr.Models;
using emr.Support;
using emr.Models.Model;
using System.Globalization;

namespace emr.Services
{
    public class AdmittingDiagnoses : IAdmittingDiagnoses
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public AdmittingDiagnoses(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Diagnosis by id info 
        /// </summary>
        /// <returns></returns>
        public AdmittingDiagnosesModel GetDiagnosisById(int id)
        {
            var result = new AdmittingDiagnosesModel();
            try
            {
                result = (from d in _dbContext.admitting_diagnoses
                          where d.id == id
                          select new AdmittingDiagnosesModel
                          {
                              id = d.id,
                              record_date = d.record_date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                              diagnosis = d.diagnosis,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id
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
        /// update the Diagnosis info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDiagnosisInfo(AdmittingDiagnosesModel model)
        {
            int returnID = 0;
            try
            {
                var diagnoses = new admitting_diagnoses();
                diagnoses = _dbContext.admitting_diagnoses.Where(id => id.id == model.id).FirstOrDefault();
                if (diagnoses != null)
                {
                    diagnoses.record_date = Convert.ToDateTime(model.record_date);
                    diagnoses.diagnosis = model.diagnosis;
                    diagnoses.modifier_id = model.modifier_id;
                    diagnoses.modified = DateTime.Now;
                }
                _dbContext.Update(diagnoses);
                _dbContext.SaveChanges();
                returnID = diagnoses.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Diagnosis info 
        /// </summary>
        /// <returns></returns>
        public AdmittingDiagnosesModel DeleteDiagnosisById(int id, int userId)
        {
            var status = new AdmittingDiagnosesModel();
            try
            {
                var model = (from m in _dbContext.admitting_diagnoses
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

