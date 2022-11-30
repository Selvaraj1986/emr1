using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class Dietaries : IDietaries
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Dietaries(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Dietaries by id info 
        /// </summary>
        /// <returns></returns>
        public DietariesModel GetDietariesById(int id)
        {
            var result = new DietariesModel();
            try
            {
                result = (from d in _dbContext.dietaries
                          where d.id == id
                          select new DietariesModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
                              type = d.type,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
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
        /// update the Dietaries info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDietariesInfo(DietariesModel model)
        {
            int returnID = 0;
            try
            {
                var dietaries = new dietaries();
                dietaries = _dbContext.dietaries.Where(id => id.id == model.id).FirstOrDefault();
                if (dietaries != null)
                {
                    dietaries.record_date = Helpers.ToDateFormat(model.record_date);
                    dietaries.type = model.type;
                    dietaries.notes = model.notes;
                    dietaries.modifier_id = model.modifier_id;
                    dietaries.modified = DateTime.Now;
                }
                _dbContext.Update(dietaries);
                _dbContext.SaveChanges();
                returnID = dietaries.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Dietaries info 
        /// </summary>
        /// <returns></returns>
        public DietariesModel DeleteDietariesById(int id, int userId)
        {
            var status = new DietariesModel();
            try
            {
                var model = (from m in _dbContext.dietaries
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
