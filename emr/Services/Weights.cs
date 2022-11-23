using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class Weights : IWeights
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Weights(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Weights by id info 
        /// </summary>
        /// <returns></returns>
        public WeightsModel GetWeightsById(int id)
        {
            var result = new WeightsModel();
            try
            {
                result = (from d in _dbContext.weights
                          where d.id == id
                          select new WeightsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              weight = d.weight,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              weight_unit = d.weight_unit,
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
        /// update the Weights info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateWeightsInfo(WeightsModel model)
        {
            int returnID = 0;
            try
            {
                var weight = new weights();
                weight = _dbContext.weights.Where(id => id.id == model.id).FirstOrDefault();
                if (weight != null)
                {
                    weight.record_date = model.record_date;
                    weight.weight = model.weight;
                    weight.weight_unit = model.weight_unit;
                    weight.modified = DateTime.Now;
                    
                }
                _dbContext.Update(weight);
                _dbContext.SaveChanges();
                returnID = weight.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Weights info 
        /// </summary>
        /// <returns></returns>
        public WeightsModel DeleteWeightsById(int id, int userId)
        {
            var status = new WeightsModel();
            try
            {
                var model = (from m in _dbContext.weights
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
