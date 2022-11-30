using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace emr.Services
{
    public class CodeStatuses : ICodeStatuses
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public CodeStatuses(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get CodeStatuses by id info 
        /// </summary>
        /// <returns></returns>
        public CodeStatusesModel GetCodeStatusesById(int id)
        {
            var result = new CodeStatusesModel();
            try
            {
                result = (from d in _dbContext.code_statuses
                          where d.id == id
                          select new CodeStatusesModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
                              code_status = d.code_status,
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
        /// update the CodeStatuses info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCodeStatusesInfo(CodeStatusesModel model)
        {
            int returnID = 0;
            try
            {
                var code = new code_statuses();
                code = _dbContext.code_statuses.Where(id => id.id == model.id).FirstOrDefault();
                if (code != null)
                {
                    code.record_date = Helpers.ToDateFormat(model.record_date);
                    code.code_status = model.code_status;
                    code.notes = model.notes;
                    code.modifier_id = model.modifier_id;
                    code.modified = DateTime.Now;
                }
                _dbContext.Update(code);
                _dbContext.SaveChanges();
                returnID = code.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a CodeStatuses info 
        /// </summary>
        /// <returns></returns>
        public CodeStatusesModel DeleteCodeStatusesById(int id, int userId)
        {
            var status = new CodeStatusesModel();
            try
            {
                var model = (from m in _dbContext.code_statuses
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


