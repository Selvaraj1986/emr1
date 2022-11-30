using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class Height : IHeight
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Height(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Height by id info 
        /// </summary>
        /// <returns></returns>
        public HeightsModel GetHeightById(int id)
        {
            var result = new HeightsModel();
            try
            {
                result = (from d in _dbContext.heights
                          where d.id == id
                          select new HeightsModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
                              height = d.height,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              height_unit = d.height_unit,
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
        /// update the Height info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateHeightInfo(HeightsModel model)
        {
            int returnID = 0;
            try
            {
                var medication = new heights();
                medication = _dbContext.heights.Where(id => id.id == model.id).FirstOrDefault();
                if (medication != null)
                {
                    medication.record_date = Helpers.ToDateFormat(model.record_date);
                    medication.height = model.height;
                    medication.height_unit = model.height_unit;
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
        /// delete a Height info 
        /// </summary>
        /// <returns></returns>
        public HeightsModel DeleteHeightById(int id, int userId)
        {
            var status = new HeightsModel();
            try
            {
                var model = (from m in _dbContext.heights
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

