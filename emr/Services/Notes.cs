using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Support;

namespace emr.Services
{
    public class Notes : INotes
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Notes(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Notes by id info 
        /// </summary>
        /// <returns></returns>
        public NotesModel GetNotesById(int id)
        {
            var result = new NotesModel();
            try
            {
                result = (from d in _dbContext.notes
                          where d.id == id
                          select new NotesModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
                              created = d.created,
                              modified = d.modified,
                              patient_id = d.patient_id,
                              note = d.note,
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
        /// update the Notes info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateNotesInfo(NotesModel model)
        {
            int returnID = 0;
            try
            {
                var notes = new notes();
                notes = _dbContext.notes.Where(id => id.id == model.id).FirstOrDefault();
                if (notes != null)
                {
                    notes.record_date = Helpers.ToDateFormat(model.record_date);
                    notes.note = model.note;
                    notes.modifier_id = model.modifier_id;
                    notes.modified = DateTime.Now;
                }
                _dbContext.Update(notes);
                _dbContext.SaveChanges();
                returnID = notes.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a Notes info 
        /// </summary>
        /// <returns></returns>
        public NotesModel DeleteNotesById(int id, int userId)
        {
            var status = new NotesModel();
            try
            {
                var model = (from m in _dbContext.notes
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