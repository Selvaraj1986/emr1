using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Support;

namespace emr.Services
{
    public class ProviderOrders : IProviderOrders
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public ProviderOrders(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// get ProviderOrders by id info 
        /// </summary>
        /// <returns></returns>
        public ProviderOrdersModel GetProviderOrdersById(int id)
        {
            var result = new ProviderOrdersModel();
            try
            {
                result = (from d in _dbContext.provider_orders
                          where d.id == id
                          select new ProviderOrdersModel
                          {
                              id = d.id,
                              record_date = Helpers.ToDateString(d.record_date),
                              provider_id = d.provider_id,
                              created = d.created,
                              modified = d.modified,
                              provider_name = (from p in _dbContext.providers where p.id == d.provider_id select p).FirstOrDefault().name ?? "",
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
        /// update the ProviderOrders info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProviderOrdersInfo(ProviderOrdersModel model)
        {
            int returnID = 0;
            try
            {
                var orders = new provider_orders();
                orders = _dbContext.provider_orders.Where(id => id.id == model.id).FirstOrDefault();
                if (orders != null)
                {
                    orders.record_date = Helpers.ToDateFormat(model.record_date);
                    orders.provider_id = model.provider_id;
                    orders.notes = model.notes;
                    orders.modifier_id = model.modifier_id;
                    orders.modified = DateTime.Now;
                }
                _dbContext.Update(orders);
                _dbContext.SaveChanges();
                returnID = orders.patient_id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// delete a ProviderOrders info 
        /// </summary>
        /// <returns></returns>
        public ProviderOrdersModel DeleteProviderOrdersById(int id, int userId)
        {
            var status = new ProviderOrdersModel();
            try
            {
                var model = (from m in _dbContext.provider_orders
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