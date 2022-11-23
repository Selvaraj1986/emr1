using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;

namespace emr.Services
{
    public class Rooms : IRooms
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Rooms(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get Rooms by id info 
        /// </summary>
        /// <returns></returns>
        public RoomsModel GetRoomsById(int id)
        {
            var result = new RoomsModel();
            try
            {
                result = (from d in _dbContext.rooms
                          where d.id == id
                          select new RoomsModel
                          {
                              id = d.id,
                              record_date = d.record_date,
                              room = d.room,                              
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
        /// update the Rooms info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateRoomsInfo(RoomsModel model)
        {
            int returnID = 0;
            try
            {
                var code = new rooms();
                code = _dbContext.rooms.Where(id => id.id == model.id).FirstOrDefault();
                if (code != null)
                {
                    code.record_date = model.record_date;
                    code.room = model.room;
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
        /// delete a Rooms info 
        /// </summary>
        /// <returns></returns>
        public RoomsModel DeleteRoomsById(int id, int userId)
        {
            var status = new RoomsModel();
            try
            {
                var model = (from m in _dbContext.rooms
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


