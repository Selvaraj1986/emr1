using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace emr.Services
{
    public class Consults : IConsults
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Consults(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Consults by id info 
        /// </summary>
        /// <returns></returns>
        public ConsultsModel GetConsultsById(int id)
        {
            var result = new ConsultsModel();
            try
            {
                result = (from d in _dbContext.consults
                          where d.id == id
                          select new ConsultsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              notes = d.notes,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
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
        /// update the Consults info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConsultsInfo(ConsultsModel model)
        {
            int returnID = 0;
            try
            {
                var consults = new consults();
                consults = _dbContext.consults.Where(id => id.id == model.id).FirstOrDefault();
                if (consults != null)
                {
                    consults.record_date = model.record_date;
                    consults.notes = model.notes;
                    consults.modifier_id = model.modifier_id;
                    consults.modified = DateTime.Now;
                }
                _dbContext.Update(consults);
                _dbContext.SaveChanges();
                returnID = consults.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Consults info 
        /// </summary>
        /// <returns></returns>
        public ConsultsModel DeleteConsultsById(int id, int userId)
        {
            var status = new ConsultsModel();
            try
            {
                var model = (from m in _dbContext.consults
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
