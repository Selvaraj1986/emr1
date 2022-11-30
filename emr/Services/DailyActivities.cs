using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class DailyActivities : IDailyActivities
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public DailyActivities(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get DailyActivities by id info 
        /// </summary>
        /// <returns></returns>
        public DailyActivitiesModel GetDailyActivitiesById(int id)
        {
            var result = new DailyActivitiesModel();
            try
            {
                result = (from d in _dbContext.daily_activities
                          where d.id == id
                          select new DailyActivitiesModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
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
                              notes = d.notes

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
        /// update the DailyActivities info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDailyActivitiesInfo(DailyActivitiesModel model)
        {
            int returnID = 0;
            try
            {
                var activity = new daily_activities();
                activity = _dbContext.daily_activities.Where(id => id.id == model.id).FirstOrDefault();
                if (activity != null)
                {
                    activity.record_date = Helpers.ToDateFormat(model.record_date);
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

                }
                _dbContext.Update(activity);
                _dbContext.SaveChanges();
                returnID = activity.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a DailyActivities info 
        /// </summary>
        /// <returns></returns>
        public DailyActivitiesModel DeleteDailyActivitiesById(int id, int userId)
        {
            var status = new DailyActivitiesModel();
            try
            {
                var model = (from m in _dbContext.daily_activities
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

