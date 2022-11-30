using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Support;

namespace emr.Services
{
    public class Treatments : ITreatments
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Treatments(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Treatments by id info 
        /// </summary>
        /// <returns></returns>
        public TreatmentsModel GetTreatmentsById(int id)
        {
            var result = new TreatmentsModel();
            try
            {
                result = (from d in _dbContext.treatments
                          where d.id == id
                          select new TreatmentsModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
                              status = d.status,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              type = d.type,
                              notes = d.notes,
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
        /// update the Treatments info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateTreatmentsInfo(TreatmentsModel model)
        {
            int returnID = 0;
            try
            {
                var treatments = new treatments();
                treatments = _dbContext.treatments.Where(id => id.id == model.id).FirstOrDefault();
                if (treatments != null)
                {
                    treatments.record_date = Helpers.ToDateFormat(model.record_date);
                    treatments.status = model.status;
                    treatments.notes = model.notes;
                    treatments.type = model.type;
                    treatments.modifier_id = model.modifier_id;
                    treatments.modified = DateTime.Now;
                }
                _dbContext.Update(treatments);
                _dbContext.SaveChanges();
                returnID = treatments.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Treatments info 
        /// </summary>
        /// <returns></returns>
        public TreatmentsModel DeleteTreatmentsById(int id, int userId)
        {
            var status = new TreatmentsModel();
            try
            {
                var model = (from m in _dbContext.treatments
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



