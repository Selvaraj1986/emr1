using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace emr.Services
{
    public class Dosages : IDosages
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Dosages(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get dosage by id info 
        /// </summary>
        /// <returns></returns>
        public dosages GetDosageById(int id)
        {
            var result = new dosages();
            try
            {
                result = (from d in _dbContext.dosages
                          where d.id == id
                          select d).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// update the dosages info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDosagesInfo(dosages dosages)
        {
            int returnID = 0;
            try
            {
                dosages.active = true;
                _dbContext.Update(dosages);
                _dbContext.SaveChanges();
                returnID = dosages.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a medications info 
        /// </summary>
        /// <returns></returns>
        public DosagesModel DeleteDosagesById(int id, int userId)
        {
            var status = new DosagesModel();
            try
            {
                var model = (from m in _dbContext.dosages
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                status.medication_id = model.medication_id;
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
