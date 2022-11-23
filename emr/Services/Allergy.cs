using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class Allergy : IAllergy
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Allergy(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Allergy by id info 
        /// </summary>
        /// <returns></returns>
        public AllergyModel GetAllergyById(int id)
        {
            var result = new AllergyModel();
            try
            {
                result = (from d in _dbContext.allergies
                          where d.id == id
                          select new AllergyModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              allergy_type = d.allergy_type,
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              allergy = d.allergy,
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
        /// update the Allergy info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateAllergyInfo(AllergyModel model)
        {
            int returnID = 0;
            try
            {
                var allergies = new allergies();
                allergies = _dbContext.allergies.Where(id => id.id == model.id).FirstOrDefault();
                if (allergies != null)
                {
                    allergies.record_date = model.record_date;
                    allergies.allergy_type = model.allergy_type;
                    allergies.allergy = model.allergy;
                    allergies.modifier_id = model.modifier_id;
                    allergies.modified = DateTime.Now;
                }
                _dbContext.Update(allergies);
                _dbContext.SaveChanges();
                returnID = allergies.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Allergy info 
        /// </summary>
        /// <returns></returns>
        public AllergyModel DeleteAllergyById(int id, int userId)
        {
            var status = new AllergyModel();
            try
            {
                var model = (from m in _dbContext.allergies
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

