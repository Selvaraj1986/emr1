using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class Precautions : IPrecautions
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Precautions(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Precautions by id info 
        /// </summary>
        /// <returns></returns>
        public PrecautionsModel GetPrecautionsById(int id)
        {
            var result = new PrecautionsModel();
            try
            {
                result = (from d in _dbContext.precautions
                          where d.id == id
                          select new PrecautionsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              status = d.status,
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
        /// update the Precautions info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePrecautionsInfo(PrecautionsModel model)
        {
            int returnID = 0;
            try
            {
                var precautions = new precautions();
                precautions = _dbContext.precautions.Where(id => id.id == model.id).FirstOrDefault();
                if (precautions != null)
                {
                    precautions.record_date = model.record_date;
                    precautions.status = model.status;
                    precautions.notes = model.notes;
                    precautions.modifier_id = model.modifier_id;
                    precautions.modified = DateTime.Now;
                }
                _dbContext.Update(precautions);
                _dbContext.SaveChanges();
                returnID = precautions.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Precautions info 
        /// </summary>
        /// <returns></returns>
        public PrecautionsModel DeletePrecautionsById(int id, int userId)
        {
            var status = new PrecautionsModel();
            try
            {
                var model = (from m in _dbContext.precautions
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



