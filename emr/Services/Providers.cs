using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;
using System.Text.Json.Nodes;
using System;
using MoreLinq;
using System.Text.Json;
using LtiLibrary.NetCore.Lis.v2;

namespace emr.Services
{
    public class Providers : IProviders
    {

        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Providers(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// store the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveProvidersInfo(providers model)
        {
            int returnID = 0;
            try
            {
                var providers = new providers()
                {
                    created = DateTime.Now,
                    creator_id = model.creator_id,
                    modified = DateTime.Now,
                    modifier_id = model.modifier_id,
                    name = model.name,
                    notes = model.notes,
                    course_id = model.course_id,
                    active = true
                };
                _dbContext.Add(providers);
                _dbContext.SaveChanges();
                returnID = providers.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// update the medications info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProvidersInfo(ProvidersModel model)
        {
            int returnID = 0;
            try
            {
                var providers = new providers();
                providers = _dbContext.providers.Where(id => id.id == model.id).FirstOrDefault();
                if (providers != null)
                {
                    providers.name = model.name;
                    providers.notes = model.notes;
                }
                _dbContext.Update(providers);
                _dbContext.SaveChanges();
                returnID = providers.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }

        /// <summary>
        /// get provider info 
        /// </summary>
        /// <returns></returns>
        public object GetProvidersAll(int courseId)
        {
            var result = new object();
            try
            {
                result = (from p in _dbContext.providers
                          where p.course_id == courseId && p.active == true
                          orderby p.name
                          select new ProvidersModel
                          {
                              id = p.id,
                              created = p.created,
                              creator_id = p.creator_id,
                              modified = p.modified,
                              modifier_id = p.modifier_id,
                              name = p.name.ToCleanString(),
                              notes = p.notes.ToCleanString(),

                          }).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// get medications info 
        /// </summary>
        /// <returns></returns>
        public ProvidersModel GetProvidersById(int id)
        {
            var result = new ProvidersModel();
            try
            {
                result = (from p in _dbContext.providers
                          where p.id == id
                          select new ProvidersModel
                          {
                              id = p.id,
                              created = p.created,
                              creator_id = p.creator_id,
                              modified = p.modified,
                              modifier_id = p.modifier_id,
                              name = p.name.ToCleanString(),
                              notes = p.notes.ToCleanString(),
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
        /// delete a provider info 
        /// </summary>
        /// <returns></returns>
        public bool DeleteProvidersInfoById(int id, int userId)
        {
            bool status = false;
            try
            {
                var model = (from m in _dbContext.providers
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return status;
        }
        /// <summary>
        /// save providers
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void ProvidersInsertDefault(int courseId)
        {
            try
            {
                var data = new List<ProvidersModel>();
                var formDatas = System.IO.File.ReadAllText(@"./assets/ProvidersInsertDefault.json");
                data = JsonSerializer.Deserialize<List<ProvidersModel>>(formDatas);
                foreach (var item in data)
                {
                    providers providers = new providers()
                    {
                        created = DateTime.Now,
                        creator_id = item.creator_id,
                        modified = DateTime.Now,
                        modifier_id = item.modifier_id,
                        name = item.name,
                        notes = null,
                        course_id = courseId,
                        active = true
                    };
                    _dbContext.Add(providers);
                }
                this.GetOptionsInfo(courseId);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }
        }
        /// <summary>
        /// get options info 
        /// </summary>
        /// <returns></returns>
        public void GetOptionsInfo(int courseId)
        {
            var options = new List<options>();

            Random gen = new Random();
            var myDate = DateTime.Now;
            var begin = myDate.AddYears(-80);
            var end = myDate.AddDays(-1);
            DateTime start = new DateTime(begin.Year, 1, 1);
            int range = (end - start).Days;
            options = (from o in _dbContext.options select o).ToList();
            var lastNames = (from o in options where o.group_name == "last_name" select new { name = o.name }).ToList();
            var firstNames = (from o in options where o.group_name.StartsWith("first_name") select new { group_name = o.group_name, name = o.name, description = o.description }).ToList();
            lastNames.Shuffle();
            firstNames.Shuffle();
            int l_count = lastNames.Count();
            int f_count = firstNames.Count();
            int i = 0;
            int count = Convert.ToInt16(_iconfiguration["Provider.init_count"]);
            while (i < count)
            {
                // var provider = new providers();
                var fname = firstNames[i % f_count];
                var lname = lastNames[i % l_count];
                providers providers = new providers()
                {
                    created = DateTime.Now,
                    creator_id = 1,
                    modified = DateTime.Now,
                    modifier_id = 1,
                    name = fname.name + " " + lname.name + " M.D.",
                    notes = null,
                    course_id = courseId,
                    active = true
                };
                _dbContext.Add(providers);
                i++;
            }
        }
        /// <summary>
        /// get provider info 
        /// </summary>
        /// <returns></returns>
        public List<providers> GetProviders(int courseId)
        {
            var result = new List<providers>();
            try
            {
                result = (from p in _dbContext.providers where p.course_id == courseId && p.active == true orderby p.name select p).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
    }
}
